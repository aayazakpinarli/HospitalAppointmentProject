using CORE.APP.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;


namespace Hospitals.APP.Domain
{
    public class Doctor : Entity
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int GroupId { get; set; }

        public int BranchId { get; set; }

        public Branch Branch { get; set; }

        public List<DoctorPatient> DoctorPatients { get; set; } = new List<DoctorPatient>();

        [NotMapped]
        public List<int> PatientIds
        {
            get => DoctorPatients.Select(dp => dp.PatientId).ToList();
            set => DoctorPatients = value.Select(v => new DoctorPatient { PatientId = v }).ToList();
        }

    }
}
