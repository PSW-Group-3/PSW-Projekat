using HospitalLibrary.Core.Model.Enums;

namespace HospitalLibrary.Core.Model
{
    public class Notification : BaseModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Role NotificationFor { get; set; }
    }
}
