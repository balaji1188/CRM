using EntityFarmeworkAPI.Data;
using EntityFarmeworkAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EntityFarmeworkAPI.Services
{
    public class SuperSerive : ISuperServices
    {
        private readonly DataContext _context;
        public  SuperSerive(DataContext context)
        {
            _context = context;
        }
      async  Task<IEnumerable<Superhero>> ISuperServices.GetAll()
        {
            return await this._context.Superheroes.ToListAsync();

        }
    }
}
