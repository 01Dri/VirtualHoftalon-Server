using Microsoft.Data.SqlClient;
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

    public Sector? GetSectorById(int? id)
    // O metodo include, é utilizado para informar ao EF, que ele deve trazer a entidade de relacionamento junto. (Doctor)

    {
        return _modelsContext.Sectors.Include(d => d.doctor).FirstOrDefault(s => s.Id == id);
    }

    public Sector SaveSector(Sector? sector)
    {
        using var transaction = _modelsContext.Database.BeginTransaction();
        try
        {
            _modelsContext.Sectors.Add(sector);
            _modelsContext.SaveChanges();
            return sector;
        }
        catch (SqlException e)
        {
            transaction.Rollback();
            throw new Exception(e.Message);
        }
    }

    public IEnumerable<Sector> GetAll()
    {
        return _modelsContext.Sectors
            .Include(s => s.doctor)
            .Include(s => s.Appointments)
            .ToList();
    }

    public Sector UpdateSector(Sector? sectorById)
    {
        // Marca a entidade como modificada, dessa forma, o EF vai saber que ela precisa ser atualizada no banco com o metodo "SaveChanges"
        _modelsContext.Entry(sectorById).State = EntityState.Modified;
        _modelsContext.SaveChanges();
        return sectorById;
    }

    public void Delete(Sector? sectorById)
    {
        _modelsContext.Sectors.Remove(sectorById);
        _modelsContext.SaveChanges();

    }
}