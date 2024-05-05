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
         if (!patientByPatientQueue.Appointments.Contains(appointmentByPatientQueue))
         {
             throw new InvalidAppointmentIdException(
                 $"Appointment id: {patient.AppointmentId} does not belong this user");
         }
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
         return ToResponseDTO(patientsQueue);
    }

    public IEnumerable<PatientQueuesResponseDTO> GetAll()
    {
        return _patientsQueuesRepository.GetPatientsQueues()
            .Select(ap => ToResponseDTO(ap))
            .ToList();
    }

    public PatientQueuesResponseDTO GetOneById(int id)
    {
        return ToResponseDTO(_patientsQueuesRepository.GetPatientsQueueById(id) ?? 
                             throw new NotFoundLastPositionPatientQueue("Not found queue"));
    }

    public PatientQueuesResponseDTO UpdateById(int id, PatientQueuesUpdateDTO patient)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<PatientQueuesResponseDTO> GetAllPatientsBySectorAndAppointmentHour(int sectorId, string hour)
    {
        return _patientsQueuesRepository.GetAllPatientsBySectorAndAppointmentHour(sectorId, hour)
            .Select(ap => ToResponseDTO(ap)).ToList();
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

    public PatientQueuesResponseDTO CallPatientOnQueueBySectorId(int sectorId)
    {
        PatientsQueue patientsQueue = _patientsQueuesRepository.CallPatientOnQueueBySectorId(sectorId) ?? throw new NotFoundSectorException("Not found Sector!");
        PatientQueuesResponseDTO  result = ToResponseDTO(_patientsQueuesRepository.CallPatientOnQueueBySectorId(sectorId));
        //_patientsQueuesRepository.DeletePatientsQueue(patientsQueue);
        return result;
    }

    public bool ConfirmServicePatient(string password)
    {
        PatientsQueue patientsQueue = _patientsQueuesRepository.GetPatientQueueByPassword(password);
        return _patientsQueuesRepository.DeletePatientsQueue(patientsQueue);
    }

    private string GeneratePasswordToPatient(int position, string tag)
    {
        return $"{tag}{position}";
    }
    private PatientQueuesResponseDTO ToResponseDTO(PatientsQueue patientsQueue)
    {
        return new PatientQueuesResponseDTO(patientsQueue.Id, patientsQueue.PatientId,
            patientsQueue.Position, patientsQueue.Appointment.Name,
            patientsQueue.Appointment.Hour,
            patientsQueue.Appointment.SectorId,
            patientsQueue.Patient.Name, patientsQueue.Password,
            patientsQueue.IsPreferred);
    }

}