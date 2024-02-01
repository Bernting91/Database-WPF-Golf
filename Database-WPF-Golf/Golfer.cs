using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_WPF_Golf
{
    public class Golfer
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Handicap { get; set; }
        public string GolfclubName { get; set; }

        public Golfer(int id, string name, string email, int handicap)
        {
            Id = id;
            Name = name;
            Email = email;
            Handicap = handicap;
        }

        public override string ToString()
        {
            return $"Golfer: {Name}, Email: {Email}, Handicap: {Handicap}, Golfclub: {GolfclubName}";
        }
    }
}
