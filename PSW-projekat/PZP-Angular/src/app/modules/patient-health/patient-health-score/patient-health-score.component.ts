import { Component, Input, OnInit, SimpleChanges } from '@angular/core';
import { Chart, registerables } from 'chart.js';
Chart.register(...registerables);

@Component({
  selector: 'app-patient-health-score',
  templateUrl: './patient-health-score.component.html',
  styleUrls: ['./patient-health-score.component.css']
})
export class PatientHealthScoreComponent implements OnInit {
  @Input() healthScore: number = 50;
  healthScoreChart: any;

  constructor() {   }

  ngOnChanges(changes: SimpleChanges): void {
    if (this.healthScoreChart) {
      this.healthScoreChart.data.datasets[0].data = [this.healthScore, 100 - this.healthScore];
      this.healthScoreChart.options.title.text = `${this.healthScore}%`;
      this.healthScoreChart.update();
    }
  }

  ngOnInit(): void {
    this.createHealthScoreChart();
  }

  createHealthScoreChart() {
    this.healthScoreChart = new Chart("healthScoreChart", {
      type: 'doughnut',
      data: {
        labels: ['Healthy', 'Unhealthy'],
        datasets: [{
          data: [this.healthScore, 100 - this.healthScore],
          backgroundColor: [
            '#36A2EB', '#FF6384'
          ]
        }]
      }
    });
  }
}