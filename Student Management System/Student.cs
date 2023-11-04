using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management_System
{
    internal abstract class Student: IComparable
    {
        public string? Name { get; set; }
        public int Age { get; set; }
        public int RollNumber { get; set; }
        public double GPA { get; set; }

        Status status;

        public Student(int _rollNumber, int _age, string _name, double _gpa, Status _status)
        {
            RollNumber = _rollNumber;
            Age = _age;
            Name = _name;
            GPA = _gpa;
            status = _status;
        }

        public abstract int CompareTo(object? obj);

        public static bool operator<(Student Left, Student Right)
        {
            return Left.GPA < Right.GPA;
        }

        public static bool operator>(Student Left, Student Right)
        {
            return Left.GPA > Right.GPA;
        }

        public override string ToString()
            => $"{RollNumber} {Name} {Age} {GPA} {status}";

    }
}
