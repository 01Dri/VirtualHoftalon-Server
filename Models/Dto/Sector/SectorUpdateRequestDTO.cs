using System.ComponentModel.DataAnnotations;

namespace VirtualHoftalon_Server.Models.Dto.Sector;

public record SectorUpdateRequestDTO(
    
    string? Name,
    int? RoomNumber
);
