using System;
using System.IO;
using System.Xml;

namespace University
{
    internal class UniversityApp
    {
        internal static void Run()
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement universityElement;

            if (File.Exists("students.xml"))
            {
                xmlDoc.Load("students.xml");
                universityElement = xmlDoc.DocumentElement;
            }
            else
            {
                universityElement = xmlDoc.CreateElement("University");
                xmlDoc.AppendChild(universityElement);
            }

            int numStudents;
            Console.Write("Enter the number of students: ");
            while (!int.TryParse(Console.ReadLine(), out numStudents) || numStudents <= 0)
            {
                Console.Write("Invalid input. Please enter a positive integer: ");
            }
            //loop for displaying student order
            for (int i = 1; i <= numStudents; i++) 
            {
                // Pass the student order to AddStudent method
                StudentManager.AddStudent(xmlDoc, universityElement, i); 
            }

            xmlDoc.Save("students.xml");
            Console.WriteLine("Student data saved to students.xml");

            // Adding an extra line for spacing
            Console.WriteLine(); 

            Console.WriteLine("\nEnter GPA or FirstName to search:");
            string searchCriteria = ReadNonEmptyString();
            StudentManager.SearchStudents(xmlDoc, searchCriteria);

            Console.WriteLine("\nEnter Student ID to delete:");
            string idToDelete = ReadNonEmptyString();
            StudentManager.DeleteStudent(xmlDoc, idToDelete);

            Console.WriteLine("\nEnter Student ID to update:");
            string idToUpdate = ReadNonEmptyString();
            // Pass the student ID to UpdateStudent method
            StudentManager.UpdateStudent(xmlDoc, idToUpdate); 

            xmlDoc.Save("students.xml");
            Console.WriteLine("Updated student data saved to students.xml");
        }

        private static string ReadNonEmptyString()
        {
            string input;
            do
            {
                input = Console.ReadLine()?.Trim();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Please enter a non-empty value.");
                }
            } while (string.IsNullOrWhiteSpace(input));
            return input;
        }
    }
}
