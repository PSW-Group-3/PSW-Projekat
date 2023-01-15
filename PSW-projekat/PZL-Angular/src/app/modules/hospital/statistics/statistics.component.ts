import { Component, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { Chart } from 'chart.js';
import { SchedulingStatisticsDTO } from '../model/scheduling-statistics-dto';
import { DoctorStat, StatisticsDto } from '../model/statisticsdto.model';
import { LoginService } from '../services/login.service';
import { StatisticsService } from '../services/statistics.service';

@Component({
  selector: 'app-statistics',
  templateUrl: './statistics.component.html',
  styleUrls: ['./statistics.component.css']
})
export class StatisticsComponent implements OnInit {

  constructor(private statisticsService: StatisticsService,private loginService: LoginService, private router: Router) { }

  public bloodTypeChart : any;
  public genderAgeChart : any;
  public ageDoctorChart : any;
  public schedulingsPerDayChart : any;
  public avarageNumberOfEachStep : any;

  public maleAgeGroupValues : any;
  public femaleAgeGroupValues : any;
  public bloodTypeValues : any;
  public bloodTypeSymbols : any;
  public allergyValues : any;
  public allergyNames : any;
  
  public doctorNames : any;
  public doctorPercentage : any;
  public barChartLegend = "";
  public barChartOptions = { }
  public stats : StatisticsDto;

  public dataSource = new MatTableDataSource<DoctorStat>();
  public displayedColumns = ['Doctor','0-18','18-30','30-45','45-60','60-80','80+'];

  public schedulingAppointmentEventsStatistic : SchedulingStatisticsDTO;
  public linearAndNonlinearSchedulingsLables = ['Linear', 'Nonlinear'];
  public linearAndNonlinearSchedulingsData : any;
  public numberOfFinishedAndUnfinishedSchedulingForAllPatients: any;
  public avarageNumberOfSteps : number;
  public avarageSchedulingDuration: number;

  
  ngOnInit(): void {
    this.statisticsService.getStatistics().subscribe( res =>{
      let stats = Object.values(JSON.parse(JSON.stringify(res)));
      this.setFields(stats);
      
      this.createBloodTypeChart();
      this.createGenderAgeChart();
    }); 
    
    this.statisticsService.GetAllEventStatistics().subscribe( res =>{
      this.schedulingAppointmentEventsStatistic = res;
      this.setSchedulingEventsFields();

      this.createSchedulingsPerDayChart();
      this.createAvarageNumberOfEachStep();
    });
    
  }

  private setFields(stats: any[]) {
    this.maleAgeGroupValues = stats[0];
    this.femaleAgeGroupValues = stats[1];
    this.bloodTypeValues = Object.values(JSON.parse(JSON.stringify(stats[2])));
    this.bloodTypeSymbols = Object.keys(JSON.parse(JSON.stringify(stats[2])));
    let allergyValues = Object.values(JSON.parse(JSON.stringify(stats[3])));
    this.allergyNames = Object.keys(JSON.parse(JSON.stringify(stats[3])));
    this.allergyValues = [{ data: allergyValues, label: "Allergies" }];
    let doctorStats = Object.values(JSON.parse(JSON.stringify(stats[4])));
    let doctorNames = Object.keys(JSON.parse(JSON.stringify(stats[4])));
    for(let i=0;i<doctorNames.length;i++){
      let nesto = (JSON.parse(JSON.stringify(doctorStats[i])))
      let stat = new DoctorStat(doctorNames[i],nesto[0],nesto[1],nesto[2],nesto[3],nesto[4],nesto[5])
      
      this.dataSource.data.push(stat)
    }
    this.dataSource.data[0].one;

  }

  createBloodTypeChart(){
    this.bloodTypeChart = new Chart("bloodTypeChart", {
      type: 'bar',
      data: {
        labels: this.bloodTypeSymbols, 
	       datasets: [
          {
            data: this.bloodTypeValues,
            backgroundColor: 'red',
            label: 'Blood types'
          } 
        ]
      },
      options: {
        aspectRatio:2.5       
      }
    });
  }

  createGenderAgeChart(){
    this.genderAgeChart = new Chart("genderAgeChart", {
      type: "line",
      data: {
        labels: ['0-18','18-30','30-45','45-60','60-80','80+'],
        datasets: [ 
          { 
            data: this.femaleAgeGroupValues,
            borderColor: "pink",
            label: "Female",
            fill: false,
            pointBackgroundColor: "pink"
          },
          { 
            data: this.maleAgeGroupValues,
            borderColor: "lightblue",
            label: "Male",
            fill: false,
            pointBackgroundColor: "lightblue",          
          }
        ]
      },
    });
  }

  setSchedulingEventsFields(){
    let array = [this.schedulingAppointmentEventsStatistic.linearSchedulingNumber, this.schedulingAppointmentEventsStatistic.nonlinearSchedulingNumber]     
    this.linearAndNonlinearSchedulingsData = [{ data: array, label: "Schedulings" }];
    this.avarageNumberOfSteps = this.schedulingAppointmentEventsStatistic.avarageNumberOfStepsForSuccessfulScheduling;
    this.avarageSchedulingDuration = this.schedulingAppointmentEventsStatistic.avarageSchedulingDuration;
    this.numberOfFinishedAndUnfinishedSchedulingForAllPatients = this.schedulingAppointmentEventsStatistic.numberOfFinishedAndUnfinishedSchedulingForAllPatients;
  }

  createSchedulingsPerDayChart(){
    this.schedulingsPerDayChart = new Chart("schedulingsPerDayChart", {
      type: 'bar',
      data: {
        labels: ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'], 
	      datasets: [
          {
            data: this.schedulingAppointmentEventsStatistic.finishedSchedulingsPerDay,
            backgroundColor: '#7fff5c',
            label: 'Finished'
          },
          {
            data: this.schedulingAppointmentEventsStatistic.unfinishedSchedulingsPerDay,
            backgroundColor: '#ffa09e',
            label: 'Unfinished'
          } 
        ]
      },
      options: {
        aspectRatio:2.5       
      }
    });
  }

  createAvarageNumberOfEachStep(){
    this.avarageNumberOfEachStep = new Chart("avarageNumberOfEachStep", {
      type: 'bar',
      data: {
        labels: ['Date', 'Specialization', 'Doctor', 'Time', 'Review'], 
	      datasets: [
          {
            data: this.schedulingAppointmentEventsStatistic.avarageNumberOfEachStepForSuccessfulScheduling,
            backgroundColor: 'lightblue',
            label: 'Steps'
          }
        ]
      },
      options: {
        aspectRatio:2.5       
      }
    });
  }

  logoutUser(){
    this.loginService.logout().subscribe(res => {
      
    })
  }

}
