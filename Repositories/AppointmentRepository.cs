using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using VirtualHoftalon_Server.Exceptions;
using VirtualHoftalon_Server.Models;
using VirtualHoftalon_Server.Repositories.Interfaces;

namespace VirtualHoftalon_Server.Repositories;

public class AppointmentRepository : IAppointmentRepository
{

    private readonly ModelsContext _modelsContext;

    public AppointmentRepository(ModelsContext modelsContext)
    {
        _modelsContext = modelsContext;
    }

    public Appointment? GetAppointmentById(int id)
    {
        return _modelsContext.Appointments
            .Include(p => p.Sector)
            .Include(p => p.doctor)
            .Include(p => p.patient)
            .FirstOrDefault(p => p.Id == id);


    }

    public Appointment SaveAppointment(Appointment appointment)
    {
        try
        {

            _modelsContext.Appointments.Add(appointment);
            _modelsContext.SaveChanges();
            return appointment;
        } catch (DbUpdateException ex)
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

    public IEnumerable<Appointment> GetAll()
    {
        return _modelsContext.Appointments
            .Include(p => p.Sector)
            .Include(p => p.doctor)
            .Include(p => p.patient)
            .ToList();
    }

    public Appointment UpdateAppointment(Appointment appointment)
    {
        _modelsContext.Entry(appointment).State = EntityState.Modified;
        _modelsContext.SaveChanges();
        return appointment;
    }

    public void Delete(Appointment appointment)
    {
        _modelsContext.Remove(appointment);
        _modelsContext.SaveChanges();
    }
}