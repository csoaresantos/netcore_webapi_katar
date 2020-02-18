using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using interview_katar.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace interview_katar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        public ShoppingCartController(IShoppingCartService service)
        {
            _service = service;
        }

        private readonly IShoppingCartService _service;
        // GET: api/ShoppingCart
        [HttpGet]
        public ActionResult<IEnumerable<ShoppingItem>> Get()
        {
            var items = _service.GetAll();
            return Ok(items);
        }

        // GET: api/ShoppingCart/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult<ShoppingItem> Get(int id)
        {
            var item = _service.GetById(id);
            if(item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        // POST: api/ShoppingCart
        [HttpPost]
        public ActionResult Post([FromBody] ShoppingItem value)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = _service.Add(value);
            return CreatedAtAction("Get", new { Id = item.Id}, item);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var existingItem = _service.GetById(id);
            if(existingItem == null)
            {
                return NotFound();
            }

            _service.Remove(id);
            return Ok();
        }
    }
}
