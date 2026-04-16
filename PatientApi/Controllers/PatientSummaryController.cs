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
        [ProducesResponseType(typeof(PatientSummaryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PatientSummaryDto>> GetPatientSummary(int id, CancellationToken cancellationToken)
        {
            if (id <= 0)
            {
                return Problem(
                    statusCode: StatusCodes.Status400BadRequest,
                    title: "Invalid id",
                    detail: "'id' must be a positive integer.");
            }

            var result = await _mediator.Send(new GetPatientSummaryByIdQuery(id), cancellationToken);

            if (result is not null)
            {
                //Extract user details from token in header

                //Dispatch message to event hub/message queue for auditing handler to log record accessed by user

                return Ok(result);
            }

            return Problem(
                statusCode: StatusCodes.Status404NotFound,
                title: "Patient not found",
                detail: $"No patient exists with id {id}.");
        }
    }

}
