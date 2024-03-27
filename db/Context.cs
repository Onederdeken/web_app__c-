using Microsoft.EntityFrameworkCore;
using Application2.Models;

namespace db
{
    public class Context: DbContext{
        public Context(DbContextOptions<Context> options):base(options){
             // удаляем бд со старой схемой
            Database.EnsureCreated(); 
        
        }
        public DbSet<Person>?  Persons{get;set;} = null!;
         public DbSet<User>?  Users{get;set;} = null!;
        public DbSet<Product>?  Products{get;set;} = null!;

        public  DbSet<Category>?  Categories{get;set;} = null!;

        public DbSet<Seller>? Sellers{get;set;} = null!;
        public DbSet<Buy>? Buys{get;set;}

        //public DbSet<Cart>  Carts{get;set;} = null;
        
        
   
    
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;user=root;password=fylhtq9049791;database=Application2", ServerVersion.AutoDetect("server=localhost;user=root;password=fylhtq9049791;database=Application2;"), b=>b.MigrationsAssembly("Application2"));
        }
    }
}