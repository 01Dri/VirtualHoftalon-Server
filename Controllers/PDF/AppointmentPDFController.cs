using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using VirtualHoftalon_Server.Models;
using VirtualHoftalon_Server.Repositories.Interfaces;
using VirtualHoftalon_Server.Services.Interfaces;

namespace VirtualHoftalon_Server.Controller.PDF;

public class AppointmentPDFController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly IPDFGenerator _pdfGenerator;
    private readonly IAppointmentRepository _appointmentRepository;

    public AppointmentPDFController(IPDFGenerator pdfGenerator, IAppointmentRepository appointmentRepository)
    {
        _pdfGenerator = pdfGenerator;
        _appointmentRepository = appointmentRepository;
    }


    [HttpGet]
    [Route("/appointments/pdf/{id}")]
    public IActionResult GeneratePDFAppointmentById(int id)
    {
        Appointment? appointment = _appointmentRepository.GetAppointmentById(id);
        _pdfGenerator.SetAppointment(appointment);
         Document document = _pdfGenerator.GenerateDocument();
         _pdfGenerator.SaveDocument(document);
        return Ok();
    }
}