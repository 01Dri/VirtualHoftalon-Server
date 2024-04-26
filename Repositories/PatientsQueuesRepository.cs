using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using VirtualHoftalon_Server.Exceptions;
using VirtualHoftalon_Server.Models;
using VirtualHoftalon_Server.Repositories.Interfaces;

namespace VirtualHoftalon_Server.Repositories;

public class PatientsQueuesRepository : IPatientsQueuesRepository
{
    private readonly ModelsContext _ModelsContext;

    public PatientsQueuesRepository(ModelsContext modelsContext)
    {
        _ModelsContext = modelsContext;
    }

    public IEnumerable<PatientsQueue?> GetPatientsQueues()
    {
        return _ModelsContext.PatientsQueues
            .Include(p => p.Patient)
            .Include(p => p.Appointment)
            .ToList();
    }

    public PatientsQueue SavePatientsQueue(PatientsQueue patients)
    {
        {
            try
            {
                _ModelsContext.PatientsQueues.Add(patients);
                _ModelsContext.SaveChanges();
                return patients;
            }
            catch (DbUpdateException ex)
            {
                {
                    var innerException = ex.InnerException as SqlException;
                    if (innerException != null &&
                        innerException.Number == 2601) // Número do erro para violação de chave única
                    {
                        throw new UniqueConstrangeException("Chave unica violada!");
                    }

                    throw new Exception(ex.Message);
                }
            }
        }
    }

    public PatientsQueue? GetPatientsQueueById(int? id)
    {
        return _ModelsContext.PatientsQueues
            .Include(a => a.Patient)
            .Include(a => a.Appointment)
            .FirstOrDefault(a => a.Id == id);
    }

    public PatientsQueue UpdatePatientsQueue(PatientsQueue patientsQueue)
    {
        _ModelsContext.Entry(patientsQueue).State = EntityState.Modified;
        _ModelsContext.SaveChanges();
        return patientsQueue;
    }

    public bool DeletePatientsQueue(PatientsQueue patients)
    {
        _ModelsContext.PatientsQueues.Remove(patients);
        _ModelsContext.SaveChanges();
        return true;
    }

    public int? GetLastPositionBySectorAndHour(string hour, int? sectorId)
    {
        try
        {
            return _ModelsContext.PatientsQueues
                .Where(pq => pq.Appointment.Hour == hour
                             && pq.Appointment.SectorId == sectorId)
                .Max(pq => pq.Position);
        }
        catch (InvalidOperationException e)
        {
            return 0;
        }
    }
}