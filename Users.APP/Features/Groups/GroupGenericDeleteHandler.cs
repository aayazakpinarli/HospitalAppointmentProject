using CORE.APP.Models;
using CORE.APP.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Users.APP.Domain;

namespace Users.APP.Features.Groups
{
    public class GroupGenericDeleteRequest : Request, IRequest<CommandResponse>
    {
    }

    internal class GroupGenericDeleteHandler : Service<Group>, IRequestHandler<GroupGenericDeleteRequest, CommandResponse>
    {
        public GroupGenericDeleteHandler(DbContext db) : base(db)
        {
        }

        protected override IQueryable<Group> Query(bool isNoTracking = true)
        {
            return base.Query(isNoTracking)
                .Include(g => g.Users);
        }

        public async Task<CommandResponse> Handle(GroupGenericDeleteRequest request, CancellationToken cancellationToken)
        {
            var entity = await Query(false).SingleOrDefaultAsync(g => g.Id == request.Id, cancellationToken);
            if (entity is null)
                return Error("Group not found!");

            if (entity.Users.Count > 0) 
                return Error("Group can't be deleted because it has relational users!");
            
            Delete(entity);
            return Success("Group deleted successfully.", entity.Id);
        }
    }
}
