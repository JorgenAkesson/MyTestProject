using System.Net.Http.Json;

namespace MyTestBlazorApp.Properties.Pages
{
    public partial class Home
    {
        private string? orderServiceEndpoint;

        private string Message = "Kalle";

        //protected override Task OnInitializedAsync()
        //{
        //    Init();
        //    return base.OnInitializedAsync();
        //}

        private async void Init()
        {
            //orderServiceEndpoint = $"{Config.GetValue<string>("BackendUrl")}/Order";

            await GetOrderItems();
        }

        private async Task GetOrderItems()
        {
            var a = await Http.GetFromJsonAsync<string>(orderServiceEndpoint);

        }
    }
}