import { Injectable } from "@angular/core";
import { Http } from "@angular/http";

import { HttpHeaders } from "../../configuration/http.headers";

import "rxjs/add/operator/toPromise";

@Injectable()
export class AssetsService {

    private getAllAssestsUrl = "/api/assets";

    constructor(private http: Http) { }
    
    getAllAssets(): Promise<any[]> {
        return this.http.get(this.getAllAssestsUrl)
                        .toPromise()
                        .then(response => response.json())
                        .catch(this.handleError);
    }

    getRecentEvent(event: any): Promise<any[]> {
        return this.http.get(this.getAllAssestsUrl + "/recent/" + event.assetId)
                        .toPromise()
                        .then(response => Object.assign(response.json(),event))
                        .catch(this.handleError);
    }

    private handleError(error: any): Promise<any> {
        console.error("An error occurred", error); 
        return Promise.reject(error.message || error);
    }

    addAsset(assetName): Promise<any> {
       return this.http
                .post(this.getAllAssestsUrl,
                    JSON.stringify({ "name": assetName }),
                    { headers: HttpHeaders.jsonPost })
                .toPromise()
                .then(res => res.json())
                .catch(this.handleError);
    }

    deleteAsset(assetId): Promise<any> {
        return this.http.delete(this.getAllAssestsUrl + "/" + assetId)
                        .toPromise()
                        .catch(this.handleError);
    }
}

