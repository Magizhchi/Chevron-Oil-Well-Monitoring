import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Params, Router } from "@angular/router";

import { FieldService } from "./field.service";

@Component({
    selector: "field-page",
    template: require("./field.component.html"),
    styles: [require("./../../../stylesheets/assetsTable.css")]
})
export class FieldComponent implements OnInit {
    fields: Field[];
    newField = new Field();
    private assetId: number;
    isCreateActive: boolean = false;
    breadCrumb: any;

    constructor(
        private router: Router,
        private route: ActivatedRoute,
        private fieldService: FieldService
    ) {}

    ngOnInit(): void {
        this.route.params
                   .forEach((params: Params) => this.assetId = params['id']);
        this.updateFields();
        this.updateBreadCrumb();
    }

    updateBreadCrumb(): void {
        this.fieldService
            .getBreadCrumbData(this.assetId)
            .then(data => {
                this.breadCrumb = data[0];
                console.log(this.breadCrumb);
            });
    }

    updateFields(): void {
        this.fieldService
            .getFieldsForAssetId(this.assetId)
            .then(fields => this.fields = fields);
    }

    createField(): void {
        this.fieldService.addField(this.newField.fieldName, this.assetId)
                .then(res => { this.updateFields(); this.hideAdd(); });
    }
    
    goToWells(field): void {
        this.router.navigate(['/well', field.fieldId]);
    }

    showAdd(): void {
        this.isCreateActive = true;
    }

    hideAdd(): void {
        this.isCreateActive = false;
    }

    deleteField(field): void {
        this.fieldService.deleteField(field.fieldId)
            .then(res => this.updateFields());
    }
}

export class Field {
    fieldName: string;
    fieldId: number;
    fieldFkAssetId: number;
}

