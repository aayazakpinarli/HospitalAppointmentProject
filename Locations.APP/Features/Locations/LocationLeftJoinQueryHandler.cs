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
    public class LocationLeftJoinQueryRequest : IRequest<IQueryable<LocationLeftJoinQueryResponse>>, 
        IPageRequest, IOrderRequest
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

    public class LocationLeftJoinQueryResponse 
    {
        public int CountryId { get; set; }

        public string CountryName { get; set; }

        public int? CityId { get; set; }

        public string CityName { get; set; }
    }

    public class LocationLeftJoinQueryHandler : Service<Country>, IRequestHandler<LocationLeftJoinQueryRequest, IQueryable<LocationLeftJoinQueryResponse>>
    {
        public LocationLeftJoinQueryHandler(DbContext db) : base(db)
        {
        }

        public Task<IQueryable<LocationLeftJoinQueryResponse>> Handle(LocationLeftJoinQueryRequest request, CancellationToken cancellationToken)
        {
            var countryQuery = Query();
            var cityQuery = Query<City>();

            var leftJoinQuery = from country in countryQuery
                                join city in cityQuery on country.Id equals city.CountryId into countryCity
                                from city in countryCity.DefaultIfEmpty() 
                                select new LocationLeftJoinQueryResponse
                                {
                                    CountryId = country.Id,
                                    CountryName = country.CountryName,
                                    CityId = city.Id,
                                    CityName = city.CityName
                                };

            if (request.OrderEntityPropertyName == nameof(Country.CountryName))
            {
                if (request.IsOrderDescending)
                    leftJoinQuery = leftJoinQuery.OrderByDescending(location => location.CountryName);
                else
                    leftJoinQuery = leftJoinQuery.OrderBy(location => location.CountryName);
            }
            else if (request.OrderEntityPropertyName == nameof(City.CityName))
            {
                if (request.IsOrderDescending)
                    leftJoinQuery = leftJoinQuery.OrderByDescending(location => location.CityName);
                else
                    leftJoinQuery = leftJoinQuery.OrderBy(location => location.CityName);
            }

            if (request.CountryName.HasAny())
            {
                leftJoinQuery = leftJoinQuery.Where(location => (location.CountryName ?? "").Contains(request.CountryName.Trim()));
            }

            if (request.CityName.HasAny())
            {
                leftJoinQuery = leftJoinQuery.Where(location => (location.CityName ?? "").Contains(request.CityName.Trim()));
            }

            request.TotalCountForPaging = leftJoinQuery.Count();

            if (request.PageNumber > 0 && request.CountPerPage > 0)
            {
                var skipValue = (request.PageNumber - 1) * request.CountPerPage;
                var takeValue = request.CountPerPage;

                leftJoinQuery = leftJoinQuery.Skip(skipValue).Take(takeValue);
            }

            return Task.FromResult(leftJoinQuery);
        }
    }
}
