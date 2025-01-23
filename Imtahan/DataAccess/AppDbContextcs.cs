using Imtahan.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Imtahan.DataAccess
{

    public class AppDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }


        public AppDbContext(DbContextOptions opt) : base(opt) { }

        
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Product>(entity =>
        //    {
        //        entity.HasKey(x => x.Id);
        //        entity.Property(e => e.Name)
        //              .HasMaxLength(100)
        //              .IsRequired(false)
        //              .HasComment("Ad 100 simvoldan çox ola bilməz.");

        //        entity.Property(e => e.Salary)
        //              .HasMaxLength(100)
        //              .IsRequired(false);

        //        entity.Property(e => e.CoverFile)
        //              .HasMaxLength(255)
        //              .IsRequired(false)
        //              .HasComment("Şəkil URL 255 simvoldan çox ola bilməz.");

        //        entity.Property(e => e.CategoryId)
        //              .IsRequired(false);

        //        entity.HasOne(e => e.Category)
        //              .WithMany(d => d.Products)
        //              .HasForeignKey(e => e.CategoryId)
        //              .OnDelete(DeleteBehavior.Cascade);
        //    });
        //    base.OnModelCreating(modelBuilder);
        //}

    }
}
