import { EmergencyBloodRequestService } from '../services/emergency-blood-request.service';
import { ToastrService } from 'ngx-toastr';
import { BloodType } from '../model/blood-type';
import { Component, OnInit } from '@angular/core';
import { Report } from '../model/report.model';

@Component({
  selector: 'app-requests-report',
  templateUrl: './requests-report.component.html',
  styleUrls: ['./requests-report.component.css'],
})
export class RequestsReportComponent implements OnInit {
  public request = {
    StartDate: new Date(),
    EndDate: new Date(),
    BloodType,
  };
  public errorMessage: any;
  public bloodTypes: BloodType[] = [
    BloodType.ON,
    BloodType.AN,
    BloodType.BN,
    BloodType.ABN,
    BloodType.OP,
    BloodType.AP,
    BloodType.BP,
    BloodType.ABP,
  ];
  public report: Report = new Report();
  constructor(
    private emergencyService: EmergencyBloodRequestService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {}
  public accept() {
    console.log('accept', this.request);
    this.emergencyService.getReport(this.request).subscribe(
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
  public ConvertToString(obj: BloodType): String {
    switch (obj) {
      case 0:
        return 'ON';
      case 1:
        return 'AN';
      case 2:
        return 'BN';
      case 3:
        return 'ABN';
      case 4:
        return 'OP';
      case 5:
        return 'AP';
      case 6:
        return 'BP';
      case 7:
        return 'ABP';
      default:
        return '333';
    }
  }
}
