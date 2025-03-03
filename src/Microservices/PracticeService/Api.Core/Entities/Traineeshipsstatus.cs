using System;
using System.Collections.Generic;
using Api.Core.Interfaces;

namespace Api.Core.Entities;

public partial class Traineeshipsstatus : IEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Traineeship> Traineeships { get; set; } = new List<Traineeship>();
}
