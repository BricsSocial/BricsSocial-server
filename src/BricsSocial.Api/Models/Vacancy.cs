
namespace BricsSocial.Domain.Entities
{
    public sealed class Vacancy : BaseEntity
    {
        public string Requirements { get; set; }

        public string Offerings { get; set; }
    }
}
