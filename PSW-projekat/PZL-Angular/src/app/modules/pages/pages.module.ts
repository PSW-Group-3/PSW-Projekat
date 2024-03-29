import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AppRoutingModule } from 'src/app/app-routing.module'; 
import { HomeDoctorComponent } from './homeDoctor/homeDoctor.component';
import { HomeManagerComponent } from './homeManager/homeManager.component';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuardDoctor } from '../hospital/services/authDoctor.guard';
import { AuthGuardManager } from '../hospital/services/authManager.guard';
import { ForbiddenAccessComponent } from './forbidden-access/forbidden-access.component';

const routes: Routes = [
  { path: 'homeManager', component: HomeManagerComponent },
  { path: 'homeDoctor', component: HomeDoctorComponent },
  { path: 'forbidden-access', component: ForbiddenAccessComponent },
];

@NgModule({
  declarations: [
    HomeDoctorComponent,
    HomeManagerComponent,
    ForbiddenAccessComponent,
  ],
  imports: [
    CommonModule,
    AppRoutingModule,
    RouterModule.forChild(routes),
  ],
  providers: [ 
    AuthGuardManager, 
    AuthGuardDoctor,
  ]
})
export class PagesModule { }
