using QuestPDF.Fluent;
using VirtualHoftalon_Server.Models;
using VirtualHoftalon_Server.Models.Dto.Appointment;

namespace VirtualHoftalon_Server.Services.Interfaces;

public interface IPDFGenerator
{
    Document GenerateDocument();
    bool SaveDocument(Document document);
    void SetAppointment(Appointment appointment);

    string GetNamePDF();
}