using CORE.APP.Extensions;
using CORE.APP.Models.Ordering;
using CORE.APP.Models.Paging;
using CORE.APP.Services;
using Locations.APP.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace Locations.APP.Features.Locations
{

    public class LocationInnerJoinQueryRequest : IRequest<IQueryable<LocationInnerJoinQueryResponse>>, 
        IPageRequest, IOrderRequest // Interface Segregation Principle (I of SOLID) is applied
    {

        public string CountryName { get; set; }

        public string CityName { get; set; }

        public int PageNumber { get; set; } = 1;

        public int CountPerPage { get; set; }

        [JsonIgnore]
        public int TotalCountForPaging { get; set; }

        public string OrderEntityPropertyName { get; set; } = "CountryName";

        public bool IsOrderDescending { get; set; }
    }

    public class LocationInnerJoinQueryResponse
    {

        public int CountryId { get; set; }

        public string CountryName { get; set; }

        public int CityId { get; set; }

        public string CityName { get; set; }
    }


    public class LocationInnerJoinQueryHandler : Service<Country>, IRequestHandler<LocationInnerJoinQueryRequest, IQueryable<LocationInnerJoinQueryResponse>>
    {
        public LocationInnerJoinQueryHandler(DbContext db) : base(db)
        {
        }

        public Task<IQueryable<LocationInnerJoinQueryResponse>> Handle(LocationInnerJoinQueryRequest request, CancellationToken cancellationToken)
        {
            var countryQuery = Query();
            var cityQuery = Query<City>();

            var innerJoinQuery = from country in countryQuery
                                 join city in cityQuery on country.Id equals city.CountryId
                                 select new LocationInnerJoinQueryResponse
                                 {
                                     CountryId = country.Id,
                                     CountryName = country.CountryName,
                                     CityId = city.Id,
                                     CityName = city.CityName
                                 };

            if (request.OrderEntityPropertyName == nameof(Country.CountryName))
            {
                if (request.IsOrderDescending)
                    innerJoinQuery = innerJoinQuery.OrderByDescending(location => location.CountryName);
                else
                    innerJoinQuery = innerJoinQuery.OrderBy(location => location.CountryName);
            }
            else if (request.OrderEntityPropertyName == nameof(City.CityName))
            {
                if (request.IsOrderDescending)
                    innerJoinQuery = innerJoinQuery.OrderByDescending(location => location.CityName);
                else
                    innerJoinQuery = innerJoinQuery.OrderBy(location => location.CityName);
            }

            if (request.CountryName.HasAny())
            {
                innerJoinQuery = innerJoinQuery.Where(location => location.CountryName.Contains(request.CountryName.Trim()));
            }

            if (request.CityName.HasAny())
            {
                innerJoinQuery = innerJoinQuery.Where(location => location.CityName.Contains(request.CityName.Trim()));
            }

            request.TotalCountForPaging = innerJoinQuery.Count();

            if (request.PageNumber > 0 && request.CountPerPage > 0)
            {
                var skipValue = (request.PageNumber - 1) * request.CountPerPage;
                var takeValue = request.CountPerPage;

                innerJoinQuery = innerJoinQuery.Skip(skipValue).Take(takeValue);
            }

            return Task.FromResult(innerJoinQuery);
        }
    }
}
