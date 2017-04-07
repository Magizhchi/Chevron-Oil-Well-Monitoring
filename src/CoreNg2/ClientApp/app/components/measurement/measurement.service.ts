﻿import { Injectable } from "@angular/core";
import { Http } from "@angular/http";

import "rxjs/add/operator/toPromise";

@Injectable()
export class MeasurementService {

    private getMeasurementsUrl = "/api/measurements/";

    constructor(private http: Http) { }
    addMeasurement(measurementName, measurementTagName, measurementGreaterThan, measurementGreaterThanAcitive): Promise<any> {
        console.log(measurementTagName + " : " + measurementName);
        //need to add database connection codes here
        return measurementTagName;
    }

    getMeasurementsForWellId(wellId: number): Promise<any[]> {
        return this.http.get(this.getMeasurementsUrl + wellId)
            .toPromise()
            .then(response => response.json())
            .catch(this.handleError);
    }

    private handleError(error: any): Promise<any> {
        console.error("An error occurred", error);
        return Promise.reject(error.message || error);
    }
}