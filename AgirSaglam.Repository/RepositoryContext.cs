using AgirSaglam.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgirSaglam.Repository
{
    //veritabanı işlemlerini yapan sınıf
    public class RepositoryContext:DbContext
    {
        public RepositoryContext(DbContextOptions options):base(options)
        {
            this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        //classlar burada tanımlanır --> Dependencies model referance eklendi

        public DbSet<Category> Categories { get; set; }
        public DbSet<Adress> Adresses { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<CategoryPropertyGroup> CategoryPropertyGroups { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductProperty> ProductProperties { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyGroup> PropertyGroups { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
