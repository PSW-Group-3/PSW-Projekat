using HospitalLibrary.Core.Model.Enums;

namespace HospitalLibrary.Core.Model
{
    public class Patient : BaseModel
    {
        public BloodType BloodType { get; set; }
        public float HealthScore { get; set; }
        public virtual Person Person { get; set; }
        public virtual Doctor Doctor { get; set; }
    }
}
