import { Component, EventEmitter, Inject, OnInit, Output } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AddWaterDialogData } from '../diet-overview/diet-overview.component';
import { MealAnswerDTO } from '../model/meal-answer-dto.model';
import { MealService } from '../services/meal.service';
import { CreateMealDTO } from '../model/meal-dto.model';

@Component({
  selector: 'app-add-water-dialog',
  templateUrl: './add-water-dialog.component.html',
  styleUrls: ['./add-water-dialog.component.css'],
})
export class AddWaterDialogComponent implements OnInit {
  @Output() waterAdded: EventEmitter<void> = new EventEmitter<void>();

  mealAnswers: MealAnswerDTO[] = [];
  titleText: string = 'Add';
  errorMessage: string = '';

  constructor(
    private mealService: MealService,
    public dialogRef: MatDialogRef<AddWaterDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: AddWaterDialogData,
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

  onAddWaterClick(): void {
    if (this.mealAnswers.some((answer) => answer.answer === undefined)) {
      this.errorMessage = 'Please answer all questions!';
    } else {
      let mealDTO: CreateMealDTO = { answers: this.mealAnswers, mealType: this.data.mealType, personId: parseInt(localStorage.getItem('currentUserId')!) };
      if (this.data.shouldEdit == false) {
        this.mealService.addWater(mealDTO).subscribe();
        this.waterAdded.emit();
        this.dialogRef.close();
      } else {
        this.mealService.editWater(mealDTO).subscribe();
        this.waterAdded.emit();
        this.dialogRef.close();
      }
    }
  }

  onNoClick(): void {
    this.dialogRef.close();
  }
}
