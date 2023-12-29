using CodeAcademySchool.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace CodeAcademySchool;

internal partial class MenuOptions
{
    public void StudentInfo()
    {
        while (true)
        {
            Console.WriteLine("Vad vill du se för elevinformation?");
            Console.WriteLine("1. Visa information om alla elever\n" +
                "2. Se alla elever i en viss klass\n" +
                "3. Se betygsättning för eleverna\n" +
                "4. Visa elev (stored procedure)");

            success = int.TryParse(Console.ReadLine(), out input);
            if (!success)
            {
                Console.WriteLine("Felaktigt val, försök igen");
                continue;
            }

            switch (input)
            {
                case 1:
                    GetStudents();
                    break;
                case 2:
                    GetClass();
                    break;
                case 3:
                    GetGradeRegistration();
                    break;
                case 4:
                    SP_StudentInfo();
                    break;
                default:
                    Console.WriteLine("Du måste välja en siffra i menyn.");
                    continue;
            }
            break;
        }
    }
    public void GetStudents()
    {
        Console.WriteLine("Här är alla information om alla elever: ");
        var query = from student in context.Students
                    join schoolClass in context.Classes
                    on student.FkClassId equals schoolClass.ClassId
                    orderby student.FirstName
                    select new
                    {
                        FirstName = student.FirstName,
                        LastName = student.LastName,
                        SchoolClass = schoolClass.ClassName,
                        SSN = student.Ssn
                    };

        foreach (var item in query)
        {
            Console.WriteLine($"{item.FirstName} {item.LastName} | Personnummer: {item.SSN} | Klass: {item.SchoolClass}");
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
                //call method to get specific class
                case 1:
                    var freshman = GetStudentClass(1);
                    foreach (var student in freshman)
                    {
                        Console.WriteLine(student.FirstName + " " + student.LastName);
                    }
                    break;
                case 2:
                    var sophomore = GetStudentClass(2);

                    foreach (var student in sophomore)
                    {
                        Console.WriteLine(student.FirstName + " " + student.LastName);
                    }
                    break;
                case 3:
                    var junior = GetStudentClass(3);
                    foreach (var student in junior)
                    {
                        Console.WriteLine(student.FirstName + " " + student.LastName);
                    }
                    break;
                case 4:
                    var senior = GetStudentClass(4);
                    foreach (var student in senior)
                    {
                        Console.WriteLine(student.FirstName + " " + student.LastName);
                    }
                    break;
                default:
                    Console.WriteLine("Du måste välja en siffra i menyn");
                    continue;
            }
            break;
        }
    }
    //method that iterates over Student and return matching class
    public IEnumerable<Student> GetStudentClass(int classId)
    {
        return context.Students.Where(x => x.FkClassId == classId);
    }
    public void GetGradeRegistration()
    {
        var query = from gradeReg in context.GradeRegistrations
                    join course in context.Courses
                    on gradeReg.FkCourseId equals course.CourseId
                    join student in context.Students
                    on gradeReg.FkStudentId equals student.StudentId
                    join employee in context.Employees
                    on gradeReg.FkEmployeeId equals employee.EmployeeId
                    orderby student.FirstName

                    select new
                    {
                        TeacherFirstName = employee.FirstName,
                        TeacherLastName = employee.LastName,
                        StudentFirstName = student.FirstName,
                        StudentLastName = student.LastName,
                        Grade = gradeReg.Grade,
                        Course = course.CourseName,
                        Date = gradeReg.RegistrationDate,
                    };

        foreach (var item in query)
        {
            Console.WriteLine($"Elev: {item.StudentFirstName} {item.StudentLastName}\n" +
                $"Kurs: {item.Course}\nBetyg: {item.Grade} \nLärare som satt betyg: {item.TeacherFirstName} {item.TeacherLastName}\n" +
                $"Datum för betyg: {item.Date}\n" +
                "----------------------------------");
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
        while (true)
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
        context.Students.Add(newStudent);
        context.SaveChanges();
        Console.WriteLine("Elev har lagts till!");
    }

    public void SP_StudentInfo()
    {
        string query = "Execute SP_StudentInfo @StudentId";
        Console.WriteLine("Skriv in studentId på den student du vill hämta information från: ");
        string userInput = Console.ReadLine();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            try
            {
                //open SQL
                connection.Open();
                //adding SQLquery and SQLconnection to execute command to database
                SqlCommand command = new SqlCommand(query, connection);
                //adding userinput to command
                command.Parameters.AddWithValue("@StudentId", userInput);
                //print out data from database
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(reader["FirstName"] + " " + reader["LastName"] + " | Personnummer: " +
                            reader["SSN"]);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}


