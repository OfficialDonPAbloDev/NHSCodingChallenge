using PatientApi.Domain.DTOs;
using PatientApi.Domain.Entities;

namespace PatientApi.Mappers
{
    public static class PatientSummaryDtoMapper
    {
        public static PatientSummaryDto ToDto(this Patient patient)
        {
            return new PatientSummaryDto
            {
                Name = $"{patient.FirstName} {patient.Surname}",
                NHSNumber = patient.NHSNumber,
                GPPractice = patient.GPPractice,
                DOB = patient.DateOfBirth,
                Title = patient.Title
            };
        }
    }
}
