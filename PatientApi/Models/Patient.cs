using Newtonsoft.Json;

namespace PatientApi.Models;

public class Patient
{
    public int Id { get; set; }
    public string PatientName { get; set; }
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

}