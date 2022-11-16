using Microsoft.AspNetCore.Identity;


namespace HospitalLibrary.Identity
{
    public class SecUser : IdentityUser
    {
        public int Id { get; set; }
    }
}
