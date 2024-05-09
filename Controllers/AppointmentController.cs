using Microsoft.AspNetCore.Authorization;
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
    
    [Authorize]
    [HttpGet]
    [Route("/appointments")]
    public IActionResult GetAll()
    {
        return Ok(_appointmentService.GetAll());
    }
    
    [HttpGet]
    [Route("/appointments/{id}")]
    [Authorize]
    public IActionResult GetById(int id)
    {
        return Ok(_appointmentService.GetOneById(id));
    }
    
    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    [Route("/appointments")]
    public IActionResult Save([FromBody] AppointmentRequestDTO appointmentRequestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        return Ok(_appointmentService.SaveAppointment(appointmentRequestDto));
    }
    [HttpPatch]
    [Authorize(Roles = "ADMIN")]
    [Route("/appointments/{id}")]
    public IActionResult UpdateById(int id, [FromBody] AppointmentUpdateRequestDTO appointment)
    {
        return Ok(_appointmentService.UpdateAppointmentById(id, appointment));
    }
    
    [HttpDelete]
    [Authorize(Roles = "ADMIN")]
    [Route("/appointments/{id}")]
    public IActionResult DeleteById(int id)
    {
        _appointmentService.DeleteAppointmentById(id);
        return NoContent();
    }

}