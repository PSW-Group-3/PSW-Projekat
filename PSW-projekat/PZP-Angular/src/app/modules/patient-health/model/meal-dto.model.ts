import { MealAnswerDTO } from "./meal-answer-dto.model";

export interface CreateMealDTO{
    answers: MealAnswerDTO[];
    mealType: number;
    personId: number;
}

export interface MealInfoDTO{
    mealStatus: string;
    mealType: number;
    answers: MealAnswerDTO[];
}