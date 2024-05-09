using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VirtualHoftalon_Server.Models.Dto;
using VirtualHoftalon_Server.Services.Interfaces;

namespace VirtualHoftalon_Server.Controller;

public class DoctorController : Microsoft.AspNetCore.Mvc.Controller
{

    private readonly IDoctorService _doctorService;
    private readonly ILogger<DoctorController> _logger;

    public DoctorController(IDoctorService doctorService, ILogger<DoctorController> logger)
    {
        _doctorService = doctorService;
        _logger = logger;
    }
    [HttpGet]
    [Route("/doctors")]
    [Authorize]
    public IActionResult GetAll()
    {
        return Ok(_doctorService.GetAll());
    }
    
    [HttpGet]
    [Route("/doctors/{id}")]
    [Authorize]
    public IActionResult GetById(int id)
    {
        return Ok(_doctorService.GetOneById(id));
    }
    
    [HttpPost]
    [Route("/doctors")]
    [Authorize(Roles = "ADMIN")]
    public IActionResult SaveDoctor([FromBody] DoctorRequestDTO doctorRequestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result  = _doctorService.SaveDoctor(doctorRequestDto);
        return Ok(result);
    }
    
    [HttpPatch]
    [Route("/doctors/{id}")]
    [Authorize(Roles = "ADMIN")]
    public IActionResult UpdateDoctor(int id, [FromBody] DoctorRequestDTO doctorRequestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result  = _doctorService.UpdateDoctorById(id, doctorRequestDto);
        return Ok(result);
    }
    
    [HttpDelete]
    [Route("/doctors/{id}")]
    [Authorize(Roles = "ADMIN")]
    public IActionResult Delete(int id)
    {
        this._doctorService.DeleteDoctorById(id);
        return NoContent();
    }
}