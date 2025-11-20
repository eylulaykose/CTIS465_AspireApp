using APP.Projects.Features.BookTags;
using CORE.APP.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Projects.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookTagsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BookTagsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public Task<CommandResponse> Create(BookTagCreateRequest request)
            => _mediator.Send(request);

        [HttpPut]
        public Task<CommandResponse> Update(BookTagUpdateRequest request)
            => _mediator.Send(request);

        [HttpDelete("{id}")]
        public Task<CommandResponse> Delete(int id)
            => _mediator.Send(new BookTagDeleteRequest { Id = id });

        [HttpGet]
        public Task<object> GetAll()
            => _mediator.Send(new BookTagQueryRequest());
    }
}