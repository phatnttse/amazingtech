using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("Users")]
    public class User : IdentityUser
    {
        public string? Name { get; set; }
        public string? Picture {  get; set; }
        public DateTime DateOfBirth { get; set; }
        [DefaultValue(true)]   
        public bool Active {  get; set; }
   
    }
}
