using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveyApp.Domain.Entities
{
    [Table("question_options")]
    public class QuestionOption
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("question_id")]
        public Guid QuestionId { get; set; }

        [Required]
        [Column("option_text")]
        public string OptionText { get; set; } = string.Empty;

        [Column("order_index")]
        public int OrderIndex { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public Question? Question { get; set; }
    }
}