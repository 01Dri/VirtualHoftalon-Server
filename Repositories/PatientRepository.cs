using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using VirtualHoftalon_Server.Exceptions;
using VirtualHoftalon_Server.Models;
using VirtualHoftalon_Server.Repositories.Interfaces;

namespace VirtualHoftalon_Server.Repositories;

public class PatientRepository : IPatientRepository
{

    private readonly ModelsContext _context;

    public PatientRepository(ModelsContext context)
    {
        _context = context;
    }

    public IEnumerable<Patient?> GetPatients()
    {
        return _context.Patients.Include(p => p.Appointments).ToList();
    }

    public Patient SavePatient(Patient patient)
    {
        try
        {
            _context.Patients.Add(patient);
            _context.SaveChanges();
            return patient;
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

    public Patient GetPatientById(int? id)
    {
        return _context.Patients.Include(p => p.Appointments).FirstOrDefault(x => x.Id == id);

    }

    public Patient GetPatientByCpf(string cpf)
    {
        return _context.Patients.FirstOrDefault(x => x.Cpf == cpf);
    }

    public Patient UpdatePatient(Patient patientToUpdate)
    {
        _context.Entry(patientToUpdate).State = EntityState.Modified;
        _context.SaveChanges();
        return patientToUpdate;
    }

    public bool DeletePatient(Patient patient)
    {
        _context.Patients.Remove(patient);
        _context.SaveChanges();
        return true;
    }
}