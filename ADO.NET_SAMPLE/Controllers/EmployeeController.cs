using ADO.NET_SAMPLE.Database;
using ADO.NET_SAMPLE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace ADO.NET_SAMPLE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly DbConnectionFactory _dbConnectionFactory;
        public EmployeeController(DbConnectionFactory dbConnectionFactory)
        {
            _dbConnectionFactory = dbConnectionFactory;
        }
        [HttpGet]
        public IActionResult GetEmployees()
        {
            List<Employee> employees = new List<Employee>();
            using (SqlConnection connection = _dbConnectionFactory.CreateConnection()){
                connection.Open();

                string query = "SELECT * FROM Employee";
                SqlCommand command = new SqlCommand(query,connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Employee employee = new Employee();

                    employee.id = Convert.ToInt32(reader["id"]);

                    employee.name = reader["name"].ToString()!;
                    employee.salary = Convert.ToDecimal(reader["salary"]);

                    employees.Add(employee);
                }
                return Ok(employees);
            }
                
        }

        [HttpPost]
        public IActionResult addEmployee(Employee employee)
        {
            using (SqlConnection connection = _dbConnectionFactory.CreateConnection())
            {
                string query = "insert into Employee(name,salary) values (@name,@salary)";
                SqlCommand cmd = new SqlCommand(query,connection);

                connection.Open();

                cmd.Parameters.AddWithValue("@name", employee.name);
                cmd.Parameters.AddWithValue("@salary", employee.salary);

                int affectedRow = cmd.ExecuteNonQuery();

                if(affectedRow > 0)
                {
                    return Ok(employee);
                }
                return BadRequest("something is wrong ");
            }
        }

        [HttpDelete]
        public IActionResult deleteEmployee(int id)
        {
            using (SqlConnection connection = _dbConnectionFactory.CreateConnection())
            {
                string query = "delete from Employee where id = @id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id",id);

                connection.Open();

                int affectedRow = cmd.ExecuteNonQuery();

                if(affectedRow > 0)
                {
                    return Ok("deleted successfully");
                }

                return NotFound("The user does not esxist");
            }
        }

        [HttpPut("update/{id}")]
        public IActionResult updateEmployee(int id , Employee employee)
        {
            using(SqlConnection connection = _dbConnectionFactory.CreateConnection())
            {
                string query = "update employee set name = @name, salary = @salary where id = @id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@id",id);
                cmd.Parameters.AddWithValue("@name", employee.name);
                cmd.Parameters.AddWithValue("@salary", employee.salary);

                connection.Open();

                int affectedRow = cmd.ExecuteNonQuery();

                if(affectedRow > 0)
                {
                    return Ok("successfull");
                }
                return NotFound("something is wrong");

            }
        }

    }
}
