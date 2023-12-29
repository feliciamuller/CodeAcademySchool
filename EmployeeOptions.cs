
using CodeAcademySchool.Data;
using CodeAcademySchool.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CodeAcademySchool;

internal partial class MenuOptions
{
    bool success;
    int input;
    string connectionString = "Data Source = (localdb)\\mssqllocaldb;Database=CodeAcademySchool; Trusted_Connection=True;MultipleActiveResultSets=true";
    //instance of class to reach Entity Framework
    CodeAcademySchoolContext context = new CodeAcademySchoolContext();

    public void EmployeesInfo()
    {
        while (true)
        {
            Console.WriteLine("Vad vill du se för personalinformation?");
            Console.WriteLine("1. Visa lärare\n" +
                "2. Visa administratörer\n" +
                "3. Visa rektorer\n" +
                "4. Visa övrig personal\n" +
                "5. Visa all personal inklusive befattning och hur länge hen arbetat.\n");

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
    //call method to get employees with professionId and sum salary for each department
    public void GetTeachers()
    {
        var teachers = GetEmployee(1);
        Console.WriteLine("Lärare:");
        foreach(var teacher in teachers)
        {
            Console.WriteLine(teacher.FirstName + " " + teacher.LastName);
        }
        Console.WriteLine("");
        var teacherSalary = teachers.Sum(s => s.Salary);
        Console.WriteLine($"Totala lönen för lärarna är: {teacherSalary}");
        var average = teachers.Average(s => s.Salary);
        Console.WriteLine($"Medellönen är: {average}");
    }
    public void GetAdministators()
    {
        var administrators = GetEmployee(2);
        Console.WriteLine("Administratörer:");
        foreach(var administrator in administrators)
        {
            Console.WriteLine(administrator.FirstName + " " + administrator.LastName);
        }
        Console.WriteLine("");
        var adminSalary = administrators.Sum(s => s.Salary);
        Console.WriteLine($"Totala lönen för administratörerna är: {adminSalary}");
        var average = administrators.Average(s => s.Salary);
        Console.WriteLine($"Medellönen är: {average}");

    }
    public void GetPrincipals()
    {
        var principals = GetEmployee(3);
        Console.WriteLine("Rektorer:");
        foreach (var principal in principals)
        {
            Console.WriteLine(principal.FirstName + " " + principal.LastName);
        }
        Console.WriteLine("");
        var principalSalary = principals.Sum(s => s.Salary);
        Console.WriteLine($"Totala lönen för rektorerna är: {principalSalary}");
        var average = principals.Average(s=> s.Salary);
        Console.WriteLine($"Medellönen är: {average}");
    }
    public void GetOtherEmployees()
    {
        var otherEmp = GetEmployee(4);
        Console.WriteLine("Övrig personal:");
        foreach(var other in otherEmp)
        {
            Console.WriteLine(other.FirstName + " " + other.LastName);
        }
        Console.WriteLine("");
        var otherSalary = otherEmp.Sum(s => s.Salary);
        Console.WriteLine($"Totala lönen för övrig peronal är: {otherSalary}");
        var average = otherEmp.Average(s => s.Salary);
        Console.WriteLine($"Medellönen är: {average}");
    }
    //method that iterates over Employee and return matching proffession
    public IEnumerable<Employee> GetEmployee(int professionId)
    {
        return context.Employees.Where(x => x.FkProfessionId == professionId);
    }

    public void GetAllEmployees()
    {
        var employees = from employee in context.Employees
                        join profession in context.Professions
                        on employee.FkProfessionId equals profession.ProfessionId
                        select new
                        {
                            FirstName = employee.FirstName,
                            LastName = employee.LastName,
                            Title = profession.ProfessionTitle,
                            StartDate = employee.StartDate
                        };

        foreach (var employee in employees)
        {
            Console.WriteLine(employee.FirstName + " " + employee.LastName + " | " + employee.Title + " | Jobbat sedan: " + employee.StartDate);
        }
    }
    public void CourseInfo()
    {
        while (true)
        {
            Console.WriteLine("Vad vill du se för kursinformation?\n");
            Console.WriteLine("1. Visa alla kurser\n" +
                "2. Se högsta, lägsta och snittbetyg för kurser");

            success = int.TryParse(Console.ReadLine(), out input);
            if (!success)
            {
                Console.WriteLine("Felaktigt val, försök igen");
                continue;
            }

            switch (input)
            {
                case 1:
                    GetCourses();
                    break;
                case 2:
                    GetCourseStatistics();
                    break;
                default:
                    Console.WriteLine("Du måste välja en siffra i menyn");
                    continue;
            }
            break;
        }
    }
    public void AddEmployees()
    {
        int professionId;
        bool success;
        int salary;
        DateTime startDate;

        Console.WriteLine("Skriv in förnamn");
        var name = Console.ReadLine();
        Console.WriteLine("Skriv in efternamn");
        var lastName = Console.ReadLine();
        while (true)
        {
            Console.WriteLine("Skriv yrke, 1. Lärare, 2. Administratör, 3. Rektor, 4. Övrig befattning");
            success = int.TryParse(Console.ReadLine(), out professionId);
            if (!success || professionId > 4)
            {
                Console.WriteLine("Du måste skriva en siffra mellan 1-4");
                continue;
            }
            break;
        }
        while (true)
        {
            Console.WriteLine("Skriv in lön: ");
            success = int.TryParse(Console.ReadLine(), out salary);
            if (!success)
            {
                Console.WriteLine("Du måste skriva siffror");
                continue;
            }
            break;
        }
        while(true)
        {
            Console.WriteLine("Skriv in startdatum: ");
            success = DateTime.TryParse(Console.ReadLine(), out startDate);
            if (!success)
            {
                Console.WriteLine("Du måste skriva siffror");
                continue;
            }
            break;
        }
        var newEmployee = new Employee()
        {
            FirstName = name,
            LastName = lastName,
            FkProfessionId = professionId,
            Salary = salary,
            StartDate = startDate
        };
        context.Employees.Add(newEmployee);
        context.SaveChanges();
        Console.WriteLine("Personal har lagts till!");
    }
    public void GetCourses()
    {
        var courses = context.Courses;
        foreach (var course in courses)
        {
            Console.WriteLine(course.CourseName);
        }
    }
    public void GetCourseStatistics()
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
                        Console.WriteLine(reader["CourseName"] + " | Snittbetyg: " + reader["AverageGrade"] + " | Högsta betyg: " +
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
    public void GradeRegTransaction()
    {
        //start transaction
        using var transaction = context.Database.BeginTransaction();
        try
        {
            bool success;
            while (true)
            {
                var teachers = GetEmployee(1);
                Console.WriteLine("Skriv in ID på lärare som satte betyget:");
                foreach (var teacher in teachers)
                {
                    Console.WriteLine(teacher.EmployeeId + " " + teacher.FirstName + " " + teacher.LastName);
                }
                success = int.TryParse(Console.ReadLine(), out int empId);

                Console.WriteLine("Skriv in datum då betyget sattes:");
                success = DateTime.TryParse(Console.ReadLine(), out DateTime regDate);

                Console.WriteLine("Skriv in vilket betyg som sattes 1-5: ");
                success = int.TryParse(Console.ReadLine(), out int grade);

                Console.WriteLine("Skriv in ID på vilken student som fick betyget: ");
                var students = context.Students.ToList();
                foreach (var student in students)
                {
                    Console.WriteLine(student.StudentId + " " + student.FirstName + " " + student.LastName);
                }
                success = int.TryParse(Console.ReadLine(), out int studId);

                Console.WriteLine("Skriv in ID på vilken kurs som betyget sattes i: ");
                var courses = context.Courses.ToList();
                foreach (var course in courses)
                {
                    Console.WriteLine(course.CourseId + " " + course.CourseName);
                }
                success = int.TryParse(Console.ReadLine(), out int courseId);
        
                GradeRegistration newGradeReg = new GradeRegistration
                {
                    FkEmployeeId = empId,
                    RegistrationDate = regDate,
                    Grade = grade,
                    FkStudentId = studId,
                    FkCourseId = courseId
                };
                context.GradeRegistrations.Add(newGradeReg);
                context.SaveChanges();
                transaction.Commit();//transaction saved
                Console.WriteLine("Transaktion lyckades");
                break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine("Transaktion misslyckades");
            transaction.Rollback();//transaction is not done
        }
    }
}
