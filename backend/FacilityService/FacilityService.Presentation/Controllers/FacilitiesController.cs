using FacilityService.Core.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.DTOs;

namespace FacilityService.Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FacilitiesController(IMediator mediator) : ControllerBase
{
    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<FacilityDto>> GetFacility(string id)
    {
        var facility = await mediator.Send(new GetFacilityByIdQuery(id));
        return Ok(facility);
    }
}
