using System;

namespace Freelanceme.Domain.Common
{
    public interface IEntity
    {
        Guid Id { get; }
    }
}