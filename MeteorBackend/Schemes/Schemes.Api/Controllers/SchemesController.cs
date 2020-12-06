using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Schemes.Api;
using Schemes.Dal;
using Schemes.Domain.Requests;
using Schemes.Domain.Results;

namespace Schemes.Controllers
{
    [ApiController]
    [Route("api/schemes")]
    public class SchemesController : ControllerBase {

        private readonly SchemeCache cache;
        private readonly ISchemesRepository repository;

        public SchemesController(ISchemesRepository repository, SchemeCache cache) {
            this.cache = cache;
            this.repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<List<SchemeResult>>> GetLatest([FromQuery] int limit = Constants.DefaultLimit) {
            if (limit > Constants.MaxLimit)
                return StatusCode(400, $"Limit can not be grater than {Constants.MaxLimit}.");
            if (limit < 1)
                return StatusCode(400, $"Limit must be positive.");

            var cachedValue = await cache.TryGetLatestList(limit);
            if (cachedValue != null) {
                return cachedValue;
            } else {
                var result = await repository.GetLatest(limit);
                if (result != null) {
                    await cache.SetLatestList(result);
                    return result;
                } else {
                    return StatusCode(500);
                }
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<SchemeResult>> Get(int id) {
            var cachedValue = await cache.TryGetSingle(id);
            if (cachedValue != null) {
                return cachedValue;
            } else {
                var result = await repository.GetSingle(id);
                if (result != null) {
                    await cache.SetSingle(result);
                    return result;
                } else {
                    return NotFound();
                }
            }
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<SchemeResult>> Post([FromBody] UpdateSchemeRequest scheme) {
            var result = await repository.Insert(scheme);
            if (result == null)
                return StatusCode(400);
            else {
                await cache.ShiftLists(result); //sort of write through cache
                return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(202)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<SchemeResult>> Put(int id, [FromBody] UpdateSchemeRequest value) {
            var result = await repository.Update(id, value);
            if (result != null) {
                await cache.Invalidate(id);
                await cache.InvalidateLists();
                return AcceptedAtAction(nameof(Get), new { id }, result);
            } else {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult> Delete(int id) {
            bool succes = await repository.DeleteSingle(id);
            if (succes) {
                await cache.Invalidate(id);
                await cache.InvalidateLists();
                return Ok();
            } else {
                return NotFound();
            }
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        public async Task<ActionResult> DeleteAll() {
            await cache.InvalidateLists();
            await repository.DeleteAll();
            return Ok();
        }

    }
}
