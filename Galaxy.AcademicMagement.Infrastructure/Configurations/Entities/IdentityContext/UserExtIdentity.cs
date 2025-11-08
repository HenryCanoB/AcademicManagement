using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Galaxy.AcademicMagement.Infrastructure.Configurations.Entities.IdentityContext
{
    public class UserExtIdentity : IdentityUser
    {
        public Guid StudentId { get; set; }
        [StringLength(200)]
        public string FullName { get; set; }
    }
}
