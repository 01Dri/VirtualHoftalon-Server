using VirtualHoftalon_Server.Exceptions;
using VirtualHoftalon_Server.Models;
using VirtualHoftalon_Server.Models.Dto;
using VirtualHoftalon_Server.Models.Dto.Appointment;
using VirtualHoftalon_Server.Repositories.Interfaces;
using VirtualHoftalon_Server.Services.Interfaces;
using VirtualHoftalon_Server.Utils;

namespace VirtualHoftalon_Server.Services;

public class DoctorService : IDoctorService {

    private readonly IDoctorRepository _doctorRepository;
    private readonly ILogger<DoctorService> _logger;

    public DoctorService(IDoctorRepository doctorRepository, ILogger<DoctorService> logger)
    {
        _doctorRepository = doctorRepository;
        _logger = logger;
    }


    public List<DoctorResponseDTO> GetAll()
    {
        return _doctorRepository.GetDoctors()
            .Select(doctor => new DoctorResponseDTO(doctor.Id, doctor.Cpf,doctor.Name, doctor.Appointments.Select
                (a => new AppointmentResponseDTO(a.Id, a.Name,a.PatientId, a.DoctorId,doctor.Name, a.SectorId,
                    a.Sector.Name,
                    DateFormatParser.ToTimestamp(a.Day, a.Month, a.Year, a.Hour), a.Description)).ToList()))
            .ToList();
    }

    public DoctorResponseDTO SaveDoctor(DoctorRequestDTO doctorRequestDto)
    {
        Doctor doctor = new Doctor(null, doctorRequestDto.Name);
        doctor.Appointments = new List<Appointment>();
        doctor = _doctorRepository.SaveDoctor(doctor);
        return ToResponseDTO(doctor);

    }

    public DoctorResponseDTO GetOneById(int id)
    {
        Doctor doctorById = _doctorRepository.GetDoctorById(id) ?? throw new NotFoundDoctorException("Not found Doctor!");
        return ToResponseDTO(doctorById);

    }

    public DoctorResponseDTO GetByUsernameLogin(string username)
    {
        Doctor doctor =
            _doctorRepository.GetDoctorByUsernameLogin(username) ?? throw new NotFoundDoctorException("Not found Doctor");
        return ToResponseDTO(doctor);

    }


    public DoctorResponseDTO UpdateDoctorById(int id, DoctorRequestDTO doctorRequestDto)
    {

        Doctor doctorToUpdate =
            _doctorRepository.GetDoctorById(id) ?? throw new NotFoundDoctorException("Not found Doctor");
        doctorToUpdate.Name = doctorRequestDto.Name;
        doctorToUpdate = _doctorRepository.UpdateDoctor(doctorToUpdate);
        return ToResponseDTO(doctorToUpdate);
    }

    private DoctorResponseDTO ToResponseDTO(Doctor doctor)
    {
        return new DoctorResponseDTO(doctor.Id, doctor.Cpf,doctor.Name,
            doctor.Appointments
                .Select(a => new AppointmentResponseDTO(a.Id, a.Name, a.PatientId, a.DoctorId ,
                    doctor.Name,a.SectorId,
                    a.Sector.Name
                    ,
                    DateFormatParser.ToTimestamp(a.Day, a.Month, a.Year, a.Hour), a.Description)).ToList());
    }

    public bool DeleteDoctorById(int id)
        {
            Doctor doctor = _doctorRepository.GetDoctorById(id) ??
                            throw new NotFoundDoctorException("Not found Doctor!");
            return _doctorRepository.DeleteDoctor(doctor);
        }
    
    }
