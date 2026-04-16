using PatientApi.Domain.Entities;

namespace PatientApi.Domain.Repositories
{
    public class PatientRepository : IPatientRepository
    {
        private readonly List<Patient> _patients =
        [
            new Patient
            {
                Id = 1,
                NHSNumber = "100 200 3000",
                FirstName = "Jimmy",
                Surname = "Johnson",
                DateOfBirth = new DateTime(1985, 1, 1),
                GPPractice = "Woodlands Medical Centre"
            },
            new Patient
            {
                Id = 2,
                NHSNumber = "100 200 3001",
                FirstName = "Pammy",
                Surname = "Peasgood",
                DateOfBirth = new DateTime(1985, 2, 2),
                GPPractice = "Highlands Medical Centre"
            },
            new Patient
            {
                Id = 3,
                NHSNumber = "100 200 3002",
                FirstName = "Terry",
                Surname = "Thompson",
                DateOfBirth = new DateTime(1985, 3, 3),
                GPPractice = "Lowlands Medical Centre"
            },
            new Patient
            {
                Id = 4,
                NHSNumber = "100 200 3003",
                FirstName = "Harry",
                Surname = "Holmes",
                DateOfBirth = new DateTime(1985, 4, 4),
                GPPractice = "Eastlands Medical Centre"
            },
            new Patient
            {
                Id = 5,
                NHSNumber = "100 200 3004",
                FirstName = "Barry",
                Surname = "Baldwin",
                DateOfBirth = new DateTime(1985, 5, 5),
                GPPractice = "Badlands Medical Centre"
            }
        ];

        public Task<Patient?> GetByIdAsync(int id)
        {
            var patient = _patients.SingleOrDefault(p => p.Id == id);
            return Task.FromResult(patient);
        }
    }
}
