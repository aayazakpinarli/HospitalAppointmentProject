using CORE.APP.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospitals.APP.Domain
{
    public class Patient : Entity
    {

        public decimal? Height { get; set; }

        public decimal? Weight { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int GroupId { get; set; }

        [Required]
        [StringLength(500)]
        public string Complaints { get; set; }

        public List<DoctorPatient> DoctorPatients { get; set; } = new List<DoctorPatient>();

        [NotMapped]
        public List<int> UserIds
        {
            get => DoctorPatients.Select(dp => dp.DoctorId).ToList();
            set => DoctorPatients = value.Select(v => new DoctorPatient { DoctorId = v }).ToList();
        }

    }
}
