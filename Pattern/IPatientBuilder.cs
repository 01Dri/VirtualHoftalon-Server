using VirtualHoftalon_Server.Enums;
using VirtualHoftalon_Server.Models;

namespace VirtualHoftalon_Server.Pattern;

public interface IPatientBuilder
{
    IPatientBuilder WithId(int? id);
    IPatientBuilder WithName(string name);
    IPatientBuilder WithPhone(string phone);
    IPatientBuilder WithCpf(string cpf);
    IPatientBuilder WithRg(string rg);
    IPatientBuilder WithEmail(string email);
    IPatientBuilder WithDateBirth(DateTime date);

    IPatientBuilder WithClassification(ClassificationPatient classification);

    IPatientBuilder WithAppointments(List<Appointment> appointments);
    Patient Build();


}