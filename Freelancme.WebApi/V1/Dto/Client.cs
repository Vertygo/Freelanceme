using System;

namespace Freelanceme.WebApi.V1.Dto
{
    public class Client
    {
        public Guid Id { get; set; }

        public string CompanyName { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string AddressId { get; set; }

        public string AddressCountry { get; set; }

        public string AddressCity { get; set; }

        public string AddressStreet { get; set; }

        public string Website { get; set; }

        public bool IsCompany { get; set; }

        public bool IsActive { get; set; }
    }
}
