using Microsoft.AspNetCore.Mvc;
using VirtualHoftalon_Server.Models.Dto.Appointment;
using VirtualHoftalon_Server.Services.Interfaces;

namespace VirtualHoftalon_Server.Controller;

public class AppointmentController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly IAppointmentService _appointmentService;

    public AppointmentController(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }
    
    [HttpGet]
    [Route("/appointments")]
    public IActionResult GetAll()
    {
        return Ok(_appointmentService.GetAll());
    }
    
    [HttpPost]
    [Route("/appointments")]
    public IActionResult Save([FromBody] AppointmentRequestDTO appointmentRequestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        return Ok(_appointmentService.SaveAppointment(appointmentRequestDto));
    }

}