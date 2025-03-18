using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;


namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public class Address : Entity{

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        protected Address() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public Address(String street, String zip, String city)
        {
            Street = street;
            Zip = zip;
            City = city;
        }

        public String Street { get; set; }
        public String Zip { get; set; }

        public String City { get; set; }

    }
}
