using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SurveyApp.Domain.Entities
{
    [Table("answers")]
    public class Answer
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; }

        [Column("response_id")]
        public Guid ResponseId { get; set; }

        [Column("question_id")]
        public Guid QuestionId { get; set; }

        [Column("answer_text")]
        public string? AnswerText { get; set; }

        [Column("selected_option_ids")]
        public List<Guid>? SelectedOptionIds { get; set; }

        [Column("rating_value")]
        public int? RatingValue { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        // Navigation properties
        public Response? Response { get; set; }
        public Question? Question { get; set; }
    }
}