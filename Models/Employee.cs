using System;
using System.Collections.Generic;

namespace CodeAcademySchool.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int FkProfessionId { get; set; }

    public int? Salary { get; set; }

    public DateTime? StartDate { get; set; }

    public virtual Profession FkProfession { get; set; } = null!;

    public virtual ICollection<GradeRegistration> GradeRegistrations { get; set; } = new List<GradeRegistration>();
}
