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
    public class TagCreateRequest : Request, IRequest<CommandResponse>
    {
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
    }

    // Handler – business logic
    public class TagCreateHandler : Handler, IRequestHandler<TagCreateRequest, CommandResponse>
    {
        private readonly ProjectsDb _db;

        public TagCreateHandler(ProjectsDb db)
            : base(new CultureInfo("en-US"))
        {
            _db = db;
        }

        public async Task<CommandResponse> Handle(TagCreateRequest request, CancellationToken cancellationToken)
        {
            if (await _db.Tags.AnyAsync(t => t.Name == request.Name, cancellationToken))
            {
                return Error("Tag with the same name already exists.");
            }

            var entity = new Tag
            {
                Name = request.Name?.Trim()
            };

            _db.Tags.Add(entity);
            await _db.SaveChangesAsync(cancellationToken);

            return Success("Tag created successfully.", entity.Id);
        }
    }
}