using VirtualHoftalon_Server.Enums;
using VirtualHoftalon_Server.Exceptions;
using VirtualHoftalon_Server.Models;
using VirtualHoftalon_Server.Models.Dto.PatientQueus;
using VirtualHoftalon_Server.Repositories.Interfaces;
using VirtualHoftalon_Server.Services.Interfaces;

namespace VirtualHoftalon_Server.Services;

public class PatientQueuesService : IPatientQueuesService
{
    private readonly IPatientsQueuesRepository _patientsQueuesRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IAppointmentRepository _appointmentRepository;

    public PatientQueuesService(IPatientsQueuesRepository patientsQueuesRepository, IPatientRepository patientRepository, IAppointmentRepository appointmentRepository)
    {
        _patientsQueuesRepository = patientsQueuesRepository;
        _patientRepository = patientRepository;
        _appointmentRepository = appointmentRepository;
    }


    public PatientQueuesResponseDTO SavePatient(PatientQueuesRequestDTO patient)
    {
         Patient patientByPatientQueue = _patientRepository.GetPatientById(patient.PatientId) ?? throw new NotFoundPatientException("Not found Patient!");
         Appointment appointmentByPatientQueue = _appointmentRepository.GetAppointmentById(patient.AppointmentId) ?? throw new NotFoundAppointmentException("Not found Appointment");
         int position = GetPosition(appointmentByPatientQueue.Hour, appointmentByPatientQueue.SectorId);
         SectorTag tag = appointmentByPatientQueue.Sector.Tag;
         string password = GeneratePasswordToPatient(position, tag.ToString());
         PatientsQueue patientsQueue = new PatientsQueue()
         {
             Id = null,
             PatientId = patient.PatientId,
             Patient = patientByPatientQueue,
             AppointmentId = patient.AppointmentId,
             Appointment = appointmentByPatientQueue,
             IsPreferred = patientByPatientQueue.ClassificationPatient != ClassificationPatient.Patient,
             Position = position, 
             Password =  password
         };
         _patientsQueuesRepository.SavePatientsQueue(patientsQueue);
         return ToResponseDTO(patientsQueue, position, password);
    }

    public IEnumerable<PatientQueuesResponseDTO> GetAll()
    {
        throw new NotImplementedException();
    }

    public PatientQueuesResponseDTO GetOneById(int id)
    {
        throw new NotImplementedException();
    }

    public PatientQueuesResponseDTO UpdateById(int id, PatientQueuesUpdateDTO patient)
    {
        throw new NotImplementedException();
    }

    public List<Patient> GetAllPatientsByAppointmentHour(string hour)
    {
        throw new NotImplementedException();
    }

    public int GetPosition(string hour, int? sectorId)
    {
        return _patientsQueuesRepository.GetLastPositionBySectorAndHour(hour, sectorId) + 1 
            ?? throw new NotFoundLastPositionPatientQueue("Not found last position");
    }

    public void DeleteById(int id)
    {
        throw new NotImplementedException();
    }

    private string GeneratePasswordToPatient(int position, string tag)
    {
        return $"{tag}{position}";
    }
    private PatientQueuesResponseDTO ToResponseDTO(PatientsQueue patientsQueue, int position, string password)
    {
        return new PatientQueuesResponseDTO(patientsQueue.Id, patientsQueue.PatientId,
            position, password);
    }

}