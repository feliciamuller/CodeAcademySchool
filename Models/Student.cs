using System;
using System.Collections.Generic;

namespace CodeAcademySchool.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public string Ssn { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int FkClassId { get; set; }

    public int? FkCourseEnrollmentId { get; set; }

    public virtual ICollection<CourseEnrollment> CourseEnrollments { get; set; } = new List<CourseEnrollment>();

    public virtual Class FkClass { get; set; } = null!;

    public virtual Course? FkCourseEnrollment { get; set; }
}
