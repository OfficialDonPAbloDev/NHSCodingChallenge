using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PatientApi.Controllers;
using PatientApi.Domain.DTOs;
using PatientApi.Domain.Models;
using PatientApi.Queries;

namespace PatientApi.Tests.Controllers
{
    public class PatientSummaryControllerTests
    {
        private readonly Mock<IMediator> _mediator = new();

        [Fact]
        public async Task GetPatientSummary_WhenRecordFound_ReturnsOkWithSummary()
        {
            var presentId = 1;
            var dto = new PatientSummaryDto
            {
                NHSNumber = "100 200 3000",
                Title = Titles.Mr,
                Name = "Jimmy Johnson",
                DOB = new DateTime(1985, 1, 1),
                GPPractice = "Woodlands Medical Centre"
            };
            _mediator
                .Setup(m => m.Send(It.Is<GetPatientSummaryByIdQuery>(q => q.Id == presentId), It.IsAny<CancellationToken>()))
                .ReturnsAsync(dto);
            var sut = new PatientSummaryController(_mediator.Object);

            var result = await sut.GetPatientSummary(presentId, CancellationToken.None);

            var ok = result.Result.Should().BeOfType<OkObjectResult>().Subject;
            ok.Value.Should().BeSameAs(dto);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public async Task GetPatientSummary_WhenPassedInParameterIsNegativeValueOrZero_ReturnsBadRequestProblemDetails(int idParam)
        {
            _mediator
                .Setup(m => m.Send(It.IsAny<GetPatientSummaryByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((PatientSummaryDto?)null);
            var sut = new PatientSummaryController(_mediator.Object);

            var result = await sut.GetPatientSummary(idParam, CancellationToken.None);

            var objectResult = result.Result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
            var problem = objectResult.Value.Should().BeOfType<ProblemDetails>().Subject;
            problem.Title.Should().Be("Invalid id");
        }

        [Fact]
        public async Task GetPatientSummary_WhenNoRecordExists_ReturnsNotFoundProblemDetails()
        {
            var notPresentId = 404;
            _mediator
                .Setup(m => m.Send(It.IsAny<GetPatientSummaryByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((PatientSummaryDto?)null);
            var sut = new PatientSummaryController(_mediator.Object);

            var result = await sut.GetPatientSummary(notPresentId, CancellationToken.None);

            var objectResult = result.Result.Should().BeOfType<ObjectResult>().Subject;
            objectResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
            var problem = objectResult.Value.Should().BeOfType<ProblemDetails>().Subject;
            problem.Title.Should().Be("Patient not found");
            problem.Detail.Should().Contain(notPresentId.ToString());
        }

        [Fact]
        public async Task GetPatientSummary_SendsQueryWithPassedInParameter()
        {
            var idParameter = 123;
            _mediator
                .Setup(m => m.Send(It.IsAny<GetPatientSummaryByIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((PatientSummaryDto?)null);
            var sut = new PatientSummaryController(_mediator.Object);

            await sut.GetPatientSummary(idParameter, CancellationToken.None);

            _mediator.Verify(
                m => m.Send(It.Is<GetPatientSummaryByIdQuery>(q => q.Id == idParameter), It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}
