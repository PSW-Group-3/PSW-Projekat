import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { AddWaterDialogData } from '../diet-overview/diet-overview.component';
import { MealAnswerDTO } from '../model/meal-answerDTO.model';
import { MealService } from '../services/meal.service';
import { MealDTO } from '../model/mealDTO.model';

@Component({
  selector: 'app-add-water-dialog',
  templateUrl: './add-water-dialog.component.html',
  styleUrls: ['./add-water-dialog.component.css']
})
export class AddWaterDialogComponent implements OnInit {

  mealAnswers: MealAnswerDTO[] = [];
  titleText: string = 'Add'
  errorMessage: string = '';

  constructor(
    private mealService: MealService,
    public dialogRef: MatDialogRef<AddWaterDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: AddWaterDialogData,
  ) {}

  ngOnInit(): void {
    this.data.mealQuestions.forEach(element => {
      this.mealAnswers.push(new MealAnswerDTO(null, element.questionId));
    });
    if(this.data.shouldEdit){
      this.titleText = 'Edit';
    }
  }

  onAddWaterClick(): void {
    if (this.mealAnswers.some(answer => answer.answer === null)) {
      this.errorMessage = 'Please answer all questions!';
    } 
    else {
      let mealDTO: MealDTO = new MealDTO(this.mealAnswers, this.data.mealTypeNumber, parseInt(localStorage.getItem('currentUserId')!));
      this.mealService.addWater(mealDTO).subscribe();      
      this.dialogRef.close();
    }
  }

  onEditWaterClick(): void {
    if (this.mealAnswers.some(answer => answer.answer === null)) {
      this.errorMessage = 'Please answer all questions!';
    }
    else {
      let mealDTO: MealDTO = new MealDTO(this.mealAnswers, this.data.mealTypeNumber, parseInt(localStorage.getItem('currentUserId')!));
      this.mealService.editWater(mealDTO).subscribe();
      this.dialogRef.close();
    }
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

}
