using FacilityService.Core.Application.Queries;
using FacilityService.Presentation.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.DTOs;
using SharedKernel.Params;

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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetFacilities([FromQuery] FacilityParams facilityParams)
    {
        var facilities = await mediator.Send(new GetFacilitiesQuery(facilityParams));

        Response.AddPaginationHeader(facilities);

        return Ok(facilities);
    }

    [HttpGet("provinces")]
    public async Task<ActionResult<IEnumerable<string>>> GetFacilityProvinces()
    {
        var provinces = await mediator.Send(new GetFacilityProvincesQuery());
        return Ok(provinces);
    }
}
