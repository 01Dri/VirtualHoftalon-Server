using Microsoft.AspNetCore.Mvc;
using VirtualHoftalon_Server.Repositories.Interfaces;

namespace VirtualHoftalon_Server.Controller;

[Route("api/doctors")]
[ApiController]
public class DoctorController : ControllerBase
{

    private readonly IDoctorRepository _doctorRepository;

    public DoctorController(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var doctors = _doctorRepository.GetDoctors();
        return Ok(doctors);
    }
}