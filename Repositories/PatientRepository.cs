using Microsoft.EntityFrameworkCore;
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
        return _context.Patients.ToList();
    }

    public Patient SavePatient(Patient patient)
    {
        _context.Patients.Add(patient);
        _context.SaveChanges();
        return patient;
    }

    public Patient GetPatientById(int id)
    {
        return _context.Patients.FirstOrDefault(x => x.Id == id);

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