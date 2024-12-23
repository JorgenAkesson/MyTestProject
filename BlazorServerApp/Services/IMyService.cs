using PatientApi.Models;

namespace BlazorServerApp.Services
{
    public interface IMyService
    {
        public void AddPatient(PatientDTO patient);
        public Task<List<PatientDTO>> GetPatients();
    }
}
