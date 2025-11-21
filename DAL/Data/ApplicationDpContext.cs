
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data
{
    public class ApplicationDpContext :DbContext
    {
        public DbSet<Caregory> Caregories { get; set; }
        public DbSet<CategorTransoulation> CategorTransoulations { get; set; }
        public ApplicationDpContext(DbContextOptions<ApplicationDpContext> options)
            : base(options)
        {
        }

        
    }
}

