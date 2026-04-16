using FluentAssertions;
using Moq;
using PatientApi.Domain.Entities;
using PatientApi.Handlers;
using PatientApi.Domain.Models;
using PatientApi.Domain.Repositories;
using PatientApi.Queries;

namespace PatientApi.Tests.Handlers
{
    public class GetPatientSummaryByIdQueryHandlerTests
    {
        private readonly Mock<IPatientRepository> _repo = new();

        [Fact]
        public async Task Handle_WhenPatientExists_ReturnsMappedDto()
        {
            var patient = new Patient
            {
                Id = 7,
                Title = Titles.Mrs,
                NHSNumber = "999 888 7777",
                FirstName = "Grace",
                Surname = "Hopper",
                DateOfBirth = new DateTime(1906, 12, 9),
                GPPractice = "Compiler Clinic"
            };
            _repo.Setup(r => r.GetByIdAsync(7)).ReturnsAsync(patient);
            var sut = new GetPatientSummaryByIdQueryHandler(_repo.Object);

            var result = await sut.Handle(new GetPatientSummaryByIdQuery(7), CancellationToken.None);

            result.Should().NotBeNull();
            result!.NHSNumber.Should().Be("999 888 7777");
            result.Name.Should().Be("Grace Hopper");
            result.Title.Should().Be(Titles.Mrs);
            result.DOB.Should().Be(new DateTime(1906, 12, 9));
            result.GPPractice.Should().Be("Compiler Clinic");
        }

        [Fact]
        public async Task Handle_WhenPatientNotFound_ReturnsNull()
        {
            _repo.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Patient?)null);
            var sut = new GetPatientSummaryByIdQueryHandler(_repo.Object);

            var result = await sut.Handle(new GetPatientSummaryByIdQuery(404), CancellationToken.None);

            result.Should().BeNull();
        }

        [Fact]
        public async Task HandleWhenCancellationRequestedThrowsCancelledExceptionAndDoesNotQueryRepository()
        {
            var sut = new GetPatientSummaryByIdQueryHandler(_repo.Object);
            using var cts = new CancellationTokenSource();
            cts.Cancel();

            await Assert.ThrowsAsync<OperationCanceledException>(() =>
                sut.Handle(new GetPatientSummaryByIdQuery(1), cts.Token));

            _repo.Verify(r => r.GetByIdAsync(It.IsAny<int>()), Times.Never);
        }
    }
}
