using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using APP.Projects.Domain;
using CORE.APP.Features;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace APP.Projects.Features.Tags
{
    public class TagDeleteRequest : Request, IRequest<CommandResponse>
    {
    }

    public class TagDeleteHandler : Handler, IRequestHandler<TagDeleteRequest, CommandResponse>
    {
        private readonly ProjectsDb _db;

        public TagDeleteHandler(ProjectsDb db)
            : base(new CultureInfo("en-US"))
        {
            _db = db;
        }

        public async Task<CommandResponse> Handle(TagDeleteRequest request, CancellationToken cancellationToken)
        {
            var entity = await _db.Tags.SingleOrDefaultAsync(
                t => t.Id == request.Id,
                cancellationToken);

            if (entity is null)
            {
                return Error("Tag not found.");
            }

            _db.Tags.Remove(entity);
            await _db.SaveChangesAsync(cancellationToken);

            return Success("Tag deleted successfully.", entity.Id);
        }
    }
}