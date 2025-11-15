using CORE.APP.Models;
using CORE.APP.Services;
using MediatR;
using Users.APP.Domain;

namespace Users.APP.Features.Groups
{
    public class GroupQueryRequest : Request, IRequest<IQueryable<GroupQueryResponse>>
    {
    }

    public class GroupQueryResponse : Response
    {
        public string Title { get; set; }
    }

    public class GroupQueryHandler : ServiceBase, IRequestHandler<GroupQueryRequest, IQueryable<GroupQueryResponse>>
    {
        private readonly UsersDb _usersDb;

        public GroupQueryHandler(UsersDb usersDb)
        {
            _usersDb = usersDb;
        }

        public Task<IQueryable<GroupQueryResponse>> Handle(GroupQueryRequest request, CancellationToken cancellationToken)
        {
            var query = _usersDb.Groups.Select(groupEntity => new GroupQueryResponse()
            {
                Id = groupEntity.Id,
                Guid = groupEntity.Guid,
                Title = groupEntity.Title
            });
            return Task.FromResult(query);
        }
    }
}
