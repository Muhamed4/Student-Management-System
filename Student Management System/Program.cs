using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Student_Management_System
{
    internal class Program
    {
        static void Main(string[] args)
        {
            System system = new System();
            system.Menu();
        }
    }
}