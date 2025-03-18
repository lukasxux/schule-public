using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public class Appointment : Entity
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        protected Appointment() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        public Appointment(DateTime date, DateTime time, Patient patient, List<AppointmentState> appointmentStates)
        {
            Date = date;
            Time = time;
            Patient = patient;
            AppointmentStates = appointmentStates;
        }

        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public Patient Patient { get; set; }
        public List<AppointmentState> AppointmentStates { get; set; } = new List<AppointmentState>();
    }
}
