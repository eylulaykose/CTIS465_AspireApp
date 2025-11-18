using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using APP.Projects.Domain;
using CORE.APP.Features;
using MediatR;

namespace APP.Projects.Features.Tags
{
    public class TagQueryRequest : Request, IRequest<IQueryable<TagQueryResponse>>
    {
        
    }

    public class TagQueryResponse : QueryResponse
    {
        public string Name { get; set; }
    }

    public class TagQueryHandler : Handler, IRequestHandler<TagQueryRequest, IQueryable<TagQueryResponse>>
    {
        private readonly ProjectsDb _db;

        public TagQueryHandler(ProjectsDb db)
            : base(new System.Globalization.CultureInfo("en-US"))
        {
            _db = db;
        }

        public Task<IQueryable<TagQueryResponse>> Handle(TagQueryRequest request, CancellationToken cancellationToken)
        {
            var query =
                _db.Tags.Select(t => new TagQueryResponse
                {
                    Id = t.Id,
                    Name = t.Name
                });

            return Task.FromResult(query);
        }
    }
}