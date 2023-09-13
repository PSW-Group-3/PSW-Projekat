import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialog, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MealQuestionDTO } from '../model/meal-question-dto.model';
import { MealService } from '../services/meal.service';
import { AddMealDialogComponent } from '../add-meal-dialog/add-meal-dialog.component';
import { AddWaterDialogComponent } from '../add-water-dialog/add-water-dialog.component';
import { Chart, registerables } from 'chart.js';
import { MealStatisticsService } from '../services/meal-statistics.service';
import { MealAnswerDTO } from '../model/meal-answer-dto.model';
import { MealType, GetMealTypeString } from '../model/enums/meal-type.enum';
Chart.register(...registerables);


export interface AddMealDialogData {
  mealType: MealType;
  mealQuestions: MealQuestionDTO[];
  answers: MealAnswerDTO[];
  shouldEdit: boolean;
}

export interface AddWaterDialogData {
  mealType: MealType;
  mealQuestions: MealQuestionDTO[];
  answers: MealAnswerDTO[];
  shouldEdit: boolean;
}

export interface MealInfo {
  mealType: MealType;
  mealStatus: string;
  isAdded: boolean;
  mealAnswers: MealAnswerDTO[];
}

@Component({
  selector: 'app-diet-overview',
  templateUrl: './diet-overview.component.html',
  styleUrls: ['./diet-overview.component.css']
})
export class DietOverviewComponent implements OnInit {
  todayDate: Date = new Date();
  breakfastInfo: MealInfo = {mealType: 0, isAdded: false, mealStatus: '', mealAnswers: []};
  lunchInfo: MealInfo = {mealType: 1, isAdded: false, mealStatus: '', mealAnswers: []};
  dinnerInfo: MealInfo = {mealType: 2, isAdded: false, mealStatus: '', mealAnswers: []};
  waterInfo: MealInfo = {mealType: 3, isAdded: false, mealStatus: '', mealAnswers: []};
  mealInfos: MealInfo[] = [this.breakfastInfo, this.lunchInfo, this.dinnerInfo];

  getMealTypeString(type: MealType): string {
    return GetMealTypeString(type);
  }

  constructor(public dialog: MatDialog, private mealService: MealService, private mealStatisticsService: MealStatisticsService) { }

  ngOnInit(): void {
    this.mealService.getMealsForPatientByDate(parseInt(localStorage.getItem('currentUserId')!), this.todayDate).subscribe(
      (data) => {
        data.forEach(element => {
          if (element.mealType == 0) {
            this.breakfastInfo.isAdded = true;
            this.breakfastInfo.mealStatus = element.mealStatus;
            this.breakfastInfo.mealAnswers = element.answers; 
          } else if (element.mealType == 1) {
            this.lunchInfo.isAdded = true;
            this.lunchInfo.mealStatus = element.mealStatus;
            this.lunchInfo.mealAnswers = element.answers;
          } else if (element.mealType == 2) {
            this.dinnerInfo.isAdded = true;
            this.dinnerInfo.mealStatus = element.mealStatus;
            this.dinnerInfo.mealAnswers = element.answers;
          } else if (element.mealType == 3) {
            this.waterInfo.isAdded = true;
            this.waterInfo.mealStatus = element.mealStatus;
            this.waterInfo.mealAnswers = element.answers;
          }
        });
      }
    );
  }

  async openMealDialog(mealType: MealType,shouldEdit: boolean, answers: MealAnswerDTO[]): Promise<void> {
    this.mealService.getQuestionsForMeal(mealType).subscribe(
      (data) => {
        const dialogRef = this.dialog.open(AddMealDialogComponent, {
          data: {mealType: mealType, mealQuestions: data, answers: answers, shouldEdit:shouldEdit },
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

  async openWaterDialog(mealType: MealType, shouldEdit: boolean): Promise<void> {
    this.mealService.getQuestionsForMeal(mealType).subscribe(
      (data) => {
        const dialogRef = this.dialog.open(AddWaterDialogComponent, {
          data: {mealType: mealType, mealQuestions: data, shouldEdit: shouldEdit, answers: this.waterInfo.mealAnswers},
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

}

