import { Component, OnInit } from "@angular/core";
import { ActivatedRoute, Params, Router } from "@angular/router";

import { FieldService } from "./field.service";
import "rxjs/add/operator/toPromise";

@Component({
    selector: "field-page",
    template: require("./field.component.html"),
    styles: [require("./../../../stylesheets/assetsTable.css")]
})
export class FieldComponent implements OnInit {
    fields: Field[];
    fieldsWithTime: any[];
    newField = new Field();
    deleteFieldObj = new Field();
    private assetId: number;
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
            });
    }

    updateFields(): void {
        this.fieldService
            .getFieldsForAssetId(this.assetId)
            .then(fields => {
                this.fields = fields;
                this.updateRecentEvent();
            });
    }

    updateRecentEvent(): void {
        Promise.all(this.fields.map(e => this.fieldService.getRecentEvent(e)))
            .then(values => this.fieldsWithTime = values);
    }

    createField(): void {
        this.fieldService.addField(this.newField.fieldName, this.assetId)
                .then(res => { this.updateFields();});
    }
    
    goToWells(field): void {
        this.router.navigate(['/well', field.fieldId]);
    }

    confirmDelete(field): void {
        this.deleteFieldObj = field;
    }

    deleteField(): void {
        this.fieldService.deleteField(this.deleteFieldObj.fieldId)
            .then(res => this.updateFields());
    }
}

export class Field {
    fieldName: string;
    fieldId: number;
    fieldFkAssetId: number;
}

