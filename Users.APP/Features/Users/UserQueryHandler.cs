using CORE.APP.Models;
using CORE.APP.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Users.APP.Domain;
using Users.APP.Features.Groups;
using Users.APP.Features.Roles;

namespace Users.APP.Features.Users
{
    public class UserQueryRequest : Request, IRequest<IQueryable<UserQueryResponse>>
    {
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Genders? Gender { get; set; }
        public DateTime? BirthDateStart { get; set; }
        public DateTime? BirthDateEnd { get; set; }
        public bool? IsActive { get; set; }
        public int? CountryId { get; set; }
        public int? CityId { get; set; }
        public int? GroupId { get; set; }
    }



    public class UserQueryResponse : Response 
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Genders Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool IsActive { get; set; }
        public string Address { get; set; }
        public int? CountryId { get; set; }
        public int? CityId { get; set; }
        


        public string FullName { get; set; }
        public string GenderF { get; set; }
        public string BirthDateF { get; set; }
        public string RegistrationDateF { get; set; }
        public string IsActiveF { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int GroupId { get; set; }

        public string Group { get; set; }

        public GroupQueryResponse GroupResponse { get; set; }

        public string Roles { get; set; }

        public List<RoleQueryResponse> RoleResponses { get; set; }
    }


    public class UserQueryHandler : Service<User>, IRequestHandler<UserQueryRequest, IQueryable<UserQueryResponse>>
    {
        public UserQueryHandler(DbContext db) : base(db)
        {
        }

        protected override IQueryable<User> Query(bool isNoTracking = true)
        {
            return base.Query(isNoTracking) 
                .Include(u => u.Group)
                .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                .OrderByDescending(u => u.IsActive) 
                .ThenBy(u => u.RegistrationDate) 
                .ThenBy(u => u.UserName); 

        }

        public Task<IQueryable<UserQueryResponse>> Handle(UserQueryRequest request, CancellationToken cancellationToken)
        {
            var entityQuery = Query();

            if (!string.IsNullOrWhiteSpace(request.UserName))
                entityQuery = entityQuery.Where(u => u.UserName == request.UserName);

            if (!string.IsNullOrWhiteSpace(request.FirstName))
                entityQuery = entityQuery.Where(u => u.FirstName.Contains(request.FirstName.Trim()));

            if (!string.IsNullOrWhiteSpace(request.LastName))
                entityQuery = entityQuery.Where(u => u.LastName.Contains(request.LastName.Trim()));

            if (request.Gender.HasValue) 
                entityQuery = entityQuery.Where(u => u.Gender == request.Gender.Value);

            if (request.BirthDateStart.HasValue)
                entityQuery = entityQuery.Where(u => u.BirthDate.HasValue && u.BirthDate.Value.Date >= request.BirthDateStart.Value.Date);

            if (request.BirthDateEnd.HasValue)
                entityQuery = entityQuery.Where(u => u.BirthDate.HasValue && u.BirthDate.Value.Date <= request.BirthDateEnd.Value.Date);

            if (request.IsActive.HasValue)
                entityQuery = entityQuery.Where(u => u.IsActive == request.IsActive.Value);

            if (request.CountryId.HasValue)
                entityQuery = entityQuery.Where(u => u.CountryId == request.CountryId.Value);

            if (request.CityId.HasValue)
                entityQuery = entityQuery.Where(u => u.CityId == request.CityId.Value);

            
            if (request.GroupId.HasValue)
                entityQuery = entityQuery.Where(u => u.GroupId == request.GroupId.Value);


            var query = entityQuery.Select(u => new UserQueryResponse 
            {
                Id = u.Id,
                Guid = u.Guid,
                UserName = u.UserName,
                Password = u.Password,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Gender = u.Gender,
                BirthDate = u.BirthDate,
                RegistrationDate = u.RegistrationDate,
                IsActive = u.IsActive,
                Address = u.Address,
                CountryId = u.CountryId,
                CityId = u.CityId,

                FullName = u.FirstName + " " + u.LastName,

                GenderF = u.Gender.ToString(), 

                BirthDateF = u.BirthDate.HasValue ? u.BirthDate.Value.ToString("MM/dd/yyyy") : string.Empty,

                RegistrationDateF = u.RegistrationDate.ToShortDateString(),
                IsActiveF = u.IsActive ? "Active" : "Inactive",
                Country = (u.CountryId ?? 0).ToString(), 
                City = (u.CityId ?? 0).ToString(),
                GroupId = u.GroupId,
                Group = u.Group.Title,

                GroupResponse = new GroupQueryResponse
                {
                    Id = u.Group.Id,
                    Guid = u.Group.Guid,
                    Title = u.Group.Title
                },

                Roles = string.Join(", ", u.UserRoles.Select(ur => ur.Role.Name)),

                RoleResponses = u.UserRoles.Select(ur => new RoleQueryResponse
                {
                    Id = ur.Role.Id,
                    Guid = ur.Role.Guid,
                    Name = ur.Role.Name
                }).ToList()
            });

            return Task.FromResult(query);
        }
    }
}
