using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class EmployeeService : IEmployeeService
    {
        private readonly HelpDeskDbContext _dbContext;

        public EmployeeService(HelpDeskDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<EmployeeResult> GetAsync(int id)
        {
            var employee = _dbContext.Employees
                .EmployeeToResult()
                .AsNoTracking()
                .SingleOrDefault(p => p.Id == id);
            return employee;
        }

        public async Task<IList<EmployeeResult>> FindAsync()
        {
            var employees = await _dbContext.Employees
                .EmployeeToResult()
                .AsNoTracking()
                .ToListAsync();

            return employees;
        }

        public async Task<ServiceResult<EmployeeResult>> CreateAsync(EmployeeRequest employee)
        {
            var newemployee = new Employee()
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
            };

            _dbContext.Employees.Add(newemployee);
            await _dbContext.SaveChangesAsync();
            var employeeresult = await GetAsync(newemployee.Id);

            var succesServiceResult = new ServiceResult<EmployeeResult>(employeeresult);
            return succesServiceResult;
        }

        public async Task<ServiceResult<EmployeeResult>> UpdateAsync(int id, EmployeeRequest employee)
        {
           var dbEmployee = await _dbContext.Employees.SingleOrDefaultAsync(p => p.Id == id);

           if (dbEmployee is null)
           {
               return new ServiceResult<EmployeeResult>().NotFound(nameof(employee));
           }

           dbEmployee.FirstName = employee.FirstName;
           dbEmployee.LastName = employee.LastName;

           await _dbContext.SaveChangesAsync();

           var Employee = await GetAsync(id);
           return new ServiceResult<EmployeeResult>(Employee);
        }

        public async Task<ServiceResult> DeleteAsync(int id)
        {
            var serviceResult = new ServiceResult();
            var employee = new Employee() { Id = id };
            _dbContext.Employees.Attach(employee);
            
            _dbContext.Employees.Remove(employee);
            var changes = await _dbContext.SaveChangesAsync();
            if (changes == 0)
            {
                serviceResult.Messages.Add( new ServiceMessage
                {
                    Code = "NothingChanged",
                    Message = "Something happened... nothing changed in the database.",
                    Type = ServiceMessageType.Warning
                });
            }

            return serviceResult;
        }
    }
}
