import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '../hospital/services/auth.guard';
import { PatientHealthScoreComponent } from './patient-health-score/patient-health-score.component';
import { MatGridListModule } from '@angular/material/grid-list';
import { DietOverviewComponent } from './diet-overview/diet-overview.component';
import { NavBarsModule } from '../nav-bar/nav-bars.module';
import { MaterialModule } from 'src/app/material/material.module';
import { MatDialogModule} from '@angular/material/dialog';
import { FormsModule } from '@angular/forms';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import { AddMealDialogComponent } from './add-meal-dialog/add-meal-dialog.component';
import {MatDividerModule} from '@angular/material/divider';


const routes: Routes = [
  { path: 'patientHealth', component: PatientHealthScoreComponent, canActivate: [ AuthGuard ] },
  { path: 'dietOverview', component: DietOverviewComponent, canActivate: [ AuthGuard ] },
]

@NgModule({
  declarations: [
    PatientHealthScoreComponent,
    DietOverviewComponent,
    AddMealDialogComponent,
  ],
  exports: [RouterModule],
  providers: [AuthGuard],
  imports: [
    CommonModule,
    MatDialogModule,
    MaterialModule,
    MatDividerModule,
    MatGridListModule,
    FormsModule,
    MatInputModule,
    MatFormFieldModule,
    RouterModule.forChild(routes),
    NavBarsModule,
  ]
})
export class PatientHealthModule { }
