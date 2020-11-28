using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Schemes.Dal;
using Schemes.Dal.Data;

namespace Schemes.Controllers
{
    [ApiController]
    [Route("api/schemes")]
    public class SchemesController : ControllerBase {

        private readonly ILogger<SchemesController> logger;
        private readonly ISchemesRepository repository;

        public SchemesController(ILogger<SchemesController> logger, ISchemesRepository repository) {
            this.logger = logger;
            this.repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<List<Scheme>>> GetAll() {
            var result = await repository.GetAll();
            if (result != null) {
                return result;
            } else {
                return StatusCode(500);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Scheme>> Get(int id) {
            var result = await repository.Get(id);
            if (result != null) {
                return result;
            } else {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Delete(int id) {
            bool succes = await repository.Delete(id);
            return succes ? Ok() : NotFound();
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        public async Task<ActionResult> DeleteAll() {
            await repository.DeleteAll();
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Scheme>> Post([FromBody] Scheme scheme) {
            var result = await repository.Insert(scheme);
            if (result == null)
                return StatusCode(400);
            else
                return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(202)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Scheme>> Put(int id, [FromBody] Scheme value) {
            var result = await repository.Update(id, value);
            if (result != null) {
                return AcceptedAtAction(nameof(Get), new { id }, result);
            } else {
                return NotFound();
            }
        }

    }
}
