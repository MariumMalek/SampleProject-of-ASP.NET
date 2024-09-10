using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleProject.Model;

namespace SampleProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly Data.DataContext _context;

        public EmployeeController(Data.DataContext context)
        {
            _context = context;
        }

        // GET api/project
        [HttpGet("getAllEmployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _context.Employees
                .Select(e => new
                {
                    e.Id,
                    e.Name,
                    e.PhoneNumber,
                    e.DeptId,
                    DepartmentName = e.Dept.Name 
                })
                .ToListAsync();

            if (employees == null || !employees.Any())
            {
                return NotFound(new { Message = "No employees found" });
            }

            return Ok(employees);
        }




        // POST api/employee
        [HttpPost("CreateEmployee")]
        public async Task<IActionResult> CreateEmployee(Employee employee)
        {
            if (employee == null)
            {
                return BadRequest("Employee data is required.");
            }

            
            var department = await _context.Departments.FindAsync(employee.DeptId);
            if (department == null)
            {
                return BadRequest("The specified department does not exist.");
            }

            // Add the employee to the database
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAllEmployees), new { id = employee.Id }, employee);
        }



        // PUT api/project/{id}
        [HttpPut("UpdateEmployee/{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, Employee updatedEmployee)
        {

            var existingEmployee = await _context.Employees.FindAsync(id);
            if (existingEmployee == null)
            {
                return NotFound(new { Message = "Project not found" });
            }


            existingEmployee.Name = updatedEmployee.Name;
            existingEmployee.PhoneNumber = updatedEmployee.PhoneNumber;
            existingEmployee.DeptId = updatedEmployee.DeptId;
            


            await _context.SaveChangesAsync();


            return Ok(existingEmployee);
        }



        // DELETE api/project/{id}
        [HttpDelete("DeleteEmployee/{id}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {

            var employee = await _context.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound(new { Message = "Project not found" });
            }


            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();


            return NoContent();
        }

        

        //Search
        // GET api/project/{name}
        [HttpGet("searchByName/{name}")]
        public async Task<IActionResult> GetEmployeeByName(string name)
        {
            var employee = await _context.Employees
                .Where(e => e.Name == name)
                .Select(e => new
                {
                    e.Id,
                    e.Name,
                    e.PhoneNumber,
                    e.DeptId,
                    DepartmentName = e.Dept.Name 
                })
                .FirstOrDefaultAsync();

            if (employee == null)
            {
                return NotFound(new { Message = "Employee not found" });
            }

            return Ok(employee);
        }

        //Search
        // GET api/project/{phonenumber}

        [HttpGet("searchByPhoneNumber/{phoneNumber}")]
        public async Task<IActionResult> GetEmployeeByPhoneNumber(string phoneNumber)
        {
            var employee = await _context.Employees
                .FirstOrDefaultAsync(e => e.PhoneNumber == phoneNumber);

            if (employee == null)
            {
                return NotFound(new { Message = "Employee not found" });
            }

            return Ok(employee);
        }


        //Search
        // GET api/project/{department}
        [HttpGet("searchByDepartment/{departmentId}")]
        public async Task<IActionResult> GetEmployeesByDepartment(int departmentId)
        {
            var employees = await _context.Employees
                .Where(e => e.DeptId == departmentId)
                .Select(e => new
                {
                    e.Id,
                    e.Name,
                    e.PhoneNumber,
                    e.DeptId,
                    DepartmentName = e.Dept.Name
                })
                .ToListAsync();

            if (employees == null || employees.Count == 0)
            {
                return NotFound(new { Message = "No employees found for the given department" });
            }

            return Ok(employees);
        }







    }
}