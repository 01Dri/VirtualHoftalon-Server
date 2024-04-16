using Microsoft.AspNetCore.Mvc;
using VirtualHoftalon_Server.Models.Dto.Patient;
using VirtualHoftalon_Server.Services.Interfaces;

namespace VirtualHoftalon_Server.Controller;

public class PatientController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly IPatientService _patientService;

    public PatientController(IPatientService patientService)
    {
        _patientService = patientService;
    }


    [HttpPost]
    [Route("/patients")]
    public IActionResult SavePatient([FromBody] PatientRequestDTO patientRequestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(this._patientService.SavePatient(patientRequestDto));

    }
}