using System;

namespace Freelanceme.WebApi.V1.Dto
{
    public class TimeTrackingDetails
    {
        public Guid Id { get; set; }

        public string ProjectName { get; set; }

        public decimal WorkingHours { get; set; }

        public DateTime Date { get; set; }
    }
}