using VirtualHoftalon_Server.Exceptions;
using VirtualHoftalon_Server.Models;
using VirtualHoftalon_Server.Models.Dto.Sector;
using VirtualHoftalon_Server.Repositories.Interfaces;
using VirtualHoftalon_Server.Services.Interfaces;

namespace VirtualHoftalon_Server.Services;

public class SectorService : ISectorService
{
    private readonly ISectorRepository _sectorRepository;
    private readonly IDoctorRepository _doctorRepository;
    private const string NotFoundMessage = "Not found Sector!";

    public SectorService(ISectorRepository sectorRepository, IDoctorRepository doctorRepository)
    {
        _sectorRepository = sectorRepository;
        _doctorRepository = doctorRepository;
    }


    public SectorResponseDTO SaveSector(SectorRequestDTO sectorRequestDto)
    {
        Doctor doctorEntity = _doctorRepository.GetDoctorById(sectorRequestDto.DoctorId) ?? throw new NotFoundDoctorException("Not found Doctor!");
        Sector sectorCreated = new Sector(null, sectorRequestDto.Name, sectorRequestDto.RoomNumber, sectorRequestDto.DoctorId, new List<Appointment>());
        sectorCreated.doctor = doctorEntity;
        sectorCreated = _sectorRepository.SaveSector(sectorCreated);
        return new SectorResponseDTO(sectorCreated.Id, sectorCreated.Name, sectorRequestDto.RoomNumber,sectorCreated.DoctorId);
    }

    public IEnumerable<SectorResponseDTO> GetAll()
    {
        
        return _sectorRepository.GetAll().Select(s => new SectorResponseDTO(s.Id, s.Name, s.RoomNumber, s.DoctorId)).ToList();
    }

    public SectorResponseDTO GetOneById(int id)
    {
        Sector sectorById = _sectorRepository.GetSectorById(id) ?? throw new NotFoundSectorException(NotFoundMessage); 
        return new SectorResponseDTO(sectorById.Id, sectorById.Name, sectorById.RoomNumber,sectorById.DoctorId);
    }

    public SectorResponseDTO UpdateById(int id, SectorUpdateRequestDTO sectorRequestDto)
    {
        Sector sectorById = _sectorRepository.GetSectorById(id) ?? throw new NotFoundSectorException(NotFoundMessage);
        sectorById = UpdateSectorAttributes(sectorRequestDto, sectorById);
        sectorById = _sectorRepository.UpdateSector(sectorById);
        return new SectorResponseDTO(sectorById.Id, sectorById.Name, sectorById.RoomNumber, sectorById.DoctorId);
    }

    public void DeleteById(int id)
    {
        Sector sectorById = _sectorRepository.GetSectorById(id) ?? throw new NotFoundSectorException(NotFoundMessage);
        _sectorRepository.Delete(sectorById);
    }

    private Sector UpdateSectorAttributes(SectorUpdateRequestDTO sectorUpdateRequestDto, Sector sector)
    {
        if (sectorUpdateRequestDto.Name == null && sectorUpdateRequestDto.RoomNumber == null)
        {
            throw new InvalidArgumentsUpdateSectorException("Arguments to update can't be null!. Please put some of these attributes:" +
                                                            "Name, RoomNumber");
        }
        if (sectorUpdateRequestDto.Name != null)
        {
            sector.Name = sectorUpdateRequestDto.Name;
        }

        if (sectorUpdateRequestDto.RoomNumber != null)
        {
            sector.RoomNumber = sectorUpdateRequestDto.RoomNumber;
        }

        return sector;

    }

}