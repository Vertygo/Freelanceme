using Freelanceme.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Freelanceme.Domain
{
    public class Address : Entity
    {
        /// <summary>
        /// Ef trickery
        /// </summary>
        private Address()
        {

        }

        [StringLength(100, ErrorMessage = "Country name cannot be longer than 100 characters.")]
        public string Country { get; set; }

        [StringLength(100, ErrorMessage = "City name cannot be longer than 100 characters.")]
        public string City { get; set; }

        [StringLength(100, ErrorMessage = "Street name cannot be longer than 100 characters.")]
        public string Street { get; set; }
    }
}