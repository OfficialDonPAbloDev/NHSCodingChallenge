using PatientApi.Domain.Models;

namespace PatientApi.Domain.DTOs
{
    public class PatientSummaryDto
    { 
        public required string NHSNumber { get; set; }
        public Titles Title { get; set; }
        public required string Name { get; set; }
        public DateTime DOB { get; set; }
        public string? GPPractice { get; set; }
    }
}
