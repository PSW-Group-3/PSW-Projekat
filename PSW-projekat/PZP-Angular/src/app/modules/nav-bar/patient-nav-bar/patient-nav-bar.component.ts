import { LoginService } from './../../hospital/services/login.service';
import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-patient-nav-bar',
  templateUrl: './patient-nav-bar.component.html',
  styleUrls: ['./patient-nav-bar.component.css']
})
export class PatientNavBarComponent implements OnInit {
  @Input() isLoggedIn: boolean = true;

  isNavActive = false;

  constructor(private loginService: LoginService) { }

  ngOnInit(): void {
  }

  toggleNav() {
    this.isNavActive = !this.isNavActive;
  }
  
  logout(){
    this.loginService.logout().subscribe(res => {
      
    }) 
  }

}
