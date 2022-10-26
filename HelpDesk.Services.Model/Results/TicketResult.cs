using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelpDesk.Core;

namespace HelpDesk.Services.Model.Results
{
    public class TicketResult
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ClientNumber { get; set; }
        public string Description { get; set; }
        public TicketStatus Status { get; set; }
        public DateTime Created { get; set; }
        public Employee? Employee { get; set; }
    }
}
