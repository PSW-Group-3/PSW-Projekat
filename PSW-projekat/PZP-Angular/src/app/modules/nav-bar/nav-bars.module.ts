import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PatientNavBarComponent } from './patient-nav-bar/patient-nav-bar.component';

@NgModule({
  declarations: [
    PatientNavBarComponent,
  ],
  imports: [
    CommonModule
  ],
  exports: [
    PatientNavBarComponent,
  ]
})
export class NavBarsModule { }
