using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class RegisterDto
    {
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; } 

        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; } 

        [Required]
        public required string Name { get; set; } 
    }
}
