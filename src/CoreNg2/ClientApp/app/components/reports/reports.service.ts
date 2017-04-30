import { Injectable } from "@angular/core";
import { Http } from "@angular/http";

import "rxjs/add/operator/toPromise";

import Httpheaders = require("../../configuration/http.headers");

@Injectable()
export class ReportsService {
    private reportsUrl = "/api/reports/events";

    constructor(private http: Http) { }

    getAllEvents(): Promise<any[]> {
        return this.http.get(this.reportsUrl)
            .toPromise()
            .then(res => res.json())
            .catch(this.handleError);
    } 

    private handleError(error: any): Promise<any> {
        console.error("An error occurred", error);
        return Promise.reject(error.message || error);
    }
}