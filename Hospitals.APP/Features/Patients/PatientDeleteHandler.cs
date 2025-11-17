using CORE.APP.Models;
using CORE.APP.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Hospitals.APP.Domain;

namespace Hospitals.APP.Features.Patients
{
    public class PatientDeleteRequest : Request, IRequest<CommandResponse>
    {
    }

    public class PatientDeleteHandler : Service<Patient>, IRequestHandler<PatientDeleteRequest, CommandResponse>
    {
        public PatientDeleteHandler(DbContext db) : base(db)
        {
        }

        public async Task<CommandResponse> Handle(PatientDeleteRequest request, CancellationToken cancellationToken)
        {
            var entity = await Query(false).SingleOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
            if (entity is null)
                return Error("Patient is not found!");

            await Delete(entity, cancellationToken);
            return Success("Patient is deleted successfully.", entity.Id);
        }
    }
}
