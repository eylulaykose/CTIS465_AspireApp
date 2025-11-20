using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using APP.Projects.Domain;
using CORE.APP.Features;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace APP.Projects.Features.BookTags
{
    public class BookTagDeleteRequest : Request, IRequest<CommandResponse>
    {
        public int Id { get; set; }
    }

    public class BookTagDeleteHandler : Handler, IRequestHandler<BookTagDeleteRequest, CommandResponse>
    {
        private readonly ProjectsDb _db;

        public BookTagDeleteHandler(ProjectsDb db)
            : base(new CultureInfo("en-US"))
        {
            _db = db;
        }

        public async Task<CommandResponse> Handle(BookTagDeleteRequest request, CancellationToken cancellationToken)
        {
            var entity = await _db.BookTags.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
            if (entity == null) return Error("BookTag not found.");

            _db.BookTags.Remove(entity);
            await _db.SaveChangesAsync(cancellationToken);

            return Success("BookTag deleted successfully.");
        }
    }
}