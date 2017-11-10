using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Freelanceme.Domain.Common
{
    public abstract class Entity : IEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; protected set; }
    }
}
