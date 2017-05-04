import { Component,OnInit } from "@angular/core";
import { ActivatedRoute, Params, Router } from "@angular/router";
import { WellService } from "./well.service";

import "rxjs/add/operator/toPromise";

@
Component({
    selector: "wells-page",
    template: require("./well.component.html"),
    styles: [require("./../../../stylesheets/assetsTable.css")]   
})
export class WellComponent implements OnInit {
    wells: Well[];
    wellsWithTime: any[];
    newWell = new Well();
    deleteWellObj = new Well();
    private fieldId: number;
    isCreateActive: boolean = false;
    breadCrumb: any;

    constructor(
        private router: Router,
        private route: ActivatedRoute,
        private wellService: WellService
    ) {}

    ngOnInit(): void {
        this.route.params
                   .forEach((params: Params) => this.fieldId = params["id"]);
        this.updateWells();
        this.updateBreadCrumb();
    }

    updateBreadCrumb(): void {
        this.wellService
            .getBreadCrumbData(this.fieldId)
            .then(data => this.breadCrumb = data[0]);
    }

    updateWells(): void {
        this.wellService
            .getWellsForFieldId(this.fieldId)
            .then(wells => {
                this.wells = wells;
                this.updateRecentEvent();
            });
    }

    updateRecentEvent(): void {
        Promise.all(this.wells.map(e => this.wellService.getRecentEvent(e)))
            .then(values => this.wellsWithTime = values);
    }
    createWell(): void {
        this.wellService.addWell(this.newWell.wellName, this.fieldId)
            .then(res => { this.updateWells(); this.hideAdd(); });
    }
    
    goToMeasurements(well): void {
        this.router.navigate(['/measurement', well.wellId]);
    }

    showAdd(): void {
        this.isCreateActive = true;
    }

    hideAdd(): void {
        this.isCreateActive = false;
    }

    confirmDelete(well): void {
        this.deleteWellObj = well;
    }

    deleteWell(): void {
        this.wellService.deleteWell(this.deleteWellObj.wellId)
            .then(res => this.updateWells());
    }

}

export class Well {
    wellName: string;
    wellId: number;
}