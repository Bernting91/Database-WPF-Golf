using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_WPF_Golf
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Salary { get; set; }
        public string GolfclubName { get; set; }
        public Employee(int id, string name, string email, int salary)
        {
            Id = id;
            Name = name;
            Email = email;
            Salary = salary;
        }
        public override string ToString()
        {
            return $"Employee: {Name}, Email: {Email}, Salary: {Salary}, Golfclub: {GolfclubName}";
        }
    }
}
