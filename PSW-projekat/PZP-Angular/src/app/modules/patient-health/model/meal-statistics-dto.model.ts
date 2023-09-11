export class MealStatisticsDTO{
    breakfastScores: number[];
    breakfastLabels: string[];
    lunchScores: number[];
    lunchLabels: string[];
    dinnerScores: number[];
    dinnerLabels: string[];
    waterIntakeScores: number[];
    waterIntakeLabels: string[];

    public constructor(
        breakfastScores: number[], breakfastLabels: string[], 
        lunchScores: number[], lunchLabels: string[],
        dinnerScores: number[], dinnerLabels: string[],
        waterIntakeScores: number[], waterIntakeLabels: string[]
        ) 
    {
        this.breakfastScores = breakfastScores;
        this.breakfastLabels = breakfastLabels;
        this.lunchScores = lunchScores;
        this.lunchLabels = lunchLabels;
        this.dinnerScores = dinnerScores;
        this.dinnerLabels = dinnerLabels;
        this.waterIntakeScores = waterIntakeScores;
        this.waterIntakeLabels = waterIntakeLabels;
    }
}