using Newtonsoft.Json;
using System.Diagnostics;

namespace PatientApi.Models;

public class AppointmentDTO
{
    public int Id { get; set; }
    public string AppointmentName { get; set; }
    public int PatientId { get; set; }
    public PatientDTO? Patient { get; set; }

    public static AppointmentDTO AppointmentToDTO(Appointment appointment, bool stop = false) =>
        new AppointmentDTO
        {
            Id = appointment.Id,
            AppointmentName = appointment.AppointmentName,
            PatientId = appointment.PatientId,
            Patient = appointment.Patient != null && !stop ? PatientDTO.PatientToDTO(appointment.Patient, true) : null
        };

    public static Appointment DTOToAppointment(AppointmentDTO dto) =>
        new Appointment
        {
            Id = dto.Id,
            AppointmentName = dto.AppointmentName,
            Patient = dto.Patient != null ? PatientDTO.DTOToPatient(dto.Patient) : null,
        };
}