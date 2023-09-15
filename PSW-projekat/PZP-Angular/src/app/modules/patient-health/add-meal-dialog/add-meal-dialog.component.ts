import { Component, EventEmitter, Inject, OnInit, Output } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AddMealDialogData } from '../diet-overview/diet-overview.component';
import { MealAnswerDTO } from '../model/meal-answer-dto.model';
import { MealService } from '../services/meal.service';
import { CreateMealDTO } from '../model/meal-dto.model';
import { MealType, GetMealTypeString } from '../model/enums/meal-type.enum';

@Component({
  selector: 'app-add-meal-dialog',
  templateUrl: './add-meal-dialog.component.html',
  styleUrls: ['./add-meal-dialog.component.css'],
})
export class AddMealDialogComponent implements OnInit {
  @Output() mealAdded: EventEmitter<void> = new EventEmitter<void>();

  titleText: string = 'Add';
  errorMessage: string = '';

  mealAnswers: MealAnswerDTO[] = [];

  getMealTypeString(type: MealType): string {
    return GetMealTypeString(type);
  }

  constructor(
    private mealService: MealService,
    public dialogRef: MatDialogRef<AddMealDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: AddMealDialogData
  ) {}

  ngOnInit(): void {
    this.data.mealQuestions.forEach((element) => {
      this.mealAnswers.push({ answer: undefined, answerId: undefined, questionId: element.questionId });
    });

    if (this.data.shouldEdit) {
      this.titleText = 'Edit';
      this.mealAnswers = this.data.answers;
    }
  }

  onAddMealClick(): void {
    if (this.mealAnswers.some((answer) => answer.answer === undefined)) {
      this.errorMessage = 'Please answer all questions!';
    } else {
      const mealDTO: CreateMealDTO = {
        answers: this.mealAnswers,
        mealType: this.data.mealType,
        personId: parseInt(localStorage.getItem('currentUserId')!),
      };
      if (this.data.shouldEdit == false) {
        this.mealService.addMeal(mealDTO).subscribe(() => {
          this.mealAdded.emit();
          this.dialogRef.close();
        });
      } else {
        this.mealService.editMeal(mealDTO).subscribe(() => {
          this.mealAdded.emit();
          this.dialogRef.close();
        });
      }
    }
  }

  onNoClick(): void {
    this.dialogRef.close();
  }
}
