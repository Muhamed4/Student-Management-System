using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Management_System
{
    internal class StudentData<T> where T: Student
    {
        static string sql = "Data Source=DESKTOP-I20ODMP\\SQLEXPRESS;Initial Catalog=StudentManagementSystem;Integrated Security=True";
        SqlConnection connection = new SqlConnection(sql);
        public StudentData() { }

        private List<Student> LoadStudentTable()
        {
            #region DataConnection&DataReader
            //string connectString = "Data Source=DESKTOP-I20ODMP\\SQLEXPRESS;Initial Catalog=BikeStores;Integrated Security=True";
            //SqlConnection con = new SqlConnection(connectString);
            //con.Open();
            //string query = "select * from production.products";
            //SqlCommand cmd = new SqlCommand(query, con);
            //SqlDataReader reader = cmd.ExecuteReader();
            //while (reader.Read())
            //{
            //    Console.WriteLine(reader["product_id"] + " " + reader["product_name"] + " " +
            //        reader["brand_id"] + " " + reader["category_id"] + " " + reader["model_year"]
            //        + " " + reader["list_price"]);
            //} 
            #endregion


            List<Student> list = new List<Student>();
            string query = "SELECT * FROM DATASTORE.Students";
            connection.Open();
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int _rollNumber = (int)reader["RollNumber"];
                string _name = (string)reader["Name"];
                int _age = (int)reader["Age"];
                decimal _gpa = (decimal)reader["GPA"];
                string _status = (string)reader["Status"];
                switch (_status)
                {
                    case "UnderGraduated":
                        UndergraduateStudent ugstudent = new UndergraduateStudent(_rollNumber, _age, _name, (double)_gpa, Status.UnderGraduated);
                        list.Add(ugstudent);
                        break;
                    case "Graduated":
                        GraduateStudent gstudent = new GraduateStudent(_rollNumber, _age, _name, (double)_gpa, Status.UnderGraduated);
                        list.Add(gstudent);
                        break;
                    case "PhDStudent":
                        PhDStudent phdstudent = new PhDStudent(_rollNumber, _age, _name, (double)_gpa, Status.UnderGraduated);
                        list.Add(phdstudent);
                        break;
                }
            }
            connection.Close();

            #region DataTable&Database
            //DataTable dataTable = new DataTable();
            //string query = "SELECT * FROM DATASTORE.StudentManagementSystem";
            //connection.Open();
            //SqlCommand cmd = new SqlCommand(query, connection);
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //da.Fill(dataTable);
            //connection.Close();
            //return dataTable; 
            #endregion

            return list;
        }

        public void AddStudent(string? name, int rollNumber, int age, double gpa, Status status)
        {
            connection.Open();
            string query = "INSERT INTO DATASTORE.Students (RollNumber, Name, Age, GPA, Status) " +
                           "VALUES(@RollNumber, @Name, @Age, @GPA, @Status)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@RollNumber", rollNumber);
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Age", age);
            cmd.Parameters.AddWithValue("@GPA", gpa);
            if(status == Status.UnderGraduated)
                cmd.Parameters.AddWithValue("@Status", "UnderGraduated");
            else if(status == Status.Graduated)
                cmd.Parameters.AddWithValue("@Status", "Graduated");
            else
                cmd.Parameters.AddWithValue("@Status", "PhDStudent");
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex) 
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public void UpdateStudent(int _rollNumber, string? name, int age, double gpa, Status status)
        {
            connection.Open();
            string query = "UPDATE DATASTORE.Students SET RollNumber=@RollNumber, Name=@Name, Age=@Age, GPA=@GPA, Status=@Status" +
                " WHERE RollNumber=@RollNumber";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@RollNumber", _rollNumber);
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Age", age);
            cmd.Parameters.AddWithValue("@GPA", gpa);
            if (status == Status.UnderGraduated)
                cmd.Parameters.AddWithValue("@Status", "UnderGraduated");
            else if (status == Status.Graduated)
                cmd.Parameters.AddWithValue("@Status", "Graduated");
            else
                cmd.Parameters.AddWithValue("@Status", "PhDStudent");

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public bool RemoveStudent(int _rollNumber)
        {
            if (!Search(_rollNumber))
                return false;

            connection.Open();
            string? query = "DELETE FROM DATASTORE.Students WHERE RollNumber = " + _rollNumber.ToString();
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.ExecuteNonQuery();
            connection.Close();

            return true;
        }

        public void DisplayStudents()
        {
            List<Student> students = LoadStudentTable();
            Console.WriteLine("\n");
            foreach (var item in students)
            {
                Console.WriteLine(item);
            }
        }

        public bool Search(int _rollNumber)
        {
            connection.Open();
            string? query = "SELECT * FROM DATASTORE.Students WHERE RollNumber = " + _rollNumber.ToString();
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
            {
                connection.Close();
                return false;
            }
            connection.Close();
            return true;

        }

        public bool SearchStudent(int _rollNumber)
        {
            connection.Open();
            string? query = "SELECT * FROM DATASTORE.Students WHERE RollNumber = " + _rollNumber.ToString();
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataReader reader = cmd.ExecuteReader();
            if (!reader.HasRows)
                return false;


            reader.Read();
            Console.WriteLine($"\n{reader["RollNumber"]} {reader["Name"]} {reader["Age"]} {reader["GPA"]} {reader["Status"]}");
            
            connection.Close();

            return true;
        }

        public decimal AvgGpa()
        {
            connection.Open();
            string? query = "SELECT AVG(GPA) as Average FROM DATASTORE.Students";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataReader reader = cmd.ExecuteReader();

            reader.Read();
            return (decimal)reader["Average"];
        }
    }
}
