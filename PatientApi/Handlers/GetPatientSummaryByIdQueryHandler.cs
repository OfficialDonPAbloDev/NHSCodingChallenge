using MediatR;
using PatientApi.Domain.DTOs;
using PatientApi.Mappers;
using PatientApi.Queries;
using PatientApi.Repositories;

namespace PatientApi.Handlers
{

    public class GetPatientSummaryByIdQueryHandler : IRequestHandler<GetPatientSummaryByIdQuery, PatientSummaryDto?>
    {
        private readonly IPatientRepository _patientRepository;

        public GetPatientSummaryByIdQueryHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<PatientSummaryDto?> Handle(GetPatientSummaryByIdQuery request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var dbResult = await _patientRepository.GetByIdAsync(request.Id);

            return dbResult?.ToDto();
        }
    }
}
