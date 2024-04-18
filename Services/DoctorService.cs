using Microsoft.AspNetCore.Components.Sections;
using VirtualHoftalon_Server.Exceptions;
using VirtualHoftalon_Server.Models;
using VirtualHoftalon_Server.Models.Dto;
using VirtualHoftalon_Server.Models.Dto.Appointment;
using VirtualHoftalon_Server.Repositories.Interfaces;
using VirtualHoftalon_Server.Services.Interfaces;

namespace VirtualHoftalon_Server.Services;

public class DoctorService : IDoctorService {

    private readonly IDoctorRepository _doctorRepository;
    private readonly ISectorRepository _sectorRepository;
    private readonly ILogger<DoctorService> _logger;

    public DoctorService(IDoctorRepository doctorRepository, ISectorRepository sectorRepository, ILogger<DoctorService> logger)
    {
        _doctorRepository = doctorRepository;
        _sectorRepository = sectorRepository;
        _logger = logger;
    }


    public List<DoctorResponseDTO> GetAll()
    {
        return _doctorRepository.GetDoctors()
            .Select(doctor => new DoctorResponseDTO(doctor.Id, doctor.Name, doctor.Appointments.Select
                (a => new AppointmentListDTO(a.Id, a.PatientId, a.DoctorId, a.SectorId, a.Timestamp)).ToList()))
            .ToList();
    }

    public DoctorResponseDTO SaveDoctor(DoctorRequestDTO doctorRequestDto)
    {
        Doctor doctor = new Doctor(null, doctorRequestDto.Name);
        doctor.Appointments = new List<Appointment>();
        doctor = _doctorRepository.SaveDoctor(doctor);
        return new DoctorResponseDTO(doctor.Id, doctor.Name,  doctor.Appointments.Select(a => new AppointmentListDTO(a.Id, a.PatientId, a.DoctorId, a.SectorId, a.Timestamp)).ToList());
    }

    public DoctorResponseDTO GetOneById(int id)
    {
        Doctor doctorById = _doctorRepository.GetDoctorById(id) ?? throw new NotFoundDoctorException("Not found Doctor!");
        Console.WriteLine(doctorById.Appointments);
        return new DoctorResponseDTO(doctorById.Id, doctorById.Name,
            doctorById.Appointments
                .Select(a => new AppointmentListDTO(a.Id, a.PatientId, a.DoctorId, a.SectorId, a.Timestamp)).ToList());

    }


    public DoctorResponseDTO UpdateDoctorById(int id, DoctorRequestDTO doctorRequestDto)
    {
        
        Doctor doctorToUpdate = _doctorRepository.GetDoctorById(id) ?? throw new NotFoundDoctorException("Not found Doctor");
        doctorToUpdate.Name = doctorRequestDto.Name;
        doctorToUpdate = _doctorRepository.UpdateDoctor(doctorToUpdate);
        return new DoctorResponseDTO(doctorToUpdate.Id, doctorToUpdate.Name,
            doctorToUpdate.Appointments
                .Select(a => new AppointmentListDTO(a.Id, a.PatientId, a.DoctorId, a.SectorId, a.Timestamp)).ToList());
    }

    public bool DeleteDoctorById(int id)
    {
        Doctor doctor = _doctorRepository.GetDoctorById(id) ?? throw new NotFoundDoctorException("Not found Doctor!");
        return _doctorRepository.DeleteDoctor(doctor);
    }
    
}