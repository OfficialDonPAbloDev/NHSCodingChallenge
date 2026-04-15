using MediatR;
using PatientApi.Domain.DTOs;
using PatientApi.Queries;
using PatientApi.Repositories;

namespace PatientApi.Handlers
{

    public class GetPatientSummaryByIdQueryHandler : IRequestHandler<GetPatientSummaryByIdQuery, PatientSummaryDto>
    {
        private readonly IPatientRepository _patientRepository;

        public GetPatientSummaryByIdQueryHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public Task<PatientSummaryDto> Handle(GetPatientSummaryByIdQuery request, CancellationToken cancellationToken)
        {
            var result = new PatientSummaryDto 
            { 
                Title = Models.Titles.Mr, 
                Name = "Jimmy Johnson", 
                NHSNumber = "100 200 3000", 
                DOB = new DateTime(1985, 1, 1),
                GPPractice = "Woodlands Medical Centre"
            };
            return Task.FromResult(result);
        }
    }
}
