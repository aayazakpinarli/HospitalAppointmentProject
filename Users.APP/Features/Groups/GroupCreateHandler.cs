using CORE.APP.Models;
using CORE.APP.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Users.APP.Domain;

namespace Users.APP.Features.Groups
{
    public class GroupCreateRequest : Request, IRequest<CommandResponse>
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
    }

    public class GroupCreateHandler : ServiceBase, IRequestHandler<GroupCreateRequest, CommandResponse>
    {
        private readonly UsersDb _usersDb;

        public GroupCreateHandler(UsersDb usersDb)
        {
            _usersDb = usersDb;
        }

        public async Task<CommandResponse> Handle(GroupCreateRequest request, CancellationToken cancellationToken)
        {
            if (await _usersDb.Groups.AnyAsync(groupEntity => groupEntity.Title == request.Title.Trim(), cancellationToken))
                return Error("Group with the same title exists!");
            var entity = new Group
            {
                Title = request.Title.Trim(),
                Guid = Guid.NewGuid().ToString()
            };
            _usersDb.Groups.Add(entity);
            await _usersDb.SaveChangesAsync(cancellationToken);
            return Success("Group created successfully.", entity.Id);
        }
    }
}