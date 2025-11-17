using CORE.APP.Models;
using CORE.APP.Services;
using MediatR;
using Hospitals.APP.Domain;

namespace Hospitals.APP.Features.Branchs
{
    public class BranchQueryRequest : Request, IRequest<IQueryable<BranchQueryResponse>>
    {
    }

    public class BranchQueryResponse : Response
    {
        public string Title { get; set; }
    }

    public class BranchQueryHandler : ServiceBase, IRequestHandler<BranchQueryRequest, IQueryable<BranchQueryResponse>>
    {
        private readonly HospitalDb _hospitalDb;

        public BranchQueryHandler(HospitalDb HospitalDb)
        {
            _hospitalDb = HospitalDb;
        }

        public Task<IQueryable<BranchQueryResponse>> Handle(BranchQueryRequest request, CancellationToken cancellationToken)
        {
            var query = _hospitalDb.Branchs.Select(groupEntity => new BranchQueryResponse()
            {
                Id = groupEntity.Id,
                Guid = groupEntity.Guid,
                Title = groupEntity.Title
            });
            return Task.FromResult(query);
        }
    }
}
