using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelpDesk.Core;
using HelpDesk.Services.Model.Requests;
using HelpDesk.Services.Model.Results;

namespace HelpDesk.Services.Extensions
{
    public static class ProjectionExtensions
    {
        public static IQueryable<TicketResult> TicketToResult(this IQueryable<Ticket> query)
        {
            return query.Select(p => new TicketResult
            {
                Id = p.Id,
                Description = p.Description,
                ClientNumber = p.ClientNumber,
                Created = p.Created,
                Employee = p.Employee,
                Status = p.status
            });
        }

        public static IQueryable<EmployeeResult> EmployeeToResult(this IQueryable<Employee> query)
        {
            return query.Select(p => new EmployeeResult
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName
            });
        }
    }
}
