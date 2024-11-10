using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CRM.DBContext;
using CRM.Model;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;
using Microsoft.Extensions.Caching.Memory;

namespace CRM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly PersonDBContext _context;
        private readonly IDistributedCache _cache;
        private readonly IMemoryCache _inMemorycache;

        public PeopleController(PersonDBContext context, IDistributedCache cache, IMemoryCache inmemoryCache)
        {
            _context = context;
            _cache = cache;
            _inMemorycache = inmemoryCache;
        }

        // GET: api/People
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> Getpersons()
        {
            var cacheKey = "GET_ALL_PRODUCTSa";
            List<Person> persons = new List<Person>();
            // Get data from cache
            var cachedData = await _cache.GetAsync(cacheKey);
            if (cachedData != null)
            {
                // If data found in cache, encode and deserialize cached data
                var cachedDataString = Encoding.UTF8.GetString(cachedData);
                persons = JsonConvert.DeserializeObject<List<Person>>(cachedDataString);
            }
            else
            {
                // If not found, then fetch data from database
                persons = await _context.persons.ToListAsync();
                // serialize data
                var cachedDataString = JsonConvert.SerializeObject(persons);
                var newDataToCache = Encoding.UTF8.GetBytes(cachedDataString);

                // set cache options 
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(2))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1));

                // Add data in cache
                await _cache.SetAsync(cacheKey, newDataToCache, options);
            }
            return persons;
        }

        // GET: api/People/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerson(int id)
        {
            var cacheKey = "GET_ALL_PRODUCTSID";

            // If data found in cache, return cached data
            if (_inMemorycache.TryGetValue(cacheKey, out List<Person> personid))
            {
                return Ok(personid);
            }

            // If not found, then fetch data from database
            personid = await _context.persons.ToListAsync();

            // Add data in cache
            _inMemorycache.Set(cacheKey, personid);

           
            if (personid == null)
            {
                return NotFound();
            }

            return Ok(personid);
        }

        // PUT: api/People/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(int id, Person person)
        {
            if (id != person.Id)
            {
                return BadRequest();
            }

            _context.Entry(person).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/People
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(Person person)
        {
            _context.persons.Add(person);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPerson", new { id = person.Id }, person);
        }

        // DELETE: api/People/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            var person = await _context.persons.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            _context.persons.Remove(person);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PersonExists(int id)
        {
            return _context.persons.Any(e => e.Id == id);
        }
    }
}
