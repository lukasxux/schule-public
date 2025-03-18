using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public class Person : Entity
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public Person() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        public Person(string firstname, string lastname, string email, Phone phone)
        {
            Firstname = firstname;
            Lastname = lastname;
            Email = email;
            Phone = phone;
        }

        public String Firstname { get; set; }
        public String Lastname { get; set; }
        public String Email{ get; set; }
        public Phone Phone { get; set; }
    }
}
