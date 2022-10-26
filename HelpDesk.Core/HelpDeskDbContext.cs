using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HelpDesk.Core
{
    public class HelpDeskDbContext : DbContext
    {
        public HelpDeskDbContext(DbContextOptions<HelpDeskDbContext> options): base(options)
        {
            
        }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Employee> Employees { get; set; }
        

        public void Seed()
        {
            if (!this.Database.IsInMemory())
            {
                return;
            }

            Employees.Add(new Employee()
            {
                FirstName = "Bavo",
                LastName = "Ketels"
            });
            Employees.Add(new Employee()
            {
                FirstName = "Robbe",
                LastName = "De Wolf"
            });
            Employees.Add(new Employee()
            {
                FirstName = "Jos",
                LastName = "Beton"
            });

            this.SaveChanges();
        }
    }
}
