import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AddWorkoutDTO } from '../model/add-workout-dto.model';
import { MatDialogRef } from '@angular/material/dialog';
import { WorkoutService } from '../services/workout.service';
import { WorkoutType, getAllWorkoutTypes, getWorkoutTypeString } from '../model/enums/workout-type.enum';

@Component({
  selector: 'app-add-workout-dialog',
  templateUrl: './add-workout-dialog.component.html',
  styleUrls: ['./add-workout-dialog.component.css']
})
export class AddWorkoutDialogComponent implements OnInit {
  @Output() workoutAdded: EventEmitter<void> = new EventEmitter<void>();
  public workoutForm: FormGroup | any;

  public workoutTypes = getAllWorkoutTypes();
  getWorkoutTypeString(type: WorkoutType): string {
    return getWorkoutTypeString(type);
  }

  private newDate = new Date();
  public currentDate: string = this.newDate.getUTCDate().toString() + '.' + (this.newDate.getUTCMonth() + 1).toString() + '.' + this.newDate.getUTCFullYear().toString();

  constructor(private workoutService: WorkoutService, private fb: FormBuilder, public dialogRef: MatDialogRef<AddWorkoutDialogComponent>) {
    // Initialize the form using FormBuilder
    this.workoutForm = this.fb.group({
      type: ['', Validators.required],
      duration: [null, [Validators.required, Validators.min(1)]],
      description: ['', Validators.maxLength(255)],
    });
    this.workoutTypes.pop();
  }

  ngOnInit(): void {
  }

  onSubmit() {
    if (this.workoutForm.valid) {
      // Create an AddWorkoutDTO object from the form values
      const workoutDTO: AddWorkoutDTO = {
        type: parseInt(this.workoutForm.value.type),
        duration: this.workoutForm.value.duration,
        description: this.workoutForm.value.description,
        personId: parseInt(localStorage.getItem('currentUserId')!),
      };

      this.workoutService.addWorkout(workoutDTO).subscribe(workoutDTO => {
        console.log(workoutDTO);
      });

      // Emit the event with the workout data
      this.workoutAdded.emit();
      this.dialogRef.close();
    }
  }

  onNoClick(): void {
    this.dialogRef.close();
  }

}
