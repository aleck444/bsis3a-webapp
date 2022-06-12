using Microsoft.EntityFrameworkCore;
using aleck3a_webapp.Models;
using aleck3a_webapp.Models.ViewModels;


namespace aleck3a_webapp.Data
{
    public class ApplicationDbContext : DbContext 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
        base(options)
        {
            
        } 

        public DbSet<Item> Items { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<Instrument> Instruments { get; set; }


    }
}