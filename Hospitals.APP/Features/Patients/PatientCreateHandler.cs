using CORE.APP.Models;
using CORE.APP.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Data;
using Hospitals.APP.Domain;

namespace Hospitals.APP.Features.Patients
{
    public class PatientCreateRequest : Request, IRequest<CommandResponse>
    {
        public decimal? Height { get; set; }

        public decimal? Weight { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int GroupId { get; set; }

        [Required]
        [StringLength(500)]
        public string Complaints { get; set; }

    }

    public class PatientCreateHandler : Service<Patient>, IRequestHandler<PatientCreateRequest, CommandResponse>
    {
        public PatientCreateHandler(DbContext db) : base(db)
        {
        }

        public async Task<CommandResponse> Handle(PatientCreateRequest request, CancellationToken cancellationToken)
        {
            if (await Query().AnyAsync(p => p.UserId == request.UserId, cancellationToken))
                return Error("Patient with the same name exists!");

            var entity = new Patient
            {
                GroupId = request.GroupId,
                UserId = request.UserId,
                Height = request.Height,
                Weight = request.Weight,
                Complaints = request.Complaints

            };
            await Create(entity, cancellationToken);
            return Success("Patient created successfully.", entity.Id);
        }
    }
}
