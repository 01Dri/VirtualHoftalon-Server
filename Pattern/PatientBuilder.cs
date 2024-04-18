using VirtualHoftalon_Server.Enums;
using VirtualHoftalon_Server.Models;

namespace VirtualHoftalon_Server.Pattern;

public class PatientBuilder : IPatientBuilder
{
    private int? Id;
    private string Name;
    private string Phone;
    private string Cpf;
    private string Rg;
    private string Email;
    private DateTime DateBirth;
    private ClassificationPatient ClassificationPatient;
    private List<Appointment> Appointments = new List<Appointment>();
    
    public IPatientBuilder WithId(int? id)
    {
        this.Id = id;
        return this;
    }

    public IPatientBuilder WithName(string name)
    {
        this.Name = name;
        return this;
    }

    public IPatientBuilder WithPhone(string phone)
    {
        this.Phone = phone;
        return this;
    }

    public IPatientBuilder WithCpf(string cpf)
    {
        this.Cpf = cpf;
        return this;
    }

    public IPatientBuilder WithRg(string rg)
    {
        this.Rg = rg;
        return this;
    }
    
    public IPatientBuilder WithDateBirth(DateTime date)
    {
        this.DateBirth = date;
        return this;
    }
    
    

    public IPatientBuilder WithEmail(string email)
    {
        this.Email = email;
        return this;
    }

    public IPatientBuilder WithClassification(ClassificationPatient classification)
    {
        this.ClassificationPatient = classification;
        return this;
    }
    
    public IPatientBuilder WithAppointments(List<Appointment> appointments)
    {
        this.Appointments = appointments;
        return this;
    }

    public Patient Build()
    {
        return new Patient(this.Id, this.Name, this.Phone, this.Cpf, this.Rg, this.Email, this.DateBirth,
            this.ClassificationPatient, this.Appointments);
    }
}