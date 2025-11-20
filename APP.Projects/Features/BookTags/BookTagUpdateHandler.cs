using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using APP.Projects.Domain;
using CORE.APP.Features;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace APP.Projects.Features.BookTags
{
    public class BookTagUpdateRequest : Request, IRequest<CommandResponse>
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }
    }

    public class BookTagUpdateHandler : Handler, IRequestHandler<BookTagUpdateRequest, CommandResponse>
    {
        private readonly ProjectsDb _db;

        public BookTagUpdateHandler(ProjectsDb db)
            : base(new CultureInfo("en-US"))
        {
            _db = db;
        }

        public async Task<CommandResponse> Handle(BookTagUpdateRequest request, CancellationToken cancellationToken)
        {
            var entity = await _db.BookTags.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (entity == null) return Error("BookTag not found.");

            entity.Name = request.Name.Trim();

            await _db.SaveChangesAsync(cancellationToken);
            return Success("BookTag updated successfully.", entity.Id);
        }
    }
}