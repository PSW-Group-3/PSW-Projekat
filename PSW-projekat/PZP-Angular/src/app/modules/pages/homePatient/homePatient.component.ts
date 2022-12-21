import { Component, OnInit } from '@angular/core';
import { AdvertisementService } from '../../hospital/services/advertisement.service';
import { LoginService } from '../../hospital/services/login.service';

@Component({
  selector: 'app-home',
  templateUrl: './homePatient.component.html',
  styleUrls: ['./homePatient.component.css']
})
export class HomePatientComponent implements OnInit {

  constructor(private loginService: LoginService,private advertisementService: AdvertisementService) { }

  advertisements : any[] = []

  ngOnInit(): void {
    this.advertisementService.getAllAdvertisements().subscribe(res => {
      this.advertisements = res;
    })
  }

  logout(){
    this.loginService.logout().subscribe(res => {
      
    }) 
  }

}
