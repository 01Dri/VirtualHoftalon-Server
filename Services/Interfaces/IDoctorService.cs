using VirtualHoftalon_Server.Models.Dto;

namespace VirtualHoftalon_Server.Services.Interfaces;

public interface IDoctorService
{
    List<DoctorResponseDTO> GetAll();
    DoctorResponseDTO SaveDoctor(DoctorRequestDTO doctorRequestDto);
    DoctorResponseDTO GetOneById(int id);

    DoctorResponseDTO UpdateDoctorById(int id, DoctorRequestDTO doctor);

    bool DeleteDoctorById(int id);

}