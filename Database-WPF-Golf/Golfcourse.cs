using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_WPF_Golf
{
    public class Golfcourse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Holes { get; set; }
        public int Greenfee { get; set; }
        public string GolfclubName { get; set; }
        public Golfcourse(int id, string name, int holes, int greenfee)
        {
            Id = id;
            Name = name;
            Holes = holes;
            Greenfee = greenfee;
        }
        public override string ToString()
        {
            return $"Golfcourse: {Name}, Holes: {Holes}, Greenfee: {Greenfee}, Golfclub: {GolfclubName}";
        }

    }
}
