using DBTest3.Data.ViewModels;

namespace DBTest3.Service
{
    public interface ITicketService
    {
        Task changeTicketSatus(TicketsVM ticket,string status);
        void deleteTicket(TicketsVM ticket);
        TicketsVM getTicketById(int v);
        List<TicketsVM> getTicketsByStatusAdmin(TicketStatusVM status,UserVM user = null);
        List<TicketsVM> getTicketsByStatusUser(TicketStatusVM ticketStatusVM, UserVM currentUser = null);
        TicketStatusVM getTicketStatus(string statusCode);
        Task initTicketStatuses();
        void saveChatToTicket(ChatVM chat);
        TicketsVM saveTicket(TicketsVM ticket);
        TicketsVM updateTicket(TicketsVM ticket);
    }
}
