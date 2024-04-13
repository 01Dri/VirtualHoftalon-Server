using VirtualHoftalon_Server.Models.Dto;
using VirtualHoftalon_Server.Repositories.Interfaces;
using VirtualHoftalon_Server.Services.Interfaces;

namespace VirtualHoftalon_Server.Services;

public class DoctorService : IDoctorService
{

    private readonly IDoctorRepository _doctorRepository;
    private readonly ILogger<DoctorService> _logger;

    public DoctorService(IDoctorRepository doctorRepository, ILogger<DoctorService> logger)
    {
        _doctorRepository = doctorRepository;
        _logger = logger;
    }

    public List<DoctorResponseDTO> GetAll()
    {
        return _doctorRepository.GetDoctors()
            .Select(doctor => new DoctorResponseDTO(doctor.Id, doctor.SectorId, doctor.Name))
            .ToList();
    }
}