using CORE.APP.Models;
using CORE.APP.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Users.APP.Domain;

namespace Users.APP.Features.Roles
{
    public class RoleQueryRequest : Request, IRequest<IQueryable<RoleQueryResponse>>
    {
    }

    public class RoleQueryResponse : Response
    {
        public string Name { get; set; }
    }

    public class RoleQueryHandler : Service<Role>, IRequestHandler<RoleQueryRequest, IQueryable<RoleQueryResponse>>
    {
        public RoleQueryHandler(DbContext db) : base(db)
        {
        }

        public Task<IQueryable<RoleQueryResponse>> Handle(RoleQueryRequest request, CancellationToken cancellationToken)
        {
            var query = Query().Select(r => new RoleQueryResponse
            {
                Guid = r.Guid,
                Id = r.Id,
                Name = r.Name
            });
            return Task.FromResult(query);
        }
    }
}