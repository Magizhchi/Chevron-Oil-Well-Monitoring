import { Component, OnInit, Input } from "@angular/core";

@Component({
    selector: "breadcrumbs",
    template: require("./breadcrumbs.component.html"),
    styles: [require("./../../../stylesheets/assetsTable.css")]
})
export class BreadCrumbComponent {
    
    @Input() private breadCrumb: any;

    constructor() {}
}
