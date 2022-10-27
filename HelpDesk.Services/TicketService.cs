using HelpDesk.Core;
using HelpDesk.Services.Abstractions;
using HelpDesk.Services.Extensions;
using HelpDesk.Services.Model.Requests;
using HelpDesk.Services.Model.Results;
using Microsoft.EntityFrameworkCore;
using Vives.Services.Model;
using Vives.Services.Model.Extensions;

namespace HelpDesk.Services
{
    public class TicketService :ITicketService
    {
        private readonly HelpDeskDbContext _dbContext;

        public TicketService(HelpDeskDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TicketResult?> GetAsync(int id)
        {
            var ticket = _dbContext.Tickets
                .TicketToResult()
                .AsNoTracking()
                .SingleOrDefault(p => p.Id == id);

            return ticket;
        }

        public async Task<IList<TicketResult>> FindAsync()
        {
            var tickets = await _dbContext.Tickets
                .TicketToResult()
                .AsNoTracking()
                .ToListAsync();

            return tickets;
        }

        public async Task<ServiceResult<TicketResult>> CreateAsync(TicketRequest ticket)
        {
            if (ticket.Description == "test")
            {
                var serviceresult = new ServiceResult<TicketResult>();
                serviceresult.Messages.Add(new ServiceMessage
                {
                    Code = "TesterDetected",
                    Message = "No need for testing here... :D",
                    Type = ServiceMessageType.Error
                });
                return serviceresult;
            }
            Ticket newticket = new Ticket
            {
                ClientNumber = ticket.ClientNumber,
                Description = ticket.Description,
                Created = DateTime.UtcNow,
                status = TicketStatus.Open
            };
            _dbContext.Tickets.Add(newticket);
            await _dbContext.SaveChangesAsync();
            var ticketresult = await GetAsync(newticket.Id);

            var succesServiceResult = new ServiceResult<TicketResult>(ticketresult);
            
            return succesServiceResult;
        }

        public async Task<ServiceResult<TicketResult>> UpdateAsync(int id, TicketRequest ticket)
        {
            var dbTicket = await _dbContext.Tickets.SingleOrDefaultAsync(p => p.Id == id);

            if (dbTicket is null)
            {
                return new ServiceResult<TicketResult>().NotFound(nameof(ticket));
            }

            dbTicket.Description = ticket.Description;
            dbTicket.ClientNumber = ticket.ClientNumber;
            dbTicket.status = ticket.Status;
            await _dbContext.SaveChangesAsync();

            var tckt = await GetAsync(id);
            return new ServiceResult<TicketResult>(tckt);
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var serviceResult = new ServiceResult();
            var ticket = new Ticket() { Id = id };
            _dbContext.Tickets.Attach(ticket);

            _dbContext.Tickets.Remove(ticket); 
            var changes = await _dbContext.SaveChangesAsync();

            if (changes == 0)
            {
              serviceResult.Messages.Add(new ServiceMessage
              {
                  Code = "NothingChanged",
                  Message = "Something went wrong... the ticket couldnt be removed."
              }); 
            }
            return serviceResult;
        }

        public async Task<ServiceResult> AssignEmployee(int ticketid, int employeeid)
        {
            var serviceresult = new ServiceResult();
            var employee = _dbContext.Employees.SingleOrDefault(p => p.Id == employeeid);
            if (employee == null)
            {
                serviceresult.Messages.Add(new ServiceMessage
                {
                    Code = "EmployeeNotFound",
                    Message = "Employee unknown",
                    Type = ServiceMessageType.Error
                });
                return serviceresult;
            }
            var ticket = _dbContext.Tickets.SingleOrDefault(p => p.Id == ticketid);
            if (ticket == null)
            {
                serviceresult.Messages.Add(new ServiceMessage
                {
                    Code = "TicketNotFound",
                    Message = "Ticket unknown",
                    Type = ServiceMessageType.Error
                });
                return serviceresult;
            }
            
           ticket.Employee = employee;
           var changes = await _dbContext.SaveChangesAsync();
           if (changes == 0)
           {
               serviceresult.Messages.Add(new ServiceMessage
               {
                   Code ="NothingChanged",
                   Message = "Nothing changed in the database"
               });
           }

           return serviceresult;
        }
        
        public async Task<ServiceResult<List<TicketResult>>> GetTicketsOfEmployee(int employeeid)
        {
            var employee = _dbContext.Employees.SingleOrDefault(p => p.Id == employeeid);
            if (employee == null)
            {
                return new ServiceResult<List<TicketResult>>().NotFound(nameof(employeeid));
            }

            var tickets = await _dbContext.Tickets.Where(p => p.Employee.Id == employeeid).TicketToResult().ToListAsync();

            return new ServiceResult<List<TicketResult>>(tickets);
        }
    }
}
