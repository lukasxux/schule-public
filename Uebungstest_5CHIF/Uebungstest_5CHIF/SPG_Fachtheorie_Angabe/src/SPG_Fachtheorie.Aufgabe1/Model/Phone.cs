using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public class Phone : Entity
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public Phone() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public Phone(string value) { 
            Value = value;
        }
        public String Value { get; set; }

        public static implicit operator Phone(String value) => new Phone(value);
        public static implicit operator String(Phone phone) => phone.Value;
    }
}
