using System;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Context;
using Infrastructure.Entity;

namespace DAL.Entities
{
    public class Question : IEntity<int>, ISoftDeleteEntity
    {
        public int Id { get; set; }

        [NotMapped]
        public string TableName { get; } = nameof(JobPortalDbContext.Questions);

        public string Text { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}