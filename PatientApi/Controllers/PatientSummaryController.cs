using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PatientApi.Domain.DTOs;
using PatientApi.Handlers;
using PatientApi.Queries;

namespace PatientApi.Controllers
{
    //[AllowAnonymous]
    //public class PatientSummaryController : Controller
    //{
    //    [HttpGet("{id:int}")]
    //    [ProducesResponseType(StatusCodes.Status200OK)]
    //    [ProducesResponseType(StatusCodes.Status404NotFound)]
    //    public async Task<IActionResult> GetById(int id)
    //    {
    //        return Ok("Ok");
    //    }
    //}



    [Route("api/[controller]")]
    [ApiController]
    public class PatientSummaryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PatientSummaryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PatientSummaryDto>> GetPatientSummary(int id)
        {
            var result = await _mediator.Send(new GetPatientSummaryByIdQuery(id));
            return result is not null ? Ok(result) : NotFound();
        }
    }

}
