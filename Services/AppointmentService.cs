using System.Globalization;
using VirtualHoftalon_Server.Exceptions;
using VirtualHoftalon_Server.Models;
using VirtualHoftalon_Server.Models.Dto.Appointment;
using VirtualHoftalon_Server.Pattern;
using VirtualHoftalon_Server.Repositories.Interfaces;
using VirtualHoftalon_Server.Services.Interfaces;
using VirtualHoftalon_Server.Utils;

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
            .Select(a => this.ToResponseDTO(a))
            .ToList();
    }

    public AppointmentResponseDTO SaveAppointment(AppointmentRequestDTO appointmentRequestDto)
    {
        this.ValidateInputsDate(appointmentRequestDto.DateFormat.Day, appointmentRequestDto.DateFormat.Month, appointmentRequestDto.DateFormat.Year);
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
            .WithDay(appointmentRequestDto.DateFormat.Day)
            .WithMonth(appointmentRequestDto.DateFormat.Month)
            .WithYear(appointmentRequestDto.DateFormat.Year)
            .WithHour(appointmentRequestDto.DateFormat.Hour)
            .WithDescription(appointmentRequestDto.Description)
            .Build();
        appointmentToSave = this._appointmentRepository.SaveAppointment(appointmentToSave);

            return this.ToResponseDTO(appointmentToSave);

    }


    public AppointmentResponseDTO GetOneById(int id)
    {
        Appointment appointment = _appointmentRepository.GetAppointmentById(id) ?? throw new NotFoundAppointmentException("Not found Appointment!");
        return this.ToResponseDTO(appointment);
    }

    public AppointmentResponseDTO UpdateAppointmentById(int id, AppointmentUpdateRequestDTO dto)
    {
        Appointment appointment = _appointmentRepository.GetAppointmentById(id) ??
                                  throw new NotFoundAppointmentException("Not found Appointment");
        ArgumentsValidation.CheckIfAllPropertiesIsNull(dto);
        this.ValidateInputsDate(dto.Day, dto.Month, dto.Year);
        this.updatePropertiesAppointment(appointment, dto);
        _appointmentRepository.UpdateAppointment(appointment);
        return ToResponseDTO(appointment);

    }
    

    public bool DeleteAppointmentById(int id)
    {
        Appointment appointment = _appointmentRepository.GetAppointmentById(id) ??
                                  throw new NotFoundAppointmentException("Not found Appointment");
        _appointmentRepository.Delete(appointment);
        return true;
    }

    private void updatePropertiesAppointment(Appointment appointment, AppointmentUpdateRequestDTO dto)
    {
        appointment.Day = dto.Day ?? appointment.Day;
        appointment.Day = dto.Day ?? appointment.Day;
        appointment.Month = dto.Month ?? appointment.Month;
        appointment.Year = dto.Year ?? appointment.Year;
        appointment.Hour = dto.Hour ?? appointment.Hour;
        if (dto.SectorId != null)
        {
            appointment.Sector = _sectorRepository.GetSectorById(dto.SectorId) ??
                                 throw new NotFoundSectorException("Not found Sector!");
            appointment.SectorId = dto.SectorId;
        }

    }

    private DateTime ToTimestamp(int? Day, int? Month, int? Year, string? Hour)
    {
        string dateStr =
        $"{Day.Value.ToString("D2")}/{Month.Value.ToString("D2")}/{Year} {Hour}";
        return DateTime.ParseExact(dateStr, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
    }
    private void ValidateInputsDate(int? Day, int? Month, int? Year)
    {
        if (Month > 12 || Month <= 0)
        {
            throw new InvalidDateAppointmentException($"Informe um mês válido!");
            
        }

        if (Year < DateTime.Now.Year)
        {
            throw new InvalidDateAppointmentException($"Informe um ano válido!");
            
        }
        if (Day > 31)
        {
            throw new InvalidDateAppointmentException($"Informe um dia válido!");
        }

        int?[] monthsWith31Days = [1, 3, 5, 7, 8, 10, 12];
        if (Day == 31 && !monthsWith31Days.Contains(Month))
        {
            throw new InvalidDateAppointmentException($"O mês {Month} não tem 31 dias!");
        }
    }

    private AppointmentResponseDTO ToResponseDTO(Appointment appointment)
    {
        
        return new AppointmentResponseDTO(appointment.Id, appointment.Name,
            appointment.PatientId, appointment.DoctorId,
            appointment.doctor.Name,appointment.SectorId,
            appointment.Sector.Name, this.ToTimestamp(appointment.Day,
                appointment.Month, appointment.Year,
                appointment.Hour), appointment.Description);
    }
    
}
    
