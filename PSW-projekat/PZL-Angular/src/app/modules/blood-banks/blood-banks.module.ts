import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { MatButtonModule } from '@angular/material/button';
import { MatFormField, MatFormFieldModule } from '@angular/material/form-field';
import { MatInput, MatInputModule } from '@angular/material/input';
import { MaterialModule } from 'src/app/material/material.module';
import { MatSelectModule} from '@angular/material/select';
import { MatDatepickerModule} from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core'; 
import { MAT_DATE_LOCALE } from '@angular/material/core';
import { FormsModule } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { HttpClientModule } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { BloodBankChangePasswordComponent } from './blood-bank-change-password/blood-bank-change-password.component';
import { BloodBankRegistrationComponent } from './blood-bank-registration/blood-bank-registration.component';
import { BloodBanksComponent } from './blood-banks/blood-banks.component';
import { BloodRequestsComponent } from './blood-requests/blood-requests.component';
import { ReportSettingsComponent } from './report-settings/report-settings.component';
import { NewsComponent } from './news/news.component';
import { NavBarsModule } from '../nav-bars/nav-bars.module';

const routes: Routes = [
  {path: 'blood-bank-registration', component: BloodBankRegistrationComponent},
  {path: 'blood-banks', component: BloodBanksComponent},
  {path: 'blood-bank-change-password', component: BloodBankChangePasswordComponent},
  {path: 'blood-requests', component: BloodRequestsComponent},
  {path: 'report-settings', component: ReportSettingsComponent},
  {path: 'news', component: NewsComponent }
];


@NgModule({
  declarations: [
    BloodBankChangePasswordComponent,
    BloodBankRegistrationComponent,
    BloodBanksComponent,
    BloodRequestsComponent,
    ReportSettingsComponent,
    NewsComponent
  ],
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
    HttpClientModule,
    FormsModule,
    MaterialModule,
    RouterModule.forChild(routes),
    CommonModule,
    ToastrModule.forRoot(),
    NavBarsModule
  ],
  providers: [  
    MatDatepickerModule,  
    { provide: MAT_DATE_LOCALE, useValue: 'en-GB' }
  ],
  
  
  exports: [ RouterModule ]
})
export class BloodBanksModule {}
