import { Component, OnInit } from '@angular/core';
import { Chart, registerables } from 'chart.js';
import { MealStatisticsService } from '../services/meal-statistics.service';
Chart.register(...registerables);

@Component({
  selector: 'app-meal-statistics',
  templateUrl: './meal-statistics.component.html',
  styleUrls: ['./meal-statistics.component.css']
})
export class MealStatisticsComponent implements OnInit {
  breakfastScoreChart: any;
  lunchScoreChart: any;
  dinnerScoreChart: any;
  waterScoreChart: any;

  constructor(private mealStatisticsService: MealStatisticsService) { }

  ngOnInit(): void {
    this.mealStatisticsService.getMealStatisticsForPatient().subscribe(
      (data) => {
        this.createBreakfastScoreChart(data.breakfastLabels, data.breakfastScores);
        this.createLunchScoreChart(data.lunchLabels, data.lunchScores);
        this.createDinnerScoreChart(data.dinnerLabels, data.dinnerScores);
        this.createWaterScoreChart(data.waterIntakeLabels, data.waterIntakeScores);
      }
    );
  }

  private createBreakfastScoreChart(breakfastLabels: string[], breakfastScores: number[]) {
    this.breakfastScoreChart = new Chart("breakfastScoreChart", {
      type: 'line',
      data: {
        labels: breakfastLabels,
        datasets: [{
          label: 'Breakfast score',
          data: breakfastScores,
          borderColor: 'rgb(33, 37, 41)',
          pointBackgroundColor: 'rgb(33, 37, 41)',
          backgroundColor: 'rgba(68, 81, 179, 0.5)',
          tension: 0.25,
          fill: true
        }]
      },
      options: {
        responsive: true,
        maintainAspectRatio: false,
        scales: {
          y: {
            
            min: -1,
            max: 1
          },
          x: {
            title: {
              display: true,
              text: 'Dates',
              font: {
                size: 14
              }
            }
          }
        },
        plugins: {
          title: {
            display: true,
            text: 'Breakfast scores in the last 30 days',
            font: {
                size: 16
            }
          }
        }
      }
    });
  }

  private createLunchScoreChart(lunchLabels: string[], lunchScores: number[]) {
    this.lunchScoreChart = new Chart("lunchScoreChart", {
      type: 'line',
      data: {
        labels: lunchLabels,
        datasets: [{
          label: 'Lunch score',
          data: lunchScores,
          borderColor: 'rgb(33, 37, 41)',
          pointBackgroundColor: 'rgb(33, 37, 41)',
          backgroundColor: 'rgba(68, 81, 179, 0.5)',
          tension: 0.25,
          fill: true
        }]
      },
      options: {
        responsive: true,
        maintainAspectRatio: false,
        scales: {
          y: {
            min: -1,
            max: 1
          }
        },
        plugins: {
          title: {
            display: true,
            text: 'Lunch scores in the last 30 days',
            font: {
                size: 16
            }
          }
        }
      }
    });
  }

  private createDinnerScoreChart(dinnerLabels: string[], dinnerScores: number[]) {
    this.dinnerScoreChart = new Chart("dinnerScoreChart", {
      type: 'line',
      data: {
        labels: dinnerLabels,
        datasets: [{
          label: 'Dinner score',
          data: dinnerScores,
          borderColor: 'rgb(33, 37, 41)',
          pointBackgroundColor: 'rgb(33, 37, 41)',
          backgroundColor: 'rgba(68, 81, 179, 0.5)',
          tension: 0.25,
          fill: true
        }]
      },
      options: {
        responsive: true,
        maintainAspectRatio: false,
        scales: {
          y: {
            min: -1,
            max: 1
          }
        },
        plugins: {
          title: {
            display: true,
            text: 'Dinner scores in the last 30 days',
            font: {
                size: 16
            }
          }
        }
      }
    });
  }

  private createWaterScoreChart(waterLabels: string[], waterScores: number[]) {
    this.waterScoreChart = new Chart("waterScoreChart", {
      type: 'line',
      data: {
        labels: waterLabels,
        datasets: [{
          label: 'Water score',
          data: waterScores,
          borderColor: 'rgb(33, 37, 41)',
          pointBackgroundColor: 'rgb(33, 37, 41)',
          backgroundColor: 'rgba(68, 81, 179, 0.5)',
          tension: 0.25,
          fill: true
        }]
      },
      options: {
        responsive: true,
        maintainAspectRatio: false,
        scales: {
          y: {
            min: -1,
            max: 1
          }
        },
        plugins: {
          title: {
            display: true,
            text: 'Water scores in the last 30 days',
            font: {
                size: 16
            }
          }
        }
      }
    });
  }
}
