using NegosudModel.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace NegosudModel.Context
{
    public class NegosudContext(DbContextOptions<NegosudContext> options) : IdentityDbContext<User>(options)
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<ArticleInventory> ArticleInventories { get; set; }
        public DbSet<ArticleOrder> ArticleOrders { get; set; }
        public DbSet<Badge> Badges { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Family> Families { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<Reason> Reasons { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<StockMovement> StockMovements { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Env.Load();
            //Env.Load("C:\\Users\\quent\\source\\repos\\negosud\\Negosud\\NegosudModel\\.env");
            //string connString = Env.GetString("CONNSTRING");

            string connString = "server=localhost;database=negosud;user=root;password=";
            //string connString = "server=192.168.1.21;database=negosud;user=quentin;password=quentin";

            optionsBuilder.UseMySql(connString, ServerVersion.AutoDetect(connString));
        }

    }
}
