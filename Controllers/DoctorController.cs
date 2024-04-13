using Microsoft.AspNetCore.Mvc;
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
    public IActionResult GetAll()
    {
        _logger.LogInformation("Controller acessado!");
        return Ok(_doctorService.GetAll());
    }
}