using DBTest3.Data.ViewModels;

namespace DBTest3.Service
{
    public interface ITicketService
    {
        List<TicketsVM> getToDoTickets(TicketStatusVM status);

        TicketStatusVM getTicketStatus(string statusCode);

        Task initTicketStatuses();
    }
}
