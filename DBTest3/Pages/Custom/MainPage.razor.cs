using DBTest3.Data.Entity;
using DBTest3.Data.ViewModels;
using DBTest3.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
namespace DBTest3.Pages.Custom
{
	public partial class MainPage
	{
        [CascadingParameter] private Task<AuthenticationState> authenticationStateTask { get; set; }

        [Inject]
        ITicketService ticketService { get; set; }
        [Inject]
        IApplicationUserService applicationUserService { get; set; }

        private UserVM CurrentUser;

        private string role;

        List<TicketsVM> ToDoTickets;
        List<TicketsVM> ProgressTickets;
        List<TicketsVM> WaitingTickets;
        List<TicketsVM> ClosedTickets;

        private bool ShowToDo = true;
        private bool ShowInProgress = true;
        private bool ShowWaitingAnswer = true;
        private bool ShowClosedStatus = false;

        protected override async Task OnInitializedAsync()
        {
            var authState = await authenticationStateTask;
            var user = authState.User;

            CurrentUser = await this.applicationUserService.getUserByEmail(user.Identity.Name);

            role = await this.applicationUserService.getUserRole(CurrentUser);
            if (role == "Admin")
            {
                ToDoTickets = ticketService.getTicketsByStatusAdmin(ticketService.getTicketStatus("ToDoStatus"));
                ProgressTickets = ticketService.getTicketsByStatusAdmin(ticketService.getTicketStatus("InProgressStatus"), CurrentUser);
                WaitingTickets = ticketService.getTicketsByStatusAdmin(ticketService.getTicketStatus("WaitingAnswerStatus"), CurrentUser);
                ClosedTickets = ticketService.getTicketsByStatusAdmin(ticketService.getTicketStatus("ClosedStatus"), CurrentUser);
            }
            else
            {
                ToDoTickets = ticketService.getTicketsByStatusUser(ticketService.getTicketStatus("ToDoStatus"));
                ProgressTickets = ticketService.getTicketsByStatusUser(ticketService.getTicketStatus("InProgressStatus"), CurrentUser);
                WaitingTickets = ticketService.getTicketsByStatusUser(ticketService.getTicketStatus("WaitingAnswerStatus"), CurrentUser);
                ClosedTickets = ticketService.getTicketsByStatusUser(ticketService.getTicketStatus("ClosedStatus"), CurrentUser);
            }
        }

        private void ChangeToDo()
        {
            if (ShowToDo)
                ShowToDo = false;
            else
                ShowToDo = true;
        }

        private void ChangeInProgress()
        {
            if (ShowInProgress)
                ShowInProgress = false;
            else
                ShowInProgress = true;
        }

        private void ChangeWaitingAnswer()
        {
            if (ShowWaitingAnswer)
                ShowWaitingAnswer = false;
            else
                ShowWaitingAnswer = true;
        }

        private void ChangeClosed()
        {
            if (ShowClosedStatus)
                ShowClosedStatus = false;
            else
                ShowClosedStatus = true;
        }
    }
}

