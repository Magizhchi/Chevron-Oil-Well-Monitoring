import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { UniversalModule } from "angular2-universal";
import { FormsModule } from "@angular/forms";
import { ChartModule } from "angular2-chartjs";
import { DatePipe } from '@angular/common';

import { AppComponent } from "./components/app/app.component"
import { NavMenuComponent } from "./components/navmenu/navmenu.component";
import { HomeComponent } from "./components/home/home.component";
import { ReportsComponent } from "./components/reports/reports.component";
import { AssetsComponent } from "./components/assets/assets.component";
import { FieldComponent } from "./components/field/field.component";
import { WellComponent } from "./components/well/well.component";
import { MeasurementComponent } from "./components/measurement/measurement.component";
import { EventViewerComponent } from "./components/eventViewer/eventViewer.component";
import { BreadCrumbComponent } from "./components/breadcrumbs/breadcrumbs.component";

import { AssetsService } from "./components/assets/assets.service";
import { FieldService } from "./components/field/field.service";
import { WellService } from "./components/well/well.service";
import { MeasurementService } from "./components/measurement/measurement.service";
import { ReportsService } from "./components/reports/reports.service";
import { EventViewerService } from "./components/eventViewer/eventViewer.service";


@NgModule({
    bootstrap: [AppComponent],
    declarations: [
        AppComponent,
        NavMenuComponent,
        HomeComponent,
        ReportsComponent,
        AssetsComponent,
        FieldComponent,
        WellComponent,
        MeasurementComponent,
        EventViewerComponent,
        BreadCrumbComponent
    ],
    providers: [
        AssetsService,
        FieldService,
        WellService,
        MeasurementService,
        ReportsService,
        EventViewerService,
        DatePipe
    ],
    imports: [
        UniversalModule, // Must be first import. This automatically imports BrowserModule, HttpModule, and JsonpModule too.
        FormsModule,
        ChartModule,
        RouterModule.forRoot([
            { path: "", redirectTo: "home", pathMatch: "full" },
            { path: "home", component: HomeComponent },
            { path: "dailyreports", component: ReportsComponent },
            { path: "assets", component: AssetsComponent },
            { path: "field/:id", component: FieldComponent },
            { path: "well/:id", component: WellComponent},
            { path: "measurement/:id", component: MeasurementComponent},
            { path: "viewEvent/:id", component:EventViewerComponent},
            { path: "**", redirectTo: "home" }
        ])
    ]
})
export class AppModule {
}
