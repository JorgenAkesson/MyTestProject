namespace PatientApi.Models;

public class Appointment
{
    public int Id { get; set; }
    public string AppointmentName { get; set; }
    public int PatientId { get; set; }
    public Patient? Patient { get; set; } = null;
}
