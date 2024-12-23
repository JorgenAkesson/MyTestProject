using BlazorServerApp.Components.Shared;
using BlazorServerApp.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using PatientApi.Models;
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
        private List<PatientDTO> patients = new List<PatientDTO>();

        protected override Task OnInitializedAsync()
        {
            Init();
            return base.OnInitializedAsync();
        }

        private async void Init()
        {
        }

        private async void GetPatients(MouseEventArgs e)
        {
            patients = await MyService.GetPatients();
            await InvokeAsync(StateHasChanged);
            child.Update();
        }

        private void AddPatient(MouseEventArgs e)
        {
            MyService.AddPatient(new PatientDTO()
            {
                PatientName = "MyPatient",
                Appointments = new List<AppointmentDTO>
                    { new AppointmentDTO
                        { AppointmentName = "MyAppointment"}}
            });
        }
    }
}