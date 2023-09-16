import { WorkoutType } from "./enums/workout-type.enum";

export interface WorkoutStatistics {
    workoutType: WorkoutType;
    numberOfWorkouts: number;
    numberOfHoursSpent: number;
}

export interface AllWorkoutsStatistics {
    workoutsStatistics: WorkoutStatistics[];
}