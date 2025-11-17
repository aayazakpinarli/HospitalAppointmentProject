using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text.RegularExpressions;

namespace Hospitals.APP.Domain
{
    public class HospitalDb : DbContext
    {
        public DbSet<Branch> Branchs { get; set; }
        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<Patient> Patients { get; set; }

        public DbSet<DoctorPatient> DoctorPatients { get; set; }

        public HospitalDb(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // define indices for optimizing query performance on frequently searched propertes
            modelBuilder.Entity<DoctorPatient>().HasIndex(doctorPatientEntity => doctorPatientEntity.PatientId);
            modelBuilder.Entity<DoctorPatient>().HasIndex(doctorPatientEntity => doctorPatientEntity.DoctorId);


            // Relationship configurations
            // ----------------------------

            // FK relationships and delete behaviors

            modelBuilder.Entity<DoctorPatient>()
                .HasOne(doctorPatientEntity => doctorPatientEntity.Doctor)
                .WithMany(doctorEntity => doctorEntity.DoctorPatients)
                .HasForeignKey(doctorPatientEntity => doctorPatientEntity.DoctorId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<DoctorPatient>()
                .HasOne(doctorPatientEntity => doctorPatientEntity.Patient)
                .WithMany(doctorEntity => doctorEntity.DoctorPatients)
                .HasForeignKey(doctorPatientEntity => doctorPatientEntity.PatientId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
