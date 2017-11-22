using Freelanceme.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Freelanceme.Domain
{
    public class TimeTracking : Entity
    {
        public Guid ClientId { get; set; }

        [ForeignKey(nameof(ClientId))]
        public virtual Client Client { get; set; }

        public Guid ProjectId { get; set; }

        [ForeignKey(nameof(ProjectId))]
        public virtual Project Project { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }

        [Column(TypeName = "Date")]
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
