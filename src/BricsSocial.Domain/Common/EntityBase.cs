using System.ComponentModel.DataAnnotations.Schema;

namespace BricsSocial.Domain.Common;

public abstract class EntityBase
{
    public int Id { get; set; }
}
