using Microsoft.AspNetCore.Identity;
using System.ComponentModel;

namespace API.Models
{
    public class User : IdentityUser
    {
        public string? Name { get; set; }
        public string? Picture {  get; set; }
        public DateTime? DateOfBirth { get; set; } 

        [DefaultValue(true)]
        public bool? Active { get; set; } = true;

        public ICollection<Photo> Photos { get; set; }
   
    }
}
