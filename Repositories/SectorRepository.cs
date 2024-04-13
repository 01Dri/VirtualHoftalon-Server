using VirtualHoftalon_Server.Models;
using VirtualHoftalon_Server.Repositories.Interfaces;

namespace VirtualHoftalon_Server.Repositories;

public class SectorRepository : ISectorRepository
{

    private readonly ModelsContext _modelsContext;

    public SectorRepository(ModelsContext modelsContext)
    {
        _modelsContext = modelsContext;
    }

    public Sector? GetSectorById(int id)
    {
        return _modelsContext.Sectors.Find(id);
    }
}