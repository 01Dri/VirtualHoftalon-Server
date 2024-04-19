using QuestPDF.Fluent;
using QuestPDF.Helpers;
using VirtualHoftalon_Server.Exceptions;
using VirtualHoftalon_Server.Models;
using VirtualHoftalon_Server.Repositories.Interfaces;
using VirtualHoftalon_Server.Services.Interfaces;

namespace VirtualHoftalon_Server.Services
{
    public class PDFGeneratorService : IPDFGenerator
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public PDFGeneratorService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public void AppointmentGeneratePDFById(int appointmentId)
        {
            Appointment appointment = _appointmentRepository.GetAppointmentById(appointmentId) ??
                                      throw new NotFoundAppointmentException("Appointment not found");
            Patient patient = appointment.patient;
            Sector sector = appointment.Sector;
            Doctor doctor = appointment.doctor;

            Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Margin(50);
                    page.Size(PageSizes.A4);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(16));

                    page.Header()
                        .AlignCenter()
                        .Text("Centro de Estudos e Pesquisas de Visão Hoftalon")
                        
                        .SemiBold().FontSize(24).FontColor(Colors.Grey.Darken4);
                    
                    page.Content().Column(column =>
                    {
                        column.Item().Text("─────────────────────────────────────────────");
                        column.Item().Text("                      Paciente:").Bold();
                        column.Item().Text($"Código: {patient.Id}");
                        column.Item().Text($"Nome: {patient.Name}");
                        column.Item().Text($"Data de Nascimento: {patient.DateBirth}");
                        column.Item().Text($"Telefone: {patient.PhoneNumber}");
                        column.Item().Text($"Email: {patient.Email}");
                        column.Item().Text("─────────────────────────────────────────────");
                        column.Item().Text("                      Agenda:").Bold();
                        column.Item().Text($"Código: {appointment.Id}");
                        column.Item().Text($"Agendado para: {appointment.Day}/{appointment.Month}/{appointment.Year}");
                        column.Item().Text($"Médico: {doctor.Name}");
                        column.Item().Text($"Setor: {sector.Name}");
                        column.Item().Text($"Itens Agendados: {appointment.Name}");
                        column.Item().Text($"Horário: {appointment.Hour}");
                        column.Item().Text("─────────────────────────────────────────────");
                        column.Item().Text($"Observações: \n");
                        column.Item().Text($"{appointment.Description}");


                    });
                });
            }).GeneratePdf($"PDF/appointment-{appointment.Id}-{appointment.patient.Name}.pdf");
        }
    }
}
