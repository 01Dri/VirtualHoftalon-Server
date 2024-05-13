namespace VirtualHoftalon_Server.Models.Dto.Administrator;

public record AdministratorResponseDTO(int Id,
    string FirstName,
    string LastName,
    string Cpf,
    string Email,
    DateTime DateBirth,
    int? LoginId
    );