using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using APP.Projects.Domain;
using CORE.APP.Features;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace APP.Projects.Features.Tags
{
    public class TagUpdateRequest : Request, IRequest<CommandResponse>
    {
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
    }

    public class TagUpdateHandler : Handler, IRequestHandler<TagUpdateRequest, CommandResponse>
    {
        private readonly ProjectsDb _db;

        public TagUpdateHandler(ProjectsDb db)
            : base(new CultureInfo("en-US"))
        {
            _db = db;
        }

        public async Task<CommandResponse> Handle(TagUpdateRequest request, CancellationToken cancellationToken)
        {
            var trimmedName = request.Name?.Trim();
            
            if (await _db.Tags.AnyAsync(
                    t => t.Id != request.Id && t.Name == trimmedName,
                    cancellationToken))
            {
                return Error("Tag with the same name already exists.");
            }

            var entity = await _db.Tags.SingleOrDefaultAsync(
                t => t.Id == request.Id,
                cancellationToken);

            if (entity is null)
            {
                return Error("Tag not found.");
            }

            entity.Name = trimmedName;

            await _db.SaveChangesAsync(cancellationToken);

            return Success("Tag updated successfully.", entity.Id);
        }
    }
}