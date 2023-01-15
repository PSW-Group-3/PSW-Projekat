import { LoginService } from './../../hospital/services/login.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-patient-nav-bar',
  templateUrl: './patient-nav-bar.component.html',
  styleUrls: ['./patient-nav-bar.component.css']
})
export class PatientNavBarComponent implements OnInit {

  constructor(private loginService: LoginService) { }

  ngOnInit(): void {

  }

  
  logout(){
    this.loginService.logout().subscribe(res => {
      
    }) 
  }

}
