using Microsoft.AspNetCore.Identity;

namespace Library.Data
{
    public class ApiUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
