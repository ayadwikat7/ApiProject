
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;

namespace DAL.Data
{
    public class ApplicationDpContext : IdentityDbContext<ApplicationUsers>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DbSet<Category> Caregories { get; set; }
        public DbSet<Review> Reviews { get; set; }


        public DbSet<CategorTransoulation> CategorTransoulations { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductTransulation> ProductsTransoulations { get; set; }
        public DbSet<ProductsImage> ProductImages { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public ApplicationDpContext(DbContextOptions<ApplicationDpContext> options,IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUsers>().ToTable("Users");
            builder.Entity<IdentityRole>().ToTable("Roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
            builder.Entity<Category>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.CreatedBy)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Cart>()
                .HasOne(c => c.User)
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.Entity<Review>()
    .HasOne(r => r.User)
    .WithMany()
    .HasForeignKey(r => r.UserId)
    .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Review>()
                .HasOne(r => r.product)
                .WithMany()
                .HasForeignKey(r => r.ProductId)
                .OnDelete(DeleteBehavior.NoAction);

        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<baseModel>();// session 12 part1
            if (_httpContextAccessor.HttpContext != null)
            {
                var CurrentId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

                foreach (var entry in entries)// session 12 part1
                {
                    if (entry.State == EntityState.Added)// session 12 part1
                    {
                        entry.Entity.createCateg = DateTime.UtcNow;// session 12 part1
                        entry.Entity.CreatedAt = DateTime.UtcNow;// session 12 part1
                        entry.Entity.CreatedBy = CurrentId;// session 12 part1
                    }
                    else if (entry.State == EntityState.Modified)// session 12 part1
                    {
                        entry.Entity.UpdatedAt = DateTime.UtcNow;// session 12 part1
                        entry.Entity.UpdatedBy = CurrentId;// session 12 part1
                    }
                }
            }
            
           
            return base.SaveChangesAsync(cancellationToken);
        }
        public override int SaveChanges()// session 12 part1
        {
            var entries = ChangeTracker.Entries<baseModel>();// session 12 part1
            var CurrentId=  _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            foreach (var entry in entries)// session 12 part1
            {
                if (entry.State == EntityState.Added)// session 12 part1
                {
                    entry.Entity.createCateg = DateTime.UtcNow;// session 12 part1
                    entry.Entity.CreatedAt = DateTime.UtcNow;// session 12 part1
                    entry.Entity.CreatedBy = CurrentId;// session 12 part1
                }
                else if (entry.State == EntityState.Modified)// session 12 part1
                {
                    entry.Entity.UpdatedAt = DateTime.UtcNow;// session 12 part1
                    entry.Entity.UpdatedBy = CurrentId;// session 12 part1
                }
            }
            return base.SaveChanges();// session 12 part1
        }




    }
}

