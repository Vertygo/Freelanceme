using Freelanceme.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace Freelanceme.Domain
{
    public class User : Entity
    {

        public User(string name, string surname, string username, string email)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Username = username;
        }

        [StringLength(100, ErrorMessage = "Username cannot be longer than 100 characters.")]
        public string Username { get; set; }

        [StringLength(150, ErrorMessage = "Email cannot be longer than 150 characters.")]
        public string Email { get; set; }

        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        public string Name { get; set; }

        [StringLength(100, ErrorMessage = "Last name cannot be longer than 100 characters.")]
        public string Surname { get; set; }
    }
}
