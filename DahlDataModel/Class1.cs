using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DahlDataModel
{

    public class Product
    {
        public Product()
        {
            this.Orders = new HashSet<Order>();
        }

        public int ProductId { get; set; }
        public int Price { get; set; }
        
        [Required]
        [StringLength(250)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string PicturePath { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<TileOffer> TileOffers { get; set; }
    }


    public class Order
    {
        public Order()
        {
            this.Products = new HashSet<Product>();
        }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        //Burde hatt mange til mange tabell her og
        public string OrderStatus {get; set;}

        public DateTime CreatedAt { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }

    public class Customer
    {
        public int CustomerId { get; set; }
        public int PermissionLevel { get; set; }
        public string GivenName { get; set; }
        public string SurName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Offer> Offers { get; set; }
    }


    //Burde hatt mange til mange tabell her og
    public class Offer
    {
        public int OfferId { get; set; }

        public string Message { get; set; }

        public string Response { get; set; }

    }

    //Burde hatt mange til mange tabell her og
    public class TileOffer
    {
        public int TileOfferId { get; set; }
        public int Price { get; set; }
    }

    public class DahlEntities : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<TileOffer> TileOffers { get; set; }


        public DahlEntities()
            : base(@"Data Source=Donau.hiof.no;Initial Catalog=andersi;Persist Security Info=True;User ID=andersi;Password=Sommer15")
        {
            //fix 1
            this.Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasMany<Product>(b => b.Products)
                .WithMany(a => a.Orders)
                .Map(ab =>
                {
                    ab.MapLeftKey("OrderRefId");
                    ab.MapRightKey("ProductRefId");
                    ab.ToTable("OrderProducts");
                });
        }
    }
}
