using System;
using Microsoft.AspNetCore.Components;


using DBTest3.Data.Entity;
using DBTest3.Data.ViewModels;
using DBTest3.Service;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;

namespace DBTest3.Pages.Custom
{
	public partial class Tickets
	{
        [Parameter]
        public string Token { get; set; }
        [CascadingParameter] private Task<AuthenticationState> authenticationStateTask { get; set; }

        [Inject]
        NavigationManager navigationManager { get; set; }

        private TicketsVM Ticket;

        private string msg;

        private string role;

        private bool IsValidator = false;

        private EditContext editContext;

        private ChatVM chat;

        private UserVM CurrentUser;

        private string ToDoStatus = "ToDoStatus";
        private string WaitingAnswerStatus = "WaitingAnswerStatus";
        private string InProgressStatus = "InProgressStatus";
        private string ClosedStatus = "ClosedStatus";

        private TicketStatusVM closed;

        [Inject]
        ITicketService TicketService { get; set; }
        [Inject]
        IApplicationUserService applicationUserService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            msg = string.Empty;
            role = string.Empty;
            chat = new ChatVM();
            if (!string.IsNullOrEmpty(Token))
            {
                var authState = await authenticationStateTask;
                var user = authState.User;

                this.CurrentUser = await this.applicationUserService.getUserByEmail(user.Identity.Name);

                role = await this.applicationUserService.getUserRole(CurrentUser);

                closed = TicketService.getTicketStatus(ClosedStatus);

                if(role.Equals("Validator"))
                {
                    IsValidator = true;
                }

                if (Token.ToLower().Equals("new"))
                {
                    Ticket = new TicketsVM();
                    Ticket.ClientId = CurrentUser.Id;
                }else
                {
                    Ticket = this.TicketService.getTicketById(int.Parse(Token));
                }

                editContext = new EditContext(chat);

            }
            else
            {

            }
        }

        private async Task SaveChat()
        {
            chat.TicketID = Ticket.Id;
            chat.UserId = CurrentUser.Id;
            
            if(!role.Equals("Admin"))
            {
                await ChangeStatus(WaitingAnswerStatus);
            }
            TicketService.saveChatToTicket(chat);
            chat = new ChatVM();
            Ticket = this.TicketService.getTicketById(int.Parse(Token));
        }

        private async Task Submit()
        {
            try
            {
                if (string.IsNullOrEmpty(Ticket.Problem) || string.IsNullOrEmpty(Ticket.Title))
                {
                    msg = "Моля попълнете задължителните полета!";
                    return;
                }

                Ticket.CreatedDate = DateTime.Now;

                var result = TicketService.saveTicket(Ticket);

                if (result.Id != 0)
                {
                    msg = "Моля изчакайте вашият билет да бъде проверен!";
                }
            }catch(Exception ex) { }
        }

        private async Task Accept()
        {
            ChatVM chat = new ChatVM();
            chat.TicketID = Ticket.Id;
            chat.UserId = Ticket.ClientId;
            chat.Message = Ticket.Problem;
            TicketService.saveChatToTicket(chat);
            TicketService.changeTicketSatus(Ticket, "ToDoStatus");
            navigationManager.NavigateTo("/MainPage");
        }
        
        private async Task ChangeStatus(string status)
        {
            await TicketService.changeTicketSatus(Ticket, status);
            Ticket = this.TicketService.getTicketById(int.Parse(Token));

        }

        private async Task Denied()
        {
            TicketService.deleteTicket(Ticket);
            navigationManager.NavigateTo("/MainPage");
        }

        private async Task TakeTicket()
        {
            Ticket.WorkerId = CurrentUser.Id;

            Ticket = TicketService.updateTicket(Ticket);
        }
    }
}

