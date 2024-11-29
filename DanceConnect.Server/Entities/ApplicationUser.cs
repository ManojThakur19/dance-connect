using DanceConnect.Server.Enums;
using Microsoft.AspNetCore.Identity;

namespace DanceConnect.Server.Entities
{
    public class ApplicationUser : IdentityUser<int>
    {
        public UserType UserType { get; set; } = UserType.User;

        public string Role { get => UserType.ToString();}
        public User? User { get; set; }
        public Instructor? Instructor { get; set; }
        public bool Active { get; set; } = true;
    }

    public class ApplicationRole : IdentityRole<int>
    {

    }
    public class ApplicationUserRole : IdentityUserRole<string>
    {
        public virtual ApplicationUser User { get; set; }
        public virtual ApplicationRole Role { get; set; }
    }

}
