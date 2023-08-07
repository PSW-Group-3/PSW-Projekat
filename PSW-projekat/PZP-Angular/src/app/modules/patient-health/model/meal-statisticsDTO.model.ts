export class MealStatisticsDTO{
    breakfastScores: number[];
    breakfastLabels: string[];
    lunchScores: number[];
    lunchLabels: string[];
    dinnerScores: number[];
    dinnerLabels: string[];
    waterScores: number[];
    waterLabels: string[];

    public constructor(
        breakfastScores: number[], breakfastLabels: string[], 
        lunchScores: number[], lunchLabels: string[],
        dinnerScores: number[], dinnerLabels: string[],
        waterScores: number[], waterLabels: string[]
        ) 
    {
        this.breakfastScores = breakfastScores;
        this.breakfastLabels = breakfastLabels;
        this.lunchScores = lunchScores;
        this.lunchLabels = lunchLabels;
        this.dinnerScores = dinnerScores;
        this.dinnerLabels = dinnerLabels;
        this.waterScores = waterScores;
        this.waterLabels = waterLabels;
    }
}