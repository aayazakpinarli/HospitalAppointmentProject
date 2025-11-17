using CORE.APP.Models;
using CORE.APP.Services;
using Hospitals.APP.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Hospitals.APP.Features.Patients
{
    public class PatientUpdateRequest : Request, IRequest<CommandResponse>
    {
        public decimal? Height { get; set; }

        public decimal? Weight { get; set; }

        public int UserId { get; set; }

        [Required]
        public int GroupId { get; set; }

        [Required]
        [StringLength(500)]
        public string Complaints { get; set; }
    }


    public class PatientUpdateHandler : Service<Patient>, IRequestHandler<PatientUpdateRequest, CommandResponse>
    {
        public PatientUpdateHandler(DbContext db) : base(db)
        {
        }

        public async Task<CommandResponse> Handle(PatientUpdateRequest request, CancellationToken cancellationToken)
        {
            if (await Query().AnyAsync(p => p.Id != request.Id, cancellationToken))
                return Error("Patient with the same name exists!");

            var entity = await Query(false).SingleOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
            if (entity is null)
                return Error("Patient is not found!");

            entity.GroupId = request.GroupId;
            entity.Height = request.Height;
            entity.Weight = request.Weight;
            entity.Complaints = request.Complaints?.Trim();

            await Update(entity, cancellationToken);
            return Success("Patient is updated successfully.", entity.Id);
        }
    }
}
