import { WorkoutType } from "./enums/workout-type.enum";
import { ExerciseDTO } from "./workout-info-dto.model";

export interface AddWorkoutDTO {
    type: WorkoutType;
    duration: number;
    description: string;
    personId: number;
}

export interface AddGymWorkoutDTO extends AddWorkoutDTO {
    exercises: ExerciseDTO[];
}