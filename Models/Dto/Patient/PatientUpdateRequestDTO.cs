using System.ComponentModel.DataAnnotations;

namespace VirtualHoftalon_Server.Models.Dto.Patient;

public record PatientUpdateRequestDTO(
    string Name,

    string Phone,

    [StringLength(11, MinimumLength = 11, ErrorMessage = "O CPF deve conter 11 caracteres.")]
    string Cpf,

    string Rg,

    [EmailAddress(ErrorMessage = "O campo Email deve ser um endereço de email válido.")]
    string Email,

    [DataType(DataType.Date, ErrorMessage = "O campo BirthDate deve ser uma data válida.")]
    DateTime BirthDate,

    string Classification
);