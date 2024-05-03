using VirtualHoftalon_Server.Models;

namespace VirtualHoftalon_Server.Repositories.Interfaces;

public interface IPatientQueuesContextModels
{
    IEnumerable<PatientsQueue> GetAllPatientsQueues();
    
    PatientsQueue SavePatientsQueue(PatientsQueue patients);
    
    PatientsQueue? GetPatientsQueueById(int? id);
    
    bool DeletePatientsQueue(PatientsQueue patients);
    
    int? GetLastPositionBySectorAndHour(string hour, int? sectorId);
    
    IEnumerable<PatientsQueue?> GetAllPatientsBySectorAndAppointmentHour(int sectorId, string? hour);
}