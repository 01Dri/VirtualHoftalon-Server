using System.Reflection;
using VirtualHoftalon_Server.Enums;
using VirtualHoftalon_Server.Exceptions;
using VirtualHoftalon_Server.Models;
using VirtualHoftalon_Server.Models.Dto.Appointment;
using VirtualHoftalon_Server.Models.Dto.Patient;
using VirtualHoftalon_Server.Pattern;
using VirtualHoftalon_Server.Repositories.Interfaces;
using VirtualHoftalon_Server.Services.Interfaces;
using VirtualHoftalon_Server.Utils;

namespace VirtualHoftalon_Server.Services;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;
    private readonly IPatientBuilder _patientBuilder;

    public PatientService(IPatientRepository patientRepository, IPatientBuilder patientBuilder)
    {
        _patientRepository = patientRepository;
        _patientBuilder = patientBuilder;
    }


    public PatientResponseDTO SavePatient(PatientRequestDTO patient)
    {

        Patient patientToSave = _patientBuilder
            .WithName(patient.Name)
            .WithClassification(ParseStringToEnum(patient.Classification))
            .WithCpf(patient.Cpf)
            .WithRg(patient.Rg)
            .WithPhone(patient.Phone)
            .WithDateBirth(patient.BirthDate)
            .WithEmail(patient.Email)
            .WithAppointments(new List<Appointment>())
            .Build();
        
        this._patientRepository.SavePatient(patientToSave);
        return toResponseDTO(patientToSave);
    }

    public IEnumerable<PatientResponseDTO> GetAll()
    {
        return this._patientRepository.GetPatients().Select(p => this.toResponseDTO(p)).ToList();

    }

    public PatientResponseDTO GetOneById(int id)
    {
        Patient patient = _patientRepository.GetPatientById(id) ?? throw new NotFoundPatientException("Not found Patient!");
        return toResponseDTO(patient);
    }

    public PatientResponseDTO GetByCPF(string cpf)
    {
        Patient patient = _patientRepository.GetPatientByCpf(cpf) ?? throw new NotFoundPatientException("Not found Patient!");
        return toResponseDTO(patient);
    }

    public PatientResponseDTO GetByEmail(string email)
    {
        Patient patient = _patientRepository.GetPatientByEmail(email) ?? throw new NotFoundPatientException("Not found Patient!");
        return toResponseDTO(patient);
    }

    public PatientResponseDTO UpdateById(int id, PatientUpdateRequestDTO patient)
    {
        Patient patientToUpdate = _patientRepository.GetPatientById(id) ?? throw new NotFoundPatientException("Not found Patient!");
        patientToUpdate = this.UpdatePatient(patient, patientToUpdate);
        patientToUpdate = _patientRepository.UpdatePatient(patientToUpdate);
        return this.toResponseDTO(patientToUpdate);
    }

    private Patient UpdatePatient(PatientUpdateRequestDTO dto, Patient entity)
    {

        ArgumentsValidation.CheckIfAllPropertiesIsNull(dto);

        PropertyInfo[] dtoProperties = dto.GetType().GetProperties();
        foreach (var dtoProperty in dtoProperties)
        {
            var dtoValue = dtoProperty.GetValue(dto);
            if (dtoValue != null)
            {
                PropertyInfo entityProperty = entity.GetType().GetProperty(dtoProperty.Name);
                if (entityProperty != null && entityProperty.CanWrite)
                {
                    entityProperty.SetValue(entity, dtoValue);
                }
            }
        }

        return entity;

    }


    public void DeleteById(int id)
    {
        Patient patientToRemove = _patientRepository.GetPatientById(id) ?? throw new NotFoundPatientException("Not found Patient!");
        this._patientRepository.DeletePatient(patientToRemove);
    }

    private ClassificationPatient ParseStringToEnum(string value)
    {
        return (ClassificationPatient)Enum.Parse(typeof(ClassificationPatient), value);
    }

    private PatientResponseDTO toResponseDTO(Patient patientToSave)
    {
        return new PatientResponseDTO(patientToSave.Id, patientToSave.Name,
            patientToSave.PhoneNumber, patientToSave.Cpf,
            patientToSave.Rg, patientToSave.Email,
            patientToSave.DateBirth, patientToSave.ClassificationPatient,
            patientToSave.Appointments.Select(a => this.ToAppointmentListDTO(a)).ToList());
    }

    private AppointmentResponseDTO ToAppointmentListDTO(Appointment a)
    {
        return new AppointmentResponseDTO(a.Id, a.Name, a.PatientId, a.DoctorId, a.doctor.Name,a.SectorId,a.Sector.Name,
            DateFormatParser.ToTimestamp(a.Day, a.Month, a.Year, a.Hour), a.Description);
    }
}