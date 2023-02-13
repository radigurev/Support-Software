using DBTest3.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBTest3.Data.ViewModels
{
    public class TicketsVM
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Problem { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ClosedDate { get; set; }

        [ForeignKey(nameof(Client))]
        public string ClientId { get; set; }
        public UserVM Client { get; set; }

        [ForeignKey(nameof(Worker))]
        public string? WorkerId { get; set; }
        public UserVM Worker { get; set; }

        [ForeignKey(nameof(Status))]
        public long? StatusId { get; set; }
        public TicketStatus Status { get; set; }
    }
}
