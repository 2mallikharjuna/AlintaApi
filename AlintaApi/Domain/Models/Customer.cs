using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.ComponentModel.DataAnnotations;

namespace AlintaApi.Domain.Models
{
    /// <summary>
    /// Customer entity including mapping implementation
    /// </summary>
    public class Customer : EntityBase, IEntityTypeConfiguration<Customer>
    { 
        /// <summary>
        /// Get or Set the first name
        /// </summary>
        [Required(ErrorMessage = "The First Name is mandatory")]
        [StringLength(32, MinimumLength = 3)]
        [RegularExpression(@"^[A-Z][a-zA-Z]*$")]
        public string FirstName
        {
            get; set;
        }
        /// <summary>
        /// Get or sets the last name
        /// </summary>
        [Required(ErrorMessage = "The Last Name is mandatory")]
        [StringLength(32, MinimumLength = 1)]
        [RegularExpression(@"^[A-Z][a-zA-Z]*$")]
        public string LastName { get; set; }

        /// <summary>
        /// Get or set the DOB
        /// </summary>
        [Required(ErrorMessage = "The DOB is mandatory")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{dd-MM-yyyy:0")]
        public DateTime DateOfBirth
        {
            get; set;
        }

        /// <summary>
        /// Configure the Customer model
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer");
            // Set key for entity
            builder.HasKey(p => p.Id);
            // Set configuration for columns            
            builder.Property(p => p.FirstName);
            builder.Property(p => p.LastName);
            builder.Property(p => p.DateOfBirth);
        }
    }
}
