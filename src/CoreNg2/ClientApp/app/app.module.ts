import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { UniversalModule } from "angular2-universal";
import { FormsModule } from "@angular/forms";

import { AppComponent } from "./components/app/app.component"
import { NavMenuComponent } from "./components/navmenu/navmenu.component";
import { HomeComponent } from "./components/home/home.component";
import { ReportsComponent } from "./components/reports/reports.component";
import { TrackerComponent } from "./components/tracker/tracker.component";
import { AssetsComponent } from "./components/assets/assets.component";
import { FieldComponent } from "./components/field/field.component";
import { WellComponent } from "./components/well/well.component";
import { MeasurementComponent } from "./components/measurement/measurement.component"

import { AssetsService } from "./components/assets/assets.service";
import { FieldService } from "./components/field/field.service";
import { WellService } from "./components/well/well.service";
import { MeasurementService } from "./components/measurement/measurement.service";

@NgModule({
    bootstrap: [AppComponent],
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        ReportsComponent,
        TrackerComponent,
        AssetsComponent,
        FieldComponent,
        WellComponent,
        MeasurementComponent
    ],
    providers: [
        AssetsService,
        FieldService,
        WellService,
        MeasurementService
    ],
    imports: [
        UniversalModule, // Must be first import. This automatically imports BrowserModule, HttpModule, and JsonpModule too.
        FormsModule,
        RouterModule.forRoot([
            { path: "", redirectTo: "home", pathMatch: "full" },
            { path: "home", component: HomeComponent },
            { path: "dailyreports", component: ReportsComponent },
            { path: "tracker", component: TrackerComponent },
            { path: "assets", component: AssetsComponent },
            { path: "field/:id", component: FieldComponent },
            { path: "well/:id", component: WellComponent},
            { path: "measurement/:id", component: MeasurementComponent},
            { path: "**", redirectTo: "home" }
        ])
    ]
})
export class AppModule {
}
