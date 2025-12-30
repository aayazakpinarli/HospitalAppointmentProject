using CORE.APP.Models;
using CORE.APP.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Users.APP.Domain;

namespace Users.APP.Features.Roles
{
    public class RoleDeleteRequest : Request, IRequest<CommandResponse>
    {
    }

    public class RoleDeleteHandler : Service<Role>, IRequestHandler<RoleDeleteRequest, CommandResponse>
    {
        public RoleDeleteHandler(DbContext db) : base(db)
        {
        }

        public async Task<CommandResponse> Handle(RoleDeleteRequest request, CancellationToken cancellationToken)
        {
            var entity = await Query(false).SingleOrDefaultAsync(r => r.Id == request.Id, cancellationToken);
            if (entity is null)
                return Error("Role not found!");
            await Delete(entity, cancellationToken);
            return Success("Role deleted successfully.", entity.Id);
        }
    }
}
