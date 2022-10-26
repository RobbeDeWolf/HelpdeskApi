using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpDesk.Core
{
    public class Ticket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        
        [Required]
        public string ClientNumber { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public TicketStatus status { get; set; }
        [Required]
        public DateTime Created { get; set; }
        
        public int? Employeeid { get; set; }
        public Employee? Employee { get; set; }= default!;
    }
}
