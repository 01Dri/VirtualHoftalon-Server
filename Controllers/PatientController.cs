using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "ADMIN")]
    public IActionResult SavePatient([FromBody] PatientRequestDTO patientRequestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return Ok(this._patientService.SavePatient(patientRequestDto));

    }
    
    [HttpGet]
    [Route("/patients")]
    [Authorize]

    public IActionResult GetAllPatients()
    {
        return Ok(this._patientService.GetAll());
    }
    
    [HttpGet]
    [Route("/patients/{id}")]
    [Authorize]
    public IActionResult GetPatientById(int id)
    {
        return Ok(this._patientService.GetOneById(id));
    }
    
    [HttpGet]
    [Route("/patients/cpf/{cpf}")]
    [Authorize]
    public IActionResult GetPatientByCpf(string cpf)
    {
        return Ok(this._patientService.GetByCPF(cpf));
    }
    
    [HttpPatch]
    [Route("/patients/{id}")]
    [Authorize(Roles = "ADMIN")]
    public IActionResult UpdatePatientById(int id, [FromBody] PatientUpdateRequestDTO patientUpdateRequestDto)
    {
        return Ok(this._patientService.UpdateById(id, patientUpdateRequestDto));
    }
    
    [HttpDelete]
    [Route("/patients/{id}")]
    [Authorize(Roles = "ADMIN")]
    public IActionResult DeleteById(int id)
    {
        this._patientService.DeleteById(id);
        return NoContent();
    }

}