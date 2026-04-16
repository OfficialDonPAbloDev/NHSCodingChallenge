using FluentAssertions;
using PatientApi.Domain.Entities;
using PatientApi.Mappers;
using PatientApi.Domain.Models;

namespace PatientApi.Tests.Mappers
{
    public class PatientSummaryDtoMapperTests
    {
        [Fact]
        public void ToDto_MapsAllFields()
        {
            var patient = new Patient
            {
                Id = 42,
                Title = Titles.Mr,
                NHSNumber = "123 456 7890",
                FirstName = "Mickey",
                Surname = "Mouse",
                DateOfBirth = new DateTime(1815, 12, 10),
                GPPractice = "TipTop Surgery"
            };

            var dto = patient.ToDto();

            dto.NHSNumber.Should().Be("123 456 7890");
            dto.Title.Should().Be(Titles.Mr);
            dto.Name.Should().Be("Mickey Mouse");
            dto.DOB.Should().Be(new DateTime(1815, 12, 10));
            dto.GPPractice.Should().Be("TipTop Surgery");
        }

        [Fact]
        public void ToDto_WithNullGpPractice_MapsToNull()
        {
            var patient = new Patient
            {
                NHSNumber = "100 200 3000",
                FirstName = "Jimmy",
                Surname = "Johnson",
                GPPractice = null
            };

            var dto = patient.ToDto();

            dto.GPPractice.Should().BeNull();
        }
    }
}
