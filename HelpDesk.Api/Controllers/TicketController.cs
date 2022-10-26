using HelpDesk.Core;
using HelpDesk.Services.Abstractions;
using HelpDesk.Services.Model.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HelpDesk.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        private readonly IEmployeeService _employeeService;

        public TicketController(ITicketService ticketService, IEmployeeService employeeService)
        {
            _ticketService = ticketService;
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> Find()
        {
            var tickets = await _ticketService.FindAsync();
            return Ok(tickets);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TicketRequest ticket)
        {
            var createdTicket = await _ticketService.CreateAsync(ticket);
            return Ok(createdTicket);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute]int id)
        {
            var ticket = await _ticketService.GetAsync(id);
            return Ok(ticket);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] TicketRequest ticket)
        {
            var updatedticket = await _ticketService.UpdateAsync(id, ticket);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var ticket  = _ticketService.GetAsync(id);
            await _ticketService.DeleteAsync(id);
            return Ok(ticket);
        }
    }
}
