using System;
using System.Collections.Generic;
using Api.Core.Interfaces;

namespace Api.Core.Entities;

public partial class Speciality : IEntity
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
