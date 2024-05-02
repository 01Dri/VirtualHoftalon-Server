using VirtualHoftalon_Server.Models;
using VirtualHoftalon_Server.Repositories.Interfaces;

namespace VirtualHoftalon_Server.Repositories;

public class PatientsQueuesRepository : IPatientsQueuesRepository
{
    private readonly IPatientQueuesContextModels _context;

    public PatientsQueuesRepository(IPatientQueuesContextModels context)
    {
        _context = context;
    }

    public IEnumerable<PatientsQueue?> GetPatientsQueues()
    {
        return _context.GetAllPatientsQueues();
    }

    public PatientsQueue SavePatientsQueue(PatientsQueue patients)
    {
        return _context.SavePatientsQueue(patients);
    }

    public PatientsQueue? GetPatientsQueueById(int? id)
    {
        return _context.GetPatientsQueueById(id);
    }

    public PatientsQueue UpdatePatientsQueue(PatientsQueue patientsQueue)
    {
        throw new NotImplementedException();
    }

    public bool DeletePatientsQueue(PatientsQueue patients)
    {
        return _context.DeletePatientsQueue(patients);
    }

    public int? GetLastPositionBySectorAndHour(string hour, int? sectorId)
    {
        return _context.GetLastPositionBySectorAndHour(hour, sectorId);
    }

    public IEnumerable<PatientsQueue?> GetAllPatientsBySectorAndAppointmentHour(int sectorId, string? hour)
    {
        return _context.GetAllPatientsBySectorAndAppointmentHour(sectorId, hour);
    }

    public PatientsQueue CallPatientOnQueueBySectorId(int sectorId)
    {
        return _context.CallPatientOnQueueBySectorId(sectorId);
    }

}