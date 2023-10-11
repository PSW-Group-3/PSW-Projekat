namespace HospitalLibrary.Core.Model
{
    public abstract class BaseModel
    {
        public int Id { get; set; }
        public bool Deleted { get; set; } = false;
    }
}
