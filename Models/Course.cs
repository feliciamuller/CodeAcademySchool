using System;
using System.Collections.Generic;

namespace CodeAcademySchool.Models;

public partial class Course
{
    public int CourseId { get; set; }

    public string CourseName { get; set; } = null!;

    public virtual ICollection<CourseEnrollment> CourseEnrollments { get; set; } = new List<CourseEnrollment>();

    public virtual ICollection<GradeRegistration> GradeRegistrations { get; set; } = new List<GradeRegistration>();
}
