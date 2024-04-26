using VirtualHoftalon_Server.Enums;
using VirtualHoftalon_Server.Models;

namespace VirtualHoftalon_Server.Tests.MocksUtils;

public class MockEntities
{

    public static Sector GetSectorMock()
    {
        return new Sector(1, "diego", 10, 1, new List<Appointment>(), SectorTag.P);
    }

    public static Doctor GetDoctorMock()
    {
        return new Doctor(1, "Doutorzao");
    }

    public static Patient GetPatientMock()
    {
        return new Patient(1, "PatientTest", "1231231231231", "528.272.490-14", "33.959.432-9", "diego@gmail.com",
            new DateTime(), ClassificationPatient.Patient, new List<Appointment>());
    }

    public static Appointment GetAppointmentMock()
    {
        return new Appointment
        {
            Id = 1,
            Name = "Mock Appointment",
            PatientId = 1,
            patient = new Patient { Id = 1, Name = "John Doe" },
            DoctorId = 2,
            doctor = new Doctor { Id = 2, Name = "Dr. Smith" },
            SectorId = 3,
            Sector = new Sector { Id = 3, Name = "Cardiology", Tag = SectorTag.P},
            Day = 25,
            Month = 4,
            Year = 2024,
            Hour = "10:00 AM",
            Description = "Mock appointment description"
            
        };

    }
    
    
}