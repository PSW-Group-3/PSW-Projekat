import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { MaterialModule } from "src/app/material/material.module";
import { MatInputModule} from '@angular/material/input';
import { CreateRoomComponent } from "./create-room/create-room.component";
import { RoomDetailComponent } from "./room-detail/room-detail.component";
import { RoomsComponent } from "./rooms/rooms.component";
import { UpdateRoomComponent } from "./update-room/update-room.component";
import { FeedbacksComponent } from './feedbacks/feedbacks.component';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { AppointmentsComponent } from './appointments/appointments.component';
import { CreateAppointmentComponent } from './create-appointment/create-appointment.component';
import { UpdateAppointmentComponent } from "./update-appointment/update-appointment.component";
import { StatisticsComponent } from './statistics/statistics.component';
import { NgChartsModule } from 'ng2-charts';
import { AdmissionPatientTreatmentComponent } from './admission-patient-treatment/admission-patient-treatment.component';
import { DischargePatientComponent } from './discharge-patient/discharge-patient.component';
import { LoginComponent } from "./login/login.component";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { AuthGuardDoctor } from '../hospital/services/authDoctor.guard';
import { AuthGuardManager } from '../hospital/services/authManager.guard';
import { CreateBloodRequestComponent } from './create-blood-request/create-blood-request.component';
import { BloodConsumptionComponent } from "./blood-consumption/blood-consumption.component";
import { CouncilsComponent } from './councils/councils.component';
import { ShowDoctorsPipe } from './councils/show-doctors.pipe';
import { MedicalExaminationFinish, MedicalExaminationPatientComponent, MedicalPrescriptionShowComponent, MedicalPrescriptionComponent, MedicalReportComponent } from './medical-examination-patient/medical-examination-patient.component';
import { ShowMedicinePipe } from './medical-examination-patient/show-medicine.pipe';

const routes: Routes = [
  { path: 'rooms', component: RoomsComponent, canActivate: [ AuthGuardManager ] },
  { path: 'rooms/add', component: CreateRoomComponent, canActivate: [ AuthGuardManager ] },
  { path: 'rooms/:id', component: RoomDetailComponent, canActivate: [ AuthGuardManager ] },
  { path: 'rooms/:id/update', component: UpdateRoomComponent, canActivate: [ AuthGuardManager ] },
  { path: 'feedbacks', component: FeedbacksComponent, canActivate: [ AuthGuardManager ] },
  { path: 'appointments', component: AppointmentsComponent, canActivate: [ AuthGuardDoctor ] },
  { path: 'appointments/doctor/:id', component: AppointmentsComponent, canActivate: [ AuthGuardDoctor ] },
  { path: 'appointments/add', component: CreateAppointmentComponent, canActivate: [ AuthGuardDoctor ] },
  { path: 'appointments/:id/update', component: UpdateAppointmentComponent, canActivate: [ AuthGuardDoctor ] },
  { path: 'statistics', component: StatisticsComponent },
  { path: 'treatments/add', component: AdmissionPatientTreatmentComponent },
  { path: 'treatments/:id/update', component: DischargePatientComponent },
  { path: 'login', component: LoginComponent },  
  { path: 'bloodRequest/add', component: CreateBloodRequestComponent },
  { path: 'bloodConsumption/add', component: BloodConsumptionComponent},
  { path: 'councilOfDoctors', component: CouncilsComponent},
  { path: 'examinations/add/:id', component: MedicalExaminationPatientComponent},
  { path: 'examinations/report', component: MedicalReportComponent},
  { path: 'examinations/prescriptionShow', component: MedicalPrescriptionShowComponent},
  { path: 'examinations/prescription', component: MedicalPrescriptionComponent},
  { path: 'examinations/finish', component: MedicalExaminationFinish}

];

@NgModule({
  declarations: [
    RoomsComponent,
    RoomDetailComponent,
    CreateRoomComponent,
    UpdateRoomComponent,
    FeedbacksComponent,
    AppointmentsComponent,
    CreateAppointmentComponent,
    UpdateAppointmentComponent,
    StatisticsComponent,
    AdmissionPatientTreatmentComponent,
    DischargePatientComponent,
    LoginComponent,
    CreateBloodRequestComponent,
    BloodConsumptionComponent,
    CouncilsComponent,
    ShowDoctorsPipe,
    MedicalExaminationPatientComponent,
    MedicalReportComponent,
    MedicalPrescriptionShowComponent,
    MedicalPrescriptionComponent,
    MedicalExaminationFinish,
    ShowMedicinePipe
  ],
  imports: [
    CommonModule,
    MaterialModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild(routes),
    MatTableModule,
    MatPaginatorModule,
    MatSortModule,
    MatInputModule,
    MatFormFieldModule,
    NgChartsModule,
    MatSelectModule
  ],
  exports: [ RouterModule ]
})
export class HospitalModule { }