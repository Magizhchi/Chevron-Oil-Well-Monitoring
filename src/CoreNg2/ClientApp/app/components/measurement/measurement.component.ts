import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Params, Router } from "@angular/router";

import { MeasurementService } from "./measurement.service";

import "rxjs/add/operator/toPromise";

@
Component({
    selector: "measurements-page",
    template: require("./measurement.component.html"),
    styles: [require("./../../../stylesheets/assetsTable.css")]
})  
export class MeasurementComponent {
    measurements: IMeasurement[];
    measurementsWithTime: any[];
    newMeasurement = new Measurement();
    deleteMeasurementObj = new Measurement();
    tagNames: String[];
    private wellId: number;
    breadCrumb: any;
    ruleTypes: RuleType[];

    constructor(
        private router: Router,
        private route: ActivatedRoute,
        private measurementService: MeasurementService
    ) { }

    ngOnInit(): void {
        this.route.params
            .forEach((params: Params) => this.wellId = params["id"]);
        this.updateMeasurements();
        this.updateTagNames();
        this.updateBreadCrumb();
        this.updateRuleTypes();
    }

    updateTagNames(): void {
        this.measurementService
            .getTagNames()
            .then(tags => this.tagNames = tags);
    }

    updateRuleTypes(): void {
        this.measurementService
            .getRuleTypes()
            .then(res => this.ruleTypes = res);
    }

    updateBreadCrumb(): void {
        this.measurementService
            .getBreadCrumbData(this.wellId)
            .then(data => this.breadCrumb = data[0]);
    }

    updateMeasurements(): void {
        this.measurementService
            .getMeasurementsForWellId(this.wellId)
            .then(measurements => {
                this.measurements = measurements;
                this.updateRecentEvent();
            });
    }

    updateRecentEvent(): void {
        Promise.all(this.measurements.map(e => this.measurementService.getRecentEvent(e)))
            .then(values => this.measurementsWithTime = values);
    }
//    createMeasurment(): void {
//        this.measurementService.addMeasurement(this.newMeasurement.measurementName, this.newMeasurement.measurementTagName, this.newMeasurement.measurementGreaterThan, this.newMeasurement.measurementGreaterThanActive)
//            .then(res => this.updateMeasurements());
//    }

    goToMeasurements(well): void {
        this.router.navigate(['/measurement', well.wellId]);
        console.log("called..." + well.wellId);
    }

    addMeasurement(): void {
        this.newMeasurement.FKWellId = this.wellId;
        console.log(this.newMeasurement);
        this.measurementService.addMeasurement(this.newMeasurement)
            .then(res => res.json());
        this.updateMeasurements();
    }

    confirmDelete(measurement): void {
        this.deleteMeasurementObj = measurement;
    }

    deleteMeasurement(measurmentObj):void {
//        this.measurementService.deleteField(this.deleteMeasurementObj.MeasurementId)
//                               .then(res => this.updateMeasurements());
    }

}


interface IMeasurement {
    MeasurementName: string;
    MeasurementTagName: string;
    MeasurementId: number;
    Value: number;
    RuleDescription: string;
}

export class Measurement {
    measurementName: string;
    measurementTagName: string;
    measurementDescription: string;
    MeasurementId: number;
    RuleTypeId: number;
    value: number;
    FKWellId: number;
}

export class RuleType {
    RuleTypeId: number;
    RuleDescription: string;
    value: number;
}