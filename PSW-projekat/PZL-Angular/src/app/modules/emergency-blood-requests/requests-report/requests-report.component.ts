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
  public dates = { start: new Date(), end: new Date() };
  public errorMessage: any;
  public report: Report = new Report();
  constructor(
    private emergencyService: EmergencyBloodRequestService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {}
  public accept() {
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
