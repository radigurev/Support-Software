using System;
using Microsoft.AspNetCore.Components;


using DBTest3.Data.Entity;
using DBTest3.Data.ViewModels;
using DBTest3.Service;
using Microsoft.AspNetCore.Components.Authorization;

namespace DBTest3.Pages.Custom
{
	public partial class Tickets
	{
        [Parameter]
        public string Token { get; set; }
        [CascadingParameter] private Task<AuthenticationState> authenticationStateTask { get; set; }

        private TicketsVM Ticket;

        private string msg;

        private string role;

        [Inject]
        ITicketService TicketService { get; set; }
        [Inject]
        IApplicationUserService applicationUserService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            msg = string.Empty;
            role = string.Empty;
            if (!string.IsNullOrEmpty(Token))
            {
                var authState = await authenticationStateTask;
                var user = authState.User;

                var CurrentUser = await this.applicationUserService.getUserByEmail(user.Identity.Name);

                role = await this.applicationUserService.getUserRole(CurrentUser);

                if (Token.ToLower().Equals("new"))
                {
                    Ticket = new TicketsVM();
                    Ticket.ClientId = CurrentUser.Id;
                }else
                {
                    Ticket = this.TicketService.getTicketById(int.Parse(Token));
                }
            }else
            {

            }
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
    }
}

