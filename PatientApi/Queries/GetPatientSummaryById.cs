using MediatR;
using PatientApi.Domain.DTOs;

namespace PatientApi.Queries
{
    public record GetPatientSummaryByIdQuery(int Id) : IRequest<PatientSummaryDto?>;
}
