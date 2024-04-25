using QuestPDF;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using VirtualHoftalon_Server.Exceptions;
using VirtualHoftalon_Server.Models;
using VirtualHoftalon_Server.Services.Interfaces;

namespace VirtualHoftalon_Server.Services
{
    public class PDFGeneratorService : IPDFGenerator
    {
        public Appointment Appointment { get; set; }
        public string NamePDF { get; set; }
        
        private string PathPDFs = "C:\\Users\\didvg\\Desktop\\PROJETOS\\VirtualHoftalon-Server\\PDFs\\"; 

        public PDFGeneratorService()
        {
            Settings.License = LicenseType.Community;
        }


        public Document GenerateDocument()
        {
            try
            {
                 Document document= Document.Create(container =>
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
                                column.Item().Text($"Código: {this.Appointment.PatientId}");
                                column.Item().Text($"Nome: {this.Appointment.patient.Name}");
                                column.Item().Text($"Data de Nascimento: {this.Appointment.patient.DateBirth.ToString("dd/MM/yyyy")}");
                                column.Item().Text($"Telefone: {this.Appointment.patient.PhoneNumber}");
                                column.Item().Text($"Email: {this.Appointment.patient.Email}");

                                column.Item().Text("─────────────────────────────────────────────");

                                column.Item().Text("Informações da Consulta:").Bold().FontSize(14);
                                column.Item().Text($"Código da Consulta: {this.Appointment.Id}");
                                column.Item()
                                    .Text(
                                        $"Data: {this.Appointment.Day}/{this.Appointment.Month.Value.ToString("D2")}/{this.Appointment.Year}");
                                column.Item().Text($"Horário: {this.Appointment.Hour}");
                                column.Item().Text($"Médico: {this.Appointment.doctor.Name}");
                                column.Item().Text($"Setor: {this.Appointment.Sector.Name}");
                                column.Item().Text($"Procedimento: {this.Appointment.Name}");

                                column.Item().Text("─────────────────────────────────────────────");

                                column.Item().Text("Observações:").Bold().FontSize(14);
                                column.Item().Text($"{this.Appointment.Description}");
                            });

                        page.Footer()
                            .AlignCenter()
                            .Text($"Emitido em: {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}")
                            .FontSize(10);
                    });
                });
                return document;
                
            } catch (Exception e) {
                throw new ErrorToGeneratePDFException($"Error to generate PDF: {e.Message}");
            }
        }

        public bool SaveDocument(Document document)
        {
            try
            {
                string name = $"{this.Appointment.Id} - {this.Appointment.patient.Name} - {Appointment.Name}.pdf";
                this.NamePDF = name;
                document.GeneratePdf(this.PathPDFs + name);
                return true;
            }
            catch (Exception e)
            {
                throw new FailedToSavePDFException($"ERRO: {e.Message}");
            }

        }

        public void SetAppointment(Appointment appointment)
        {
            if (appointment == null)
            {
                throw new FailedToSetAppointmentOnPDFGeneratorException("Consulta não pode ser nula!");
            }
            this.Appointment = appointment;
        }

        public string GetNamePDF()
        {
            return NamePDF;
        }
    }
}
