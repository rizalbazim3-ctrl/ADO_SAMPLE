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
            using (SqlConnection connection = _dbConnectionFactory.CreateConnection()){
                connection.Open();

                string query = "SELECT * FROM Employee";
                SqlCommand command = new SqlCommand(query,connection);
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Employee employee = new Employee();

                    employee.name = reader["name"].ToString();
                }
                return Ok();
            }
                
        }

    }
}
