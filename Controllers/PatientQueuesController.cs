using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using VirtualHoftalon_Server.Models.Dto.PatientQueus;
using VirtualHoftalon_Server.Services.Interfaces;

namespace VirtualHoftalon_Server.Controller;

public class PatientQueuesController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly IPatientQueuesService _patientQueuesService;

    public PatientQueuesController(IPatientQueuesService patientQueues)
    {
        _patientQueuesService = patientQueues;
    }

    [HttpPost]
    [Route("/patients/queues")]
    [Authorize(Roles = "ADMIN")]

    public IActionResult SavePatientOnQueue([FromBody] PatientQueuesRequestDTO patientQueuesRequestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(_patientQueuesService.SavePatient(patientQueuesRequestDto));
    }

    [HttpGet]
    [Route("/patients/queues")]
    [Authorize]
    public IActionResult GetAll()
    {
        return Ok(_patientQueuesService.GetAll());
    }
    [HttpGet]
    [Route("/patients/queues/{id}")]
    [Authorize]
    public IActionResult GetById(int id)
    {
        return Ok(_patientQueuesService.GetOneById(id));
    }
    
    [HttpGet] 
    [Route("/patients/queues/sectors/{id}")]
    [Authorize]
    public IActionResult GetByHour(int id, [FromQuery] string hour)
    {
        return Ok(_patientQueuesService.GetAllPatientsBySectorAndAppointmentHour(id, hour));
    }
    
    [HttpGet] 
    [Route("/patients/queues/call/{id}")]
    [Authorize]
    public IActionResult GetByHour(int id)
    {
        return Ok(_patientQueuesService.CallPatientOnQueueBySectorId(id));
    }
    [HttpDelete] 
    [Route("/patients/queues/confirm/{password}")]
    [Authorize(Roles = "ADMIN")]
    public IActionResult ConfirmPatientByPassword(string password)
    {
        _patientQueuesService.ConfirmServicePatient(password); 
        return NoContent();
    }
}