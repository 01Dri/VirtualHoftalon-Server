using VirtualHoftalon_Server.Models;

namespace VirtualHoftalon_Server.Repositories.Interfaces;

public interface IDoctorRepository
{
    IEnumerable<Doctor?> GetDoctors();
    Doctor SaveDoctor(Doctor doctor);

    Doctor GetDoctorById(int id);

    Doctor GetDoctorByName(string doctorName);
    Doctor GetDoctorByUsernameLogin(string username);

    Doctor GetDoctorByCPF(string cpf);

    Doctor UpdateDoctor(Doctor doctorToUpdate);
    bool DeleteDoctor(Doctor doctor);
}