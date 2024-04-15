using Microsoft.AspNetCore.Mvc;
using VirtualHoftalon_Server.Models;
using VirtualHoftalon_Server.Models.Dto.Sector;
using VirtualHoftalon_Server.Services.Interfaces;

namespace VirtualHoftalon_Server.Controller;

public class SectorController : Microsoft.AspNetCore.Mvc.Controller
{

    private readonly ISectorService _sectorService;

    public SectorController(ISectorService sectorService)
    {
        _sectorService = sectorService;
    }

    [HttpPost]
    [Route("/sectors")]
    public IActionResult SaveSector([FromBody] SectorRequestDTO sectorRequestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result  = _sectorService.SaveSector(sectorRequestDto);
        return Ok(result);
    }

    [HttpGet]
    [Route("/sectors")]
    public IActionResult GetAllSectors()
    {
        return Ok(_sectorService.GetAll());
    }
    
    [HttpGet]
    [Route("/sectors/{id}")]
    public IActionResult GetSectorById(int id)
    {
        var resultSector = _sectorService.GetOneById(id);
        Console.WriteLine($"DOCTOR ID: {resultSector.doctor}");
        return Ok(resultSector);
    }

}