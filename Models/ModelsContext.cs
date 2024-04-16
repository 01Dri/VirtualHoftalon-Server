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
    public virtual DbSet<SectorPatient> SectorPatients { get; set;}
    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Doctor>(doctor =>
        {
            doctor.HasKey(k => k.Id);
        });
        modelBuilder.Entity<Patient>(patient =>
        {
            patient.HasKey(k => k.Id);
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
        });
        modelBuilder.Entity<SectorPatient>(sectorPatient =>
        {
            sectorPatient.HasKey(k => k.Id);
        });
        OnModelCreatingPartial(modelBuilder);
    }

    private void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
    }
}