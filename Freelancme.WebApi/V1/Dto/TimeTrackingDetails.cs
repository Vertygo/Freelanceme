using System;

namespace Freelancme.WebApi.V1.Dto
{
    public class TimeTrackingDetails
    {
        public Guid Id { get; internal set; }

        public decimal WorkingHours { get; set; }

        public DateTime Date { get; set; }
    }
}