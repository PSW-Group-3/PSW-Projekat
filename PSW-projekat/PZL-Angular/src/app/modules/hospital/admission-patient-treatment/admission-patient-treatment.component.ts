import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { BedDto } from '../model/bedDto';
import { PatientDto } from '../model/patientDto';
import { Room } from '../model/room.model';
import { Bed } from '../model/bed';
import { Therapy } from '../model/therapy';
import { Treatment } from '../model/treatment';
import { TreatmentState } from '../model/treatmentState';
import { RoomService } from '../services/room.service';
import { TreatmentService } from '../services/treatment.service';
import { UserService } from '../services/user.service';
import { BedService } from '../services/bed.service';
import { PatientService } from '../services/patient.service';
import { Role } from '../model/role';
import { RoomDto } from "../model/roomDto";
import { Blood } from '../model/blood';
import { Medicine } from '../model/medicine';
import { TherapyService } from '../services/therapy.service';
import { MedicineService } from '../services/medicine.service';
import { BloodService } from '../services/blood.service';
import { LoginService } from '../services/login.service';
import { RoomName } from '../model/roomName';
import { RoomFloor } from '../model/roomFloor';
import { RoomType } from '../model/roomType';



@Component({
  selector: 'app-admission-patient-treatment',
  templateUrl: './admission-patient-treatment.component.html',
  styleUrls: ['./admission-patient-treatment.component.css']
})
export class AdmissionPatientTreatmentComponent implements OnInit {

  public treatment: Treatment = new Treatment(0, false, PatientDto, Date(), new Date(),'', '', TreatmentState.close, Therapy, RoomDto);
  public dataSourcePatients = new MatTableDataSource<PatientDto>();
  public dataSourceRooms = new MatTableDataSource<RoomDto>();
  public dataSourceBeds = new MatTableDataSource<BedDto>();
  public dataSourceMedicines = new MatTableDataSource<Medicine>();
  public dataSourceBloods = new MatTableDataSource<Blood>();

  public patients: PatientDto[] = [];
  public rooms: RoomDto[] = [];
  public kreveti: BedDto[] = [];
  public bloods: Blood[] = [];
  public medicines: Medicine[] = [];

  public idk: number = 0;
  public pomK: BedDto;
  public idp: number = 0;
  public soba: RoomDto;
  public bloodType: String;

  public therapy: Therapy = new Therapy(0, false, Medicine, Blood, 0, 0);

  constructor(private loginService: LoginService, private treatmentService: TreatmentService, private roomService: RoomService, 
              private bedService: BedService, private patientService: PatientService, 
              private therapyService: TherapyService, private medicineService: MedicineService,
              private bloodService: BloodService, private router: Router) { }

  public handleOptionChangeRoom() {
     this.idk = this.treatment.roomDto.id;
     this.kreveti = [];
    this.roomService.GetAllBedsByRoom(this.idk).subscribe(res =>
      {
        let result = Object.values(JSON.parse(JSON.stringify(res)));
        result.forEach((element: any) => {
          var bed = new BedDto(element.id, element.deleted, element.name, element.bedState, element.patientDto, element.quantity);
          this.kreveti.push(bed);
      })
      this.dataSourceBeds.data = this.kreveti;
      console.log(this.kreveti);

    })
  } 

  ngOnInit(): void { 
    this.patientService.GetPatientsNoTreatment().subscribe(res => {
      let result = Object.values(JSON.parse(JSON.stringify(res)));
      result.forEach((element: any) => {
        var app = new PatientDto(element.id, element.name, element.surname, element.email, element.username, element.role);
        this.patients.push(app);
      });
      this.dataSourcePatients.data = this.patients;
    })

    this.roomService.getRooms().subscribe(res => {
      let result = Object.values(JSON.parse(JSON.stringify(res)));
      result.forEach((element: any) => {

        var app = new RoomDto(element.id, element.number, element.floor, element.roomType, element.bedDtos);
        this.rooms.push(app);
        console.log(this.rooms);
      });
      this.dataSourceRooms.data = this.rooms;
    })

    this.roomService.getAllStorageMedicnes().subscribe(res => {
      let result = Object.values(JSON.parse(JSON.stringify(res)));
      result.forEach((element: any) => {
        var app = new Medicine(element.id, element.deleted, element.name, element.quantity);
        this.medicines.push(app);
      });
      this.dataSourceMedicines.data = this.medicines;
    })

    this.roomService.getAllStorageBloods().subscribe(res => {
      let result = Object.values(JSON.parse(JSON.stringify(res)));
      result.forEach((element: any) => {
        var app = new Blood(element.id, element.deleted, element.bloodType, element.quantity);
        this.bloods.push(app);
      });
      this.dataSourceBloods.data = this.bloods;
    })

  }

  public handleOptionChangePatient() {
    return this.treatment.patient;
  }

  public ConvertToNumber(obj: any): any{
    switch(obj){
      case 'O-': return 7;
      case 'A-': return 4;
      case 'B-': return 5;
      case 'AB-': return 6;
      case 'O+': return 3;
      case 'A+': return 0;
      case 'B+': return 1;
      case 'AB+': return 2;
      default: return 0; 
    }
  }

  logoutUser(){
    this.loginService.logout().subscribe(res => {
    })
  }

  public handleOptionChangeBed() {
    this.pomK.patientDto = this.handleOptionChangePatient();
    this.bedService.updateBed(this.pomK).subscribe(res => {
      window.confirm("The bed was succesed update!");
    })
  }

  public createTreatment() {
    console.log(this.treatment);

    if (!this.isValidInput()){
      window.confirm("The fields are not valid entered!")
    }

    this.treatment.therapy = this.therapy;


    if(!this.ZaKrv() && !this.ZaLek()){

    this.roomService.getAllStorageMedicnes().subscribe(res => {
      let result = Object.values(JSON.parse(JSON.stringify(res)));
      result.forEach((element: any) => {
        var app = new Medicine(element.id, element.deleted, element.name, element.quantity);

        if(element.name == this.therapy.medicine.name)
        {
          if(element.quantity < this.therapy.quantitytMedicine) {
          window.confirm("There is not enough amount of selected medicine"); }
          else {

            this.medicineService.updateQuantityMedicine(this.therapy.medicine, this.therapy.quantitytMedicine).subscribe(res =>{
              window.confirm("The quantity of medicine is changed!");
            });

            this.roomService.getAllStorageBloods().subscribe(res => {
              let result = Object.values(JSON.parse(JSON.stringify(res)));
              result.forEach((element: any) => {
                var app = new Blood(element.id, element.deleted, element.bloodType, element.quantity);
        
                if(element.bloodType == this.therapy.blood.bloodType)
                {
                  if(element.quantity < this.therapy.quantityBlood) {
                  window.confirm("There is not enough amount of selected blood"); }
                  else {
        
                    this.bloodService.updateQuantityBlood(this.therapy.blood, this.therapy.quantityBlood).subscribe(res =>{
                      window.confirm("The quantity of blood is changed!");
                    });
        
                    this.treatmentService.createTreatment(this.treatment).subscribe(res => {
                      //window.confirm("The patient was admitted for inpatient treatment!");
                      this.router.navigate(['/treatments']);

                    });
                  } 
                }});})
          } 
        }});})
    }

  }

  public ZaKrv(): boolean
  {
    if(this.therapy.quantityBlood != 0 && this.therapy.quantitytMedicine == 0)
    {
    this.roomService.getAllStorageBloods().subscribe(res => {
      let result = Object.values(JSON.parse(JSON.stringify(res)));
      result.forEach((element: any) => {
        var app = new Blood(element.id, element.deleted, element.bloodType, element.quantity);

        if(element.bloodType == this.therapy.blood.bloodType)
        {
          if(element.quantity < this.therapy.quantityBlood) {
          window.confirm("There is not enough amount of selected blood"); }
          else {

            this.bloodService.updateQuantityBlood(this.therapy.blood, this.therapy.quantityBlood).subscribe(res =>{
              window.confirm("The quantity of blood is changed!");
            });

            this.treatmentService.createTreatment(this.treatment).subscribe(res => {
              //window.confirm("The patient was admitted for inpatient treatment!");
              this.router.navigate(['/treatments']);

            });
          } 
        }});})
        return true;
      } 
      else{
      return false;
      }
  }

  public ZaLek() : boolean
  {
    if(this.therapy.quantitytMedicine != 0 && this.therapy.quantityBlood == 0)
    {
    this.roomService.getAllStorageMedicnes().subscribe(res => {
      let result = Object.values(JSON.parse(JSON.stringify(res)));
      result.forEach((element: any) => {
        var app = new Medicine(element.id, element.deleted, element.name, element.quantity);

        if(element.name == this.therapy.medicine.name)
        {
          if(element.quantity < this.therapy.quantitytMedicine) {
          window.confirm("There is not enough amount of selected medicine"); }
          else {

            this.medicineService.updateQuantityMedicine(this.therapy.medicine, this.therapy.quantitytMedicine).subscribe(res =>{
              window.confirm("The quantity of medicine is changed!");
            });

            this.treatmentService.createTreatment(this.treatment).subscribe(res => {
              //window.confirm("The patient was admitted for inpatient treatment!");
              this.router.navigate(['/treatments']);

            });
          } 
        }});})
        return true;
      }
      else{
      return false;
      }
  }

  private isValidInput(): boolean {
    return this.treatment?.patient.toString() != ''  && this.treatment?.dateAdmission.toString() != '' && this.treatment?.reasonForAdmission.toString() != ''  && this.treatment?.roomDto.toString() != '';  }

}
