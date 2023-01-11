import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CouncilDTO } from '../model/CounciDTO';
import { DoctorDto } from '../model/doctorDto';
import { Specialization } from '../model/specialization';
import { DoctorsCouncilService } from '../services/doctors-council.service';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-create-doctors-council',
  templateUrl: './create-doctors-council.component.html',
  styleUrls: ['./create-doctors-council.component.css']
})
export class CreateDoctorsCouncilComponent implements OnInit {

  public councilDTO: CouncilDTO= new  CouncilDTO(0,localStorage.getItem("currentUserId"),'','','','');
  public doctors: DoctorDto[]=[];
  public doctorsCouncil:  DoctorDto[]=[];
  public displayedColumns = ['number',  'delete'];
  public specialization: number[] = [];
  public chooseDoctors: Boolean = false;
  public chooseSpecializations: boolean = false;
  toppings = new FormControl('');
  
  constructor(private doctorsCouncilService: DoctorsCouncilService, private userService: UserService, private router: Router, private toastr: ToastrService) { }

  ngOnInit(): void {

    this.userService.GetAllDoctors().subscribe(res => {
      let result = Object.values(JSON.parse(JSON.stringify(res)));
      result.forEach((element: any) => {
        var app = new DoctorDto(element.id, element.name, element.surname, element.email, element.username, element.role);
        this.doctors.push(app);
      });
    })

  }

  public showDoctors(){
    this.chooseSpecializations=false;
    this.specialization =[];
    this.chooseDoctors = true;
  }

  public showSpecializations(){
    this.chooseDoctors = false;
    this.doctorsCouncil = [];
    this.chooseSpecializations =true;
  }

 
  public ConvertToNumber(obj: any): any{
      switch(obj){
        case '0': return 0;
        case '1': return 1; 
        case '2': return 2; 
      }
  }

  public submit(){
    this.councilDTO.specializations = this.specialization ;
    this.councilDTO.doctors = this.doctorsCouncil ;
    console.log(this.councilDTO)

    let pom12 = new Array();
    this.specialization.forEach((element: any) => {
      pom12.push(this.ConvertToNumber(element));
    });
    this.councilDTO.specializations  = pom12;
    
    
    if (!this.isValidInput())
    {
      this.toastr.show("Fill in all fields correctly");
      return;
    }
    this.doctorsCouncilService.create(this.councilDTO).subscribe(res => {
      this.router.navigate(['/councilOfDoctors']);
    });
  }

  private isValidInput(): boolean {
    return this.councilDTO?.start.toString() != '' && this.councilDTO?.end.toString() != '' &&
    this.councilDTO?.duration.toString() != '' && this.councilDTO?.topic !='' && (this.doctorsCouncil.length> 0 || this.specialization.length >0);
  }
}
