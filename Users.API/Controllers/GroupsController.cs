using CORE.APP.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Users.APP.Features.Groups;

namespace Users.API.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GroupsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = await _mediator.Send(new GroupQueryRequest());
            var list = await query.ToListAsync();
            if (list.Count == 0)
                return NoContent(); // 204
            return Ok(list); // 200
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var query = await _mediator.Send(new GroupQueryRequest());
            var item = await query.SingleOrDefaultAsync(groupResponse => groupResponse.Id == id);
            if (item is null)
                return NotFound("Group not found!"); // 404
            return Ok(item); // 200
        }

        [HttpPost]
        public async Task<IActionResult> Post(GroupCreateRequest request)
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(request);
                if (response.IsSuccessful)
                    return CreatedAtAction("Get", new { id = response.Id }, response); // 201
                return BadRequest(response);
            }
            return BadRequest(ModelState);
        }

        [HttpPut]
        public async Task<IActionResult> Put(GroupUpdateRequest request)
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(request);
                if (response.IsSuccessful)
                    return NoContent();
                return BadRequest(response);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _mediator.Send(new GroupDeleteRequest() { Id = id });
            if (response.IsSuccessful)
                return NoContent(); // 204
            return BadRequest(response);
        }
    }
}