import { Component, OnInit } from "@angular/core";
import { DatePipe, Location } from "@angular/common";
import { Params, ActivatedRoute } from "@angular/router";

import { EventViewerService } from "./eventViewer.service";

@
    Component({
        selector: "event-viewer",
        template: require("./eventViewer.component.html"),
        styles: [require("./../../../stylesheets/assetsTable.css")]
    })
export class EventViewerComponent implements OnInit {
    eventId: number;
    eventDetails: any;
    drillDownValues: any;
    chartingComplete = false;

    type = 'line';
    data = {
        labels: ["January", "February", "March", "April", "May", "June", "July"],
        datasets: [
            {
                label: "Values",
                data: [65, 59, 80, 81, 56, 55, 40],
                backgroundColor: ['rgba(30, 91, 62, 0.1)'],
                borderColor: ['rgba(30, 72, 91,0.7)']
            }
        ]
    };
    line
    options = {
        legend: {
            display: false
        },
        tooltips: {
            callbacks: {
                label: function (tooltipItem) {
                    return tooltipItem.yLabel;
                }
            }
        },
        elements: {
            line: {
                tension: 0.1
            }
        },
        responsive: true,
        maintainAspectRatio: false,
        scales: {
            xAxes: [{
                ticks: {
                    fontSize: 10
                }
            }]
        }
    };

    constructor(
        private route: ActivatedRoute,
        private eventViewerService: EventViewerService,
        private datePipe: DatePipe,
        private location: Location
    ) {}

    ngOnInit(): void {
        this.route.params
            .forEach((params: Params) => this.eventId = params['id']);
        this.updateAllValues();
        this.updateDrillDownValues();
    }

    updateAllValues(): void {
        this.eventViewerService.getEventDetails(this.eventId)
            .then(res => this.eventDetails = res);
    }

    updateDrillDownValues(): void {
        this.eventViewerService.getDrillDownDetails(this.eventId)
            .then(res => this.extractDrillDownValues(res));

//        console.log(this.drillDownValues.map(e => e.value));
    }

    extractDrillDownValues(values): void {
        this.data.datasets[0].data = values.map(e => e.value);
        this.data.labels = values.map(e => this.datePipe.transform(e.time, 'mediumTime'));
        this.chartingComplete = true;
    }

    goBack(): void {
        this.location.back();
    }
}