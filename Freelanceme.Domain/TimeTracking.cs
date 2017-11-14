using Freelanceme.Domain.Common;
using System;

namespace Freelanceme.Domain
{
    public class TimeTracking : Entity
    {
        public Client Client { get; set; }

        public User User { get; set; }

        public DateTime Date { get; set; }

        public int WorkingHours { get; set; }

        /// <summary>
        /// Ef trickery
        /// </summary>
        private TimeTracking()
        {

        }
    }
}
