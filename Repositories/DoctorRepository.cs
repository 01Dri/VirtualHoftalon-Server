using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using VirtualHoftalon_Server.Exceptions;
using VirtualHoftalon_Server.Models;
using VirtualHoftalon_Server.Repositories.Interfaces;

namespace VirtualHoftalon_Server.Repositories;

public class DoctorRepository : IDoctorRepository
{
    private readonly ModelsContext _context;

    public DoctorRepository(ModelsContext context)
    {
        _context = context;
    }

    public IEnumerable<Doctor> GetDoctors()
    {
        return _context.Doctors.Include(d => d.Appointments);
    }

    public Doctor SaveDoctor(Doctor doctor)
    {
        using var transaction = _context.Database.BeginTransaction();
        try
        {


            _context.Doctors.Add(doctor);
            _context.SaveChanges();
            return doctor;
        }
        catch (SqlException e)
        {
            transaction.Rollback();
            throw new Exception(e.Message);
        }
    }

    public Doctor GetDoctorById(int? id)
    {
        return _context.Doctors.Include(d => d.Appointments)
            .FirstOrDefault(x => x.Id == id);
    }

    public Doctor GetDoctorByName(string doctorName)
    {
        return _context.Doctors.FirstOrDefault(d => d.Name == doctorName);
    }

    public Doctor GetDoctorByUsernameLogin(string username)
    {
        return _context.Doctors.Include(d => d.Appointments)
            .Where(d => d.Login.Username == username)
            .FirstOrDefault();
    }

    public Doctor GetDoctorByCPF(string cpf)
    {
        return _context.Doctors.FirstOrDefault(d => d.Cpf == cpf);

    }

    public Doctor UpdateDoctor(Doctor doctorToUpdate)
    {
        // Marca a entidade como modificada, dessa forma, o EF vai saber que ela precisa ser atualizada no banco com o metodo "SaveChanges"
        _context.Entry(doctorToUpdate).State = EntityState.Modified;
        _context.SaveChanges();
        return doctorToUpdate;
    }

    public bool DeleteDoctor(Doctor doctor)
    {
        _context.Doctors.Remove(doctor);
        _context.SaveChanges();
        return true;
    }
}