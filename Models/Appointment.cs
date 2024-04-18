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
    public DateTime? Timestamp { get; set; }

    public Appointment()
    {
        
    }

    public Appointment(int? id, string name, int? patientId, Patient? patient, int? doctorId, Doctor? doctor, int? sectorId, Sector? sector, DateTime? timestamp)
    {
        Id = id;
        Name = name;
        PatientId = patientId;
        this.patient = patient;
        DoctorId = doctorId;
        this.doctor = doctor;
        SectorId = sectorId;
        Sector = sector;
        Timestamp = timestamp;
    }
}