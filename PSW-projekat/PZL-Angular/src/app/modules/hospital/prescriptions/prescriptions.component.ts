import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { Examination } from '../model/examination';
import { Prescription } from '../model/prescription';
import { ExaminationService } from '../services/examination.service';

@Component({
  selector: 'app-prescriptions',
  templateUrl: './prescriptions.component.html',
  styleUrls: ['./prescriptions.component.css']
})
export class PrescriptionsComponent implements OnInit {

  public dataSource = new MatTableDataSource<Examination>();
  displayedColumns: string[] = ['patient', 'Medicine', 'Description'];
  public descriptions: string[] = [];
  public searchedDescriptions: string[] = [];
  public emptyDescriptions: string[] = [];

  public examinations: Examination[] = [];
  public searchedExaminations: Examination[] = [];
  public uzorakPretrage: string;

  public recepti: Prescription[] = [];
  public pomocna: Prescription[] = [];
  
  constructor(private examinationService: ExaminationService, private router: Router) { }

  ngOnInit(): void {

    this.examinationService.GetAllExaminationsByDoctor(Number(localStorage.getItem("currentUserId"))).subscribe(res => {
      let result = Object.values(JSON.parse(JSON.stringify(res)));
      result.forEach((element: any) => {
        var app = new Examination(0, false, null, null, null , '');
        var app = new Examination(element.id, element.deleted, element.appointmentDto, element.prescriptions, element.symptoms, element.report);
        console.log(app.prescriptions);
        
        if (app.prescriptions.length != 0) {
          this.examinations.push(app);
          this.recepti = app.prescriptions;
        }
      });
      this.dataSource.data = this.examinations;
    })
  }

  public searchReports() {
    console.log(this.descriptions);
    console.log(this.uzorakPretrage);

    this.searchedExaminations = [];

    this.examinationService.GetAllExaminationsBySearchPrescription(this.uzorakPretrage, Number(localStorage.getItem("currentUserId"))).subscribe(res => {
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



