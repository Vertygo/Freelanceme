using System;

namespace Freelancme.WebApi.V1.Dto
{
    public class TimeLog
    {
        public DateTime Date { get; set; }

        public int WorkingHours { get; set; }

        public Guid ClientId { get; set; }

        public Guid ProjectId { get; set; }
    }
}
