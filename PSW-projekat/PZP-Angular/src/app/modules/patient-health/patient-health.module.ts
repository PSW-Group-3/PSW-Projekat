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
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import { AddMealDialogComponent } from './add-meal-dialog/add-meal-dialog.component';
import {MatDividerModule} from '@angular/material/divider';
import { AddWaterDialogComponent } from './add-water-dialog/add-water-dialog.component';
import { WorkoutsCalendarComponent } from './workouts-calendar/workouts-calendar.component';
import { WorkoutsOverviewComponent } from './workouts-overview/workouts-overview.component';
import { MealStatisticsComponent } from './meal-statistics/meal-statistics.component';
import { AddWorkoutDialogComponent } from './add-workout-dialog/add-workout-dialog.component';
import { AddGymworkoutDialogComponent } from './add-gymworkout-dialog/add-gymworkout-dialog.component';
import { WorkoutsStatisticsComponent } from './workouts-statistics/workouts-statistics.component';


const routes: Routes = [
  { path: 'patientHealth', component: PatientHealthScoreComponent, canActivate: [ AuthGuard ] },
  { path: 'dietOverview', component: DietOverviewComponent, canActivate: [ AuthGuard ] },
  { path: 'workoutsOverview', component: WorkoutsOverviewComponent, canActivate: [ AuthGuard ] },

]

@NgModule({
  declarations: [
    PatientHealthScoreComponent,
    DietOverviewComponent,
    AddMealDialogComponent,
    AddWaterDialogComponent,
    WorkoutsCalendarComponent,
    WorkoutsOverviewComponent,
    MealStatisticsComponent,
    AddWorkoutDialogComponent,
    AddGymworkoutDialogComponent,
    WorkoutsStatisticsComponent,
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
    ReactiveFormsModule,
    MatInputModule,
    MatFormFieldModule,
    RouterModule.forChild(routes),
    NavBarsModule,
  ]
})
export class PatientHealthModule { }
