import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialog, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MealQuestionDTO } from '../model/meal-questionDTO.model';
import { MealService } from '../services/meal.service';
import { AddMealDialogComponent } from '../add-meal-dialog/add-meal-dialog.component';
import { AddWaterDialogComponent } from '../add-water-dialog/add-water-dialog.component';
import { Chart, registerables } from 'chart.js';
import { MealStatisticsService } from '../services/meal-statistics.service';
Chart.register(...registerables);


export interface AddMealDialogData {
  mealTypeString: string;
  mealTypeNumber: number;
  mealQuestions: MealQuestionDTO[];
}

export interface AddWaterDialogData {
  mealTypeString: string;
  mealTypeNumber: number;
  mealQuestions: MealQuestionDTO[];
  shouldEdit: boolean;
}

export interface MealInfo {
  mealTypeString: string;
  mealTypeNumber: number;
  mealScore: string;
  isAdded: boolean;
}


@Component({
  selector: 'app-diet-overview',
  templateUrl: './diet-overview.component.html',
  styleUrls: ['./diet-overview.component.css']
})
export class DietOverviewComponent implements OnInit {

  todayDate: Date = new Date();
  breakfastInfo: MealInfo = {mealTypeString: 'Breakfast', mealTypeNumber: 0, isAdded: false, mealScore: ''};
  lunchInfo: MealInfo = {mealTypeString: 'Lunch', mealTypeNumber: 1, isAdded: false, mealScore: ''};
  dinnerInfo: MealInfo = {mealTypeString: 'Dinner', mealTypeNumber: 2, isAdded: false, mealScore: ''};
  waterInfo: MealInfo = {mealTypeString: 'Water intake', mealTypeNumber: 3, isAdded: false, mealScore: ''};
  mealInfos: MealInfo[] = [this.breakfastInfo, this.lunchInfo, this.dinnerInfo];
  
  breakfastScoreChart: any;
  lunchScoreChart: any;
  dinnerScoreChart: any;
  waterScoreChart: any;

  constructor(public dialog: MatDialog, private mealService: MealService, private mealStatisticsService: MealStatisticsService) { }

  ngOnInit(): void {
    this.mealService.getMealsForPatientByDate(parseInt(localStorage.getItem('currentUserId')!), this.todayDate).subscribe(
      (data) => {
        data.forEach(element => {
          if (element.mealType == 0) {
            this.breakfastInfo.isAdded = true;
            this.breakfastInfo.mealScore = element.mealScore;
          } else if (element.mealType == 1) {
            this.lunchInfo.isAdded = true;
            this.lunchInfo.mealScore = element.mealScore;
          } else if (element.mealType == 2) {
            this.dinnerInfo.isAdded = true;
            this.dinnerInfo.mealScore = element.mealScore;
          } else if (element.mealType == 3) {
            this.waterInfo.isAdded = true;
            this.waterInfo.mealScore = element.mealScore;
          }
        });
      }
    );
    this.mealStatisticsService.getMealStatisticsForPatient().subscribe(
      (data) => {
        this.createBreakfastScoreChart(data.breakfastLabels, data.breakfastScores);
        this.createLunchScoreChart(data.lunchLabels, data.lunchScores);
        this.createDinnerScoreChart(data.dinnerLabels, data.dinnerScores);
        this.createWaterScoreChart(data.waterIntakeLabels, data.waterIntakeScores);
      }
    );
  }

  async openMealDialog(mealTypeNumber: number, mealTypeString: string): Promise<void> {
    this.mealService.getQuestionsForMeal(mealTypeNumber).subscribe(
      (data) => {
        const dialogRef = this.dialog.open(AddMealDialogComponent, {
          data: {mealTypeString: mealTypeString, mealTypeNumber: mealTypeNumber, mealQuestions: data},
        });
        dialogRef.componentInstance.mealAdded.subscribe(async () => {
          await this.refreshPage()
        });
      },
      (error) => {
        console.log(error);
      }
    );
  }

  async openWaterDialog(mealTypeNumber: number, mealTypeString: string, shouldEdit: boolean): Promise<void> {
    this.mealService.getQuestionsForMeal(mealTypeNumber).subscribe(
      (data) => {
        const dialogRef = this.dialog.open(AddWaterDialogComponent, {
          data: {mealTypeString: mealTypeString, mealTypeNumber: mealTypeNumber, mealQuestions: data, shouldEdit: shouldEdit},
        });
        dialogRef.componentInstance.waterAdded.subscribe(async () => {
          await this.refreshPage();
        });
      },
      (error) => {
        console.log(error);
      }
    );
  }

  async refreshPage() {
    location.reload();
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

