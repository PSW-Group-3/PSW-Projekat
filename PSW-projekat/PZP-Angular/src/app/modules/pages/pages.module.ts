import { NgModule } from '@angular/core';
import { Routes, RouterModule } from "@angular/router";
import { CommonModule } from '@angular/common';
import { AppRoutingModule } from 'src/app/app-routing.module'; 
import { HomePatientComponent } from './homePatient/homePatient.component';
import { BankHomeComponent } from './bank-home/bank-home.component';
import { MdbCarouselModule } from 'mdb-angular-ui-kit/carousel';
import { NavBarsModule } from "../nav-bar/nav-bars.module";
import { HospitalModule } from "../hospital/hospital.module";

const routes: Routes = [
  { path: 'homePatient', component: HomePatientComponent },
  { path: 'bank-home', component: BankHomeComponent },
];

@NgModule({
    declarations: [
        HomePatientComponent,
        BankHomeComponent
    ],
    imports: [
        CommonModule,
        AppRoutingModule,
        RouterModule.forChild(routes),
        MdbCarouselModule,
        NavBarsModule,
        HospitalModule
    ]
})
export class PagesModule { }
