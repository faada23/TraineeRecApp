using System;
using System.Collections.Generic;
using Api.Core.Interfaces;

namespace Api.Core.Entities;

public partial class Traineeship : IEntity
{
    public int Id { get; set; }

    public DateOnly Startdate { get; set; }

    public DateOnly Enddate { get; set; }

    public short? Grade { get; set; }

    public int StatusId { get; set; }

    public int StudentId { get; set; }

    public virtual Traineeshipsstatus Status { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;

    public virtual ICollection<Traineeshipsupervisor> Traineeshipsupervisors { get; set; } = new List<Traineeshipsupervisor>();
}
