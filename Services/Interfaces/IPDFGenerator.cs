using VirtualHoftalon_Server.Models;

namespace VirtualHoftalon_Server.Services.Interfaces;

public interface IPDFGenerator
{
    void AppointmentGeneratePDFById(int appointmentId);
}