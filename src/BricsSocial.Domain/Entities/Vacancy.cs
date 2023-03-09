
using BricsSocial.Domain.Enums;

namespace BricsSocial.Domain.Entities
{
    public sealed class Vacancy : EntityBase
    {
        public string Name { get; set; }
        public string Requirements { get; set; }
        public string Offerings { get; set; }
        public VacancyStatus Status { get; set; }

        public string CompanyId { get; set; }
        public Company Company { get; set; }
        public List<VacancyReply> VacancyReplies { get; set; }
    }
}
