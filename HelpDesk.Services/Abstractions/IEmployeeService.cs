using HelpDesk.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelpDesk.Services.Model.Requests;
using HelpDesk.Services.Model.Results;

namespace HelpDesk.Services.Abstractions
{
    public interface IEmployeeService
    {
        Task<EmployeeResult?> GetAsync(int id);
        Task<IList<EmployeeResult>> FindAsync();
        Task<EmployeeResult> CreateAsync(EmployeeRequest employee);
        Task<EmployeeResult> UpdateAsync(int id, EmployeeRequest employee );
        Task<EmployeeResult> DeleteAsync(int id);

    }
}
