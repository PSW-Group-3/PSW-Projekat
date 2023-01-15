import { NodeWithI18n } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { PatientDto } from '../model/patientDto';
import { Room } from '../model/room.model';
import { RoomDto } from '../model/roomDto';
import { Treatment } from '../model/treatment';
import { TreatmentState } from '../model/treatmentState';
import { LoginService } from '../services/login.service';
import { RoomService } from '../services/room.service';
import { TreatmentService } from '../services/treatment.service';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-discharge-patient',
  templateUrl: './discharge-patient.component.html',
  styleUrls: ['./discharge-patient.component.css']
})
export class DischargePatientComponent implements OnInit {

  public treatment: Treatment = new Treatment(0, false, PatientDto, Date(), new Date(),'', '', TreatmentState.close, null, Room);
  public dataSourcePatients = new MatTableDataSource<PatientDto>();
  public dataSourceRooms = new MatTableDataSource<RoomDto>();
  public patients: PatientDto[] = [];
  public rooms: RoomDto[] = [];
  public newPatient1: PatientDto = new PatientDto(0, '','','','', 0);

  constructor(private loginService: LoginService, private treatmentService: TreatmentService, private userService: UserService, private roomService: RoomService, private router: Router, private route: ActivatedRoute ) { }

  ngOnInit(): void {
    this.route.params.subscribe((params: Params) => {
      this.treatmentService.getTreatment(params['id']).subscribe(res => {
        this.newPatient1 = res.patient;
        console.log(this.newPatient1);
        console.log(res);
        this.treatment = res;
    })

    this.roomService.getRooms().subscribe(res => {
      let result = Object.values(JSON.parse(JSON.stringify(res)));
      result.forEach((element: any) => {
        var app = new RoomDto(element.id, element.number, element.floor, element.roomType, element.bedDtos);
        this.rooms.push(app);
      });
      this.dataSourceRooms.data = this.rooms;
    })
   });
  }
  public updateTreatment(): void {
    if (!this.isValidInput()) return;
    this.treatmentService.updateTreatment(this.treatment).subscribe(res => {
      console.log(this.treatment);
      let fileName = 'report';
        let blob: Blob = res.body as Blob;
        let a = document.createElement('a');
        a.download=fileName;
        a.href = window.URL.createObjectURL(blob);
        a.click();
      window.confirm("The patient was discharged from hospital!");
      this.router.navigate(['/treatments']);

    });
  }

  private isValidInput(): boolean {
    return this.treatment?.dateDischarge.toString() != '' && this.treatment?.reasonForDischarge.toString() != '';
  }

  logoutUser(){
    this.loginService.logout().subscribe(res => {
    })
  }

}
