using System.Globalization;
using VirtualHoftalon_Server.Exceptions;
using VirtualHoftalon_Server.Models;
using VirtualHoftalon_Server.Models.Dto.Appointment;
using VirtualHoftalon_Server.Pattern;
using VirtualHoftalon_Server.Repositories.Interfaces;
using VirtualHoftalon_Server.Services.Interfaces;

namespace VirtualHoftalon_Server.Services;

public class AppointmentService : IAppointmentService
{

    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IAppointmentBuilder _appointmentBuilder;
    private readonly IDoctorRepository _doctorRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly ISectorRepository _sectorRepository;

    public AppointmentService(IAppointmentRepository appointmentRepository, IAppointmentBuilder appointmentBuilder,
        IDoctorRepository doctorRepository, IPatientRepository patientRepository, ISectorRepository sectorRepository)
    {
        _appointmentRepository = appointmentRepository;
        _appointmentBuilder = appointmentBuilder;
        _doctorRepository = doctorRepository;
        _patientRepository = patientRepository;
        _sectorRepository = sectorRepository;
    }


    public List<AppointmentResponseDTO> GetAll()
    {
        var appointments = _appointmentRepository.GetAll();
        return appointments
            .Select(a => new AppointmentResponseDTO(a.Name,a.Id, a.PatientId, a.DoctorId, a.SectorId, a.Timestamp))
            .ToList();
    }

    public AppointmentResponseDTO SaveAppointment(AppointmentRequestDTO appointmentRequestDto)
    {
        this.ValidateInputsDate(appointmentRequestDto.DateFormat);
        Sector sector = this._sectorRepository.GetSectorById(appointmentRequestDto.SectorId) ??
                        throw new NotFoundSectorException("Not found Sector");
        Doctor doctor = this._doctorRepository.GetDoctorById(sector.DoctorId) ??
                        throw new NotFoundDoctorException("Not found Doctor");
        Patient patient = this._patientRepository.GetPatientById(appointmentRequestDto.PatientId) ??
                          throw new NotFoundPatientException("Not found Patient");

        Appointment appointmentToSave = this._appointmentBuilder
            .WithName(appointmentRequestDto.Name)
            .WithDoctor(doctor)
            .WithSector(sector)
            .WithPatient(patient)
            .WithTimestamp(this.ToTimestamp(appointmentRequestDto.DateFormat))
            .Build();
        appointmentToSave = this._appointmentRepository.SaveAppointment(appointmentToSave);

        return new AppointmentResponseDTO(appointmentToSave.Name,appointmentToSave.Id, appointmentRequestDto.PatientId,
            appointmentToSave.DoctorId, appointmentRequestDto.SectorId, appointmentToSave.Timestamp);
    }


    public AppointmentResponseDTO GetOneById(int id)
    {
        throw new NotImplementedException();
    }

    public AppointmentResponseDTO UpdateAppointmentById(int id, AppointmentRequestDTO Appointment)
    {
        throw new NotImplementedException();
    }

    public bool DeleteAppointmentById(int id)
    {
        throw new NotImplementedException();
    }

    private DateTime ToTimestamp(AppointmentDateFormatDTO date)
    {
        string dateStr =
        $"{date.Day.Value.ToString("D2")}/{date.Month.Value.ToString("D2")}/{date.Year} {date.Hour}";
        return DateTime.ParseExact(dateStr, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
    }
    private void ValidateInputsDate(AppointmentDateFormatDTO date)
    {
        if (date.Month > 12 || date.Month <= 0)
        {
            throw new InvalidDateAppointmentException($"Informe um mês válido!");
            
        }

        if (date.Year < DateTime.Now.Year)
        {
            throw new InvalidDateAppointmentException($"Informe um ano válido!");
            
        }
        if (date.Day > 31)
        {
            throw new InvalidDateAppointmentException($"Informe um dia válido!");
        }

        int?[] monthsWith31Days = [1, 3, 5, 7, 8, 10, 12];
        if (date.Day == 31 && !monthsWith31Days.Contains(date.Month))
        {
            throw new InvalidDateAppointmentException($"O mês {date.Month} não tem 31 dias!");
        }
    }
}
    
