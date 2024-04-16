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
        return _context.Doctors.ToList();
    }

    public Doctor SaveDoctor(Doctor doctor)
    {
        _context.Doctors.Add(doctor);
        _context.SaveChanges();
        return doctor;
    }

    public Doctor GetDoctorById(int id)
    {
        return _context.Doctors.Find(id);
    }

    public Doctor GetDoctorByName(string doctorName)
    {
        return _context.Doctors.FirstOrDefault(d => d.Name == doctorName);
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