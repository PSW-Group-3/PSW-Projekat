export class MealAnswerDTO{
    answer: number|null;
    questionId: number;

    public constructor(answer: number|null, questionId: number) {
        this.answer = answer;
        this.questionId = questionId;
    }
}