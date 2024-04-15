using Microsoft.EntityFrameworkCore;
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
    // O metodo include, é utilizado para informar ao EF, que ele deve trazer a entidade de relacionamento junto. (Doctor)

    {
        return _modelsContext.Sectors.Include(d => d.doctor).FirstOrDefault(s => s.Id == id);
    }

    public Sector SaveSector(Sector sector)
    {

         _modelsContext.Sectors.Add(sector);
         _modelsContext.SaveChanges();
         return sector;
    }

    public IEnumerable<Sector> GetAll()
    {
        return _modelsContext.Sectors.Include(s => s.doctor).ToList();
    }
}