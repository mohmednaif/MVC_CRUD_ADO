using Infrastructure;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Service
{
    public class EmployeeService : IEmployeeService
    {
        string connStr = ConfigurationManager.ConnectionStrings["MVC_CRUD"].ConnectionString;

        public bool DeleteEmployeeById(int id)
        {
            string query = "DELETE FROM Employee WHERE Id=@Id";
            using (SqlConnection con = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            return true;
        }

        public List<Department> GetAllDepartments()
        {
            List<Department> departments = new List<Department>();

            string query = "select * from department";
            using (SqlConnection con = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataReader sqlReader = cmd.ExecuteReader();
                    while (sqlReader.Read())
                    {
                        Department department = new Department();
                        department.Id = sqlReader.GetInt32(0);
                        department.DepartmentName = sqlReader.GetString(1);

                        departments.Add(department);
                    }
                    sqlReader.Close();
                    con.Close();
                }
            }

            return departments;
        }

        public List<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();

            string query = "select * from Employee e join Department d on e.DepartmentId=d.Id";
            using (SqlConnection con = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    DataTable DT = new DataTable();
                    using (SqlDataAdapter DA = new SqlDataAdapter(cmd))
                    {
                        DA.Fill(DT);
                    }

                    foreach (DataRow row in DT.Rows)
                    {
                        Employee employee = new Employee();

                        employee.Id = Convert.ToInt32(row["Id"]);
                        employee.DepartmentId = Convert.ToInt32(row["DepartmentId"]);
                        employee.FirstName = Convert.ToString(row["FirstName"]);
                        employee.LastName = Convert.ToString(row["LastName"]);
                        employee.Email = Convert.ToString(row["Email"]);
                        employee.Phone = Convert.ToString(row["Phone"]);
                        employee.BirthDate = Convert.ToDateTime(row["BirthDate"]);
                        employee.Gender = Convert.ToByte(row["Gender"]);
                        employee.Address = Convert.ToString(row["Address"]);
                        employee.Department.Id = Convert.ToInt32(row["DepartmentId"]);
                        employee.Department.DepartmentName = Convert.ToString(row["DepartmentName"]);

                        employees.Add(employee);
                    }

                }
            }

            return employees;
        }

        public Employee GetEmployeeById(int id)
        {
            Employee employee = new Employee();
            string query = "select * from Employee e join Department d on e.DepartmentId=d.Id where e.Id=@Id";
            using (SqlConnection con = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    con.Open();
                    SqlDataReader sqlReader = cmd.ExecuteReader();
                    while (sqlReader.Read())
                    {
                        employee.Id = Convert.ToInt32(sqlReader["Id"]);
                        employee.DepartmentId = Convert.ToInt32(sqlReader["DepartmentId"]);
                        employee.FirstName = Convert.ToString(sqlReader["FirstName"]);
                        employee.LastName = Convert.ToString(sqlReader["LastName"]);
                        employee.Email = Convert.ToString(sqlReader["Email"]);
                        employee.Phone = Convert.ToString(sqlReader["Phone"]);
                        employee.BirthDate = Convert.ToDateTime(sqlReader["BirthDate"]);
                        employee.Gender = Convert.ToByte(sqlReader["Gender"]);
                        employee.Address = Convert.ToString(sqlReader["Address"]);
                        employee.Department.Id = Convert.ToInt32(sqlReader["DepartmentId"]);
                        employee.Department.DepartmentName = Convert.ToString(sqlReader["DepartmentName"]);
                    }
                    sqlReader.Close();
                    con.Close();
                }
            }
            return employee;
        }

        public int SaveEmployee(Employee employee)
        {
            int result = 0;
            if (employee.Id == 0)
            {

                string query = "INSERT INTO Employee(DepartmentId,FirstName,LastName,Phone,Email,Address,BirthDate,Gender) VALUES(@DepartmentId, @FirstName,@LastName,@Phone,@Email,@Address,@BirthDate,@Gender)";
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Parameters.AddWithValue("@DepartmentId", employee.DepartmentId);
                        cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                        cmd.Parameters.AddWithValue("@Phone", employee.Phone);
                        cmd.Parameters.AddWithValue("@Email", employee.Email);
                        cmd.Parameters.AddWithValue("@Address", employee.Address);
                        cmd.Parameters.AddWithValue("@BirthDate", employee.BirthDate);
                        cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                        cmd.Connection = con;
                        con.Open();
                        result = cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            else
            {
                string query = @"update Employee set DepartmentId=@DepartmentId,FirstName=@FirstName,LastName=@LastName,
                                 Phone=@Phone,Email=@Email,Address=@Address,BirthDate=@BirthDate,Gender=@Gender where Id=@Id";
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand(query))
                    {
                        cmd.Parameters.AddWithValue("@Id", employee.Id);
                        cmd.Parameters.AddWithValue("@DepartmentId", employee.DepartmentId);
                        cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                        cmd.Parameters.AddWithValue("@Phone", employee.Phone);
                        cmd.Parameters.AddWithValue("@Email", employee.Email);
                        cmd.Parameters.AddWithValue("@Address", employee.Address);
                        cmd.Parameters.AddWithValue("@BirthDate", employee.BirthDate);
                        cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                        cmd.Connection = con;
                        con.Open();
                        result = cmd.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }

            return result;
        }




    }
}
