using CORE.APP.Models;
using CORE.APP.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Users.APP.Domain;

namespace Users.APP.Features.Groups
{
    public class GroupDeleteRequest : Request, IRequest<CommandResponse>
    {
    }

    public class GroupDeleteHandler : ServiceBase, IRequestHandler<GroupDeleteRequest, CommandResponse>
    {
        private readonly UsersDb _usersDb;

        public GroupDeleteHandler(UsersDb usersDb)
        {
            _usersDb = usersDb;
        }

        public async Task<CommandResponse> Handle(GroupDeleteRequest request, CancellationToken cancellationToken)
        {
            var entity = await _usersDb.Groups.FindAsync(request.Id, cancellationToken);
            if (entity is null)
                return Error("Group not found!");

            if (entity.Users.Count > 0) 
                return Error("Group can't be deleted because it has relational users!");

            _usersDb.Groups.Remove(entity);
            await _usersDb.SaveChangesAsync(cancellationToken);
            return Success("Group deleted successfully.", entity.Id);
        }
    }
}