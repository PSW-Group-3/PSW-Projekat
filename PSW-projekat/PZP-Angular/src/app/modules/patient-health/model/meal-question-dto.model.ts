export class MealQuestionDTO{
    questionId: number;
    questionText: string;

    public constructor(questionId: number,questionText: string) {
        this.questionId = questionId;
        this.questionText = questionText;
    }
}