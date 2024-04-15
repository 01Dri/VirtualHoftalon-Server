using VirtualHoftalon_Server.Models.Dto.Sector;

namespace VirtualHoftalon_Server.Services.Interfaces;

public interface ISectorService
{

    SectorResponseDTO SaveSector(SectorRequestDTO sectorRequestDto);

    IEnumerable<SectorResponseDTO> GetAll();

    SectorResponseDTO GetOneById(int id);

}