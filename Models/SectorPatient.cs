namespace VirtualHoftalon_Server.Models;

public class SectorPatient
{
    
    public int? Id { get; set;}
    
    public int? SectorId { get; set; }
    public Sector? Sector { get; set; }

    public int? PatientId { get; set; }
    public Patient? Patient { get; set; }
}