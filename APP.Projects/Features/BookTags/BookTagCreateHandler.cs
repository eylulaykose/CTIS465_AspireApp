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
    public class BookTagCreateRequest : Request, IRequest<CommandResponse>
    {
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
    }

    public class BookTagCreateHandler : Handler, IRequestHandler<BookTagCreateRequest, CommandResponse>
    {
        private readonly ProjectsDb _db;

        public BookTagCreateHandler(ProjectsDb db)
            : base(new CultureInfo("en-US"))
        {
            _db = db;
        }

        public async Task<CommandResponse> Handle(BookTagCreateRequest request, CancellationToken cancellationToken)
        {
            if (await _db.BookTags.AnyAsync(t => t.Name == request.Name, cancellationToken))
                return Error("BookTag with the same name already exists.");

            var entity = new BookTag { Name = request.Name?.Trim() };

            _db.BookTags.Add(entity);
            await _db.SaveChangesAsync(cancellationToken);

            return Success("BookTag created successfully.", entity.Id);
        }
    }
}