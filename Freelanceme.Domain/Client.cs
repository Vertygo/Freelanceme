using Freelanceme.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Freelanceme.Domain
{
    public class Client : Entity
    {
        /// <summary>
        /// Ef trickery
        /// </summary>
        private Client()
        {

        }

        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        public string Name { get; set; }

        [StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
        public string Surname { get; set; }
        
        public bool IsCompany { get; set; }

        [StringLength(100, ErrorMessage = "Company name cannot be longer than 100 characters.")]
        public string CompanyName { get; set; }

        public bool IsTaxPayer { get; set; }

        public Address Address { get; set; }

        [StringLength(100, ErrorMessage = "Website cannot be longer than 100 characters.")]
        public string Website { get; set; }

        public bool IsActive { get; set; }

        public Guid UserId { get; set; }

        [Required]
        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        public List<Project> Projects { get; set; }
    }
}