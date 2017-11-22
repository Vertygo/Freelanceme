using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Freelanceme.WebApi.V1.Dto
{
    public class ClientInfo
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string ContactName { get; set; }

        public string Surname { get; set; }

        public string Address { get; set; }

        public bool IsCompany { get; set; }

        public bool IsActive { get; set; }

        public List<ProjectInfo> Projects { get; set; }
    }
}
