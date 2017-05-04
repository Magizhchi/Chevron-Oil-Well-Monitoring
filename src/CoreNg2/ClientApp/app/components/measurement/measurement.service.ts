import { Injectable } from "@angular/core";
import { Http } from "@angular/http";

import "rxjs/add/operator/toPromise";

import Httpheaders = require("../../configuration/http.headers");

@Injectable()
export class MeasurementService {

    private getMeasurementsUrl = "/api/measurements/";
    private getTagNamesUrl = "/api/values";

    constructor(private http: Http) { }

    addMeasurement(measurement): Promise<any> {
        return this.http.post(this.getMeasurementsUrl,
                              JSON.stringify(measurement),
                              { headers: Httpheaders.HttpHeaders.jsonPost })
                   .toPromise()
                   .then(res => res.json())
                   .catch(this.handleError);
        
    }

    getMeasurementsForWellId(wellId: number): Promise<any[]> {
        return this.http.get(this.getMeasurementsUrl + wellId)
                        .toPromise()
                        .then(response => response.json())
                        .catch(this.handleError);
    }

    getRecentEvent(event: any): Promise<any[]> {
        return this.http.get(this.getMeasurementsUrl + "/recent/" + event.assetId)
            .toPromise()
            .then(response => Object.assign(response.json(), event))
            .catch(this.handleError);
    }

    getTagNames(): Promise<any[]> {
        return this.http.get(this.getTagNamesUrl)
            .toPromise()
            .then(res => res.json())
            .catch(this.handleError);
    }

    getBreadCrumbData(wellId): Promise<any> {
        return this.http.get(this.getMeasurementsUrl + "breadcrumb/" + wellId)
            .toPromise()
            .then(response => response.json())
            .catch(this.handleError);
    }

    getRuleTypes(): Promise<any> {
        return this.http.get(this.getMeasurementsUrl + "ruletypes")
            .toPromise()
            .then(response => response.json())
            .catch(this.handleError);
    }

    deleteField(measurementId): Promise<any> {
        return this.http.delete(this.getMeasurementsUrl + measurementId)
            .toPromise()
            .catch(this.handleError);
    }

    private handleError(error: any): Promise<any> {
        console.error("An error occurred", error);
        return Promise.reject(error.message || error);
    }
}