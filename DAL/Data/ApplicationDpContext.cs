using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data
{
    public class ApplicationDpContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public ApplicationDpContext(DbContextOptions<ApplicationDpContext> options)
            : base(options)
        {
        }

        // Example DbSet:
        // public DbSet<Category> Categories { get; set; }
    }
}

