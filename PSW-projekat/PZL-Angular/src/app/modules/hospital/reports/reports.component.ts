import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { Examination } from '../model/examination';
import { ExaminationService } from '../services/examination.service';

@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html',
  styleUrls: ['./reports.component.css']
})
export class ReportsComponent implements OnInit {

  public dataSource = new MatTableDataSource<Examination>();
  displayedColumns: string[] = ['patient', 'dateTime', 'reportDescription'];
  public reports: string[] = [];
  public searchedReports: string[] = [];
  public emptyReports: string[] = [];

  public examinations: Examination[] = [];
  public searchedExaminations: Examination[] = [];
  public uzorakPretrage: string;
  
  constructor(private examinationService: ExaminationService, private router: Router) { }

  ngOnInit(): void {

    this.examinationService.GetAllExaminationsByDoctor(Number(localStorage.getItem("currentUserId"))).subscribe(res => {
      let result = Object.values(JSON.parse(JSON.stringify(res)));
      result.forEach((element: any) => {
        var app = new Examination(element.id, element.deleted, element.appointmentDto, element.prescriptions, element.symptoms, element.report);
        this.examinations.push(app);
      });
      this.dataSource.data = this.examinations;
    })
  }

  public searchReports() {
    console.log(this.reports);
    console.log(this.uzorakPretrage);

    this.searchedExaminations = [];

    this.examinationService.GetAllExaminationsBySearchReport(this.uzorakPretrage, Number(localStorage.getItem("currentUserId"))).subscribe(res => {
      let result = Object.values(JSON.parse(JSON.stringify(res)));
      result.forEach((element: any) => {
        var app = new Examination(element.id, element.deleted, element.appointmentDto, element.prescriptions, element.symptoms, element.report);
        this.searchedExaminations.push(app);
      });
      
      this.dataSource.data = this.searchedExaminations;
      console.log(this.searchedExaminations);

    })
  }

  public restart() {
    this.uzorakPretrage = '';
    this.dataSource.data = this.examinations;
  }
}
