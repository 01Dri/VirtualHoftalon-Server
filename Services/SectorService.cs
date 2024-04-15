using VirtualHoftalon_Server.Exceptions;
using VirtualHoftalon_Server.Models;
using VirtualHoftalon_Server.Models.Dto;
using VirtualHoftalon_Server.Models.Dto.Sector;
using VirtualHoftalon_Server.Repositories.Interfaces;
using VirtualHoftalon_Server.Services.Interfaces;

namespace VirtualHoftalon_Server.Services;

public class SectorService : ISectorService
{
    private readonly ISectorRepository _sectorRepository;
    private readonly IDoctorService _doctorService;
    private readonly IDoctorRepository _doctorRepository;

    public SectorService(ISectorRepository sectorRepository, IDoctorService doctorService, IDoctorRepository doctorRepository)
    {
        _sectorRepository = sectorRepository;
        _doctorService = doctorService;
        _doctorRepository = doctorRepository;
    }


    public SectorResponseDTO SaveSector(SectorRequestDTO sectorRequestDto)
    {
        Doctor doctorEntity = _doctorRepository.GetDoctorById(sectorRequestDto.DoctorId);
        Sector sectorCreated = new Sector(null, sectorRequestDto.Name, sectorRequestDto.RoomNumber);
        sectorCreated.doctor = doctorEntity;
        sectorCreated = _sectorRepository.SaveSector(sectorCreated);
        return new SectorResponseDTO(sectorCreated.Id, sectorCreated.Name, sectorCreated.doctor);
    }

    public IEnumerable<SectorResponseDTO> GetAll()
    {
        
        return _sectorRepository.GetAll().Select(s => new SectorResponseDTO(s.Id, s.Name, s.doctor)).ToList();
    }

    public SectorResponseDTO GetOneById(int id)
    {
        Sector sectorById = _sectorRepository.GetSectorById(id);
        if (sectorById == null)
        {
            throw new NotFoundSectorException("Not found Sector!");
        }
        
        return new SectorResponseDTO(sectorById.Id, sectorById.Name, sectorById.doctor);
    }
}