using VirtualHoftalon_Server.Enums;
using VirtualHoftalon_Server.Models;
using VirtualHoftalon_Server.Models.Dto.Patient;
using VirtualHoftalon_Server.Pattern;
using VirtualHoftalon_Server.Repositories.Interfaces;
using VirtualHoftalon_Server.Services.Interfaces;

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
        Console.WriteLine(patient.ToString());

        Patient patientToSave = _patientBuilder
            .WithId(null)
            .WithName(patient.Name)
            .WithClassification(ParseStringToEnum(patient.Classification))
            .WithCpf(patient.Cpf)
            .WithRg(patient.Rg)
            .WithPhone(patient.Phone)
            .WithDateBirth(patient.BirthDate)
            .WithEmail(patient.Email)
            .Build();
        
        this._patientRepository.SavePatient(patientToSave);
        return toResponseDTO(patientToSave);
    }

    public IEnumerable<PatientResponseDTO> GetAll()
    {
        throw new NotImplementedException();
    }

    public PatientResponseDTO GetOneById(int id)
    {
        throw new NotImplementedException();
    }

    public PatientResponseDTO UpdateById(int id, PatientRequestDTO patient)
    {
        throw new NotImplementedException();
    }

    public void DeleteById(int id)
    {
        throw new NotImplementedException();
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
            patientToSave.DateBirth, patientToSave.ClassificationPatient);
    }
}