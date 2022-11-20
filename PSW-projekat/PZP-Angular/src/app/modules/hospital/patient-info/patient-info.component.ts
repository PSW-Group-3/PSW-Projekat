import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { RegisterPatientDto } from '../model/registerPatientDto.model';
import { LoginService } from '../services/login.service';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-user-info',
  templateUrl: './patient-info.component.html',
  styleUrls: ['./patient-info.component.css']
})
export class PatientInfoComponent implements OnInit {

  public patientInfo: RegisterPatientDto = new RegisterPatientDto();

  constructor(private userService: UserService, private router: Router, private loginService: LoginService) { }

  ngOnInit(): void {
    this.userService.getLoggedPatient().subscribe(res => {
      this.patientInfo = res;
    });
  }

  logout(){
    this.loginService.logout().subscribe(res => {
      
    }) 
  }

}
