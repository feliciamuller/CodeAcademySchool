using CodeAcademySchool.Data;
using CodeAcademySchool.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CodeAcademySchool;

internal class MenuOptions
{
    bool success;
    int input;
    string connectionString = "Data Source = (localdb)\\mssqllocaldb;Database=CodeAcademySchool; Trusted_Connection=True;MultipleActiveResultSets=true";
    //instance of class to reach Entity Framework
    CodeAcademySchoolContext context = new CodeAcademySchoolContext();

    public void GetEmployees()
    {
        while (true)
        {
            Console.WriteLine("Vilken personal vill du se?");
            Console.WriteLine("1. Lärare\n" +
                "2. Administratörer\n" +
                "3. Rektor\n" +
                "4. Övrig personal\n" +
                "5. All personal");

        success = int.TryParse(Console.ReadLine(), out input);
        if (!success)
        {
            Console.WriteLine("Felaktigt val, försök igen");
            continue;
        }
        switch (input)
        {
            case 1:
                GetTeachers();
                break;
            case 2:
                GetAdministators();
                break;
            case 3:
                GetPrincipals();
                break;
            case 4:
                GetOtherEmployees();
                break;
            case 5:
                GetAllEmployees();
                break;
            default:
                Console.WriteLine("Du måste skriva en siffra som finns i menyn");
                continue;
            }
            break;
        }
    }
    //methods to get employees with specifik professionId
    public void GetTeachers()
    {
        var teachers = context.Employees.Where(t => t.FkProfessionId == 1);
        foreach(var teacher in teachers)
        {
            Console.WriteLine(teacher.FirstName + " " + teacher.LastName);
        }
    }
    public void GetAdministators()
    {
        var administrators = context.Employees.Where(a => a.FkProfessionId == 2);
        foreach(var administrator in administrators)
        {
            Console.WriteLine(administrator.FirstName + " " + administrator.LastName);
        }
    }
    public void GetPrincipals()
    {
        var principals = context.Employees.Where(p => p.FkProfessionId == 3);
        foreach (var principal in principals)
        {
            Console.WriteLine(principal.FirstName + " " + principal.LastName);
        }
    }
    public void GetOtherEmployees()
    {
        var otherEmp = context.Employees.Where(o => o.FkProfessionId == 4);
        foreach(var other in otherEmp)
        {
            Console.WriteLine(other.FirstName + " " + other.LastName);
        }
    }
    public void GetAllEmployees()
    {
        var employees = context.Employees;
        foreach (var employee in employees)
        {
            Console.WriteLine(employee.FirstName + " " + employee.LastName);
        }
    }
    public void GetStudents()
    {
        while (true)
        {
            Console.WriteLine("Hur vill du sortera eleverna?\n" +
           "1. Förnamn\n" +
           "2. Efternamn");
            success = int.TryParse(Console.ReadLine(), out input);
            if (!success)
            {
                Console.WriteLine("Felaktigt val, försök igen.");
                continue;
            }
            switch (input)
            {
                case 1:
                    StudentFirstName();
                    break;
                case 2:
                    StudentLastName();
                    break;
                default:
                    Console.WriteLine("Du måste välja en siffra i menyn");
                    continue;
            }
            break;
        }
    }
    public void StudentFirstName()
    {
        while (true)
        {
            Console.WriteLine("Vill du sortera förnamnen på stigande eller fallande ordning?\n" +
             "1. Stigande\n" +
             "2. Fallande ");
            success = int.TryParse(Console.ReadLine(), out input);
            if(!success)
            {
                Console.WriteLine("Felaktigt val, försök igen");
                continue;
            }
            switch (input)
            {
                case 1:
                    //order students by ascending
                    var studentsAsc = context.Students.OrderBy(x => x.FirstName);
                    foreach (var student in studentsAsc)
                    {
                        Console.WriteLine(student.FirstName);
                    }
                    break;
                case 2:
                    //order students by descending
                    var studentsDesc = context.Students.OrderByDescending(x => x.FirstName);
                    foreach (var student in studentsDesc)
                    {
                        Console.WriteLine(student.FirstName);
                    }
                    break;
                default:
                    Console.WriteLine("Du måste välja en siffra i menyn");
                    continue;
            }
            break;
        }
    }
    public void StudentLastName()
    {
        while (true)
        {
            Console.WriteLine("Vill du sortera efternamnen på stigande eller fallande ordning?\n" +
             "1. Stigande\n" +
             "2. Fallande ");
            success = int.TryParse(Console.ReadLine(), out input);
            if(!success)
            {
                Console.WriteLine("Felaktigt val, försök igen.");
                continue;
            }
            switch (input)
            {
                case 1:
                    var studentsAsc = context.Students.OrderBy(x => x.LastName);
                    foreach (var student in studentsAsc)
                    {
                        Console.WriteLine(student.LastName);
                    }
                    break;
                case 2:
                    var studentsDesc = context.Students.OrderByDescending(x => x.LastName);
                    foreach (var student in studentsDesc)
                    {
                        Console.WriteLine(student.LastName);
                    }
                    break;
                default:
                    Console.WriteLine("Du måste välja en siffra i menyn");
                    continue;
            }
            break;
        }
    }
    public void GetClass()
    {
        while (true)
        {
        Console.WriteLine("Välj klass:\n" +
        "1. Freshman\n" +
        "2. Sophomore\n" +
        "3. Junior\n" +
        "4. Senior");
        success = int.TryParse(Console.ReadLine(), out input);
        if (!success)
        {
            Console.WriteLine("Felaktigt val, försök igen.");
            continue;
        }
            switch (input)
            {
                //get students by specifik classId
                case 1:
                    var freshman = context.Students.Where(x => x.FkClassId == 1);
                    foreach(var student in freshman)
                    {
                        Console.WriteLine(student.FirstName);
                    }
                    break;
                case 2:
                    var sophomore = context.Students.Where(x => x.FkClassId == 2);

                    foreach (var student in sophomore)
                    {
                        Console.WriteLine(student.FirstName);
                    }
                    break;
                case 3:
                    var junior = context.Students.Where(x => x.FkClassId == 3);
                    foreach (var student in junior)
                    {
                        Console.WriteLine(student.FirstName);
                    }
                    break;
                case 4:
                    var senior = context.Students.Where(x => x.FkClassId == 4);
                    foreach (var student in senior)
                    {
                        Console.WriteLine(student.FirstName);
                    }
                    break;
                default:
                    Console.WriteLine("Du måste välja en siffra i menyn");
                    continue;
            }
            break;
        }
    }
    public void GetGradeRegistration()
    {
        //object to save a specific date
        DateTime firstOfDecember = new DateTime(2023, 12, 1);
        
        //join classes with keys and print out data
        var query = from graderegistration in context.GradeRegistrations
                     join student in context.Students on graderegistration.FkStudentId equals student.StudentId
                     join course in context.Courses on graderegistration.FkCourseId equals course.CourseId
                     where graderegistration.RegistrationDate < firstOfDecember
                     select new
                     {
                         RegDate = graderegistration.RegistrationDate,
                         Grading = graderegistration.Grade,
                         Name = student.FirstName,
                         Title = course.CourseName
                     };
        foreach (var item in query)
        {
            Console.WriteLine($"Kurs: {item.Title} | Elev: {item.Name} | Betyg: {item.Grading} | Datum: {item.RegDate}");
        }
    }
    public void AverageGrade()
    {
        //sql query to join classes with keys to get gradings in courses
        string query = "SELECT Course.CourseName, AVG(Grade) AS AverageGrade, MIN(Grade) AS TopGrade, MAX(Grade) AS LowestGrade" +
            "\nFROM GradeRegistration" +
            "\nJOIN Course ON Course.CourseId = GradeRegistration.[(FK)CourseId]" +
            "\nGROUP BY CourseName ";

        //object of SqlConnection class with declared connectionstring
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            try
            {
                //open SQL
                connection.Open();
                //adding SQLquery and SQLconnection to execute command to database
                SqlCommand command = new SqlCommand(query, connection);
                //print out data from database
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("Kurs: " + reader["CourseName"] + " | Snittbetyg: " + reader["AverageGrade"] + " | Högsta betyg: " +
                            reader["TopGrade"] + " | Lägsta betyg: " + reader["LowestGrade"]);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
    public void AddStudents()
    {
        int schoolclass;
        Console.WriteLine("Skriv in förnamn");
        var name = Console.ReadLine();
        Console.WriteLine("Skriv in efternamn");
        var lastName = Console.ReadLine();
        Console.WriteLine("Skriv in personnummer enligt XXXX-XX-XX");
        var ssn = Console.ReadLine();
        while(true)
        {
            Console.WriteLine("Skriv in siffran för klassen hen ska gå i.\n1. Freshman, 2. Sophomore 3. Junior 4. Senior");
            bool success = int.TryParse(Console.ReadLine(), out schoolclass);
            if (!success || schoolclass > 4)
            {
                Console.WriteLine("Du måste skriva en siffra mellan 1-4");
            }
            else 
            {
                break;
            }
        }
        //object to add new student with user input
        var newStudent = new Student()
        {
            FirstName = name,
            LastName = lastName,
            Ssn = ssn,
            FkClassId = schoolclass
        };
        //adding new student to database through Entity Framework
        context.Add(newStudent);
        context.SaveChanges();
        Console.WriteLine("Elev har lagts till!");
    }
    public void AddEmployees()
    {
        int professionId;
        Console.WriteLine("Skriv in förnamn");
        var name = Console.ReadLine();
        Console.WriteLine("Skriv in efternamn");
        var lastName = Console.ReadLine();
        while(true)
        {
            Console.WriteLine("Skriv yrke, 1. Lärare, 2. Administratör, 3. Rektor, 4. Övrig befattning");
            bool success = int.TryParse(Console.ReadLine(), out professionId);
            if(!success || professionId > 4)
            {
                Console.WriteLine("Du måste skriva en siffra mellan 1-4");
            }
            else
            {
                break;
            }
        }
        var newEmployee = new Employee()
        {
            FirstName = name,
            LastName = lastName,
            FkProfessionId = professionId
        };
        context.Employees.Add(newEmployee);
        context.SaveChanges();
        Console.WriteLine("Personal har lagts till!");
    }
}
