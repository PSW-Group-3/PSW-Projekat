import { Component, OnInit } from '@angular/core';
import { LoginService } from '../../hospital/services/login.service';

@Component({
  selector: 'app-manager-nav-bar',
  templateUrl: './manager-nav-bar.component.html',
  styleUrls: ['./manager-nav-bar.component.css']
})
export class ManagerNavBarComponent implements OnInit {

  constructor(private loginService: LoginService) { }

  ngOnInit(): void {
  }

  logout(){
    this.loginService.logout().subscribe(res => {
      
    })
  }

}
