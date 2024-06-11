using Microsoft.AspNetCore.Identity;

namespace ChatRoom.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool IsActive { get; set; }
    }
}
