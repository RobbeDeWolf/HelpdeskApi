using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelpDesk.Core;
using HelpDesk.Services.Abstractions;
using HelpDesk.Services.Extensions;
using HelpDesk.Services.Model.Requests;
using HelpDesk.Services.Model.Results;
using Microsoft.EntityFrameworkCore;

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

        public async Task<TicketResult> CreateAsync(TicketRequest ticket)
        {
            Ticket newticket = new Ticket
            {
                ClientNumber = ticket.ClientNumber,
                Description = ticket.Description,
                Created = DateTime.UtcNow,
                status = TicketStatus.Open,
                Employeeid = null
            };
            _dbContext.Tickets.Add(newticket);
            await _dbContext.SaveChangesAsync();
            var ticketresult = await GetAsync(newticket.Id);
            return ticketresult;
        }

        public async Task<TicketResult> UpdateAsync(int id, TicketRequest ticket)
        {
            var dbTicket = await _dbContext.Tickets.SingleOrDefaultAsync(p => p.Id == id);

            if (dbTicket is null)
            {
                return new TicketResult();
            }

            dbTicket.Description = ticket.Description;
            dbTicket.ClientNumber = ticket.ClientNumber;
            dbTicket.status = ticket.Status;
            dbTicket.Employeeid = ticket.Employeeid;
            await _dbContext.SaveChangesAsync();

            var tckt = await GetAsync(id);
            return tckt;
        }

        public async Task<TicketResult> DeleteAsync(int id)
        {
            var ticket = new Ticket() { Id = id };
            _dbContext.Tickets.Attach(ticket);

            _dbContext.Tickets.Remove(ticket); 
            await _dbContext.SaveChangesAsync();

            TicketResult result = new TicketResult() { Id = id };
            return result;
        }
    }
}
