import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { WorkoutInfoDTO } from '../model/workout-info-dto.model';
import { getWorkoutTypeString, WorkoutType } from '../model/enums/workout-type.enum';
import { MatDialog } from '@angular/material/dialog';
import { AddWorkoutDialogComponent } from '../add-workout-dialog/add-workout-dialog.component';
import { WorkoutService } from '../services/workout.service';

@Component({
  selector: 'app-workouts-calendar',
  templateUrl: './workouts-calendar.component.html',
  styleUrls: ['./workouts-calendar.component.css'],
})
export class WorkoutsCalendarComponent implements OnInit {
  workoutInfoDTOs: WorkoutInfoDTO[][] = [];
  patientsWorkouts: WorkoutInfoDTO[] = [];
  patientsGymWorkouts: WorkoutInfoDTO[] = [];
  numberOfDaysInMonth: number = 0;
  firstDayOfMonth: number = 0;
  startOfCalendar: Date = new Date();
  endOfCalendar: Date = new Date();

  getWorkoutTypeString(type: WorkoutType): string {
    return getWorkoutTypeString(type);
  }

  month: string = new Date().toLocaleString('default', { month: 'long' });
  year: number = new Date().getFullYear();

  months: string[] = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
  weekDays: string[] = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];

  availabilityCalendarForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    public dialog: MatDialog,
    private workoutService: WorkoutService,
  ) {
    this.availabilityCalendarForm = this.formBuilder.group({
      month: [this.month, Validators.required],
      year: [this.year, [Validators.required, Validators.min(2000), Validators.max(2500)]],
    });
  }

  ngOnInit(): void {
    // Initialize data and fetch availability dates
    this.initCalendar();
    this.getPatientWorkouts();
  }

  initCalendar(): void {
    this.numberOfDaysInMonth = this.getNumberOfDaysInMonth(this.month, this.year);

    this.firstDayOfMonth = new Date(this.year, this.months.indexOf(this.month), 0).getDay();

    this.startOfCalendar = new Date(
      this.year,
      this.months.indexOf(this.month) - 1,
      this.getNumberOfDaysInMonth(this.months[this.months.indexOf(this.month) - 1], this.year) - this.firstDayOfMonth,
    );
    this.endOfCalendar = new Date(
      this.year,
      this.months.indexOf(this.month) + 1,
      7 - new Date(this.year, this.months.indexOf(this.month), this.numberOfDaysInMonth).getDay(),
    );
  }

  async getPatientWorkouts(): Promise<void> {
    try {
      const data = await (await this.workoutService.getWorkoutInfoDTOs(
        parseInt(localStorage.getItem('currentUserId')!),
        this.startOfCalendar,
        this.endOfCalendar
      )).toPromise();
  
      data?.forEach((element) => {
        element.date = new Date(element.date);
        this.patientsWorkouts.push(element);
      });

      const gymWorkouts = await (await this.workoutService.getGymWorkoutInfoDTOs(
        parseInt(localStorage.getItem('currentUserId')!),
        this.startOfCalendar,
        this.endOfCalendar
      )).toPromise();

      gymWorkouts?.forEach((element) => {
        element.date = new Date(element.date);
        this.patientsGymWorkouts.push(element);
      });
  
      this.renderCalendar(); // Call renderCalendar after fetching patientsWorkouts
    } catch (error) {
      console.error('Error fetching patient workouts:', error);
    }
  }

  renderCalendar(): void {  
    let patientsWorkoutsIndex = 0;
    let workoutInfoDTOsIndex = 0;
    let startOfCalendarTemp = this.startOfCalendar
    while (this.startOfCalendar < this.endOfCalendar) {
      const tempDate = new Date(this.startOfCalendar);

      if (this.patientsWorkouts[patientsWorkoutsIndex]?.date?.toDateString() === tempDate.toDateString()) {
        let j = patientsWorkoutsIndex;
        this.workoutInfoDTOs.push([]);
        while (this.patientsWorkouts[j]?.date?.toDateString() === tempDate.toDateString()) {
          this.workoutInfoDTOs[workoutInfoDTOsIndex].push(this.patientsWorkouts[j]);
          j++;
        }
        patientsWorkoutsIndex = j;
      } else {
        this.workoutInfoDTOs.push([]);
        this.workoutInfoDTOs[workoutInfoDTOsIndex].push({
          date: tempDate,
          type: WorkoutType.undefined,
          duration: 0,
          description: '',
          personId: parseInt(localStorage.getItem('personId')!),
        });
      }
      workoutInfoDTOsIndex++;
      this.startOfCalendar.setDate(this.startOfCalendar.getDate() + 1);
    }
    patientsWorkoutsIndex = 0;
    workoutInfoDTOsIndex = 0;
    while (startOfCalendarTemp < this.endOfCalendar) {
      const tempDate = new Date(startOfCalendarTemp);

      if (this.patientsGymWorkouts[patientsWorkoutsIndex]?.date?.toDateString() === tempDate.toDateString()) {
        let j = patientsWorkoutsIndex;
        this.workoutInfoDTOs.push([]);
        while (this.patientsGymWorkouts[j]?.date?.toDateString() === tempDate.toDateString()) {
          this.workoutInfoDTOs[workoutInfoDTOsIndex].push(this.patientsGymWorkouts[j]);
          j++;
        }
        patientsWorkoutsIndex = j;
      } else {
        this.workoutInfoDTOs.push([]);
        this.workoutInfoDTOs[workoutInfoDTOsIndex].push({
          date: tempDate,
          type: WorkoutType.undefined,
          duration: 0,
          description: '',
          personId: parseInt(localStorage.getItem('personId')!),
        });
      }
      workoutInfoDTOsIndex++;
      startOfCalendarTemp.setDate(startOfCalendarTemp.getDate() + 1);
    }
  }

  setColor(type: WorkoutType | undefined): string {
    switch (type) {
      case WorkoutType.walking:
        return 'lightyellow';
      case WorkoutType.jogging:
        return 'yellow';
      case WorkoutType.running:
        return '#FFD700';
      case WorkoutType.cardio:
        return 'orange';
      case WorkoutType.cycling:
        return 'lightred';
      case WorkoutType.swimming:
        return 'blue';
      case WorkoutType.strength:
        return 'green';
      case WorkoutType.sports:
        return 'purple';
      default:
        return 'white';
    }
  }

  getNumberOfDaysInMonth = (month: string, year: number) => {
    if (month === 'February') {
      if (year % 4 === 0) {
        return 29;
      } else {
        return 28;
      }
    }
    if (month === 'April' || month === 'June' || month === 'September' || month === 'November') {
      return 30;
    }
    return 31;
  };

  onSubmit(): void {
    const formData = this.availabilityCalendarForm.value;
    this.month = formData.month;
    this.year = formData.year;
    this.renderCalendar();
  }

  openAddWorkoutDialog() {
    const dialogRef = this.dialog.open(AddWorkoutDialogComponent);
    dialogRef.componentInstance.workoutAdded.subscribe(async () => {
      await this.refreshPage();
    });
  }

  async refreshPage() {
    location.reload();
  }
}
