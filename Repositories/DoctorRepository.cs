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
}