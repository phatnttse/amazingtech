using API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext()
        {
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }    
        public DbSet<SalaryCalculation> SalaryCalculation { get; set;}
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Form> Forms { get; set; }




    }
}
