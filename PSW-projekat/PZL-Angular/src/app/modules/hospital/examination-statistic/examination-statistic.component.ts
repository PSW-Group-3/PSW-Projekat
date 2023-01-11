import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Chart } from 'chart.js';
import { ExaminationsStatisticsDTO } from '../model/examinationStatisticDTO';
import { LoginService } from '../services/login.service';
import { StatisticsService } from '../services/statistics.service';

@Component({
  selector: 'app-examination-statistic',
  templateUrl: './examination-statistic.component.html',
  styleUrls: ['./examination-statistic.component.css']
})
export class ExaminationStatisticComponent implements OnInit {

  constructor(private statisticsService: StatisticsService,private loginService: LoginService, private router: Router) { }

 
  public schedulingsPerDayChart : any;
  public avarageNumberOfEachStep : any;

 
  
  public barChartLegend = "";
  public barChartOptions = { }
  
  public schedulingAppointmentEventsStatistic : ExaminationsStatisticsDTO;
  public linearAndNonlinearSchedulingsLables = ['Sequentially', 'Non Sequentially'];
  public linearAndNonlinearSchedulingsData : any;
  public linearLables = ['Finished', 'Non Finished'];
  public linearData : any;

  public avarageNumberOfSteps : number;
  public avarageSchedulingDuration: number;

  
  ngOnInit(): void {    
    this.statisticsService.GetAllExaminationEventStatistics().subscribe( res =>{
      this.schedulingAppointmentEventsStatistic = res;
      this.setSchedulingEventsFields();

      this.createSchedulingsPerDayChart();
      this.createAvarageNumberOfEachStep();
    });
    
  }

 

  

  setSchedulingEventsFields(){
    let array = [this.schedulingAppointmentEventsStatistic.linearSchedulingNumber, this.schedulingAppointmentEventsStatistic.nonlinearSchedulingNumber]     
    this.linearAndNonlinearSchedulingsData = [{ data: array, label: "Schedulings" }];
    let array1 = [this.schedulingAppointmentEventsStatistic.numberFinished, this.schedulingAppointmentEventsStatistic.numberNonFinished]     
    this.linearData = [{ data: array1, label: "Schedulings" }];
    
    this.avarageNumberOfSteps = this.schedulingAppointmentEventsStatistic.avarageNumberOfStepsForSuccessfulScheduling;
    this.avarageSchedulingDuration = this.schedulingAppointmentEventsStatistic.avarageSchedulingDuration;
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
        labels: ['Back Symptom','Back','Sympthom', 'Prescription', 'Report'], 
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
}
