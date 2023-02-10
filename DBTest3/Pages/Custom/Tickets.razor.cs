using System;
using Microsoft.AspNetCore.Components;


using DBTest3.Data.Entity;
using DBTest3.Data.ViewModels;

namespace DBTest3.Pages.Custom
{
	public partial class Tickets
	{
        [Parameter]
        public string Token { get; set; }

        private TicketsVM Ticket;

        protected override async Task OnInitializedAsync()
        {

            if (!string.IsNullOrEmpty(Token))
            {
                if (Token.ToLower().Equals("new"))
                {
                    Ticket = new TicketsVM();
                }
            }else
            {

            }
        }
    }
}

