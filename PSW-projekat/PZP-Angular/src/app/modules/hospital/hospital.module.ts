import { CreateAppointment } from './create-appointment/create-appointment.component';
import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { RouterModule, Routes } from "@angular/router";
import { MaterialModule } from "src/app/material/material.module";
import { CreateRoomComponent } from "./create-room/create-room.component";
import { RoomDetailComponent } from "./room-detail/room-detail.component";
import { RoomsComponent } from "./rooms/rooms.component";
import { UpdateRoomComponent } from "./update-room/update-room.component";
import { FeedbacksComponent } from './feedbacks/feedbacks.component';
import { CreateFeedbackComponent } from './create-feedback/create-feedback.component';
import { WelcomeComponent } from "./welcome/welcome.component";
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AccountActivationThanks } from "./account-activation/account-activation.component";
import { AccountActivationInfo } from "./account-activation-info/account-activation-info.component";
import { PatientInfoComponent } from './patient-info/patient-info.component';
import { AuthGuard } from "./services/auth.guard";
import { ScheduleAppointmentComponent } from './schedule-appointment/schedule-appointment.component';
import { ViewAllOpenTendersComponent } from './blood-bank-tenders/view-all-open-tenders/view-all-open-tenders.component';
import { TenderDetailsComponent } from './blood-bank-tenders/tender-details/tender-details.component';
import { BankLoginComponent } from './bank-login/bank-login.component';
import { MatNativeDateModule } from '@angular/material/core';
import { PatientAppointmentsComponent } from './patient-appointments/patient-appointments.component';
import { NavBarsModule } from "../nav-bar/nav-bars.module";
import { PatientHealthScoreComponent } from './patient-health-score/patient-health-score.component';

const routes: Routes = [
  { path: 'rooms', component: RoomsComponent },
  { path: 'rooms/add', component: CreateRoomComponent },
  { path: 'rooms/:id', component: RoomDetailComponent },
  { path: 'rooms/:id/update', component: UpdateRoomComponent },
  { path: 'feedbacks', component: FeedbacksComponent },
  { path: 'feedbacks/add', component: CreateFeedbackComponent },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'account-activation', component: AccountActivationThanks },
  { path: 'account-activation-info', component: AccountActivationInfo },
  { path: 'patientInfo', component: PatientInfoComponent, canActivate: [ AuthGuard ] },
  { path: 'patientHealth', component: PatientHealthScoreComponent, canActivate: [ AuthGuard ] },
  { path: 'scheduleAppointment', component: ScheduleAppointmentComponent, canActivate: [ AuthGuard ] },
  { path: 'view-all-open-tenders', component:ViewAllOpenTendersComponent},
  { path: 'tenders-details/:id', component:TenderDetailsComponent},
  { path: 'bank-login', component: BankLoginComponent},
  { path: 'appointments', component: PatientAppointmentsComponent, canActivate: [ AuthGuard ] },
  { path: 'createAppointment', component: CreateAppointment, canActivate: [ AuthGuard ] },

]

@NgModule({
    declarations: [
        RoomsComponent,
        RoomDetailComponent,
        CreateRoomComponent,
        UpdateRoomComponent,
        FeedbacksComponent,
        CreateFeedbackComponent,
        WelcomeComponent,
        LoginComponent,
        RegisterComponent,
        PatientInfoComponent,
        ScheduleAppointmentComponent,
        ViewAllOpenTendersComponent,
        TenderDetailsComponent,
        BankLoginComponent,
        PatientAppointmentsComponent,
        CreateAppointment,
        PatientHealthScoreComponent,
    ],
    exports: [RouterModule],
    providers: [AuthGuard],
    imports: [
        CommonModule,
        MaterialModule,
        FormsModule,
        ReactiveFormsModule,
        MatNativeDateModule,
        RouterModule.forChild(routes),
        NavBarsModule
    ]
})
export class HospitalModule { }
