using CORE.APP.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hospitals.APP.Domain
{
    public class DoctorPatient : Entity
    {
        public int DoctorId { get; set; }
        public Doctor Doctor{ get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }

    }
}
