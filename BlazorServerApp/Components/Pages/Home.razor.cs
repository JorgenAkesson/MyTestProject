using BlazorServerApp.Components.Shared;
using BlazorServerApp.Services;
using CompanyApi.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Net.Http;
using System.Net.Http.Json;

namespace BlazorServerApp.Components.Pages
{
    public partial class Home
    {
        [Inject]
        protected IMyService MyService { get; set; }

        private Table child;

        private string Header = "Welcome to your new app.";
        private List<AccountDTO> accounts = new List<AccountDTO>();

        protected override Task OnInitializedAsync()
        {
            Init();
            return base.OnInitializedAsync();
        }

        private async void Init()
        {
        }

        private async void GetAccounts(MouseEventArgs e)
        {
            accounts = await MyService.GetAccounts();
            await InvokeAsync(StateHasChanged);
            child.Update();
        }

        private void AddAccount(MouseEventArgs e)
        {
            MyService.AddAccount(new AccountDTO() { AccountName = "MyAccount", Orders = new List<OrderDTO> { new OrderDTO { OrderName = "MyOrder" } }  });
        }
    }
}