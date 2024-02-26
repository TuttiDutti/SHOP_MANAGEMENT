using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;
using ShopManagement.Models;
using ShopManagement.ViewModels;

namespace ShopManagement.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Absence> Absences { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<AbsenceType> AbsenceTypes { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }  
        public DbSet <Product> Products { get; set; }
        public DbSet <Photo> Photos { get; set; }
        public DbSet <Category> Categorys { get; set; }
        public DbSet <Order> Orders { get; set; }
        public DbSet <OrderItem> OrderItems { get; set; }
        public DbSet<Announcement> Announcements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Absence>()
                .HasOne(a => a.User)
                .WithMany(u => u.Absence)
                .HasForeignKey(a => a.UserId);
            modelBuilder.Entity<User>()
                .HasOne(a => a.Role)
                .WithMany(u => u.Users)
                .HasForeignKey(a => a.RoleId);
            modelBuilder.Entity<Absence>()
                .HasOne(a => a.AbsenceType)
                .WithMany(u => u.Absence)
                .HasForeignKey(a => a.AbsenceTypeId);
            modelBuilder.Entity<Document>()
                .HasOne(a => a.User)
                .WithMany(u => u.Documents)
                .HasForeignKey(a => a.UserId);
            modelBuilder.Entity<Document>()
                .HasOne(a => a.DocumentType)
                .WithMany(u => u.Documents)
                .HasForeignKey(a => a.DocumentTypeId);
            modelBuilder.Entity<Photo>()
                .HasOne(a => a.Product)
                .WithMany(u => u.Photos)
                .HasForeignKey(a => a.ProductId);
            modelBuilder.Entity<Product>()
                .HasOne(a => a.Category)
                .WithMany(u => u.Products)
                .HasForeignKey(a => a.CategoryId);
            modelBuilder.Entity<OrderItem>()
                .HasOne(a => a.Order)
                .WithMany(u => u.OrderItems)
                .HasForeignKey(a => a.OrderId);
            modelBuilder.Entity<OrderItem>()
                .HasOne(a => a.Product)
                .WithMany(u => u.OrderItems)
                .HasForeignKey(a => a.ProductId);
            modelBuilder.Entity<Announcement>()
                .HasOne(a => a.User)
                .WithMany(u => u.Announcements)
                .HasForeignKey(a => a.UserId);

        }

        public void AddEntity<TEntity>(TEntity entity) where TEntity : class, new()
        {
            Set<TEntity>().Add(entity);
        }

        public void AddEntityAndSaveChanges<TEntity>(TEntity entity) where TEntity : class, new()
        {
            AddEntity(entity);
            SaveChanges();
        }

        public void AddEntitiesRange<TEntity>(IEnumerable<TEntity> entity) where TEntity : class, new()
        {
            Set<TEntity>().AddRange(entity);
        }

        public void AddEntitiesRangeAndSaveChanges<TEntity>(IEnumerable<TEntity> entity) where TEntity : class, new()
        {
            AddEntitiesRange(entity);
            SaveChanges();
        }

        public void UpdateEntity<TEntity>(TEntity entity) where TEntity : class, new()
        {
            Entry(entity).State = EntityState.Modified;
        }

        public void UpdateEntitiesRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, new()
        {
            foreach (var entity in entities)
            {
                UpdateEntity(entity);
            }
        }

        public void UpdateEntitiesRangeAndSaveChanges<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, new()
        {
            UpdateEntitiesRange(entities);
            SaveChanges();
        }

        public void RemoveEntity<TEntity>(TEntity entity) where TEntity : class, new()
        {
            Set<TEntity>().Remove(entity);
        }

        public void RemoveEntitiesRange<TEntity>(IEnumerable<TEntity> entity) where TEntity : class, new()
        {
            Set<TEntity>().RemoveRange(entity);
        }

        public void RemoveEntitiesRangeAndSaveChanges<TEntity>(IEnumerable<TEntity> entity) where TEntity : class, new()
        {
            RemoveEntitiesRange(entity);
            SaveChanges();
        }

        public DbSet<ShopManagement.ViewModels.OrderViewModel>? OrderViewModel { get; set; }

        public DbSet<ShopManagement.ViewModels.ChangePassViewModel>? ChangePassViewModel { get; set; }


    }
}
