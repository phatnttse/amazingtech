using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class LoginDto
    {
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; } 

        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; } 
    }
}
