import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { AddWorkoutDialogComponent } from '../add-workout-dialog/add-workout-dialog.component';
import { AddGymWorkoutDTO, AddWorkoutDTO } from '../model/add-workout-dto.model';
import { getAllWorkoutTypes, WorkoutType, getWorkoutTypeString } from '../model/enums/workout-type.enum';
import { WorkoutService } from '../services/workout.service';
import { ExerciseDTO } from '../model/workout-info-dto.model';

@Component({
  selector: 'app-add-gymworkout-dialog',
  templateUrl: './add-gymworkout-dialog.component.html',
  styleUrls: ['./add-gymworkout-dialog.component.css']
})
export class AddGymworkoutDialogComponent implements OnInit {
  @Output() workoutAdded: EventEmitter<void> = new EventEmitter<void>();
  public workoutForm: FormGroup | any;
  public exerciseForm: FormGroup | any;
  public exercises: ExerciseDTO[] = [];

  private newDate = new Date();
  public currentDate: string = this.newDate.getUTCDate().toString() + '.' + (this.newDate.getUTCMonth() + 1).toString() + '.' + this.newDate.getUTCFullYear().toString();

  constructor(private workoutService: WorkoutService, private fb: FormBuilder, public dialogRef: MatDialogRef<AddWorkoutDialogComponent>) {
    this.workoutForm = this.fb.group({
      duration: [null, [Validators.required, Validators.min(1)]],
      description: ['', Validators.maxLength(255)],
    });

    this.exerciseForm = this.fb.group({
      name: ['', Validators.required],
      description: ['', Validators.maxLength(255)],
      sets: [null, [Validators.required, Validators.min(1)]],
      reps: [null, [Validators.required, Validators.min(1)]],
      weightLifted: [null, [Validators.required, Validators.min(1)]],
    });

  }

  ngOnInit(): void {
  }

  onSubmitExercise() {
    if(this.exerciseForm.valid) {
      const exerciseDTO: ExerciseDTO = {
        name: this.exerciseForm.value.name,
        description: this.exerciseForm.value.description,
        sets: this.exerciseForm.value.sets,
        reps: this.exerciseForm.value.reps,
        weightLifted: this.exerciseForm.value.weightLifted,
      };

      this.exercises.push(exerciseDTO);
      this.exerciseForm.reset();
    }
  }

  onSubmit() {
    if (this.workoutForm.valid && this.exercises.length > 0) {
      const dto: AddGymWorkoutDTO = {
        type: WorkoutType.strength,
        duration: this.workoutForm.value.duration,
        description: this.workoutForm.value.description,
        personId: parseInt(localStorage.getItem('currentUserId')!),
        exercises: this.exercises,
      };

      this.workoutService.addGymWorkout(dto).subscribe(res => {
        console.log(res);
      });

      this.workoutAdded.emit();
      this.dialogRef.close();
    }
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

}
