using System;
using System.Collections.Generic;

namespace CodeAcademySchool.Models;

public partial class CourseEnrollment
{
    public int CourseEnrollmentId { get; set; }

    public int FkStudentId { get; set; }

    public int FkGradeRegistration { get; set; }

    public int FkCourseId { get; set; }

    public virtual Course FkCourse { get; set; } = null!;

    public virtual Student FkStudent { get; set; } = null!;
}
