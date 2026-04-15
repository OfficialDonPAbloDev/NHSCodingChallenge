using PatientApi.Models;

namespace PatientApi.Domain.Entities
{
    public class Patient
    {
        public int Id { get; set; }
        public Titles Title { get; set; }
        public required string NHSNumber { get; set; }
        public required string FirstName { get; set; }
        public required string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? GPPractice { get; set; }
    }
}
