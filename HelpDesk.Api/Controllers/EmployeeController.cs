using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelpDesk.Services.Abstractions;
using HelpDesk.Services.Model.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> Find()
        {
            var employees = await _employeeService.FindAsync();
            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmployeeRequest employee)
        {
            var createdEmployee = await _employeeService.CreateAsync(employee);
            return Ok(createdEmployee);
        }
        
        [HttpGet("{id}")] // gets employee by id in route.
        public async Task<IActionResult> Get([FromRoute]int id)
        {
            var employee = await _employeeService.GetAsync(id);
            return Ok(employee);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] EmployeeRequest employee)
        {
            var updatedticket = await _employeeService.UpdateAsync(id, employee);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {

            var employee = _employeeService.GetAsync(id);
            await _employeeService.DeleteAsync(id);
            return Ok(employee);
        }
    }
}
