namespace HospitalLibrary.Core.Model
{
    public class DailyDiet : BaseModel
    {
        public virtual Person Person { get; set; }
        public virtual Meal Breakfast { get; set; }
        public virtual Meal Lunch { get; set; }
        public virtual Meal Dinner { get; set; }
    }
}
