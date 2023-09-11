import { WorkoutType } from "./enums/workout-type.enum";

export interface WorkoutInfoDTO {
    type: WorkoutType;
    date: Date;
    duration: number;
    description: string;
    personId: number;
  }