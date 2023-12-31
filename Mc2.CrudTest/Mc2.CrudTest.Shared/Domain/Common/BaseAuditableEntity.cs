﻿namespace Mc2.CrudTest.Shared.Domain.Common;

public abstract class BaseAuditableEntity : BaseEntity
{
    public DateTimeOffset Created { get; set; }

    public string? CreatedBy { get; set; }

    public DateTimeOffset LastModified { get; set; }

    public string? LastModifiedBy { get; set; }
    public bool IsDeleted { get; set; } = false;
    public DateTimeOffset? Deleted { get; set; }
    public void Undo()
    {
        IsDeleted = false;
        Deleted = null;
    }
}
