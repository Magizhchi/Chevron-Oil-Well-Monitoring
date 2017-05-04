import { Component, OnInit } from "@angular/core";
import { Http } from "@angular/http";
import { Router } from "@angular/router";

import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { ReportsService } from "./reports.service";


@Component({
    selector: "daily-reports",
    template: require("./reports.component.html"),
    styles: [require("./../../../stylesheets/assetsTable.css")]
})
    

export class ReportsComponent implements OnInit {
    public eventsCollection: any[];
    private reports: any[];

    constructor(
        private reportsService: ReportsService,
        private router: Router
    ) { }

    ngOnInit(): void {
        this.reportsService
            .getAllEvents()
            .then(res => this.eventsCollection = res);
    }

    viewEvent(event): void {
        this.router.navigate(['/viewEvent', event.id]);
    }
}
