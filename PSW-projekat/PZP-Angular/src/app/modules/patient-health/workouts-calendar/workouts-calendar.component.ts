import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { WorkoutInfoDTO } from '../model/workout-info-dto.model';
import { getWorkoutTypeString, WorkoutType } from '../model/enums/workout-type.enum';

@Component({
  selector: 'app-workouts-calendar',
  templateUrl: './workouts-calendar.component.html',
  styleUrls: ['./workouts-calendar.component.css'],
})
export class WorkoutsCalendarComponent implements OnInit {
  workoutInfoDTOs: WorkoutInfoDTO[] = [];
  getWorkoutTypeString(type: WorkoutType): string {
    return getWorkoutTypeString(type);
  }
  
  month: string = new Date().toLocaleString('default', { month: 'long' });
  year: number = new Date().getFullYear();

  months: string[] = [
    'January',
    'February',
    'March',
    'April',
    'May',
    'June',
    'July',
    'August',
    'September',
    'October',
    'November',
    'December',
  ];
  weekDays: string[] = [
    'Sunday',
    'Monday',
    'Tuesday',
    'Wednesday',
    'Thursday',
    'Friday',
    'Saturday',
  ];

  availabilityCalendarForm: FormGroup;

  constructor(private formBuilder: FormBuilder) {
    this.availabilityCalendarForm = this.formBuilder.group({
      month: [this.month, Validators.required],
      year: [
        this.year,
        [Validators.required, Validators.min(2000), Validators.max(2500)],
      ],
    });
  }

  ngOnInit(): void {
    // Initialize data and fetch availability dates
    this.renderCalendar();
  }

  renderCalendar(): void {
    const numberOfDaysInMonth = this.getNumberOfDaysInMonth(this.month, this.year);

    const firstDayOfMonth = new Date(this.year, this.months.indexOf(this.month), 0).getDay();

    const startOfCalendar = new Date(
      this.year,
      this.months.indexOf(this.month) - 1,
      this.getNumberOfDaysInMonth(this.months[this.months.indexOf(this.month) - 1], this.year) - firstDayOfMonth
    );
    const endOfCalendar = new Date(
      this.year,
      this.months.indexOf(this.month) + 1,
      7 - new Date(this.year, this.months.indexOf(this.month), numberOfDaysInMonth).getDay()
    );

    const patientsWorkouts: WorkoutInfoDTO[] = new Array<WorkoutInfoDTO>();
    const res: any = null/*= await GetAvailableDatesForAccommodation({
      accommodationId: selectedAccommodation.id,
      dateFrom: startOfCalendar,
      dateTo: endOfCalendar,
    });*/

    if (res) {
      res.availabilityDates?.forEach((availabilityDate: any) => {
        patientsWorkouts.push(/*add workout */);
      });
    }

    let i = 0;
    while (startOfCalendar < endOfCalendar) {
      const tempDate = new Date(startOfCalendar);
      if (
        patientsWorkouts[i]?.date?.toDateString() ===
        tempDate.toDateString()
      ) {
        this.workoutInfoDTOs.push(patientsWorkouts[i]);
        i++;
      } else {
        this.workoutInfoDTOs.push({
          date: tempDate,
          type: WorkoutType.undefined,
          duration: 0,
          description: '',
          personId: parseInt(localStorage.getItem('personId')!),
        });
      }
      startOfCalendar.setDate(startOfCalendar.getDate() + 1);
    }
  }

  setColor(type: WorkoutType | undefined): string {
    switch (type) {
      case WorkoutType.walking:
        return 'lightyellow';
      case WorkoutType.jogging:
        return 'yellow';
      case WorkoutType.running:
        return 'lightorange';
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
    if (
      month === 'April' ||
      month === 'June' ||
      month === 'September' ||
      month === 'November'
    ) {
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
}
