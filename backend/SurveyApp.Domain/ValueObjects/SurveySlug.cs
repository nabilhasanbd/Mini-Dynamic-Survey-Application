using System;
using System.Linq;

namespace SurveyApp.Domain.ValueObjects
{
    public class SurveySlug
    {
        public string Value { get; private set; }

        private SurveySlug(string value)
        {
            Value = value;
        }

        public static SurveySlug Generate()
        {
            // Generate a random slug, e.g., abc123
            var random = new Random();
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
            var slug = new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            return new SurveySlug(slug);
        }

        public static SurveySlug FromString(string slug)
        {
            if (string.IsNullOrWhiteSpace(slug) || slug.Length != 6 || !slug.All(c => char.IsLetterOrDigit(c)))
            {
                throw new ArgumentException("Invalid slug format");
            }
            return new SurveySlug(slug.ToLower());
        }

        public override string ToString() => Value;
    }
}