using System;
using System.Collections.Generic;
using Api.Core.Interfaces;

namespace Api.Core.Entities;

public partial class Student : IEntity
{
    public int Id { get; set; }

    public string Fullname { get; set; } = null!;

    public DateOnly Birthday { get; set; }

    public string? Phonenumber { get; set; }

    public char Course { get; set; }

    public int SpecialityId { get; set; }

    public int GroupnameId { get; set; }

    public virtual Groupsname Groupname { get; set; } = null!;

    public virtual Speciality Speciality { get; set; } = null!;

    public virtual ICollection<Traineeship> Traineeships { get; set; } = new List<Traineeship>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
