using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Api.Core.Entities;

namespace Api.Persistence;

public partial class DatabaseContext : DbContext
{
    public DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Groupsname> Groupsnames { get; set; }

    public virtual DbSet<Organization> Organizations { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Speciality> Specialities { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Supervisorstype> Supervisorstypes { get; set; }

    public virtual DbSet<Traineeship> Traineeships { get; set; }

    public virtual DbSet<Traineeshipsstatus> Traineeshipsstatuses { get; set; }

    public virtual DbSet<Traineeshipsupervisor> Traineeshipsupervisors { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Groupsname>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("groupsname_pkey");

            entity.ToTable("groupsname");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(60)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("organizations_pkey");

            entity.ToTable("organizations");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(70)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Speciality>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("specialities_pkey");

            entity.ToTable("specialities");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("students_pkey");

            entity.ToTable("students");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Birthday).HasColumnName("birthday");
            entity.Property(e => e.Course)
                .HasMaxLength(1)
                .HasColumnName("course");
            entity.Property(e => e.Fullname)
                .HasMaxLength(120)
                .HasColumnName("fullname");
            entity.Property(e => e.GroupnameId).HasColumnName("groupname_id");
            entity.Property(e => e.Phonenumber)
                .HasMaxLength(20)
                .HasColumnName("phonenumber");
            entity.Property(e => e.SpecialityId).HasColumnName("speciality_id");

            entity.HasOne(d => d.Groupname).WithMany(p => p.Students)
                .HasForeignKey(d => d.GroupnameId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("students_groupname_id_fkey");

            entity.HasOne(d => d.Speciality).WithMany(p => p.Students)
                .HasForeignKey(d => d.SpecialityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("students_speciality_id_fkey");
        });

        modelBuilder.Entity<Supervisorstype>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("supervisorstype_pkey");

            entity.ToTable("supervisorstype");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(25)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Traineeship>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("traineeships_pkey");

            entity.ToTable("traineeships");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Enddate).HasColumnName("enddate");
            entity.Property(e => e.Grade).HasColumnName("grade");
            entity.Property(e => e.Startdate).HasColumnName("startdate");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.StudentId).HasColumnName("student_id");

            entity.HasOne(d => d.Status).WithMany(p => p.Traineeships)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("traineeships_status_id_fkey");

            entity.HasOne(d => d.Student).WithMany(p => p.Traineeships)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("traineeships_student_id_fkey");

            entity.HasMany(d => d.Traineeshipsupervisors).WithMany(p => p.Traineeships)
                .UsingEntity<Dictionary<string, object>>(
                    "TraineeshipsTraineeshipsupervisor",
                    r => r.HasOne<Traineeshipsupervisor>().WithMany()
                        .HasForeignKey("TraineeshipsupervisorId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("traineeships_traineeshipsuperviso_traineeshipsupervisor_id_fkey"),
                    l => l.HasOne<Traineeship>().WithMany()
                        .HasForeignKey("TraineeshipId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("traineeships_traineeshipsupervisors_traineeship_id_fkey"),
                    j =>
                    {
                        j.HasKey("TraineeshipId", "TraineeshipsupervisorId").HasName("traineeships_traineeshipsupervisors_pkey");
                        j.ToTable("traineeships_traineeshipsupervisors");
                        j.IndexerProperty<int>("TraineeshipId").HasColumnName("traineeship_id");
                        j.IndexerProperty<int>("TraineeshipsupervisorId").HasColumnName("traineeshipsupervisor_id");
                    });
        });

        modelBuilder.Entity<Traineeshipsstatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("traineeshipsstatus_pkey");

            entity.ToTable("traineeshipsstatus");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(25)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Traineeshipsupervisor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("traineeshipsupervisors_pkey");

            entity.ToTable("traineeshipsupervisors");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Fullname)
                .HasMaxLength(120)
                .HasColumnName("fullname");
            entity.Property(e => e.OrganizationId).HasColumnName("organization_id");
            entity.Property(e => e.Phonenumber)
                .HasMaxLength(20)
                .HasColumnName("phonenumber");
            entity.Property(e => e.SupervisortypeId).HasColumnName("supervisortype_id");

            entity.HasOne(d => d.Organization).WithMany(p => p.Traineeshipsupervisors)
                .HasForeignKey(d => d.OrganizationId)
                .HasConstraintName("traineeshipsupervisors_organization_id_fkey");

            entity.HasOne(d => d.Supervisortype).WithMany(p => p.Traineeshipsupervisors)
                .HasForeignKey(d => d.SupervisortypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("traineeshipsupervisors_supervisortype_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .HasColumnName("email");
            entity.Property(e => e.Fullname)
                .HasMaxLength(120)
                .HasColumnName("fullname");
            entity.Property(e => e.Passwordhash)
                .HasMaxLength(80)
                .HasColumnName("passwordhash");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.StudentId).HasColumnName("student_id");
            entity.Property(e => e.TraineeshipsupervisorId).HasColumnName("traineeshipsupervisor_id");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("users_role_id_fkey");

            entity.HasOne(d => d.Student).WithMany(p => p.Users)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("users_student_id_fkey");

            entity.HasOne(d => d.Traineeshipsupervisor).WithMany(p => p.Users)
                .HasForeignKey(d => d.TraineeshipsupervisorId)
                .HasConstraintName("users_traineeshipsupervisor_id_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
