import { Component } from "@angular/core";
import { Http } from "@angular/http";

@Component({
    selector: "daily-reports",
    template: require("./reports.component.html"),
    styles: [require("./reports.component.css")]
})
export class ReportsComponent {
    public eventsCollection: EventReport[];

    constructor(http: Http) {
        http.get("/api/Reports/GetReport").subscribe(result => {
            this.eventsCollection = result.json();
        });
    }
}

interface EventReport {
    tag: string;
    time: string;
    value: number;
}
