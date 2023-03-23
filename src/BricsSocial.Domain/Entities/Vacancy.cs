
using BricsSocial.Domain.Enums;

namespace BricsSocial.Domain.Entities
{
    public sealed class Vacancy : EntityBase
    {
        public string Name { get; set; }
        public string Requirements { get; set; }
        public string Offerings { get; set; }
        public VacancyStatus Status { get; set; }
        public string SkillTags { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public List<Reply> Replies { get; set; } = new List<Reply>();

        public static class Invariants
        {
            public const int NameMinLength = 1;
            public const int NameMaxLength = 200;
            public const int RequirementsMinLength = 1;
            public const int RequirementsMaxLength = 10000;
            public const int OfferingsMinLength = 1;
            public const int OfferingsMaxLength = 10000;
        }
    }
}
