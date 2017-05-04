import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";

import { AssetsService } from "./assets.service";
import "rxjs/add/operator/toPromise";


@Component({
    selector: "assets",
    template: require("./assets.component.html"),
    styles: [require("./../../../stylesheets/assetsTable.css")]
})
export class AssetsComponent implements OnInit {
    assets: Asset[];
    assetsWithTime: any[];
    newAsset = new Asset();
    deleteAssetObj = new Asset();
    

    constructor(
        private assetsService: AssetsService,
        private router: Router
    ) { }

    ngOnInit(): void {
        this.updateAllFields();
    }

    updateAllFields(): void {
        this.assetsService
            .getAllAssets()
            .then(assets => {
                this.assets = assets;
                this.updateRecentEvent();
            });
    }

    updateRecentEvent(): void {
        Promise.all(this.assets.map(e => this.assetsService.getRecentEvent(e)))
            .then(values => this.assetsWithTime = values);
    }

    createAsset(): void {
        this.assetsService.addAsset(this.newAsset.assetName)
            .then(res => { this.updateAllFields(); });
    }

    goToFields(asset): void {
        this.router.navigate(["/field", asset.assetId]);
    }

    confirmDelete(asset): void {
        this.deleteAssetObj = asset;
    }
    deleteAsset(): void {
        this.assetsService.deleteAsset(this.deleteAssetObj.assetId)
                .then(res => this.updateAllFields());
    }
}

export class Asset {
    assetName: string;
    assetId: number;
    eventId: number;
    endTime: number;
}