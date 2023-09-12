namespace HospitalLibrary.Core.DTOs
{
    public class MealAnswerDTO
    {
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public float Answer { get; set; }

        public MealAnswerDTO(int questionId, int answerId, float answer)
        {
            QuestionId = questionId;
            AnswerId = answerId;
            Answer = answer;
        }
    }
}
