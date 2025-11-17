using CORE.APP.Models;
using CORE.APP.Services;
using MediatR;
using Hospitals.APP.Domain;

namespace Hospitals.APP.Features.Branchs
{
    public class BranchDeleteRequest : Request, IRequest<CommandResponse>
    {
    }

    public class BranchDeleteHandler : ServiceBase, IRequestHandler<BranchDeleteRequest, CommandResponse>
    {
        private readonly HospitalDb _hospitalDb;

        public BranchDeleteHandler(HospitalDb hospitalDb)
        {
            _hospitalDb = hospitalDb;
        }

        public async Task<CommandResponse> Handle(BranchDeleteRequest request, CancellationToken cancellationToken)
        {
            var entity = await _hospitalDb.Branchs.FindAsync(request.Id, cancellationToken);
            if (entity is null)
                return Error("Branch not found!");
            _hospitalDb.Branchs.Remove(entity);
            await _hospitalDb.SaveChangesAsync(cancellationToken);
            return Success("Branch deleted successfully.", entity.Id);
        }
    }
}
