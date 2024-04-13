using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VirtualHoftalon_Server.Models;

public class Sector
{

    public int Id { get; set; }
    [Required]
    public string Name { get; set;}
    public int RoomNumber { get; set;}
    public Doctor Doctor { get; set; }
    public ICollection<SectorPatient> SectorPatients { get; set; }

}