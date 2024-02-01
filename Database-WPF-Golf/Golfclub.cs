using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_WPF_Golf
{
    public class Golfclub
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }

        public Golfclub(int id, string name, string country, string city)
        {
            Id = id;
            Name = name;
            Country = country;
            City = city;
        }
        public override string ToString()
        {
            return $"Golfclub: {Name}, Country: {Country}, City: {City}";
        }
    }
}
