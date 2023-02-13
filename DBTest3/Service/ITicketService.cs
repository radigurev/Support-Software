using DBTest3.Data.ViewModels;

namespace DBTest3.Service
{
    public interface ITicketService
    {
        List<TicketsVM> getTicketsByStatusAdmin(TicketStatusVM status,UserVM user = null);
        List<TicketsVM> getTicketsByStatusUser(TicketStatusVM ticketStatusVM, UserVM currentUser = null);
        TicketStatusVM getTicketStatus(string statusCode);

        Task initTicketStatuses();
        TicketsVM saveTicket(TicketsVM ticket);
    }
}
