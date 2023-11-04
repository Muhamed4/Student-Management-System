﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management_System
{
    internal class PhDStudent : Student, IStudent
    {
        public PhDStudent(int _rollNumber, int _age, string _name, double _gpa, Status _status):
            base(_rollNumber, _age, _name, _gpa, _status) { }


        public override int CompareTo(object? obj)
        {
            PhDStudent? Right = obj as PhDStudent;
            if (Right is null)
                return 1;
            return GPA.CompareTo(Right.GPA);
        }

        public void DisplayStudentInfo()
        {
            Console.WriteLine($"Roll Number: {RollNumber}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Age: {Age}");
            Console.WriteLine($"GPA: {GPA}");
        }
    }
}
