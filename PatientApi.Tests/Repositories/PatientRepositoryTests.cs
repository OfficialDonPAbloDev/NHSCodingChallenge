using FluentAssertions;
using PatientApi.Domain.Repositories;

namespace PatientApi.Tests.Repositories
{
    public class PatientRepositoryTests
    {
        [Theory]
        [InlineData(1, "Jimmy", "Johnson")]
        [InlineData(2, "Pammy", "Peasgood")]
        [InlineData(3, "Terry", "Thompson")]
        [InlineData(4, "Harry", "Holmes")]
        [InlineData(5, "Barry", "Baldwin")]
        public async Task GetByIdAsync_WithSeededId_ReturnsPatient(int id, string firstName, string surname)
        {
            var sut = new PatientRepository();

            var result = await sut.GetByIdAsync(id);

            result.Should().NotBeNull();
            result!.Id.Should().Be(id);
            result.FirstName.Should().Be(firstName);
            result.Surname.Should().Be(surname);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(6)]
        [InlineData(-1)]
        [InlineData(999)]
        public async Task GetByIdAsync_WithUnknownId_ReturnsNull(int id)
        {
            var sut = new PatientRepository();

            var result = await sut.GetByIdAsync(id);

            result.Should().BeNull();
        }
    }
}
