using CORE.APP.Models;
using CORE.APP.Services;
using Hospitals.APP.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospitals.APP.Features.Doctors
{
    public class DoctorCreateRequest : Request, IRequest<CommandResponse>
    {
        [Required]
        public int UserId { get; set; }

        public int BranchId { get; set; }

        public Branch Branch { get; set; }

        public List<int> PatientIds { get; set; } = new List<int>();

    }

    public class DoctorCreateHandler : Service<Doctor>, IRequestHandler<DoctorCreateRequest, CommandResponse>
    {
        public DoctorCreateHandler(DbContext db) : base(db)
        {
        }

        public async Task<CommandResponse> Handle(DoctorCreateRequest request, CancellationToken cancellationToken)
        {
            if (await Query().AnyAsync(d => d.UserId== request.UserId, cancellationToken))
                return Error("Doctor with the same user id exists!");

            var entity = new Doctor
            {
                BranchId = request.BranchId,
                UserId = request.UserId,

                PatientIds = request.PatientIds
            };

            Create(entity);

            return Success("Doctor created successfully.", entity.Id);
        }
    }
}
