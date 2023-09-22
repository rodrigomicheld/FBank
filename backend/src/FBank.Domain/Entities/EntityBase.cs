﻿namespace FBank.Domain.Entities
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; }
        public DateTime UpdateDate { get; set; }

        protected EntityBase()
        {
            Id = Guid.NewGuid();
        }
    }
}