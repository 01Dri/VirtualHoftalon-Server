using System.ComponentModel.DataAnnotations;
using VirtualHoftalon_Server.Enums;

namespace VirtualHoftalon_Server.Models;

public class Sector
{

    public int? Id { get; set; }
    [Required] public string? Name { get; set; }
    public int? RoomNumber { get; set; }
    
    public int DoctorId { get; set; }
    public virtual Doctor doctor { get; set;}
    public virtual List<Appointment> Appointments { get; set; }
    public SectorTag Tag { get; set; }
    
    public Sector()
    {

    }

    public Sector(int? id, string? name, int? roomNumber, int doctorId, List<Appointment> appointments, SectorTag tag)
    {
        Id = id;
        Name = name;
        RoomNumber = roomNumber;
        DoctorId = doctorId;
        Appointments = appointments;
        Tag = tag;
    }
}

