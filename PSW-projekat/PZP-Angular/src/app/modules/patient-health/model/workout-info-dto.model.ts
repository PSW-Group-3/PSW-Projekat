import { WorkoutType } from './enums/workout-type.enum';

export interface WorkoutInfoDTO {
  type: WorkoutType;
  date: Date;
  duration: number;
  description: string;
  personId: number;
}

export interface GymWorkoutInfoDTO extends WorkoutInfoDTO {
  exercises: ExerciseDTO[];
}

export interface ExerciseDTO {
  name: string;
  description: string;
  sets: number;
  reps: number;
  weightLifted: number;
}
