import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AddMealDialogData } from '../diet-overview/diet-overview.component';
import { MatFormField } from '@angular/material/form-field';
import { MealAnswerDTO } from '../model/meal-answerDTO.model';
import { MealService } from '../services/meal.service';
import { MealDTO } from '../model/mealDTO.model';

@Component({
  selector: 'app-add-meal-dialog',
  templateUrl: './add-meal-dialog.component.html',
  styleUrls: ['./add-meal-dialog.component.css']
})
export class AddMealDialogComponent implements OnInit{

  mealAnswers: MealAnswerDTO[] = [];
  errorMessage: string = '';

  constructor(
    private mealService: MealService,
    public dialogRef: MatDialogRef<AddMealDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: AddMealDialogData,
  ) {}

  ngOnInit(): void {
    this.data.mealQuestions.forEach(element => {
      this.mealAnswers.push(new MealAnswerDTO(null, element.questionId));
    });
  }

  onAddMealClick(): void {
    if (this.mealAnswers.some(answer => answer.answer === null)) {
      this.errorMessage = 'Please answer all questions!';
    }
    else {
      let mealDTO: MealDTO = new MealDTO(this.mealAnswers, this.data.mealTypeNumber, parseInt(localStorage.getItem('currentUserId')!));
      this.mealService.addMeal(mealDTO).subscribe();
      this.dialogRef.close();
    }
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

}
