using CRM.Model;
using Microsoft.EntityFrameworkCore;

namespace CRM.DBContext
{
    public class PersonDBContext : DbContext
    {
        public PersonDBContext(DbContextOptions<PersonDBContext> options) : base(options)
        {

        }
        public DbSet<Person> persons { get; set; }
    }
}
