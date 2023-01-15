import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Blood } from '../model/blood';
import { BloodConsumption } from '../model/blood-consumption';
import { BloodType } from '../model/bloodType';
import { ToastrService } from 'ngx-toastr';
import { BloodConsumptionService } from '../services/blood-consumption.service';
import { LoginService } from '../services/login.service';

@Component({
  selector: 'app-blood-consumption',
  templateUrl: './blood-consumption.component.html',
  styleUrls: ['./blood-consumption.component.css']
})
export class BloodConsumptionComponent implements OnInit {
  public bloodConsumption: BloodConsumption = new BloodConsumption(0,new Blood(0,false,'',''),'', localStorage.getItem("currentUserId"))

  public bloodType: String = ''
  constructor(private loginService: LoginService,private bloodConsumptionService: BloodConsumptionService, private router: Router,private toastr: ToastrService) { }

ngOnInit(): void {
    
  }

  public ConvertFromString(obj: any): any{
    switch(obj){
      case '0': return BloodType.APlus;
      case '1': return BloodType.BPlus;
      case '2': return BloodType.ABPlus;
      case '3': return BloodType.OPlus;
      case '4': return BloodType.AMinus;
      case '5': return BloodType.BMinus;
      case '6': return BloodType.ABMinus;
      case '7': return BloodType.OMinus;      
    }
  }

  public createRoom() {
    this.bloodConsumption.blood.bloodType = this.ConvertFromString(this.bloodType );
    console.log( localStorage.getItem("currentUserId") )
    if (!this.isValidInput())  {
      this.toastr.show("Fill in all fields correctly");
      return;
    }
    this.bloodConsumptionService.createBloodConsumption(this.bloodConsumption).subscribe(res => {
      this.router.navigate(['/homeDoctor']);
    },(error) => {
      this.toastr.show("There are not enough units of blood in the system. Please enter a smaller number.");
    });
  }

  logoutUser(){
    this.loginService.logout().subscribe(res => {
      
    })
  }

  private isValidInput(): boolean {
    return this.bloodConsumption?.purpose != '' && this.bloodConsumption?.blood.quantity.toString() != '' && this.bloodType !="";
  }
}
