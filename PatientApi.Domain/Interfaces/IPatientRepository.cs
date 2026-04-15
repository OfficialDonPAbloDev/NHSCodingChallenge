using PatientApi.Domain.Entities;

namespace PatientApi.Repositories
{
    public interface IPatientRepository
    {
        Task<Patient?> GetByIdAsync(int id);
    }
}
