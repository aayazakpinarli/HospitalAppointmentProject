using CORE.APP.Models;
using CORE.APP.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Hospitals.APP.Domain;

namespace Hospitals.APP.Features.Branchs
{
    public class BranchCreateRequest : Request, IRequest<CommandResponse>
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
    }

    public class BranchCreateHandler : ServiceBase, IRequestHandler<BranchCreateRequest, CommandResponse>
    {
        private readonly HospitalDb _hospitalDb;

        public BranchCreateHandler(HospitalDb hospitalDb)
        {
            _hospitalDb = hospitalDb;
        }

        public async Task<CommandResponse> Handle(BranchCreateRequest request, CancellationToken cancellationToken)
        {
            if (await _hospitalDb.Branchs.AnyAsync(branchEntity => branchEntity.Title == request.Title.Trim(), cancellationToken))
                return Error("Branch with the same title exists!");

            var entity = new Branch
            {
                Title = request.Title.Trim(),
                Guid = Guid.NewGuid().ToString()
            };
            _hospitalDb.Branchs.Add(entity);
            await _hospitalDb.SaveChangesAsync(cancellationToken);
            return Success("Branch created successfully.", entity.Id);
        }
    }
}
