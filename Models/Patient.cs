using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using VirtualHoftalon_Server.Enums;
using VirtualHoftalon_Server.Validates;

namespace VirtualHoftalon_Server.Models;

public class Patient
{
    
    public int? Id { get; set; }

    [Required(ErrorMessage = "O campo Name é obrigatório.")]
    [MaxLength(100, ErrorMessage = "O campo Name deve ter no máximo 100 caracteres.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "O campo PhoneNumber é obrigatório.")]
    [MaxLength(20, ErrorMessage = "O campo PhoneNumber deve ter no máximo 20 caracteres.")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "O campo Cpf é obrigatório.")]
    [ValidateCpf(ErrorMessage = "Precisa ter 11 caracteres '. e - não contam'")]
    public string Cpf { get; set; }

    [Required(ErrorMessage = "O campo Rg é obrigatório.")]
    [ValidateRg(ErrorMessage = "Precisa ter 9 caracteres '. e - não contam'")]
    public string Rg { get; set; }

    [Required(ErrorMessage = "O campo Email é obrigatório.")]
    [EmailAddress(ErrorMessage = "O campo Email deve ser um endereço de email válido.")]
    [MaxLength(100, ErrorMessage = "O campo Email deve ter no máximo 100 caracteres.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo DateBirth é obrigatório.")]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
    public DateTime DateBirth { get; set; }

    [Required(ErrorMessage = "O campo ClassificationPatient é obrigatório.")]
    public ClassificationPatient ClassificationPatient { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; }
    
    public int? LoginId { get; set; }
    public Login? Login { get; set; }


    public Patient()
    {
        
    }

    public Patient(int? id, string name, string phoneNumber, string cpf, string rg, string email, DateTime dateBirth, ClassificationPatient classificationPatient, ICollection<Appointment> appointments)
    {
        Id = id;
        Name = name;
        PhoneNumber = phoneNumber;
        Cpf = cpf;
        Rg = rg;
        Email = email;
        DateBirth = DateTime.SpecifyKind(dateBirth, DateTimeKind.Utc);
        ClassificationPatient = classificationPatient;
        Appointments = appointments;
    }
}

