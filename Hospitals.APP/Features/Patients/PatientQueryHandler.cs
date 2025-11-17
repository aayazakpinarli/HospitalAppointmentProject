using CORE.APP.Models;
using CORE.APP.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Hospitals.APP.Domain;

namespace Hospitals.APP.Features.Patients
{
    public class PatientQueryRequest : Request, IRequest<IQueryable<PatientQueryResponse>>
    {
    }

    public class PatientQueryResponse : Response
    {
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public string Complaints { get; set; }

    }

    public class PatientQueryHandler : Service<Patient>, IRequestHandler<PatientQueryRequest, IQueryable<PatientQueryResponse>>
    {
        public PatientQueryHandler(DbContext db) : base(db)
        {
        }

        public Task<IQueryable<PatientQueryResponse>> Handle(PatientQueryRequest request, CancellationToken cancellationToken)
        {
            var query = Query().Select(p => new PatientQueryResponse
            {
                Guid = p.Guid,
                Id = p.Id,
                Height = p.Height,
                Weight = p.Weight,
                UserId = p.UserId,
                GroupId = p.GroupId,
            });
            return Task.FromResult(query);
        }
    }
}
