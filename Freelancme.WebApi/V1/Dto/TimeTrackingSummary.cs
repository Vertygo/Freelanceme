using System;

namespace Freelanceme.WebApi.V1.Dto
{
    public class TimeTrackingSummary
    {
        public Guid ClientID { get; set; }

        /// <summary>
        /// Name of the client
        /// </summary>
        public string Client { get; set; }

        /// <summary>
        /// Last working date in range
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// First working date in range
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Working time summary (hours, monutes)
        /// </summary>
        public decimal WorkingHours { get; set; }

    }
}
