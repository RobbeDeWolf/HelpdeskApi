using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelpDesk.Core;
using HelpDesk.Services.Model.Requests;
using HelpDesk.Services.Model.Results;
using Vives.Services.Model;

namespace HelpDesk.Services.Abstractions
{
    public interface ITicketService
    {
        Task<TicketResult?> GetAsync(int id);
        Task<IList<TicketResult>> FindAsync();
        Task<ServiceResult<TicketResult>> CreateAsync(TicketRequest ticket);
        Task<ServiceResult<TicketResult>> UpdateAsync(int id, TicketRequest ticket);
        Task<ServiceResult> DeleteAsync(int id);
    }
}
