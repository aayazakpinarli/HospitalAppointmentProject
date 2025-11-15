using CORE.APP.Models;
using CORE.APP.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Users.APP.Domain;

namespace Users.APP.Features.Groups
{
    public class GroupUpdateRequest : Request, IRequest<CommandResponse>
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
    }

    public class GroupUpdateHandler : ServiceBase, IRequestHandler<GroupUpdateRequest, CommandResponse>
    {
        private readonly UsersDb _usersDb;

        public GroupUpdateHandler(UsersDb usersDb)
        {
            _usersDb = usersDb;
        }

        public async Task<CommandResponse> Handle(GroupUpdateRequest request, CancellationToken cancellationToken)
        {
            if (await _usersDb.Groups.AnyAsync(groupEntity => groupEntity.Id != request.Id && groupEntity.Title == request.Title, cancellationToken))
                return Error("Group with the same title exists!");
            var entity = await _usersDb.Groups.SingleOrDefaultAsync(g => g.Id == request.Id, cancellationToken);
            if (entity is null)
                return Error("Group not found!");
            entity.Title = request.Title?.Trim();
            _usersDb.Groups.Update(entity);
            await _usersDb.SaveChangesAsync(cancellationToken);
            return Success("Group updated successfully", entity.Id);
        }
    }
}
