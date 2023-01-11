import { EmergencyBloodRequestService } from '../services/emergency-blood-request.service';
import { ToastrService } from 'ngx-toastr';
import { Component, OnInit } from '@angular/core';
import { Report } from '../model/report.model';

@Component({
  selector: 'app-requests-report',
  templateUrl: './requests-report.component.html',
  styleUrls: ['./requests-report.component.css'],
})
export class RequestsReportComponent implements OnInit {
  public dates = { StartDate: new Date(), EndDate: new Date() };
  public errorMessage: any;
  public report: Report = new Report();
  constructor(
    private emergencyService: EmergencyBloodRequestService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {}
  public accept() {
    // const datesString = {
    //   StartDate: this.dates.StartDate.toJSON,
    //   EndDate: this.dates.EndDate.toJSON,
    // };
    console.log('accept', this.dates);
    this.emergencyService.getReport(this.dates).subscribe(
      (res) => {
        console.log(res);
        this.report = res;
      },
      (error) => {
        this.errorMessage = error;
        this.toastr.error(error);
      }
    );
  }
}
