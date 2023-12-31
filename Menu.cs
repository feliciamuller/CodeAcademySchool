﻿
using CodeAcademySchool.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CodeAcademySchool;

internal class Menu
{
    MenuOptions menu = new MenuOptions();
    bool success;
    int input;
    public void PrintMenu()
    {
        Console.WriteLine(" _____                                                                     _____ \r" +
                        "\n( ___ )                                                                   ( ___ )\r" +
                        "\n |   |~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|   | \r" +
                        "\n |   |   ____          _         _                 _                       |   | \r" +
                        "\n |   |  / ___|___   __| | ___   / \\   ___ __ _  __| | ___ _ __ ___  _   _  |   | \r" +
                        "\n |   | | |   / _ \\ / _` |/ _ \\ / _ \\ / __/ _` |/ _` |/ _ \\ '_ ` _ \\| | | | |   | \r" +
                        "\n |   | | |__| (_) | (_| |  __// ___ \\ (_| (_| | (_| |  __/ | | | | | |_| | |   | \r" +
                        "\n |   |  \\____\\___/ \\__,_|\\___/_/   \\_\\___\\__,_|\\__,_|\\___|_| |_| |_|\\__, | |   | \r" +
                        "\n |   |                 / ___|  ___| |__   ___   ___ | |             |___/  |   | \r" +
                        "\n |   |                 \\___ \\ / __| '_ \\ / _ \\ / _ \\| |                    |   | \r" +
                        "\n |   |                  ___) | (__| | | | (_) | (_) | |                    |   | \r" +
                        "\n |   |                 |____/ \\___|_| |_|\\___/ \\___/|_|                    |   | \r" +
                        "\n |___|~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~|___| \r" +
                        "\n(_____)                                                                   (_____)");
        while (true)
        {
            Console.WriteLine("Välj i menyn vad du vill göra:");
            Console.WriteLine("");
            Console.WriteLine("1. Hämta personalinformation\n" +
            "2. Hämta elevinformation\n" +
            "3. Hämta kursinformation\n" +
            "4. Sätta betyg på elev (med transaction)\n" +//via transactions
            "5. Lägga till nya elever\n" +
            "6. Lägga till ny personal\n" +
            "0. Logga ut");
            success = int.TryParse(Console.ReadLine(), out input);
            if (!success)
            {
                Console.WriteLine("Felaktigt val, försök igen.");
                continue;
            }
            Console.Clear();
            switch (input)
            {  
                case 1:
                    menu.EmployeesInfo();
                    break;
                case 2:
                    menu.StudentInfo();
                    break;
                case 3: menu.CourseInfo();
                    break;
                case 4: menu.GradeRegTransaction();
                    break;
                case 5:
                    menu.AddStudents();
                    break;
                case 6:
                    menu.AddEmployees();
                    break;
                case 0:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Du måste skriva en siffra som finns i menyn");
                    continue;
            }
            Console.WriteLine("");
            Console.WriteLine("Tryck på valfri knapp för att gå tillbaka.");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
