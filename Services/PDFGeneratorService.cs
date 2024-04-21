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
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header()
                        .AlignCenter()
                        .Text("Hoftalon Centro de EST E PSQ da visão \n")
                        .SemiBold().FontSize(20).FontColor(Colors.Black);

                    page.Content()
                        .Column(column =>
                        {
                            column.Spacing(5);

                            column.Item().Text("Comprovante de Agendamento").Bold().FontSize(16);
                            column.Item().Text("─────────────────────────────────────────────");

                            column.Item().Text("Dados do Paciente:").Bold().FontSize(14);
                            column.Item().Text($"Código: {patient.Id}");
                            column.Item().Text($"Nome: {patient.Name}");
                            column.Item().Text($"Data de Nascimento: {patient.DateBirth.ToString("dd/MM/yyyy")}");
                            column.Item().Text($"Telefone: {patient.PhoneNumber}");
                            column.Item().Text($"Email: {patient.Email}");

                            column.Item().Text("─────────────────────────────────────────────");

                            column.Item().Text("Informações da Consulta:").Bold().FontSize(14);
                            column.Item().Text($"Código da Consulta: {appointment.Id}");
                            column.Item().Text($"Data: {appointment.Day}/{appointment.Month.Value.ToString("D2")}/{appointment.Year}");
                            column.Item().Text($"Horário: {appointment.Hour}");
                            column.Item().Text($"Médico: {doctor.Name}");
                            column.Item().Text($"Setor: {sector.Name}");
                            column.Item().Text($"Procedimento: {appointment.Name}");

                            column.Item().Text("─────────────────────────────────────────────");

                            column.Item().Text("Observações:").Bold().FontSize(14);
                            column.Item().Text($"{appointment.Description}");
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text($"Emitido em: {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}")
                        .FontSize(10);
                });
            }).GeneratePdf($"PDFs/consulta-{appointment.Id}-{patient.Name}.pdf");
        }
    }
}
