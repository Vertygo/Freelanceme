using System;

namespace Freelanceme.WebApi.V1.Dto.Request
{
    public class TimeTrackingSummaryRequest
    {
        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }
    }
}
