using VirtualHoftalon_Server.Exceptions;
using VirtualHoftalon_Server.Models;
using VirtualHoftalon_Server.Models.Dto;
using VirtualHoftalon_Server.Repositories.Interfaces;
using VirtualHoftalon_Server.Services.Interfaces;

namespace VirtualHoftalon_Server.Services;

public class DoctorService : IDoctorService {

    private readonly IDoctorRepository _doctorRepository;
    private readonly ISectorRepository _sectorRepository;
    private readonly ILogger<DoctorService> _logger;

    public DoctorService(IDoctorRepository doctorRepository, ISectorRepository sectorRepository, ILogger<DoctorService> logger)
    {
        _doctorRepository = doctorRepository;
        _sectorRepository = sectorRepository;
        _logger = logger;
    }


    public List<DoctorResponseDTO> GetAll()
    {
        return _doctorRepository.GetDoctors()
            .Select(doctor => new DoctorResponseDTO(doctor.Id, doctor.SectorId, doctor.Name))
            .ToList();
    }

    public DoctorResponseDTO SaveDoctor(DoctorRequestDTO doctorRequestDto)
    {
        Sector? sector = _sectorRepository.GetSectorById(doctorRequestDto.SectorId);
        if (sector == null)
        {
            throw new NotFoundSectorException("Not found Sector!");
        }

        Doctor doctor = new Doctor(null, doctorRequestDto.Name, doctorRequestDto.SectorId);
        doctor = _doctorRepository.SaveDoctor(doctor);
        return new DoctorResponseDTO(doctor.Id, doctor.SectorId, doctor.Name);
    }
}