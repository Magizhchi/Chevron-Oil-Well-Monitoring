import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";

import { AssetsService } from "./assets.service";

@Component({
    selector: "assets",
    template: require("./assets.component.html"),
    styles: [require("./../../../stylesheets/assetsTable.css")]
})
export class AssetsComponent implements OnInit {
    assets: Asset[];
    newAsset = new Asset();
    isCreateActive: boolean = false;

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
            .then(assets => this.assets = assets);
    }

    createAsset(): void {
        this.assetsService.addAsset(this.newAsset.assetName)
            .then(res => { this.updateAllFields(); this.hideAdd(); });
    }

    goToFields(asset): void {
        this.router.navigate(["/field", asset.assetId]);
    }

    showAdd(): void {
        this.isCreateActive = true;
    }

    hideAdd(): void {
        this.isCreateActive = false;
    }

    deleteAsset(asset): void {
        this.assetsService.deleteAsset(asset.assetId)
            .then(res => this.updateAllFields());
    }
}

export class Asset {
    assetName: string;
    assetId: number;
}