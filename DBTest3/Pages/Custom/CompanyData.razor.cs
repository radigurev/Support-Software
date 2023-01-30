using DBTest3.Data.ViewModels;
using DBTest3.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace DBTest3.Pages.Custom
{

    public partial class CompanyData
    {
        [Parameter]
        public string Token { get; set; }

        private CompanyVM company;
        EditContext editContext;

        private List<string> errorMessages = new List<string>();

        [Inject]
        private ICompanyService companyService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (!string.IsNullOrWhiteSpace(Token))
            {
                if (Token.Equals("New"))
                {
                    company = new CompanyVM();
                }
                else
                {
                    company = companyService.getCompanyById(long.Parse(Token));
                }
                 editContext = new EditContext(company);
                editContext.EnableDataAnnotationsValidation();

            }
        }

        private async Task Submit()
        {
            var validate = this.editContext.Validate();



            if(validate)
            {
                if (company.Id == 0)
                    company = this.companyService.CrateCompany(company);
                else
                    this.companyService.UpdateCompany(company);

                errorMessages.Clear();
            }
            else
            {
                errorMessages.AddRange(editContext.GetValidationMessages());
            }
        }
    }
}
