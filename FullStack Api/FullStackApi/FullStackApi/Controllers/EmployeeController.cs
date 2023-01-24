using FullStackApi.Data;
using FullStackApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FullStackApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : Controller
    {
        private readonly FullStackDbContext _fullStackDbContext;

        public EmployeeController(FullStackDbContext fullStackDbContext)
        {
            _fullStackDbContext = fullStackDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
           var employees = await _fullStackDbContext.Employees.ToListAsync();
            return Ok(employees);
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody]Employee employeeRequest)
        {
            employeeRequest.Id = Guid.NewGuid();
            await _fullStackDbContext.Employees.AddAsync(employeeRequest);
            await _fullStackDbContext.SaveChangesAsync();

            return Ok(employeeRequest); 

        }
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> getEmployee([FromRoute] Guid id) 
        {
           var employee = await _fullStackDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);
           
            if(employee == null)
            {
                return NotFound();
            }
            return Ok(employee);    
        }
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id,Employee updateEmployee)
        {
          var employee =  await _fullStackDbContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound(); 
            }
            employee.Name = updateEmployee.Name;
            employee.Email = updateEmployee.Email;
            employee.salary = updateEmployee.salary;
            employee.number = updateEmployee.number;
            employee.Department = updateEmployee.Department;

            await _fullStackDbContext.SaveChangesAsync();

            return Ok(employee);

        }
        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> deleteEmployee([FromRoute] Guid id)
        {
            var employee = await _fullStackDbContext.Employees.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            _fullStackDbContext.Employees.Remove(employee);
            await _fullStackDbContext.SaveChangesAsync();
            return Ok();
        }

    }

 
}
