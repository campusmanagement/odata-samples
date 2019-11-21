using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using ODataService.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ODataService
{
    [Produces("application/json")]
    public class AuthorsController : ODataController
    {
        private readonly DataContext _context;
        public AuthorsController(DataContext context)
        {
            _context = context;
        }

        // GET: odata/authors
        [EnableQuery]
        public IEnumerable<Author> Get()
        {
            return _context.Authors;
        }

        // GET: odata/authors/5
        [EnableQuery]
        public Author Get(int key)
        {
            return _context.Authors.FirstOrDefault(c => c.Id == key);
        }

        // POST: odata/authors
        [EnableQuery]
        public async Task<IActionResult> Post([FromBody]Author value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //if an ID was set, set it to 0 since the ID is Auto Increment
            value.Id = 0;

            //add to context and save
            _context.Authors.Add(value);
            await _context.SaveChangesAsync();
            return Created(value);
        }

        // PUT: odata/authors/5
        public async Task<IActionResult> Put([FromODataUri] int key, [FromBody] Author value)
        {
            //OPTION 1, retrieve and perform sets
            /*var entity = await _context.Authors.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }

            update entity
            entity.Name = value.Name;
            */

            //OPTION 2, use _context.Update
            if (!_context.Authors.Any(e => e.Id == key))
                return NotFound();

            value.Id = key;

            //update and save
            _context.Update(value);
            await _context.SaveChangesAsync();
            return Ok(value);
        }

        // DELETE: odata/authors/5
        public async Task<IActionResult> Delete([FromODataUri] int key)
        {
            var entity = await _context.Authors.FindAsync(key);
            if (entity == null)
            {
                return NotFound();
            }

            //remove and save
            _context.Authors.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
