using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelpDesk.Core;

namespace HelpDesk.Services.Model.Requests
{
    public class TicketRequest
    {
        public string ClientNumber { get; set; }
        public string Description { get; set; }
        public TicketStatus Status { get; set; } // on new creation this will be set to 0 bec it will be open all the time. on edit they need to be able to adjust it.
    }
}
