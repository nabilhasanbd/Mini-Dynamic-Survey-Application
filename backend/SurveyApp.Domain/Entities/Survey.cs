using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveyApp.Domain.Entities
{
    [Table("surveys")]
    public class Survey
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Required]
        [Column("title")]
        [MaxLength(500)]
        public string Title { get; set; } = string.Empty;

        [Column("description")]
        public string? Description { get; set; }

        [Required]
        [Column("slug")]
        [MaxLength(100)]
        public string Slug { get; set; } = string.Empty;

        [Column("is_published")]
        public bool IsPublished { get; set; } = false;

        [Column("expiry_date")]
        public DateTime? ExpiryDate { get; set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;

        [Column("user_id")]
        public Guid UserId { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public User? User { get; set; }
        public ICollection<Question> Questions { get; set; } = new List<Question>();
        public ICollection<Response> Responses { get; set; } = new List<Response>();
    }
}