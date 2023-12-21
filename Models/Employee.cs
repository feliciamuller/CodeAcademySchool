using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CodeAcademySchool.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int FkProfessionId { get; set; }

    public virtual Profession FkProfession { get; set; } = null!;

    public virtual ICollection<GradeRegistration> GradeRegistrations { get; set; } = new List<GradeRegistration>();

    
}
