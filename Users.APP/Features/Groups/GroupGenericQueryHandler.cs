using CORE.APP.Models;
using CORE.APP.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Users.APP.Domain;
using Users.APP.Features.Users;

namespace Users.APP.Features.Groups
{
    public class GroupGenericQueryRequest : Request, IRequest<IQueryable<GroupGenericQueryResponse>>
    {
    }

    public class GroupGenericQueryResponse : Response
    {
        public string Title { get; set; }

        public int UserCount { get; set; }
        public string Users { get; set; }

        public List<UserQueryResponse> UserList { get; set; }
    }

    public class GroupGenericQueryHandler : Service<Group>, IRequestHandler<GroupGenericQueryRequest, IQueryable<GroupGenericQueryResponse>>
    {
        public GroupGenericQueryHandler(DbContext db) : base(db)
        {
        }

        protected override IQueryable<Group> Query(bool isNoTracking = true)
        {
            return base.Query(isNoTracking)
                .Include(g => g.Users)
                .OrderBy(g => g.Title);
        }

        public Task<IQueryable<GroupGenericQueryResponse>> Handle(GroupGenericQueryRequest request, CancellationToken cancellationToken)
        {
            var query = Query().Select(g => new GroupGenericQueryResponse
            {
                Guid = g.Guid,
                Id = g.Id,
                Title = g.Title,

                UserCount = g.Users.Count,
                Users = string.Join(", ", g.Users.Select(u => u.UserName)),

                UserList = g.Users.Select(u => new UserQueryResponse
                {
                    Id = u.Id,
                    Guid = u.Guid,
                    UserName = u.UserName, 
                    FirstName = u.FirstName, 
                    LastName = u.LastName,
                    BirthDate = u.BirthDate,
                    IsActive = u.IsActive,
                    RegistrationDate = u.RegistrationDate,
                    Address = u.Address,
                    CityId = u.CityId,
                    CountryId = u.CountryId,
                    Gender = u.Gender,
                    GroupId = u.GroupId,

                }).ToList()
            });

            return Task.FromResult(query);
        }
    }
}
