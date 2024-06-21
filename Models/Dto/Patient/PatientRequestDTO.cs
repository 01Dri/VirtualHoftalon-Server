using System;
using System.ComponentModel.DataAnnotations;
using VirtualHoftalon_Server.Validates;

namespace VirtualHoftalon_Server.Models.Dto.Patient
{
    public record PatientRequestDTO
    (
        [Required(ErrorMessage = "O campo Name é obrigatório.")]
        string Name,

        [Required(ErrorMessage = "O campo Phone é obrigatório.")]
        string Phone,

        [Required(ErrorMessage = "O campo CPF é obrigatório.")]
        [ValidateCpf(ErrorMessage = "Precisa ter 11 caracteres '. e - não contam'")]
        string Cpf,

        [Required(ErrorMessage = "O campo RG é obrigatório.")]
        [ValidateRg(ErrorMessage = "Precisa ter 9 caracteres '. e - não contam'")]
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