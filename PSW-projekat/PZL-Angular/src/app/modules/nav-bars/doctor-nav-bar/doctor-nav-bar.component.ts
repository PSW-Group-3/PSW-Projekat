import { Component, OnInit } from '@angular/core';
import { LoginService } from '../../hospital/services/login.service';

@Component({
  selector: 'app-doctor-nav-bar',
  templateUrl: './doctor-nav-bar.component.html',
  styleUrls: ['./doctor-nav-bar.component.css']
})
export class DoctorNavBarComponent implements OnInit {

  constructor(private loginService: LoginService) { }

  ngOnInit(): void {
  }

  logout(){
    this.loginService.logout().subscribe(res => {
      
    })
  }
}
