using System;
using System.Collections.Generic;

namespace CodeAcademySchool.Models;

public partial class GradeRegistration
{
    public int GradeRegistrationId { get; set; }

    public DateTime RegistrationDate { get; set; }

    public int Grade { get; set; }

    public int FkEmployeeId { get; set; }

    public int FkCourseId { get; set; }

    public int FkStudentId { get; set; }

    public virtual ICollection<CourseEnrollment> CourseEnrollments { get; set; } = new List<CourseEnrollment>();

    public virtual Course FkCourse { get; set; } = null!;

    public virtual Employee FkEmployee { get; set; } = null!;
}
