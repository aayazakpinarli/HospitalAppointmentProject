using CORE.APP.Domain;
using System.ComponentModel.DataAnnotations;

namespace Hospitals.APP.Domain
{
    public class Branch : Entity
    {
        [Required]
        public string Title{ get; set; }

        public List<Doctor> Doctors { get; set; } = new List<Doctor>();
    }
}
