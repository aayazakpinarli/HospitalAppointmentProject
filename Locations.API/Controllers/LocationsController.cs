using Locations.APP.Features.Locations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Locations.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LocationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        
        [HttpPost("[action]")]
        public async Task<IActionResult> InnerJoin(LocationInnerJoinQueryRequest request)
        {
            var response = await _mediator.Send(request);

            var list = await response.ToListAsync();

            if (list.Any())
                return Ok(list);

            return NoContent();
        }

        
        [HttpPost("[action]")]
        public async Task<IActionResult> LeftJoin(LocationLeftJoinQueryRequest request)
        {
            var response = await _mediator.Send(request);

            var list = await response.ToListAsync();

            if (list.Any())
                return Ok(list);

            return NoContent();
        }
    }
}
