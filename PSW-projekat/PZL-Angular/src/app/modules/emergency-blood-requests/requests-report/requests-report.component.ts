import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-requests-report',
  templateUrl: './requests-report.component.html',
  styleUrls: ['./requests-report.component.css'],
})
export class RequestsReportComponent implements OnInit {
  public dates = { start: new Date(), end: new Date() };
  public requests = 'asd';
  constructor() {}

  ngOnInit(): void {}
  public accept() {
    console.log('accept', this.dates);
  }
}
