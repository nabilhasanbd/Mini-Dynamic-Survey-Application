using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SurveyApp.Domain.Enums;

namespace SurveyApp.Domain.Entities
{
    [Table("questions")]
    public class Question
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("survey_id")]
        public Guid SurveyId { get; set; }

        [Required]
        [Column("question_text")]
        public string QuestionText { get; set; } = string.Empty;

        [Required]
        [Column("question_type")]
        public QuestionType QuestionType { get; set; }

        [Column("is_required")]
        public bool IsRequired { get; set; } = false;

        [Column("order_index")]
        public int OrderIndex { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public Survey? Survey { get; set; }
        public ICollection<QuestionOption> QuestionOptions { get; set; } = new List<QuestionOption>();
        public ICollection<Answer> Answers { get; set; } = new List<Answer>();
    }
}