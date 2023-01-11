import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RequestBloodComponent } from './request-blood/request-blood.component';
import { RouterModule, Routes } from '@angular/router';
import { ToastrModule } from 'ngx-toastr';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatInputModule } from '@angular/material/input';
import { MatRadioModule } from '@angular/material/radio';
import { MaterialModule } from 'src/app/material/material.module';
import { RequestsReportComponent } from './requests-report/requests-report.component';

const routes: Routes = [
  { path: 'emergency-blood-request', component: RequestBloodComponent },
  {
    path: 'emergency-blood-request-report',
    component: RequestsReportComponent,
  },
];

@NgModule({
  declarations: [RequestBloodComponent, RequestsReportComponent],
  imports: [
    CommonModule,
    BrowserModule,
    BrowserAnimationsModule,
    MatTableModule,
    MatButtonModule,
    MatFormFieldModule,
    MatSelectModule,
    MatInputModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatRadioModule,
    HttpClientModule,
    FormsModule,
    MaterialModule,
    RouterModule.forChild(routes),
    ToastrModule.forRoot(),
  ],

  exports: [RouterModule],
})
export class EmergencyBloodRequestsModule {}
