using Microsoft.AspNetCore.Mvc;
using VirtualHoftalon_Server.Services.Interfaces;

namespace VirtualHoftalon_Server.Controller.PDF;

public class AppointmentPDFController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly IPDFGenerator _pdfGenerator;

    public AppointmentPDFController(IPDFGenerator pdfGenerator)
    {
        _pdfGenerator = pdfGenerator;
    }
    
    [HttpGet]
    [Route("/appointments/pdf/{id}")]
    public IActionResult GeneratePDFAppointmentById(int id)
    {
        _pdfGenerator.AppointmentGeneratePDFById(id);
        return Ok();
    }
}