using System;
using Infrastructure.Entity;

namespace DAL.Entities
{
    public class Question : IEntity<int>, ISoftDeleteEntity
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}