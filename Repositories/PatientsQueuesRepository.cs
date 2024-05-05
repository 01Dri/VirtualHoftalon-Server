using VirtualHoftalon_Server.Exceptions;
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
        IEnumerable<PatientsQueue> patientsQueuesBySectorId = _context.GetAllPatientsBySectorAndAppointmentHour(sectorId, null);
        if (patientsQueuesBySectorId == null || patientsQueuesBySectorId.Count() == 0)
        {
            throw new EmptyListPatientQueuesException($"Patients queues from sector: {sectorId} is empty!");
        }

        IEnumerable<PatientsQueue> patientsPreffered = patientsQueuesBySectorId.Where(ap => ap.IsPreferred);
        if (patientsPreffered.Any())
        {
            return patientsPreffered.First();
        }
        // Retorna um paciente não preferencial baseado na sua posição
        return patientsQueuesBySectorId.OrderBy(ap => ap.Position).First();
    }

    public PatientsQueue GetPatientQueueByPassword(string password)
    {
        return _context.GetPatientsQueueByPassword(password) ?? throw new NotFoundPatientOnQueueException("Not found patient on queue!");
    }
}