import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { DoctorsCouncilDto } from '../model/doctorsCouncilDto';
import { DoctorsCouncilService } from '../services/doctors-council.service';
import { ShowDoctorsPipe } from './show-doctors.pipe';

@Component({
  selector: 'app-councils',
  templateUrl: './councils.component.html',
  styleUrls: ['./councils.component.css']
})
export class CouncilsComponent implements OnInit {

  public dataSource = new MatTableDataSource<DoctorsCouncilDto>();
  displayedColumns: string[] = ['Topic', 'DateTime', 'Duration', 'Doctors'];
  public councils: DoctorsCouncilDto[] = [];


  constructor(private doctorsCouncilService: DoctorsCouncilService, private router: Router) { }

  ngOnInit(): void { 
    this.doctorsCouncilService.GetAllCouncilByDoctor(Number(localStorage.getItem("currentUserId"))-1).subscribe(res => {
      let result = Object.values(JSON.parse(JSON.stringify(res)));
      result.forEach((element: any) => {
        var coun = new DoctorsCouncilDto(element.id, element.topic, element.start, element.duration, element.doctors);
        this.councils.push(coun);
      });
      console.log(this.councils);
      this.dataSource.data = this.councils;
    })
  }

  public addCouncil() {
    this.router.navigate(['/council/add']);
  }

}
