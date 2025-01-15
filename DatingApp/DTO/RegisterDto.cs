using System.ComponentModel.DataAnnotations;

namespace DatingApp.DTO
{
    public class RegisterDto
    {
        [MaxLength(50)]
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
