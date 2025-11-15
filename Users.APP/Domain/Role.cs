using CORE.APP.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Users.APP.Domain
{
    public class Role : Entity
    {
        [Required]
        [StringLength(25)]
        public string Name { get; set; }

        public List<UserRole> UserRoles { get; set; } = new List<UserRole>();

        [NotMapped]
        public List<int> UserIds
        {
            get => UserRoles.Select(ur => ur.UserId).ToList();
            set => UserRoles = value.Select(v => new UserRole { UserId = v }).ToList();
        }
    }
}