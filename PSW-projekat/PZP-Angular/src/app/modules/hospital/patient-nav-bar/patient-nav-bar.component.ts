import { Component, OnInit } from '@angular/core';
import { LoginService } from '../services/login.service';

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
    this.loginService.logout().subscribe(res => {}) 
  }
}
