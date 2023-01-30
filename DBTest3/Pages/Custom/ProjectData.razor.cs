using System;
using Microsoft.AspNetCore.Components;

namespace DBTest3.Pages.Custom
{
	public partial class ProjectData
	{
        [Parameter]
        public string Token { get; set; }

        protected override async Task OnInitializedAsync()
        {

        }
    }
}

