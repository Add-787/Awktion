using System;
namespace Awktion.Domain.Common;

public class BaseEntity
{
    public int Id { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
}