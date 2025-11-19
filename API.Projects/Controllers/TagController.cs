using System.Threading.Tasks;
using APP.Projects.Features.Tags;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Projects.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TagsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST api/Tags
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TagCreateRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        // PUT api/Tags/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TagUpdateRequest request)
        {
            request.Id = id;
            var response = await _mediator.Send(request);
            return Ok(response);
        }

        // DELETE api/Tags/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var request = new TagDeleteRequest
            {
                Id = id
            };

            var response = await _mediator.Send(request);
            return Ok(response);
        }

        // GET api/Tags
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var request = new TagQueryRequest();

            var query = await _mediator.Send(request);

            var list = await query.ToListAsync();

            return Ok(list);
        }
    }
}