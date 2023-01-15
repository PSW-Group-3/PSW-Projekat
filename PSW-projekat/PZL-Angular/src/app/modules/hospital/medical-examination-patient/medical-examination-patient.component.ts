import { Component, ComponentFactoryResolver, Inject, Input, OnInit } from '@angular/core';
import { ViewChild, AfterViewInit } from '@angular/core';
import { MatDialog, MatDialogConfig, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { Appointment } from '../model/appointment.model';
import { Examination } from '../model/examination';
import { Medicine } from '../model/medicine';
import { PatientDto } from '../model/patientDto';
import { Prescription } from '../model/prescription';
import { Symptom } from '../model/symptom';
import { AppointmentService } from '../services/appointment.service';
import { ExaminationService } from '../services/examination.service';
import { RoomService } from '../services/room.service';
import { SymptomService } from '../services/symptom.service';
import { Output, EventEmitter } from '@angular/core';
import { DoctorExaminationEventService } from '../services/doctor-examination-event.service';
import { DoctorExaminationDTO } from '../model/doctorExaminationDTO';

@Component({
  selector: 'app-medical-examination-patient',
  templateUrl: './medical-examination-patient.component.html',
  styleUrls: ['./medical-examination-patient.component.css']
})
export class MedicalExaminationPatientComponent implements OnInit{

  public dataSourceSymptoms = new MatTableDataSource<Symptom>();

  public sRecepti: Prescription[] = [];
  public symptoms: Symptom[] = [];
  public simptomi: Symptom[] = [];
  public prescriptions: Prescription[] = [];
  public patient: PatientDto = new PatientDto(0, '','','','', 0);
  public appointment: any;
  public appointmentProba: Appointment = new Appointment(0, false, '', '', Date(), Date());
  public examination: Examination = new Examination(0, false, Appointment, this.sRecepti, this.simptomi, '');
  public terminId: number;

  constructor(private symptomService: SymptomService, private route: ActivatedRoute, private appointmentService: AppointmentService,
     private router: Router, private dialog: MatDialog,private eventSourcingService: DoctorExaminationEventService) { }

  ngOnInit(): void {
      this.appointmentService.getAppointment(this.terminId).subscribe(res => {
        this.patient = res.patient;
        this.appointment = res;

        this.appointmentProba = new Appointment(this.appointment.id, false,
          this.appointment.patient, this.appointment.doctor, this.appointment.dateTime, this.appointment.cancelationDate);
                
        this.examination.appointment = this.appointmentProba;
      })

    this.symptomService.getSymptoms().subscribe(res => {
      let result = Object.values(JSON.parse(JSON.stringify(res)));
      result.forEach((element: any) => {
        var app = new Symptom(element.id, element.deleted, element.name);
        this.symptoms.push(app);
      });
      this.dataSourceSymptoms.data = this.symptoms;
    })
    this.DoctorExaminationAggregateStartTime();
  }

  public handleOptionChangeSymptoms() {
    return this.examination.symptoms;
  }

  public next(id: number){
    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = false;
    dialogConfig.id = "modal-component-simptomi-next";
    dialogConfig.height = "600px";
    dialogConfig.width = "500px";

    const modalDi = this.dialog.closeAll();
    const modalDialog = this.dialog.open(MedicalReportComponent, dialogConfig);

    modalDialog.componentInstance.identifikator = id;
    modalDialog.componentInstance.pregled = this.examination;
    this.DoctorChoosingExaminationSymptoms();
  }



  public DoctorExaminationAggregateStartTime() {
    this.eventSourcingService.DoctorExaminationAggregateStartTime().subscribe(res => {
      localStorage.setItem('aggregateId', res.toString());
    });
  }

  public DoctorChoosingExaminationSymptoms(){
    let dto: DoctorExaminationDTO = new DoctorExaminationDTO(parseInt(localStorage.getItem("aggregateId")!), this.examination.symptoms,[],"");   
    this.eventSourcingService.DoctorChoosingExaminationSymptoms(dto).subscribe(res => {}); 
  }
}

@Component({
  selector: 'medical-examination-report',
  templateUrl: 'medical-report.component.html',
  styleUrls: ['./medical-examination-patient.component.css']
})
export class MedicalReportComponent implements OnInit {

  public sRecepti: Prescription[] = [];
  public examination: Examination = new Examination(0, false, Appointment, this.sRecepti, null, '');
  public report: string = '';
  public id: number;
  public identifikator: number;
  public pregled: Examination = new Examination(0, false, Appointment, null, null, '')
  public terminId: number;

  constructor(private router: Router, private dialog: MatDialog,private eventSourcingService: DoctorExaminationEventService) { }

  ngOnInit(): void {
    this.terminId =this.pregled.appointment.id;
    this.id = this.identifikator - 1;
    this.examination = this.pregled;
  }

  public BackToExaminationSymptomsChoosing(){
    let dto: DoctorExaminationDTO = new DoctorExaminationDTO(parseInt(localStorage.getItem("aggregateId")!), [],[],"");   
    this.eventSourcingService.BackToExaminationSymptomsChoosing(dto).subscribe(res => {});
  }
  public back(){
    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = false;
    dialogConfig.id = "modal-component-izvestaj-back";
    dialogConfig.height = "600px";
    dialogConfig.width = "500px";

    const modalDi = this.dialog.closeAll();
    const modalDialog = this.dialog.open(MedicalExaminationPatientComponent, dialogConfig);
    modalDialog.componentInstance.terminId = this.pregled.appointment.id;
    this.BackToExaminationSymptomsChoosing();
  }

  public DoctorChoosingExaminationReport() {
    let dto: DoctorExaminationDTO = new DoctorExaminationDTO(parseInt(localStorage.getItem("aggregateId")!), [],[],this.examination.report);   
    this.eventSourcingService.DoctorChoosingExaminationReport(dto).subscribe(res => {}); 
  }

  public next(){
    this.examination.report = this.report;

    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = false;
    dialogConfig.id = "modal-component-izvestaj-next";
    dialogConfig.height = "600px";
    dialogConfig.width = "500px";

    const modalDi = this.dialog.closeAll();
    const modalDialog = this.dialog.open(MedicalPrescriptionShowComponent, dialogConfig);
    modalDialog.componentInstance.pregled = this.examination;
    this.DoctorChoosingExaminationReport();
  }
}

@Component({
  selector: 'medical-examination-prescription-show',
  templateUrl: 'medical-prescription-show.html',
  styleUrls: ['./medical-examination-patient.component.css']
})
export class MedicalPrescriptionShowComponent implements OnInit {

  public lekovi: Medicine[] = [];
  public medicines: Medicine[] = [];
  public prescription: Prescription = new Prescription(0, false, this.lekovi, '');
  public prescriptions: Prescription[] = [];
  public dataSourceMedicines = new MatTableDataSource<Medicine>();
  public sRecepti: Prescription[] = [];
  public examination: Examination = new Examination(0, false, Appointment, this.sRecepti, null, '');
  public pregled: Examination = new Examination(0, false, Appointment, this.sRecepti, null, '');
  public dataSource = new MatTableDataSource<Prescription>();
  displayedColumns: string[] = ['medicineName', 'prescriptionDescription'];
  public recepti: Prescription[] = [];
  public recept: Prescription = new Prescription(0, false, this.lekovi, '');
 
  constructor(private roomService: RoomService, private router: Router, private dialog: MatDialog, private eventSourcingService: DoctorExaminationEventService) { }

  ngOnInit(): void {

    this.roomService.getAllStorageMedicnes().subscribe(res => {
      let result = Object.values(JSON.parse(JSON.stringify(res)));
      result.forEach((element: any) => {
        var app = new Medicine(element.id, element.deleted, element.name, element.quantity);
        this.medicines.push(app);
      });
      this.dataSourceMedicines.data = this.medicines;
    })
    
    this.examination = this.pregled;
    this.dataSource.data = this.recepti;
    this.examination.prescriptions = this.recepti;
  }

  addNewItem(medicines: any, description: any){
    this.recepti.push(new Prescription(0, false, medicines, description));
    this.ngOnInit();
  }

  public handleOptionChangeMedicines() {
    console.log(this.recept.medicines);
    console.log(this.recept);
    return this.recept.medicines;
  }

  public BackToExaminationReportChoosing() {
    let dto: DoctorExaminationDTO = new DoctorExaminationDTO(parseInt(localStorage.getItem("aggregateId")!), [],[],"");   
    this.eventSourcingService.BackToExaminationReportChoosing(dto).subscribe(res => {});
  }

  public back(){
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = false;
    dialogConfig.id = "modal-component-recept-back";
    dialogConfig.height = "600px";
    dialogConfig.width = "500px";

    const modalDi = this.dialog.closeAll();
    const modalDialog = this.dialog.open(MedicalReportComponent, dialogConfig);
    modalDialog.componentInstance.pregled = this.examination;
    this.BackToExaminationReportChoosing();
  }


  public DoctorChoosingExaminationPrescriptions() {
    let dto: DoctorExaminationDTO = new DoctorExaminationDTO(parseInt(localStorage.getItem("aggregateId")!), [],this.examination.prescriptions,"");   
    this.eventSourcingService.DoctorChoosingExaminationPrescriptions(dto).subscribe(res => {}); 
  }

  public next(){    
    const dialogConfig = new MatDialogConfig();
    dialogConfig.disableClose = false;
    dialogConfig.id = "modal-component-recept-next";
    dialogConfig.height = "600px";
    dialogConfig.width = "500px";

    const modalDi = this.dialog.closeAll();
    const modalDialog = this.dialog.open(MedicalExaminationFinish, dialogConfig);
    modalDialog.componentInstance.pregled = this.examination;
    this.DoctorChoosingExaminationPrescriptions();
  }
}

@Component({
  selector: 'medical-examination-finish',
  templateUrl: 'medical-examination-finish.html',
  styleUrls: ['./medical-examination-patient.component.css']
})
export class MedicalExaminationFinish implements OnInit {

  public examination: Examination = new Examination(0, false, null, null, null, '');
  public appointmentProba: Appointment = new Appointment(0, false, '', '', Date(), Date());
  public pregled: Examination = new Examination(0, false, Appointment, null, null, '')
  public recept: Prescription = new Prescription(0, false, null, 'nesto');
  public pomocnaLista: Medicine[] = [];
  public pom: Medicine[] = [];

  constructor(private examinationService: ExaminationService, private router: Router, private dialog: MatDialog,private eventSourcingService: DoctorExaminationEventService) { }

  ngOnInit(): void {
    this.examination = this.pregled;
    this.pom = this.pomocnaFunkcija();

    this.appointmentProba = new Appointment(this.examination.appointment.id, this.examination.appointment.deleted,
      this.examination.appointment.patient, this.examination.appointment.doctor, this.examination.appointment.dateTime, 
      this.examination.appointment.cancelationDate);

    this.examination.appointment = this.appointmentProba;
  }

  pomocnaFunkcija(): any{
    this.examination.prescriptions.forEach((element: any) => {
      element.medicines.forEach((element1: any) => {
        this.pomocnaLista.push(element1);
      })
    });
    return this.pomocnaLista;
  }

  public BackToExaminationPerscriptiosChoosing() {
    let dto: DoctorExaminationDTO = new DoctorExaminationDTO(parseInt(localStorage.getItem("aggregateId")!), [],[],"");   
    this.eventSourcingService.BackToExaminationPerscriptiosChoosing(dto).subscribe(res => {});
  }

  public back(){
    const dialogConfig = new MatDialogConfig();

    dialogConfig.disableClose = false;
    dialogConfig.id = "modal-component-finish-back";
    dialogConfig.height = "600px";
    dialogConfig.width = "500px";

    const modalDi = this.dialog.closeAll();
    const modalDialog = this.dialog.open(MedicalPrescriptionShowComponent, dialogConfig);
    modalDialog.componentInstance.pregled = this.examination;
    this.BackToExaminationPerscriptiosChoosing();
  }

  
  public DoctorExaminationAggregateEndTime() {
    let dto: DoctorExaminationDTO = new DoctorExaminationDTO(parseInt(localStorage.getItem("aggregateId")!), [],[],"");   
    this.eventSourcingService.DoctorExaminationAggregateEndTime(dto).subscribe(res => {});
  }

  public createExamination() {     

    let cb1 = document.getElementById("simptomi") as HTMLInputElement;
    let cb2 = document.getElementById("izvestaj") as HTMLInputElement;
    let cb3 = document.getElementById("lekovi") as HTMLInputElement;
    let sympts = false
    let report = false
    let medication = false

    if(cb1.checked) {
      sympts = true
    }
    if(cb2.checked){
      report = true
    }
    if(cb3.checked){
      medication = true
    }
    
    this.examinationService.createExamination(this.examination, sympts, report, medication).subscribe(res => {
      window.confirm("The medical examination of patient is succesful finished!");
      const modalDi = this.dialog.closeAll();
      
    let fileName = 'report';
    let blob: Blob = res.body as Blob;
    let a = document.createElement('a');
    //a.download=fileName;
    //a.href = window.URL.createObjectURL(blob); 
    const fileURL = URL.createObjectURL(blob);
    window.open(fileURL, '_blank');
    a.click();
    });
    this.DoctorExaminationAggregateEndTime();
  }
}


