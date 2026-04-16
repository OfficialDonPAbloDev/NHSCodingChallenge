using PatientApi.Domain.Entities;

namespace PatientApi.Domain.Repositories
{
    public interface IPatientRepository
    {
        Task<Patient?> GetByIdAsync(int id);
    }
}
