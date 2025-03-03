using System;
using System.Collections.Generic;
using Api.Core.Interfaces;

namespace Api.Core.Entities;

public partial class User : IEntity
{
    public int Id { get; set; }

    public string Fullname { get; set; } = null!;

    public string Passwordhash { get; set; } = null!;

    public string? Email { get; set; }

    public DateTime? Createdat { get; set; }

    public int? StudentId { get; set; }

    public int? TraineeshipsupervisorId { get; set; }

    public int RoleId { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual Student? Student { get; set; }

    public virtual Traineeshipsupervisor? Traineeshipsupervisor { get; set; }
}
