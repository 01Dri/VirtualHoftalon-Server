namespace VirtualHoftalon_Server.Models;

public class Appointment
{
    public int? Id { get; set; }
    public string Name { get; set;}
    public int? PatientId { get; set; }
    public virtual Patient? patient { get; set; }
    public int? DoctorId { get; set; }
    public virtual Doctor? doctor { get; set; }
    public int? SectorId { get; set; }
    public virtual Sector? Sector { get; set; }
    public int? Day { get; set; }
    public int? Month { get; set; }
    public int? Year { get; set;}
    public string? Hour { get; set;}

    public Appointment()
    {
        
    }

    public Appointment(int? id, string name, int? patientId, Patient? patient, int? doctorId, Doctor? doctor, int? sectorId, Sector? sector, 
        int? day, int? month, int? year, string? hour)
    {
        Id = id;
        Name = name;
        PatientId = patientId;
        this.patient = patient;
        DoctorId = doctorId;
        this.doctor = doctor;
        SectorId = sectorId;
        Sector = sector;
        Day = day;
        Month = month;
        Year = year;
        Hour = hour;
    }
    
}