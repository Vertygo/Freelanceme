using Freelanceme.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Freelanceme.Domain
{
    public class Project : Entity
    {
        /// <summary>
        /// Ef trickery
        /// </summary>
        public Project()
        {

        }

        [StringLength(50, ErrorMessage = "Project name cannot be longer than 50 characters.")]
        public string Name { get; set; }

        public bool IsActive { get; set; }
    }
}
