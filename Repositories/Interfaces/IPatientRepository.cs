using VirtualHoftalon_Server.Models;

namespace VirtualHoftalon_Server.Repositories.Interfaces;

public interface IPatientRepository
{
    
    IEnumerable<Patient?> GetPatients();
    Patient SavePatient(Patient patient);

    Patient GetPatientById(int id);

    Patient GetPatientByCpf(string cpf);
    Patient UpdatePatient(Patient patientToUpdate);
    bool DeletePatient(Patient patient);
    
}