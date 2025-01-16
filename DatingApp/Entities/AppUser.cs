using DatingApp.Extensions;
using System.ComponentModel.DataAnnotations;

namespace DatingApp.Entities
{
    public class AppUser
    {
        [Key]
        public int userId { get; set; }
        public required string userName { get; set; }
        public byte[] PasswordHash { get; set; } = [];
        public byte[] PasswordSalt { get; set; } = [];

        public DateOnly DateofBirth  { get; set; }
        public required string KnownAs { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime LastActive { get; set; } = DateTime.Now;
        public required string Gender { get; set; }
        public string? Introduction { get; set; }
        public string? Interests { get; set; }
        public required string LookingFor { get; set; }
        public required string City { get; set; }
        public required string Country { get; set; }
        public List<Photo> Photos { get; set; } = [];

        public int GetAge()
        {
            return DateofBirth.CalculateAge();
        }
    }
}
