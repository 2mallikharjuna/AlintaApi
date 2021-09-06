using Microsoft.EntityFrameworkCore;
using System;

namespace AlintaApi.Domain.Models
{
    /// <summary>
    /// Common model contract definition
    /// </summary>    
    public abstract class EntityBase
    {
        /// <summary>
        /// Get or Set the Entity Id
        /// </summary>
        public Guid Id { get; set; }
    }
}
