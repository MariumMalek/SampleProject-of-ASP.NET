

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleProject.Model;

namespace SampleProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly Data.DataContext _context;

        public DepartmentController(Data.DataContext context)
        {
            _context = context;
        }

        // GET api/project
        [HttpGet("getAllDepartment")]
        public async Task<IActionResult> GetAlllDepartments()
        {
            var departmentes = await _context.Departments
                .Select(e => new
                {
                    e.Id,
                    e.Name
                    
                })
                .ToListAsync();

            if (departmentes == null || !departmentes.Any())
            {
                return NotFound(new { Message = "No employees found" });
            }

            return Ok(departmentes);
        }




        // POST api/employee
        [HttpPost("createDepartment")]
        public async Task<IActionResult> CreateDepartment([FromBody] Department department)
        {
            if (department == null || string.IsNullOrEmpty(department.Name))
            {
                return BadRequest(new { Message = "Invalid department data" });
            }

            
            if (department.Employees == null)
            {
                department.Employees = new List<Employee>();
            }

            _context.Departments.Add(department);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreateDepartment), new { id = department.Id }, department);
        }






        // PUT api/project/{id}
        [HttpPut("UpdateDepartment/{id}")]
        public async Task<IActionResult> UpdateDepartmente(int id, Department updatedepartment)
        {

            var existingDepartment = await _context.Departments.FindAsync(id);
            if (existingDepartment == null)
            {
                return NotFound(new { Message = "Project not found" });
            }


            existingDepartment.Name = updatedepartment.Name;
            
         


            await _context.SaveChangesAsync();


            return Ok(existingDepartment);
        }



        //// DELETE api/project/{id}
        [HttpDelete("DeleteDepartment/{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {

            var department = await _context.Departments.FindAsync(id);
            if (department == null)
            {
                return NotFound(new { Message = "Project not found" });
            }


            _context.Departments.Remove(department);
            await _context.SaveChangesAsync();


            return NoContent();
        }

        
        //Search
        //GET api/project/{name}
        [HttpGet("searchDepartmentByName/{name}")]
        public async Task<IActionResult> GetDepartmentByName(string name)
        {
            var department = await _context.Departments
                .Where(e => e.Name == name)
                .Select(e => new
                {
                    e.Id,
                    e.Name
                    
                })
                .FirstOrDefaultAsync();

            if (department == null)
            {
                return NotFound(new { Message = "Department not found" });
            }

            return Ok(department);
        }

        

    }
}