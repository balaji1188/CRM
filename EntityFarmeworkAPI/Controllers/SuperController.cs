using EntityFarmeworkAPI.Data;
using EntityFarmeworkAPI.Entities;
using EntityFarmeworkAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EntityFarmeworkAPI.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    public class SuperController : ControllerBase
    {
        private readonly DataContext _context;

        public SuperController(DataContext context)
        {
            _context = context;
        }
        [HttpGet(Name = "GETSuper")]
        public async Task<ActionResult<List<Superhero>>> GetAll()
        {
            return Ok(await this._context.Superheroes.ToListAsync());
        }

    }
}
