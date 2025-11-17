using CORE.APP.Models;
using CORE.APP.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Hospitals.APP.Domain;

namespace Hospitals.APP.Features.Doctors
{
    public class DoctorDeleteRequest : Request, IRequest<CommandResponse>
    {
    }

    public class DoctorDeleteHandler : Service<Doctor>, IRequestHandler<DoctorDeleteRequest, CommandResponse>
    {
        public DoctorDeleteHandler(DbContext db) : base(db)
        {
        }

        public async Task<CommandResponse> Handle(DoctorDeleteRequest request, CancellationToken cancellationToken)
        {
            var entity = await Query(false).SingleOrDefaultAsync(d => d.Id == request.Id, cancellationToken);
            if (entity is null)
                return Error("Doctor not found!");

            Delete(entity.DoctorPatients);

            Delete(entity);

            return Success("Doctor deleted successfully.", entity.Id);
        }
    }
}
