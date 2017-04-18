import { Injectable } from "@angular/core";
import { Http } from "@angular/http";

import "rxjs/add/operator/toPromise";

import Httpheaders = require("../../configuration/http.headers");


@Injectable()
export class WellService {

    private getWellUrl = "/api/wells/";

    constructor(private http: Http) { }

    addWell(wellName, fkFieldId): Promise<any> {
        return this.http
                .post(this.getWellUrl,
                        JSON.stringify({ "Name": wellName, "FkFieldsId": fkFieldId }),
                        { headers: Httpheaders.HttpHeaders.jsonPost })
                .toPromise()
                .then(res => res.json())
                .catch(this.handleError);
    }
    
    getWellsForFieldId(fieldId: number): Promise<any[]> {
        return this.http.get(this.getWellUrl + fieldId)
                    .toPromise()
                    .then(response => response.json())
                    .catch(this.handleError);
    }

    deleteWell(wellId): Promise<any> {
        return this.http.delete(this.getWellUrl + wellId)
            .toPromise()
            .catch(this.handleError);
    }

    getBreadCrumbData(fieldId): Promise<any> {
        return this.http.get(this.getWellUrl + "breadcrumb/" + fieldId)
            .toPromise()
            .then(response => response.json())
            .catch(this.handleError);
    }

    private handleError(error: any): Promise<any> {
        console.error("An error occurred", error); 
        return Promise.reject(error.message || error);
    }
}
