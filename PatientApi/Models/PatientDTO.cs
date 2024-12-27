using Newtonsoft.Json;

namespace PatientApi.Models;

public class PatientDTO
{
    public int Id { get; set; }
    public string PatientName { get; set; }
    public ICollection<AppointmentDTO>? Appointments { get; set; }

    public static PatientDTO PatientToDTO(Patient patient, bool stop = false) =>
    new PatientDTO
    {
        Id = patient.Id,
        PatientName = patient.PatientName,
        Appointments = !stop ? patient.Appointments.Select(x => AppointmentDTO.AppointmentToDTO(x, true)).ToList() : null,
    };

    public static Patient DTOToPatient(PatientDTO dto) =>
        new Patient
        {
            Id = dto.Id,
            PatientName = dto.PatientName,
            Appointments = dto.Appointments.Select(AppointmentDTO.DTOToAppointment).ToList()
        };
}