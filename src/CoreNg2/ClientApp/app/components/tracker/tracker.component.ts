import { Component } from "@angular/core";
import { Http } from "@angular/http";


@Component({
    selector: "tracker",
    template: require("./tracker.component.html"),
})
export class TrackerComponent {
    newEvent: any;
    trackerCollection: Trackers[];

    constructor(http: Http) {
        http.get("/api/SampleData/SampleTracker").subscribe(result => {
            this.trackerCollection = result.json();
            console.log(this.trackerCollection);
        });
        this.newEvent = {};
    }

    onSave(): void {
        console.log("Details saved Succesfully" + this.newEvent);
        this.trackerCollection.push(this.newEvent);
    }

}

interface Trackers {
    trackerName: string;
    valueGreaterThan: number;
}