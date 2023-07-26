import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialog, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MealQuestionDTO } from '../model/meal-questionDTO.model';
import { MealService } from '../services/meal.service';
import { AddMealDialogComponent } from '../add-meal-dialog/add-meal-dialog.component';


export interface DialogData {
  mealTypeString: string;
  mealTypeNumber: number;
  mealQuestions: MealQuestionDTO[];
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
  mealInfos: MealInfo[] = [this.breakfastInfo, this.lunchInfo, this.dinnerInfo];

  constructor(public dialog: MatDialog, private mealService: MealService) { }

  ngOnInit(): void {
    this.mealService.getMealsForPatient(parseInt(localStorage.getItem('currentUserId')!)).subscribe(
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
          }
        });
      }
    );
  }

  async openMealDialog(mealType: number): Promise<void> {
    let mealTypeString: string;
    if (mealType == 0) {
      mealTypeString = "Breakfast";
    } else if (mealType == 1) {
      mealTypeString = "Lunch";
    } else if (mealType == 2) {
      mealTypeString = "Dinner";
    } else {
      mealTypeString = "Undefined";
    }

    this.mealService.getQuestionsForMeal(mealType).subscribe(
      (data) => {
        const dialogRef = this.dialog.open(AddMealDialogComponent, {
          data: {mealTypeString: mealTypeString, mealTypeNumber: mealType, mealQuestions: data},
        });
        dialogRef.afterClosed().subscribe(() => {
          this.refreshPage()
        });
      },
      (error) => {
        console.log(error);
      }
    );
  }

  private refreshPage() {
    this.ngOnInit();
  }

}

