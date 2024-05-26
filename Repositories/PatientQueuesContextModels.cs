using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using VirtualHoftalon_Server.Exceptions;
using VirtualHoftalon_Server.Models;
using VirtualHoftalon_Server.Repositories.Interfaces;

namespace VirtualHoftalon_Server.Repositories;

public class PatientQueuesContextModels : IPatientQueuesContextModels
{

    private readonly ModelsContext _modelsContext;

    public PatientQueuesContextModels(ModelsContext modelsContext)
    {
        _modelsContext = modelsContext;
    }

    public IEnumerable<PatientsQueue> GetAllPatientsQueues()
    {
        return _modelsContext.PatientsQueues
            .Include(p => p.Patient)
            .Include(p => p.Appointment)
            .ToList();
    }

    public PatientsQueue SavePatientsQueue(PatientsQueue patients)
    {
            using var transaction = _modelsContext.Database.BeginTransaction();
            
            try
            {
                _modelsContext.PatientsQueues.Add(patients);
                _modelsContext.SaveChanges();
                return patients;
            }
            catch (DbUpdateException ex)
            {
                {
                    transaction.Rollback();
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

    public PatientsQueue? GetPatientsQueueById(int? id)
    {
        return _modelsContext.PatientsQueues
            .Include(a => a.Patient)
            .Include(a => a.Appointment)
            .FirstOrDefault(a => a.Id == id);
    }

    public PatientsQueue? GetPatientsQueueByPassword(string password)
    {
        return QueryWithIncludeEntities().FirstOrDefault(ap => ap.Password == password);
    }

    public bool DeletePatientsQueue(PatientsQueue patients)
    {
        _modelsContext.PatientsQueues.Remove(patients);
        _modelsContext.SaveChanges();
        return true;
    }

    public int? GetLastPositionBySectorAndHour(string hour, int? sectorId)
    {
        try
        {
            return _modelsContext.PatientsQueues
                .Where(pq => pq.Appointment.Hour == hour
                             && pq.Appointment.SectorId == sectorId)
                .Max(pq => pq.Position);
        }
        catch (InvalidOperationException e)
        {
            return 0;
        }
    }

    public IEnumerable<PatientsQueue?> GetAllPatientsBySectorAndAppointmentHour(int sectorId, string? hour)
    {
        if (hour == null)
        {
            return QueryWithIncludeEntities()
                .Where(pq => pq.Appointment.SectorId == sectorId)
                .ToList();
        }
        return QueryWithIncludeEntities()
            .Where(pq => pq.Appointment.SectorId == sectorId && pq.Appointment.Hour == hour)
            .ToList();
    }



    private IQueryable<PatientsQueue> QueryWithIncludeEntities()
    {
        return _modelsContext.PatientsQueues
            .Include(pq => pq.Appointment)
            .Include(pq => pq.Patient);
    }

}