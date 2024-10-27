using EntityFarmeworkAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EntityFarmeworkAPI.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base (options) 
        {
        
        }
      
      

        public  DbSet<Superhero> Superheroes { get; set; }

    }
}
