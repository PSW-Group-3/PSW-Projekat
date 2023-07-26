import { MealAnswerDTO } from "./meal-answerDTO.model";

export class MealDTO{
    answers: MealAnswerDTO[];
    mealType: number;
    personId: number;

    public constructor(answers: MealAnswerDTO[], mealType: number, personId: number) {
        this.answers = answers;
        this.mealType = mealType;
        this.personId = personId;
    }
}

export class MealInfoDTO{
    mealScore: string;
    mealType: number;

    public constructor(mealScore: string, mealType: number) {
        this.mealScore = mealScore;
        this.mealType = mealType;
    }
}