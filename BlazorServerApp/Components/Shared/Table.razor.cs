using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using PatientApi.Models;

namespace BlazorServerApp.Components.Shared
{
    public partial class Table
    {
        [Parameter]
        public List<PatientDTO> Patients { get; set; }

        Grid<PatientDTO> grid = default!;
        private HashSet<PatientDTO> selectedPatient = new();

        private async Task<GridDataProviderResult<PatientDTO>> PatientDataProvider(GridDataProviderRequest<PatientDTO> request)
        {
            return await Task.FromResult(request.ApplyTo(Patients));
        }

        private Task OnSelectedItemsChanged(HashSet<PatientDTO> patients)
        {
            selectedPatient = patients is not null && patients.Any() ? patients : new();
            return Task.CompletedTask;
        }

        // Method called from parent to update table
        public async void Update()
        {
            await grid.RefreshDataAsync();
        }
    }
}