using System;
using System.Collections.Generic;
    using Api.Core.Interfaces;

namespace Api.Core.Entities;

public partial class Traineeshipsupervisor: IEntity
{
    public int Id { get; set; }

    public string Fullname { get; set; } = null!;

    public string? Phonenumber { get; set; }

    public int SupervisortypeId { get; set; }

    public int? OrganizationId { get; set; }

    public virtual Organization? Organization { get; set; }

    public virtual Supervisorstype Supervisortype { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();

    public virtual ICollection<Traineeship> Traineeships { get; set; } = new List<Traineeship>();
}
