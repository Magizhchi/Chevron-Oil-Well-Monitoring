﻿import { Component } from "@angular/core";
import { ActivatedRoute, Params, Router } from "@angular/router";

import { MeasurementService } from "./measurement.service";

@
Component({
    selector: "measurements-page",
    template: require("./measurement.component.html")
})
export class MeasurementComponent {
    measurements: IMeasurement[];
    newMeasurement= new Measurement;
    private wellId: number;

    constructor(
        private router: Router,
        private route: ActivatedRoute,
        private measurementService: MeasurementService
    ) { }

    ngOnInit(): void {
        this.route.params
            .forEach((params: Params) => this.wellId = params["id"]);
        this.updateMeasurements();
    }

    updateMeasurements(): void {
        this.measurementService
            .getMeasurementsForWellId(this.wellId)
            .then(measurements => this.measurements = measurements);
    }
    createMeasurment(): void {
        this.measurementService.addMeasurement(this.newMeasurement.measurementName, this.newMeasurement.measurementTagName, this.newMeasurement.measurementGreaterThan, this.newMeasurement.measurementGreaterThanActive)
            .then(res => this.updateMeasurements());
    }

    goToMeasurements(well): void {
        this.router.navigate(['/measurement', well.wellId]);
        console.log("called..." + well.wellId);
    }

}


interface IMeasurement {
    measurementName: string;
    measurementTagName: string;
    measurementId: number;
    measurementGreaterThan: number;
    measurementFkWellsId: number;
    measurementGreaterThanActive: boolean;

}

export class Measurement {
    measurementName: string;
    measurementTagName: string;
    measurementGreaterThan: number;
    measurementGreaterThanActive: number;
}