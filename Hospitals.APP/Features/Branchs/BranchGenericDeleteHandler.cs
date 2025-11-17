using CORE.APP.Models;
using CORE.APP.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Hospitals.APP.Domain;

namespace Hospitals.APP.Features.Branchs
{
    public class BranchGenericDeleteRequest : Request, IRequest<CommandResponse>
    {
    }

    internal class BranchGenericDeleteHandler : Service<Branch>, IRequestHandler<BranchGenericDeleteRequest, CommandResponse>
    {
        public BranchGenericDeleteHandler(DbContext db) : base(db)
        {
        }

        protected override IQueryable<Branch> Query(bool isNoTracking = true)
        {
            return base.Query(isNoTracking)
                .Include(b => b.Doctors);
        }

        public async Task<CommandResponse> Handle(BranchGenericDeleteRequest request, CancellationToken cancellationToken)
        {
            var entity = await Query(false).SingleOrDefaultAsync(b => b.Id == request.Id, cancellationToken);
            if (entity is null)
                return Error("Branch is not found!");

            if (entity.Doctors.Count > 0)
                return Error("Branch can't be deleted because it has relational doctors!");

            Delete(entity);
            return Success("Branch deleted successfully.", entity.Id);
        }
    }
}
