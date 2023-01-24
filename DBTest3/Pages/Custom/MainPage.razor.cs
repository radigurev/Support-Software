using DBTest3.Data.ViewModels;
using DBTest3.Service;
using Microsoft.AspNetCore.Components;
using System;
namespace DBTest3.Pages.Custom
{
	public partial class MainPage
	{

        [Inject]
        ITicketService ticketService { get; set; }

        List<TicketsVM> ToDoTickets;
        List<TicketsVM> ProgressTickets;
        List<TicketsVM> WaitingTickets;
        List<TicketsVM> ClosedTickets;

        protected override async Task OnInitializedAsync()
        {
             ToDoTickets = ticketService.getToDoTickets(ticketService.getTicketStatus("ToDoStatus"));
             ProgressTickets = ticketService.getToDoTickets(ticketService.getTicketStatus("InProgressStatus"));
             WaitingTickets = ticketService.getToDoTickets(ticketService.getTicketStatus("WaitingAnswerStatus"));
             ClosedTickets = ticketService.getToDoTickets(ticketService.getTicketStatus("ClosedStatus"));

        }
    }
}

