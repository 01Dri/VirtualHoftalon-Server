using Microsoft.EntityFrameworkCore;

namespace VirtualHoftalon_Server.Models;

public class ModelsContext : DbContext
{
    
    public ModelsContext(DbContextOptions<ModelsContext> options)
        :base(options)
    {
    }
    
    public virtual DbSet<Doctor> Doctors { get; set;}
    public virtual DbSet<Patient> Patients { get; set;}
    public virtual DbSet<Sector> Sectors { get; set;}
    
    public virtual DbSet<Appointment> Appointments { get; set;}

    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<Doctor>(doctor =>
        {
            doctor.HasKey(k => k.Id);
            doctor.HasMany(a => a.Appointments)
                .WithOne(a => a.doctor)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<Patient>(patient =>
        {
            patient.HasKey(k => k.Id);
            patient.HasMany(p => p.Appointments)
                .WithOne(p => p.patient)
                .HasForeignKey(p => p.PatientId)
                .OnDelete(DeleteBehavior.Restrict);
                
            patient.HasIndex(p => p.Cpf)
                .IsUnique();
            
            patient.HasIndex(p => p.Rg)
                .IsUnique();
            
            patient.HasIndex(p => p.Email)
                .IsUnique();

        });
        modelBuilder.Entity<Sector>(sector =>
        {
            sector.HasKey(k => k.Id);
            sector.HasMany(s => s.Appointments)
                .WithOne(s => s.Sector)
                .HasForeignKey(s => s.SectorId)
                .OnDelete(DeleteBehavior.Restrict);
        });
        
        modelBuilder.Entity<Appointment>(appointment =>
        {
            appointment.HasKey(k => k.Id);;

        });

        
        OnModelCreatingPartial(modelBuilder);
    }

    private void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
    }
}