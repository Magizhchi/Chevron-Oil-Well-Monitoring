import { Injectable } from "@angular/core";
import { Http } from "@angular/http";

import "rxjs/add/operator/toPromise";
import Httpheaders = require("../../configuration/http.headers");

@Injectable()
export class FieldService {

    private getFieldUrl = "/api/fields/";

    constructor(private http: Http) { }

    addField(fieldName, fkAssetId): Promise<any> {
        console.log(fkAssetId + " : " + fieldName);
        return this.http.post(this.getFieldUrl,
                              JSON.stringify({ "Name": fieldName, "FkAssetId": fkAssetId }),
                              { headers: Httpheaders.HttpHeaders.jsonPost })
                        .toPromise()
                        .then(res => res.json())
                        .catch(this.handleError);
    }
    
    getFieldsForAssetId(assetId: number): Promise<any[]> {
        return this.http.get(this.getFieldUrl + assetId)
                    .toPromise()
                    .then(response => response.json())
                    .catch(this.handleError);
    }

    getRecentEvent(event: any): Promise<any[]> {
        return this.http.get(this.getFieldUrl + "/recent/" + event.fieldId)
            .toPromise()
            .then(response => Object.assign(response.json(), event))
            .catch(this.handleError);
    }

    deleteField(fieldId): Promise<any> {
        return this.http.delete(this.getFieldUrl + fieldId)
            .toPromise()
            .catch(this.handleError);
    }

    getBreadCrumbData(assetId): Promise<any> {
        return this.http.get(this.getFieldUrl + "breadcrumb/" + assetId)
            .toPromise()
            .then(response => response.json())
            .catch(this.handleError);
    }

    private handleError(error: any): Promise<any> {
        console.error("An error occurred", error); 
        return Promise.reject(error.message || error);
    }
}
