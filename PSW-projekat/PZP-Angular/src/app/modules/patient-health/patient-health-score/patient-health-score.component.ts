import { Component, Input, OnInit, SimpleChanges } from '@angular/core';
import { Chart, registerables } from 'chart.js';
Chart.register(...registerables);

@Component({
  selector: 'app-patient-health-score',
  templateUrl: './patient-health-score.component.html',
  styleUrls: ['./patient-health-score.component.css']
})
export class PatientHealthScoreComponent implements OnInit {
  @Input() healthScore: number = 70;
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

  angularGradientPlugin: any = {
    beforeDraw: (chart: any) => {
      const ctx = chart.ctx;
      const dataset = chart.data.datasets[0];
      const gradient = ctx.createRadialGradient(
        chart.width / 2, chart.height / 2, 0,
        chart.width / 2, chart.height / 2, chart.width / 2
      );
      
      // Set gradient colors based on dataset data
      gradient.addColorStop(0, 'red');
      gradient.addColorStop(0.5, 'yellow');
      gradient.addColorStop(1, 'green');
      
      // Apply gradient to the doughnut chart background
      ctx.fillStyle = gradient;
      ctx.beginPath();
      ctx.arc(chart.width / 2, chart.height / 2, chart.width / 2, 0, 2 * Math.PI);
      ctx.fill();
    }
  };
  

  createHealthScoreChart() {
    let color;
    if (this.healthScore < 20) {
      color = '#f70000';
    }
    else if (this.healthScore <= 20 && this.healthScore < 40) {
      color = '#f88410';
    }
    else if (this.healthScore <= 40 && this.healthScore < 60) {
      color = '#fdf62c';
    }
    else if (this.healthScore <= 60 && this.healthScore < 80) {
      color = '#96ff2f';
    }
    else {
      color = '#40ff30';
    }

    this.healthScoreChart = new Chart("healthScoreChart", {
      type: 'doughnut',
      data: {
        labels: ['Left to 100%', 'Health Score'],
        datasets: [{
          data: [100 - this.healthScore, this.healthScore],
          backgroundColor: [
            'rgba(0, 0, 0, 0.0)', color 
          ],
          borderColor: [
            color, color
          ]
        }]
      },
      options: {
        responsive: true,
        maintainAspectRatio: false,
        plugins: {
          legend: {
            display: false
          },
          title: {
            display: true,
            text: `${this.healthScore}/100`,
            font: {
              lineHeight: 1.2,
              size: 45,
              weight: 'bold'
            }
          },
          tooltip: {
            enabled: false
          }
        }
      }
    });
  }
}