using VirtualHoftalon_Server.Models;

namespace VirtualHoftalon_Server.Repositories.Interfaces;

public interface ISectorRepository
{
    Sector GetSectorById(int? id);

    Sector SaveSector(Sector? sector);

    IEnumerable<Sector> GetAll();
    Sector UpdateSector(Sector? sectorById);
    void Delete(Sector? sectorById);
}