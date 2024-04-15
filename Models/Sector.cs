using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualHoftalon_Server.Models;

public class Sector
{

    public int? Id { get; set; }
    [Required] public string? Name { get; set; }
    public int? RoomNumber { get; set; }
    public int DoctorId { get; set; }
    public Doctor doctor { get; set;}
    
    public ICollection<SectorPatient> SectorPatients { get; set; }

    public Sector()
    {

    }

    public Sector(int? id, string? name, int? roomNumber)
    {
        Id = id;
        Name = name;
        RoomNumber = roomNumber;
    }
}

