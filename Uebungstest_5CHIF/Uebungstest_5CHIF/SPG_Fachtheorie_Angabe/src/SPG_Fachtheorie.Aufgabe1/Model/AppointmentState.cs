using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public class AppointmentState : Entity
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        protected AppointmentState() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        public AppointmentState(DateTime created, AppointmentStateType type, String? note) { 
            //Appointment = appointment;
            Created = created;
            Note = note;
            Type = type;
        }

        //public Appointment Appointment { get; set; }
        public DateTime Created { get; set; }
        public String? Note { get; set; }
        public AppointmentStateType Type { get; set; }
    }
}
