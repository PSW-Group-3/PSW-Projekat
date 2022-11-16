using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Identity
{
    public static class ClaimsExtensionMethod
    {
        public static int GetUserId(this IEnumerable<Claim> claims)
        {
            return int.Parse(claims.First(c => c.Type == "UserId").Value);
        }
    }
}
