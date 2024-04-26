using VirtualHoftalon_Server.Models;
using VirtualHoftalon_Server.Models.Dto.PatientQueus;

namespace VirtualHoftalon_Server.Services.Interfaces;

public interface IPatientQueuesService
{
    PatientQueuesResponseDTO SavePatient(PatientQueuesRequestDTO patient);

    IEnumerable<PatientQueuesResponseDTO> GetAll();

    PatientQueuesResponseDTO GetOneById(int id);

    PatientQueuesResponseDTO UpdateById(int id, PatientQueuesUpdateDTO patient);

    List<Patient> GetAllPatientsByAppointmentHour(string hour);

    int GetPosition(string hour, int? sectorId);
    void DeleteById(int id);
    
}

