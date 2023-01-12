import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DoctorNavBarComponent } from './doctor-nav-bar/doctor-nav-bar.component';
import { ManagerNavBarComponent } from './manager-nav-bar/manager-nav-bar.component';



@NgModule({
  declarations: [
    DoctorNavBarComponent,
    ManagerNavBarComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    DoctorNavBarComponent,
    ManagerNavBarComponent
  ]
})
export class NavBarsModule { }
