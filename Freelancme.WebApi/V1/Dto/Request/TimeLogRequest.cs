using System;

namespace Freelanceme.WebApi.V1.Dto.Request
{
    public class TimeLogRequest
    {
        public DateTime Date { get; set; }

        public int WorkingHours { get; set; }

        public Guid ClientId { get; set; }

        public Guid ProjectId { get; set; }
    }
}
