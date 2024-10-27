using EntityFarmeworkAPI.Entities;

namespace EntityFarmeworkAPI.Services
{
    public interface ISuperServices
    {
        Task<IEnumerable<Superhero>> GetAll();
    }
}
