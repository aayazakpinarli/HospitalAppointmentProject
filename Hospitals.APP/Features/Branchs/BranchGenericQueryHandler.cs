using CORE.APP.Models;
using CORE.APP.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Hospitals.APP.Domain;
using Hospitals.APP.Features.Doctors;

namespace Hospitals.APP.Features.Branchs
{
    public class BranchGenericQueryRequest : Request, IRequest<IQueryable<BranchGenericQueryResponse>>
    {
    }

    public class BranchGenericQueryResponse : Response
    {
        public string Title { get; set; }

        public int DoctorCount { get; set; }
        public string Doctors { get; set; }

        public List<DoctorQueryResponse> DoctorList { get; set; }
    }

    public class BranchGenericQueryHandler : Service<Branch>, IRequestHandler<BranchGenericQueryRequest, IQueryable<BranchGenericQueryResponse>>
    {
        public BranchGenericQueryHandler(DbContext db) : base(db)
        {
        }

        protected override IQueryable<Branch> Query(bool isNoTracking = true)
        {
            return base.Query(isNoTracking)
                .Include(b => b.Doctors)
                .OrderBy(b => b.Title);
        }

        public Task<IQueryable<BranchGenericQueryResponse>> Handle(BranchGenericQueryRequest request, CancellationToken cancellationToken)
        {
            var query = Query().Select(b => new BranchGenericQueryResponse
            {
                Guid = b.Guid,
                Id = b.Id,
                Title = b.Title,

                DoctorCount = b.Doctors.Count,
                Doctors = string.Join(", ", b.Doctors.Select(d => d.UserId  )),

                DoctorList = b.Doctors.Select(u => new DoctorQueryResponse
                {
                    Id = u.Id,
                    Guid = u.Guid,
                    UserId = u.UserId

                }).ToList()
            });

            return Task.FromResult(query);
        }
    }
}
