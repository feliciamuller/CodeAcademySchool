using System;
using System.Collections.Generic;

namespace CodeAcademySchool.Models;

public partial class Profession
{
    public int ProfessionId { get; set; }

    public string ProfessionTitle { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
