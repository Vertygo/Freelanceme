using System;

namespace Freelancme.WebApi.V1.Dto.Request
{
    public class TimeTrackingDetailRequest
    {
        public Guid ClientID { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
