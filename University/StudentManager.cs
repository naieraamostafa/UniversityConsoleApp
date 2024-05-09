using System;
using System.Xml;

namespace University
{
    internal static class StudentManager
    {
        internal static void AddStudent(XmlDocument xmlDoc, XmlElement universityElement, int studentOrder)
        {
            XmlElement studentElement = xmlDoc.CreateElement("Student");
            universityElement.AppendChild(studentElement);

            //Student ID
            string id;
            bool idExists;
            do
            {
                Console.Write($"Enter Student ID {studentOrder}: ");
                id = Console.ReadLine();
                idExists = xmlDoc.SelectSingleNode($"//Student[@ID='{id}']") != null;

                if (string.IsNullOrWhiteSpace(id) || idExists)
                {
                    Console.WriteLine("Student ID cannot be empty or already exists.");
                }
            } while (string.IsNullOrWhiteSpace(id) || idExists);
            studentElement.SetAttribute("ID", id);

            //First Name
            string firstName;
            do
            {
                Console.Write($"Enter First Name {studentOrder}: ");
                firstName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(firstName) || !IsAllLetters(firstName))
                {
                    Console.WriteLine("First Name must contain only letters (a-z) and cannot be empty.");
                }
            } while (string.IsNullOrWhiteSpace(firstName) || !IsAllLetters(firstName));
            XmlElement firstNameElement = xmlDoc.CreateElement("FirstName");
            firstNameElement.InnerText = firstName;
            studentElement.AppendChild(firstNameElement);

            //Last Name
            string lastName;
            do
            {
                Console.Write($"Enter Last Name {studentOrder}: ");
                lastName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(lastName) || !IsAllLetters(lastName))
                {
                    Console.WriteLine("Last Name must contain only letters (a-z) and cannot be empty.");
                }
            } while (string.IsNullOrWhiteSpace(lastName) || !IsAllLetters(lastName));
            XmlElement lastNameElement = xmlDoc.CreateElement("LastName");
            lastNameElement.InnerText = lastName;
            studentElement.AppendChild(lastNameElement);

            //Gender
            Console.Write($"Enter Gender {studentOrder}: ");
            string gender = Console.ReadLine();
            XmlElement genderElement = xmlDoc.CreateElement("Gender");
            genderElement.InnerText = gender;
            studentElement.AppendChild(genderElement);

            // GPA
            string gpa;
            do
            {
                Console.Write($"Enter GPA {studentOrder}: ");
                gpa = Console.ReadLine();

                if (!IsGpaValid(gpa))
                {
                    Console.WriteLine("GPA must be a number between 0 and 4.");
                }
            } while (!IsGpaValid(gpa));
            XmlElement gpaElement = xmlDoc.CreateElement("GPA");
            gpaElement.InnerText = gpa;
            studentElement.AppendChild(gpaElement);

            //Level
            Console.Write($"Enter Level {studentOrder}: ");
            string level = Console.ReadLine();
            XmlElement levelElement = xmlDoc.CreateElement("Level");
            levelElement.InnerText = level;
            studentElement.AppendChild(levelElement);

            //Address
            string address;
            do
            {
                Console.Write($"Enter Address {studentOrder}: ");
                address = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(address) || !IsAllLetters(address))
                {
                    Console.WriteLine("Address must contain only letters (a-z) and cannot be empty.");
                }
            } while (string.IsNullOrWhiteSpace(address) || !IsAllLetters(address));
            XmlElement addressElement = xmlDoc.CreateElement("Address");
            addressElement.InnerText = address;
            studentElement.AppendChild(addressElement);
        }

        internal static void SearchStudents(XmlDocument xmlDoc, string searchCriteria)
        {
            XmlNodeList studentNodes = xmlDoc.SelectNodes($"//Student[GPA='{searchCriteria}' or FirstName='{searchCriteria}']");
            if (studentNodes.Count > 0)
            {
                Console.WriteLine("\nSearch Results:");
                foreach (XmlNode studentNode in studentNodes)
                {
                    Console.WriteLine($"Student ID: {studentNode.Attributes["ID"].Value}");
                    Console.WriteLine($"First Name: {studentNode.SelectSingleNode("FirstName").InnerText}");
                    Console.WriteLine($"Last Name: {studentNode.SelectSingleNode("LastName").InnerText}");
                    Console.WriteLine($"Gender: {studentNode.SelectSingleNode("Gender").InnerText}");
                    Console.WriteLine($"GPA: {studentNode.SelectSingleNode("GPA").InnerText}");
                    Console.WriteLine($"Level: {studentNode.SelectSingleNode("Level").InnerText}");
                    Console.WriteLine($"Address: {studentNode.SelectSingleNode("Address").InnerText}\n");
                }
            }
            else
            {
                Console.WriteLine("No matching records found.");
            }
        }

        internal static void DeleteStudent(XmlDocument xmlDoc, string idToDelete)
        {
            XmlNode studentNode = xmlDoc.SelectSingleNode($"//Student[@ID='{idToDelete}']");
            if (studentNode != null)
            {
                studentNode.ParentNode.RemoveChild(studentNode);
                Console.WriteLine("Student record deleted successfully.");
            }
            else
            {
                Console.WriteLine("Student ID not found.");
            }
        }

        internal static void UpdateStudent(XmlDocument xmlDoc, string idToUpdate)
        {
            XmlNode studentNode = xmlDoc.SelectSingleNode($"//Student[@ID='{idToUpdate}']");
            if (studentNode == null)
            {
                Console.WriteLine("Student ID not found.");
                return;
            }

            Console.WriteLine("Enter the new details (leave blank to preserve existing values):");

            Console.Write("Enter First Name: ");
            string firstName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(firstName))
            {
                XmlElement firstNameElement = studentNode.SelectSingleNode("FirstName") as XmlElement;
                if (firstNameElement != null)
                {
                    firstNameElement.InnerText = firstName;
                }
                else
                {
                    firstNameElement = xmlDoc.CreateElement("FirstName");
                    firstNameElement.InnerText = firstName;
                    studentNode.AppendChild(firstNameElement);
                }
            }

            Console.Write("Enter Last Name: ");
            string lastName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(lastName))
            {
                XmlElement lastNameElement = studentNode.SelectSingleNode("LastName") as XmlElement;
                if (lastNameElement != null)
                {
                    lastNameElement.InnerText = lastName;
                }
                else
                {
                    lastNameElement = xmlDoc.CreateElement("LastName");
                    lastNameElement.InnerText = lastName;
                    studentNode.AppendChild(lastNameElement);
                }
            }

            Console.Write("Enter Gender: ");
            string gender = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(gender))
            {
                XmlElement genderElement = studentNode.SelectSingleNode("Gender") as XmlElement;
                if (genderElement != null)
                {
                    genderElement.InnerText = gender;
                }
                else
                {
                    genderElement = xmlDoc.CreateElement("Gender");
                    genderElement.InnerText = gender;
                    studentNode.AppendChild(genderElement);
                }
            }

            Console.Write("Enter GPA: ");
            string gpa = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(gpa))
            {
                XmlElement gpaElement = studentNode.SelectSingleNode("GPA") as XmlElement;
                if (gpaElement != null)
                {
                    gpaElement.InnerText = gpa;
                }
                else
                {
                    gpaElement = xmlDoc.CreateElement("GPA");
                    gpaElement.InnerText = gpa;
                    studentNode.AppendChild(gpaElement);
                }
            }

            Console.Write("Enter Level: ");
            string level = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(level))
            {
                XmlElement levelElement = studentNode.SelectSingleNode("Level") as XmlElement;
                if (levelElement != null)
                {
                    levelElement.InnerText = level;
                }
                else
                {
                    levelElement = xmlDoc.CreateElement("Level");
                    levelElement.InnerText = level;
                    studentNode.AppendChild(levelElement);
                }
            }

            Console.Write("Enter Address: ");
            string address = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(address))
            {
                XmlElement addressElement = studentNode.SelectSingleNode("Address") as XmlElement;
                if (addressElement != null)
                {
                    addressElement.InnerText = address;
                }
                else
                {
                    addressElement = xmlDoc.CreateElement("Address");
                    addressElement.InnerText = address;
                    studentNode.AppendChild(addressElement);
                }
            }

            xmlDoc.Save("students.xml");
            Console.WriteLine("Student data updated successfully.");
        }

        private static bool IsAllLetters(string input)
        {
            foreach (char c in input)
            {
                if (!char.IsLetter(c))
                {
                    return false;
                }
            }
            return true;
        }

        private static bool IsGpaValid(string gpa)
        {
            if (!double.TryParse(gpa, out double gpaValue))
            {
                return false;
            }
            return gpaValue >= 0 && gpaValue <= 4;
        }
    }
}
