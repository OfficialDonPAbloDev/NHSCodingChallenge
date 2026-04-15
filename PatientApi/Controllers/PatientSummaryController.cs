using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PatientApi.Domain.DTOs;
using PatientApi.Queries;

namespace PatientApi.Controllers
{
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
            if (result is not null)
            {
                //Extract user details from token in header

                //Dispatch message to event hub/message queue for auditing handler to log record accessed by user
                
                return Ok(result);
            }

            return NotFound();
        }
    }

}
