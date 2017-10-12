using System;

namespace Infrastructure.Entity
{
    public interface ISoftDeleteEntity
    {
        DateTime? DeletedDate { get; set; }
    }
}