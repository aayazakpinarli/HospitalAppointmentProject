using CORE.APP.Models;
using CORE.APP.Services;
using Hospitals.APP.Domain;
using Hospitals.APP.Features.Branchs;
using Hospitals.APP.Features.Patients;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Hospitals.APP.Features.Doctors
{
    public class DoctorUpdateRequest : Request, IRequest<CommandResponse>
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int GroupId { get; set; }

        public int BranchId { get; set; }

        public Branch Branch { get; set; }

        public List<int> PatientIds { get; set; } = new List<int>();

    }

    public class DoctorUpdateHandler : Service<Doctor>, IRequestHandler<DoctorUpdateRequest, CommandResponse>
    {
        public DoctorUpdateHandler(DbContext db) : base(db)
        {
        }

        public async Task<CommandResponse> Handle(DoctorUpdateRequest request, CancellationToken cancellationToken)
        {
            if (await Query().AnyAsync(d => d.Id != request.Id, cancellationToken))
                return Error("Doctor with the same id exists!");

            var entity = await Query(false).SingleOrDefaultAsync(d => d.Id == request.Id, cancellationToken);
            if (entity is null)
                return Error("Doctor is not found!");

            Delete(entity.DoctorPatients);
            entity.BranchId = request.BranchId;
            entity.UserId = request.UserId;
            entity.GroupId = request.GroupId;
            entity.BranchId = request.BranchId;
            entity.PatientIds = request.PatientIds;

            Update(entity);

            return Success("Doctor is updated successfully.", entity.Id);
        }
    }
}
