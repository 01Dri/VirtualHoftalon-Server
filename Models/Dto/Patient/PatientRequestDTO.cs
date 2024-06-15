using System;
using System.ComponentModel.DataAnnotations;

namespace VirtualHoftalon_Server.Models.Dto.Patient
{
    public record PatientRequestDTO
    (
        [Required(ErrorMessage = "O campo Name é obrigatório.")]
        string Name,

        [Required(ErrorMessage = "O campo Phone é obrigatório.")]
        string Phone,

        [Required(ErrorMessage = "O campo CPF é obrigatório.")]
        [StringLength(14, MinimumLength = 8, ErrorMessage = "O CPF deve conter 14 caracteres.")]
        string Cpf,

        [Required(ErrorMessage = "O campo RG é obrigatório.")]
        string Rg,

        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo Email deve ser um endereço de email válido.")]
        string Email,

        [Required(ErrorMessage = "O campo BirthDate é obrigatório.")]
        [DataType(DataType.Date, ErrorMessage = "O campo BirthDate deve ser uma data válida.")]
        DateTime BirthDate,

        [Required(ErrorMessage = "O campo Classification é obrigatório.")]
        string Classification
    );
}