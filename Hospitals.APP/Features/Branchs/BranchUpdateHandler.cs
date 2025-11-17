using CORE.APP.Models;
using CORE.APP.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Hospitals.APP.Domain;

namespace Hospitals.APP.Features.Branchs
{
    public class BranchUpdateRequest : Request, IRequest<CommandResponse>
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
    }

    public class BranchUpdateHandler : ServiceBase, IRequestHandler<BranchUpdateRequest, CommandResponse>
    {
        private readonly HospitalDb _hospitalDb;

        public BranchUpdateHandler(HospitalDb hospitalDb)
        {
            _hospitalDb = hospitalDb;
        }

        public async Task<CommandResponse> Handle(BranchUpdateRequest request, CancellationToken cancellationToken)
        {
            if (await _hospitalDb.Branchs.AnyAsync(branchEntity => branchEntity.Id != request.Id && branchEntity.Title == request.Title, cancellationToken))
                return Error("Branch with the same title exists!");

            var entity = await _hospitalDb.Branchs.SingleOrDefaultAsync(b => b.Id == request.Id, cancellationToken);
            if (entity is null)
                return Error("Branch is not found!");
            entity.Title = request.Title?.Trim();

            _hospitalDb.Branchs.Update(entity);
            await _hospitalDb.SaveChangesAsync(cancellationToken);
            return Success("Branch is updated successfully", entity.Id);
        }
    }
}
