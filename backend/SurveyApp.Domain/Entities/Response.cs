using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveyApp.Domain.Entities
{
    [Table("responses")]
    public class Response
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("survey_id")]
        public Guid SurveyId { get; set; }

        [Column("submitted_at")]
        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;

        [Column("ip_address")]
        [MaxLength(45)]
        public string? IpAddress { get; set; }

        [Column("user_agent")]
        public string? UserAgent { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public Survey? Survey { get; set; }
        public ICollection<Answer> Answers { get; set; } = new List<Answer>();
    }
}