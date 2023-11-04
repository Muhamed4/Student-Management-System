using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management_System
{
    internal class System
    {
        StudentData<Student> sdata;

        public System()
        {
            sdata = new StudentData<Student>();
        }
        public void Menu()
        {
            int op;
            do
            {
                Console.WriteLine("MENU:");
                Console.WriteLine("1: Adding Student.");
                Console.WriteLine("2: Removing Student.");
                Console.WriteLine("3: Update Data of Student.");
                Console.WriteLine("4: Display Students.");
                Console.WriteLine("5: Search Student.");
                Console.WriteLine("6: Get Average GPA For All Studetns.");
                Console.Write("Enter the operation you want to do it: ");
            } while (!int.TryParse(Console.ReadLine(), out op) || (op < 1 || op > 6));

            switch (op)
            {
                case 1:
                    AddingStudent();
                    break;
                case 2:
                    int roll;
                    do
                    {
                        Console.Write("Enter the Roll Number of the Student: ");
                    } while (!int.TryParse(Console.ReadLine(), out roll));
                    RemovingStudent(roll);
                    break;
                case 3:
                    UpdateStudent();
                    break;
                case 4:
                    DisplayingStudents();
                    break;
                case 5:
                    do
                    {
                        Console.Write("Enter the Roll Number of the Student: ");
                    } while (!int.TryParse(Console.ReadLine(), out roll));
                    SearchStudent(roll);
                    break;
                case 6:
                    AvgGPA();
                    break;
            }
        }

        private void UpdateStudent()
        {
            int rollNumber;
            do
            {
                Console.Write("Enter the Roll Number of the Student that you want to update his data: ");
            } while (!int.TryParse(Console.ReadLine(), out rollNumber));


            bool exist = sdata.Search(rollNumber);

            if (!exist)
            {
                Console.WriteLine("This Student is not Exist");
                return;
            }

            Console.WriteLine("New Data of the Student:");

            Console.Write("Enter Student Name: ");
            string? _name = Console.ReadLine();

            int age;
            do
            {
                Console.Write("Enter Student Age: ");
            } while (!int.TryParse(Console.ReadLine(), out age) || (age < 6 || age > 60));

            double gpa;
            do
            {
                Console.Write("Enter Student GPA: ");
            } while (!double.TryParse(Console.ReadLine(), out gpa) || (gpa < 0.0 || gpa > 4.0));

            int choice;
            do
            {
                Console.WriteLine("1: Under Graduated");
                Console.WriteLine("2: Graduated");
                Console.WriteLine("3: PhD Student");
                Console.Write("Choose a number from 1 - 3 : ");
            } while (!int.TryParse(Console.ReadLine(), out choice) || (choice < 1 || choice > 3));

            Status status = (Status)(choice - 1);

            sdata.UpdateStudent(rollNumber, _name, age, gpa, status);

        }

        private void AddingStudent()
        {
            Console.WriteLine("Student Data:");

            Console.Write("Enter Student Name: ");
            string? _name = Console.ReadLine();

            int rollNumber;
            do
            {
                Console.Write("Enter Student Roll Number: ");
            } while (!int.TryParse(Console.ReadLine(), out rollNumber));

            int age;
            do
            {
                Console.Write("Enter Student Age: ");
            } while (!int.TryParse(Console.ReadLine(), out age) || (age < 6 || age > 60));

            double gpa;
            do
            {
                Console.Write("Enter Student GPA: ");
            } while (!double.TryParse(Console.ReadLine(), out gpa) || (gpa < 0.0 || gpa > 4.0));

            int choice;
            do
            {
                Console.WriteLine("1: Under Graduated");
                Console.WriteLine("2: Graduated");
                Console.WriteLine("3: PhD Student");
                Console.Write("Choose a number from 1 - 3 : ");
            } while (!int.TryParse(Console.ReadLine(), out choice) || (choice < 1 || choice > 3));

            Status status = (Status)(choice - 1);

            sdata.AddStudent(_name, rollNumber, age, gpa, status);
        }

        private void RemovingStudent(int _rollNumber)
        {
            bool check = sdata.RemoveStudent(_rollNumber);

            if(check == false)
                Console.WriteLine("This Student is Not Exist, Unsuccessful Operation...!");
            else
                Console.WriteLine("Student have been Deleted, Successful Operation...");
        }

        private void DisplayingStudents()
        {
            sdata.DisplayStudents();
        }

        private void SearchStudent(int _rollNumber)
        {
            bool check = sdata.SearchStudent(_rollNumber);
            if (!check)
                Console.WriteLine("\nThis is not a valid roll number for any student...");
        }


        private void AvgGPA()
        {
            Console.WriteLine($"\n{sdata.AvgGpa()}");
        }

    }
}
