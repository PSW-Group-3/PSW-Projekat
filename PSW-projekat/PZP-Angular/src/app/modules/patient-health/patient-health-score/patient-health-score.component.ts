import { Component, Input, OnInit, SimpleChanges } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Chart, registerables } from 'chart.js';
import { PatientHealthService } from '../services/patient-health.service';
import { PatientHealthInformationMessagesDTO, PatientInfoDTO } from '../model/patient-info-dto.model';
import { switchMap } from 'rxjs';
Chart.register(...registerables);

@Component({
  selector: 'app-patient-health-score',
  templateUrl: './patient-health-score.component.html',
  styleUrls: ['./patient-health-score.component.css']
})
export class PatientHealthScoreComponent implements OnInit {
  
  patientInfo: PatientInfoDTO = {} as PatientInfoDTO;
  patientHealthInformationMessages: PatientHealthInformationMessagesDTO | any;

  updateDate: Date = new Date();
  public patientInfoForm: FormGroup | any;

  healthScoreChart: any;
  healthChartData: any;
  gaugeNeedlePlugin: any;
  gaugeScorePlugin: any;

  constructor(private patientHealthService: PatientHealthService, private fb: FormBuilder) {   }

  ngOnInit(): void {
    this.patientHealthService.getPatientInformation().subscribe(res => {
      if (res != null) {
      this.patientInfo = res;
      }
      this.updateDate = new Date(this.patientInfo.selectedDate);

      this.patientInfoForm = this.fb.group({
        weight: [this.patientInfo.weight, [Validators.required, Validators.min(1), Validators.max(250)]],
        height: [this.patientInfo.height, [Validators.required, Validators.min(1), Validators.max(250)]],
        bmi: [{value: this.patientInfo.bmi,  disabled: true}, [Validators.required]],
        bloodPressure: [this.patientInfo.bloodPressure, [Validators.required, Validators.pattern('[0-9]{2,3}/[0-9]{2,3}')]],
        heartRate: [this.patientInfo.heartRate, [Validators.required, Validators.min(1), Validators.max(250)]],
      });

      this.initHealthScoreChartData();
      this.createGaugeNeedlePlugin();
      this.createGaugeScorePlugin();
      this.createHealthScoreChart();
    });
  }

  updatePatientInfo(): void {
    this.patientInfo.weight = this.patientInfoForm.value.weight;
    this.patientInfo.height = this.patientInfoForm.value.height;
    this.patientInfo.bloodPressure = this.patientInfoForm.value.bloodPressure;
    this.patientInfo.heartRate = this.patientInfoForm.value.heartRate;
    this.patientInfo.selectedDate = new Date();
    
    this.patientHealthService.updatePatientInformation(this.patientInfo)
      .pipe(
        switchMap((res: PatientInfoDTO) => {
          this.patientInfo = res;
          this.patientInfoForm.reset();
          this.patientInfoForm.patchValue({
            weight: this.patientInfo.weight,
            height: this.patientInfo.height,
            bmi: this.patientInfo.bmi,
            bloodPressure: this.patientInfo.bloodPressure,
            heartRate: this.patientInfo.heartRate
          });
          this.updateDate = new Date(this.patientInfo.selectedDate);
          this.healthChartData.datasets[0].healthScore = this.patientInfo.healthScore;
          this.healthScoreChart.update();
          return this.patientHealthService.getPatientHealthInformationMessages();
        })
      )
      .subscribe((res: PatientHealthInformationMessagesDTO) => {
        this.patientHealthInformationMessages = res;
        console.log(this.patientHealthInformationMessages);
      });
  }

  createHealthScoreChart() {
    this.healthScoreChart = new Chart("healthScoreChart", {
      type: 'doughnut',
      data: this.healthChartData, 
      options: {
        cutout: '85%',
        responsive: true,
        maintainAspectRatio: false,
        layout: {
          padding: {
            bottom: 45
          },
        },
        plugins: {
          legend: {
            display: false,
          },
          tooltip: {
            enabled: true,
            callbacks: {
              title: function(context: any) {
                return 'Health Status: ' + context[0].label;
              },
              label: function(context: any) {
                if(context.dataIndex == 0) {
                  let messageArray = [];
                  messageArray.push('You are at high risk of developing a chronic disease');
                  return messageArray;
                }
                else if(context.dataIndex == 1) {
                  let messageArray = [];
                  messageArray.push('You are at low risk of developing a chronic disease');
                  return messageArray;
                }
                else if(context.dataIndex == 2) {
                  let messageArray = [];
                  messageArray.push('You should try to improve your health');
                  return messageArray;
                }
                else if(context.dataIndex == 3) {
                  let messageArray = [];
                  messageArray.push('You are in good health');
                  return messageArray;
                }
                else if(context.dataIndex == 4) {
                  let messageArray = [];
                  messageArray.push('You are in excellent health');
                  return messageArray;
                }
                else {
                  return 'Your health score is: ' + context.dataset.healthScore;
                }
              },
            }
          }
        }
      },
      plugins: [this.gaugeNeedlePlugin, this.gaugeScorePlugin]
    });
  }

  createGaugeNeedlePlugin() {
    this.gaugeNeedlePlugin = {
      id: 'gaugeNeedle',
      afterDatasetsDraw: (chart: any, args:any, plugins:any) => {
        const {ctx, data} = chart;
        ctx.save();
        const xCenter = chart.getDatasetMeta(0).data[0].x;
        const yCenter = chart.getDatasetMeta(0).data[0].y;
        const outerRadius = chart.getDatasetMeta(0).data[0].outerRadius;
        const innerRadius = chart.getDatasetMeta(0).data[0].innerRadius;
        const sliceValue = (outerRadius - innerRadius) / 2;
        const radius = 15;
        const needleValue = data.datasets[0].healthScore;

        ctx.translate(xCenter, yCenter);
        ctx.rotate((needleValue / 100 * 180 + 270) * Math.PI / 180);

        //Needle
        ctx.beginPath();
        ctx.strokeStyle = 'gray';
        ctx.fillStyle = 'gray';
        ctx.moveTo(0 - radius, 0);
        ctx.lineTo(0, 0 - outerRadius + sliceValue);
        ctx.lineTo(0 + radius, 0);
        ctx.stroke();
        ctx.fill();

        //Center circle
        ctx.beginPath();
        ctx.arc(0 , 0, radius, 0, Math.PI*2, false);
        ctx.fill();

        ctx.restore();
      }
    };
  }

  createGaugeScorePlugin(){
    this.gaugeScorePlugin = {
      id: 'gaugeScore',
      afterDatasetsDraw: (chart: any, args:any, plugins:any) => {
        const {ctx, data} = chart;
        ctx.save();
        const xCenter = chart.getDatasetMeta(0).data[0].x;
        const yCenter = chart.getDatasetMeta(0).data[0].y;
        const needleValue = data.datasets[0].healthScore.toFixed(2);

        ctx.font = 'bold 25px Arial';
        ctx.textAlign = 'center';
        ctx.fillStyle = 'gray';
        ctx.fillText(`${needleValue}%`, xCenter, yCenter + 40);
        ctx.restore();
      }
    };
  }

  initHealthScoreChartData() {
    this.healthChartData = {
      labels: ['Very unhealty', 'Unhealty', 'Average', 'Healty', 'Very healty'],
      datasets: [{
        data: [20, 20, 20, 20, 20],
        backgroundColor: [
          'red', 'orange', 'yellow', 'lightgreen', 'green'
        ],
        borderColor: [
          'red', 'orange', 'yellow', 'lightgreen', 'green'
        ],
        borderWidth: 1,
        circumference:180,
        rotation: 270,
        healthScore: this.patientInfo.healthScore
      }]
    }
  }
}