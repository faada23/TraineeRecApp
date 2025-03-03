using System;
using System.Collections.Generic;
using Api.Core.Interfaces;

namespace Api.Core.Entities;

public partial class Supervisorstype : IEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Traineeshipsupervisor> Traineeshipsupervisors { get; set; } = new List<Traineeshipsupervisor>();
}
