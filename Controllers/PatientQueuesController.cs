using Microsoft.AspNetCore.Mvc;
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
    public IActionResult SavePatientOnQueue([FromBody] PatientQueuesRequestDTO patientQueuesRequestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(_patientQueuesService.SavePatient(patientQueuesRequestDto));
    }
    
}