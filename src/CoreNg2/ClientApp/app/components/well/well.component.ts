import { Component } from "@angular/core";
import { ActivatedRoute, Params, Router } from "@angular/router";
import { WellService } from "./well.service";

@
Component({
    selector: "wells-page",
    template: require("./well.component.html"),
    styles: [require("./../../../stylesheets/assetsTable.css")]   
})
export class WellComponent {
    wells: Well[];
    newWell= new Well();
    private fieldId: number;
    isCreateActive: boolean = false;

    constructor(
        private router: Router,
        private route: ActivatedRoute,
        private wellService: WellService
    ) {}

    ngOnInit(): void {
        this.route.params
                   .forEach((params: Params) => this.fieldId = params["id"]);
        this.updateWells();
    }

    updateWells(): void {
        this.wellService
            .getWellsForFieldId(this.fieldId)
            .then(wells => this.wells = wells);
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

    deleteWell(well): void {
        this.wellService.deleteWell(well.wellId)
            .then(res => this.updateWells());
    }

}

export class Well {
    wellName: string;
    wellId: number;
}