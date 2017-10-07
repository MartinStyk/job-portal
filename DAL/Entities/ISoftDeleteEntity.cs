using System;

namespace DAL.Entities
{
    public interface ISoftDeleteEntity
    {
        DateTime? DeletedDate { get; set; }
    }
}