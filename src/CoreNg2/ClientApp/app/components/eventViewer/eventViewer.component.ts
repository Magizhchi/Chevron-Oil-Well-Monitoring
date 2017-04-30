import { Component, OnInit } from "@angular/core";
import { Params, ActivatedRoute } from "@angular/router";

import { EventViewerService } from "./eventViewer.service";

@
Component({
    selector: "event-viewer",
    template: require("./eventViewer.component.html")
})
export class EventViewerComponent implements OnInit {
    eventId : number;
    eventDetails : any;

    constructor(
        private route: ActivatedRoute,
        private eventViewerService: EventViewerService
    ) {}

    ngOnInit(): void {
        this.route.params
            .forEach((params: Params) => this.eventId = params['id']);
        this.updateAllValues();
    }

    updateAllValues(): void {
        console.log("From EventViewer : " + this.eventId);
        this.eventViewerService.getEventDetails(this.eventId)
            .then(res => this.eventDetails = res);
    }
}