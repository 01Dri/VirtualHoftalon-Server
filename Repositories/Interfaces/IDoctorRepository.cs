using VirtualHoftalon_Server.Models;

namespace VirtualHoftalon_Server.Repositories.Interfaces;

public interface IDoctorRepository
{
    IEnumerable<Doctor?> GetDoctors();
    Doctor SaveDoctor(Doctor doctor);

    Doctor GetDoctorById(int id);

    Doctor GetDoctorByName(string doctorName);
    Doctor UpdateDoctor(Doctor doctorToUpdate);
}