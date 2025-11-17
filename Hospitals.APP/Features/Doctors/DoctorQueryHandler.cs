using CORE.APP.Models;
using CORE.APP.Services;
using Hospitals.APP.Domain;
using Hospitals.APP.Features.Branchs;
using Hospitals.APP.Features.Patients;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hospitals.APP.Features.Doctors
{
    public class DoctorQueryRequest : Request, IRequest<IQueryable<DoctorQueryResponse>>
    {
        public int UserId { get; set; }

        public int GroupId { get; set; }

        public int BranchId { get; set; }

    }

    public class DoctorQueryResponse : Response
    {
        public int UserId { get; set; }

        public int GroupId { get; set; }

        public int BranchId { get; set; }

        public string Branch { get; set; }

        public BranchQueryResponse BranchResponse { get; set; }

        public string Patients { get; set; }

        public List<PatientQueryResponse> PatientResponse { get; set; }

    }


    public class DoctorQueryHandler : Service<Doctor>, IRequestHandler<DoctorQueryRequest, IQueryable<DoctorQueryResponse>>
    {
        public DoctorQueryHandler(DbContext db) : base(db)
        {
        }

        protected override IQueryable<Doctor> Query(bool isNoTracking = true)
        {
            return base.Query(isNoTracking)
                .Include(d => d.Branch)
                .Include(d => d.DoctorPatients).ThenInclude(dp => dp.Patient)
                .OrderByDescending(d => d.BranchId)
                .ThenBy(d => d.UserId);

        }

        public Task<IQueryable<DoctorQueryResponse>> Handle(DoctorQueryRequest request, CancellationToken cancellationToken)
        {
            var entityQuery = Query();

            var query = entityQuery.Select(d => new DoctorQueryResponse
            {
                Id = d.Id,
                Guid = d.Guid,
                UserId = d.UserId,
                GroupId = d.GroupId,
                BranchId = d.BranchId,
                Branch = d.Branch.Title,
                BranchResponse = new BranchQueryResponse
                {
                    Id = d.Branch.Id,
                    Guid = d.Branch.Guid,
                    Title = d.Branch.Title
                },

                Patients = string.Join(", ", d.DoctorPatients.Select(dp => dp.Patient.Id + " " + dp.Patient.Complaints)),
                PatientResponse = d.DoctorPatients.Select(pr => new PatientQueryResponse
                {
                    Id = pr.Patient.Id,
                    Guid = pr.Patient.Guid,
                    Height = pr.Patient.Height,
                    Weight = pr.Patient.Weight,
                    UserId = pr.Patient.UserId,
                    GroupId = pr.Patient.GroupId,
                    Complaints = pr.Patient.Complaints

                }).ToList()
            });

            return Task.FromResult(query);
        }
    }
}
