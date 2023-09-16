import { Component, OnInit } from '@angular/core';
import { Chart, registerables } from 'chart.js';
import { WorkoutService } from '../services/workout.service';
import { WorkoutType, getWorkoutTypeString } from '../model/enums/workout-type.enum';
Chart.register(...registerables);


interface NumberOfWorkotsPerTypeChartData {
    workoutType: string;
    numberOfWorkouts: number;
}

interface NumberOfHoursSpentPerTypeChartData {
    workoutType: string;
    numberOfHoursSpent: number;
}

@Component({
  selector: 'app-workouts-statistics',
  templateUrl: './workouts-statistics.component.html',
  styleUrls: ['./workouts-statistics.component.css']
})
export class WorkoutsStatisticsComponent implements OnInit {
  numberOfWorkotsPerTypeChart: any;
  numberOfHoursSpentPerTypeChart: any;

  getWorkoutTypeString(type: WorkoutType): string {
    return getWorkoutTypeString(type);
  }

  constructor(private workoutService: WorkoutService) { }

  ngOnInit(): void {
    this.workoutService.getMealStatisticsForPatient().subscribe(
      (data) => {
        let numberOfWorkoutsStatistics: NumberOfWorkotsPerTypeChartData[] = []
        let numberOfHoursSpentStatistics: NumberOfHoursSpentPerTypeChartData[] = []

        data.workoutsStatistics.forEach(element => {
          numberOfHoursSpentStatistics.push({
            workoutType: getWorkoutTypeString(element.workoutType),
            numberOfHoursSpent: element.numberOfHoursSpent
          });
          numberOfWorkoutsStatistics.push({
            workoutType: getWorkoutTypeString(element.workoutType),
            numberOfWorkouts: element.numberOfWorkouts
          });
        });

        this.createNumberOfWorkotsPerTypeChart(numberOfWorkoutsStatistics);
        this.createNumberOfHoursSpentPerTypeChart(numberOfHoursSpentStatistics);
      });  
  }

  private createNumberOfWorkotsPerTypeChart(workoutsStatistics: NumberOfWorkotsPerTypeChartData[]) {
    const labels = workoutsStatistics.map(w => w.workoutType);
    const data = workoutsStatistics.map(w => w.numberOfWorkouts);

    this.numberOfWorkotsPerTypeChart = new Chart("numberOfWorkotsPerTypeChart", {
      type: 'bar',
      data: {
        labels: labels,
        datasets: [{
          label: 'Number of workouts',
          data: data,
          backgroundColor: 'rgba(68, 81, 179, 0.5)',
          borderColor: 'rgb(33, 37, 41)',
          borderWidth: 1
        }]
      },
      options: {
        responsive: true,
        maintainAspectRatio: false,
        scales: {
          y: {
            beginAtZero: true,
          }
        }
      }
    });
  }

  private createNumberOfHoursSpentPerTypeChart(workoutsStatistics: NumberOfHoursSpentPerTypeChartData[]) {
    const labels = workoutsStatistics.map(w => w.workoutType);
    const data = workoutsStatistics.map(w => w.numberOfHoursSpent);

    this.numberOfHoursSpentPerTypeChart = new Chart("numberOfHoursSpentPerTypeChart", {
      type: 'bar',
      data: {
        labels: labels,
        datasets: [{
          label: 'Number of hours spent',
          data: data,
          backgroundColor: 'rgba(68, 81, 179, 0.5)',
          borderColor: 'rgb(33, 37, 41)',
          borderWidth: 1
        }]
      },
      options: {
        responsive: true,
        maintainAspectRatio: false,
        scales: {
          y: {
            beginAtZero: true,
          }
        }
      }
    });
  }

}
