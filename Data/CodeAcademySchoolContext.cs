using System;
using System.Collections.Generic;
using CodeAcademySchool.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeAcademySchool.Data;

public partial class CodeAcademySchoolContext : DbContext
{
    public CodeAcademySchoolContext()
    {
    }

    public CodeAcademySchoolContext(DbContextOptions<CodeAcademySchoolContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<CourseEnrollment> CourseEnrollments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<GradeRegistration> GradeRegistrations { get; set; }

    public virtual DbSet<Profession> Professions { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDb; Database = CodeAcademySchool; Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.ToTable("Class");

            entity.Property(e => e.ClassName).HasMaxLength(10);
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.ToTable("Course");

            entity.Property(e => e.CourseName).HasMaxLength(50);
        });

        modelBuilder.Entity<CourseEnrollment>(entity =>
        {
            entity.ToTable("CourseEnrollment");

            entity.Property(e => e.FkCourseId).HasColumnName("(FK)CourseId");
            entity.Property(e => e.FkGradeRegistrationId).HasColumnName("(FK)GradeRegistrationId");
            entity.Property(e => e.FkStudentId).HasColumnName("(FK)StudentId");

            entity.HasOne(d => d.FkCourse).WithMany(p => p.CourseEnrollments)
                .HasForeignKey(d => d.FkCourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CourseEnrollment_Course");

            entity.HasOne(d => d.FkGradeRegistration).WithMany(p => p.CourseEnrollments)
                .HasForeignKey(d => d.FkGradeRegistrationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CourseEnrollment_GradeRegistration");

            entity.HasOne(d => d.FkStudent).WithMany(p => p.CourseEnrollments)
                .HasForeignKey(d => d.FkStudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CourseEnrollment_Students");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.FkProfessionId).HasColumnName("(FK)ProfessionId");
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.StartDate).HasColumnType("date");

            entity.HasOne(d => d.FkProfession).WithMany(p => p.Employees)
                .HasForeignKey(d => d.FkProfessionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Employees_Profession");
        });

        modelBuilder.Entity<GradeRegistration>(entity =>
        {
            entity.ToTable("GradeRegistration");

            entity.Property(e => e.FkCourseId).HasColumnName("(FK)CourseId");
            entity.Property(e => e.FkEmployeeId).HasColumnName("(FK)EmployeeId");
            entity.Property(e => e.FkStudentId).HasColumnName("(FK)StudentId");
            entity.Property(e => e.RegistrationDate).HasColumnType("datetime");

            entity.HasOne(d => d.FkCourse).WithMany(p => p.GradeRegistrations)
                .HasForeignKey(d => d.FkCourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GradeRegistration_Course");

            entity.HasOne(d => d.FkEmployee).WithMany(p => p.GradeRegistrations)
                .HasForeignKey(d => d.FkEmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GradeRegistration_Employees");
        });

        modelBuilder.Entity<Profession>(entity =>
        {
            entity.ToTable("Profession");

            entity.Property(e => e.ProfessionTitle).HasMaxLength(50);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.Property(e => e.FirstName).HasMaxLength(20);
            entity.Property(e => e.FkClassId).HasColumnName("(FK)ClassId");
            entity.Property(e => e.FkCourseEnrollmentId).HasColumnName("(FK)CourseEnrollmentId");
            entity.Property(e => e.LastName).HasMaxLength(30);
            entity.Property(e => e.Ssn)
                .HasMaxLength(13)
                .HasColumnName("SSN");

            entity.HasOne(d => d.FkClass).WithMany(p => p.Students)
                .HasForeignKey(d => d.FkClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Students_Class");

            entity.HasOne(d => d.FkCourseEnrollment).WithMany(p => p.Students)
                .HasForeignKey(d => d.FkCourseEnrollmentId)
                .HasConstraintName("FK_Students_CourseEnrollment");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
