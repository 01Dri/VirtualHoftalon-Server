namespace VirtualHoftalon_Server.Models;

public class PatientsQueue
{
    public int? Id { get; set; }
    public int? PatientId { get; set;}
    public Patient Patient { get; set; }
    public int AppointmentId { get; set; }
    public Appointment Appointment { get; set; }
    public bool IsPreferred { get; set; }
    public int Position { get; set; }
    public string Password { get; set; }
    
    
}