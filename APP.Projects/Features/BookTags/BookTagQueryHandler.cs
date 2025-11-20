using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using APP.Projects.Domain;
using CORE.APP.Features;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace APP.Projects.Features.BookTags
{
    public class BookTagQueryRequest : Request, IRequest<object>
    {
    }

    public class BookTagQueryHandler : Handler, IRequestHandler<BookTagQueryRequest, object>
    {
        private readonly ProjectsDb _db;

        public BookTagQueryHandler(ProjectsDb db)
            : base(new CultureInfo("en-US"))
        {
            _db = db;
        }

        public async Task<object> Handle(BookTagQueryRequest request, CancellationToken cancellationToken)
        {
            var list = await _db.BookTags
                .Select(x => new { x.Id, x.Name })
                .ToListAsync(cancellationToken);

            return list; 
        }
    }
}