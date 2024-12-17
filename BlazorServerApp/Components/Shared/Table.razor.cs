using BlazorBootstrap;
using CompanyApi.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorServerApp.Components.Shared
{
    public partial class Table
    {
        [Parameter]
        public List<AccountDTO> Accounts { get; set; }

        Grid<AccountDTO> grid = default!;
        private HashSet<AccountDTO> selectedAccount = new();

        private async Task<GridDataProviderResult<AccountDTO>> AccountDataProvider(GridDataProviderRequest<AccountDTO> request)
        {
            return await Task.FromResult(request.ApplyTo(Accounts));
        }

        private Task OnSelectedItemsChanged(HashSet<AccountDTO> accounts)
        {
            selectedAccount = accounts is not null && accounts.Any() ? accounts : new();
            return Task.CompletedTask;
        }

        // Method called from parent to update table
        public async void Update()
        {
            await grid.RefreshDataAsync();
        }
    }
}