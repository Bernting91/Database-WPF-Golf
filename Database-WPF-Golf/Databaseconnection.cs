using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;

namespace Database_WPF_Golf
{
    public class Databaseconnection
    {
        string server = "localhost";
        string database = "InlämningDatabaser";
        string username = "root";
        string password = "";

        string connectionString = "";
        MySqlConnection connection;

        public Databaseconnection(string server, string database, string username, string password)
        {
            connectionString =
                "SERVER=" + server + ";" +
                "DATABASE=" + database + ";" +
                "UID=" + username + ";" +
                "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);
        }
        public Dictionary<int, Golfclub> GetGolfClubs()
        {
            Dictionary<int, Golfclub> golfclubDictionary = new Dictionary<int, Golfclub>();

            connection.Open();

            string query = "SELECT * FROM Golfclub";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Golfclub golfclub = new Golfclub((int)reader["Golfclub_ID"], (string)reader["Golfclub_Name"], (String)reader["Golfclub_Country"], (string)reader["Golfclub_City"]);
                golfclubDictionary.Add(golfclub.Id, golfclub);
            }
            connection.Close();
            return golfclubDictionary;
        }

        public Dictionary<int, Golfer> GetGolfers()
        {
            Dictionary<int, Golfer> golferDictionary = new Dictionary<int, Golfer>();

            connection.Open();

            string query = "SELECT * FROM Golfer";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Golfer golfer = new Golfer((int)reader["Golfer_ID"], (string)reader["Golfer_Name"], (String)reader["Golfclub_Email"], (int)reader["Golfclub_Handicap"]);
                golferDictionary.Add(golfer.Id, golfer);
            }
            connection.Close();
            return golferDictionary;
        }

        public Dictionary<int, Golfcourse> GetGolfcourses()
        {
            Dictionary<int, Golfcourse> golfcourseDictionary = new Dictionary<int, Golfcourse>();

            connection.Open();

            string query = "SELECT * FROM GolfCourse";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Golfcourse golfcourse = new Golfcourse((int)reader["Golfcourse_ID"], (string)reader["Golfcourse_Name"], (int)reader["Golfcourse_Holes"], (int)reader["Golfcourse_Greenfee"]);
                golfcourseDictionary.Add(golfcourse.Id, golfcourse);
            }
            connection.Close();
            return golfcourseDictionary;
        }

        public Dictionary<int, Employee> GetEmployees()
        {
            Dictionary<int, Employee> employeeDictionary = new Dictionary<int, Employee>();

            connection.Open();

            string query = "SELECT * FROM Employee";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Employee employee = new Employee((int)reader["Employee_ID"], (string)reader["Employee_Name"], (String)reader["Employee_Email"], (int)reader["Employee_Salary"]);
                employeeDictionary.Add(employee.Id, employee);
            }
            connection.Close();
            return employeeDictionary;
        }

        public Dictionary<int, Employee> GetEmployeesForGolfclub(int golfclubId)
        {
            Dictionary<int, Employee> employeeDictionary = new Dictionary<int, Employee>();

            connection.Open();

            string query = $"SELECT * FROM Employee WHERE Golfclub_ID = {golfclubId}";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Employee employee = new Employee((int)reader["Employee_ID"], (string)reader["Employee_Name"], (String)reader["Employee_Email"], (int)reader["Employee_Salary"]);
                employeeDictionary.Add(employee.Id, employee);
            }

            connection.Close();
            return employeeDictionary;
        }

        public Dictionary<int, Golfcourse> GetGolfcoursesForGolfclub(int golfclubId)
        {
            Dictionary<int, Golfcourse> golfcourseDictionary = new Dictionary<int, Golfcourse>();

            connection.Open();

            string query = $"SELECT * FROM Golfcourse WHERE Golfclub_ID = {golfclubId}";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Golfcourse golfcourse = new Golfcourse((int)reader["Golfcourse_ID"], (string)reader["Golfcourse_Name"], (int)reader["Golfcourse_Holes"], (int)reader["Golfcourse_Greenfee"]);
                golfcourseDictionary.Add(golfcourse.Id, golfcourse);
            }

            connection.Close();
            return golfcourseDictionary;
        }

        public Dictionary<int, Golfer> GetGolfersForGolfclub(int golfclubId)
        {
            Dictionary<int, Golfer> golferDictionary = new Dictionary<int, Golfer>();

            connection.Open();

            string query = $"SELECT G.* FROM Golfer G " +
                           $"JOIN Golfer_Has_Golfclub GHG ON G.Golfer_ID = GHG.Golfer_ID " +
                           $"WHERE GHG.Golfclub_ID = {golfclubId}";

            MySqlCommand command = new MySqlCommand(query, connection);

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Golfer golfer = new Golfer((int)reader["Golfer_ID"], (string)reader["Golfer_Name"], (string)reader["Golfer_Email"], (int)reader["Golfer_Handicap"]);
                golferDictionary.Add(golfer.Id, golfer);
            }

            connection.Close();
            return golferDictionary;
        }

        public Golfer AddGolfer(string Name, string Email, int Handicap, int Golfclub_ID)
        {
            connection.Open();

            string golferQuery = $"INSERT INTO Golfer (Golfer_Name, Golfer_Email, Golfer_Handicap) " +
                                 $"VALUES (\"{Name}\", \"{Email}\", {Handicap})";

            MySqlCommand golferCommand = new MySqlCommand(golferQuery, connection);
            golferCommand.ExecuteNonQuery();

            string linkQuery = $"INSERT INTO Golfer_Has_Golfclub (Golfer_ID, Golfclub_ID) " +
                               $"VALUES ((SELECT MAX(Golfer_Id) FROM Golfer), {Golfclub_ID})";

            MySqlCommand linkCommand = new MySqlCommand(linkQuery, connection);
            linkCommand.ExecuteNonQuery();

            string getIdQuery = "SELECT MAX(Golfer_Id) AS \"id\" FROM Golfer;";
            MySqlCommand getIdCommand = new MySqlCommand(getIdQuery, connection);
            MySqlDataReader reader = getIdCommand.ExecuteReader();
            reader.Read();

            int Golfer_Id = (int)reader["id"];
            connection.Close();

            Golfer golfer = new Golfer(Golfer_Id, Name, Email, Handicap);
            return golfer;
        }

        public Golfcourse AddGolfcourse(string Name, int Holes, int Greenfee, int Golfclub_ID)
        {
            connection.Open();

            string golfcourseQuery = $"INSERT INTO Golfcourse (Golfcourse_Name, Golfcourse_Holes, Golfcourse_Greenfee, Golfclub_ID) " +
                                        $"VALUES (\"{Name}\", {Holes}, {Greenfee}, {Golfclub_ID})";
            MySqlCommand golfcourseCommand = new MySqlCommand(golfcourseQuery, connection);
            golfcourseCommand.ExecuteNonQuery();

            string getIdQuery = "SELECT MAX(Golfcourse_Id) AS \"id\" FROM Golfcourse;";
            MySqlCommand getIdCommand = new MySqlCommand(getIdQuery, connection);
            MySqlDataReader reader = getIdCommand.ExecuteReader();
            reader.Read();

            int Golfcourse_Id = (int)reader["id"];
            connection.Close();

            Golfcourse golfcourse = new Golfcourse(Golfcourse_Id, Name, Holes, Greenfee);
            return golfcourse;
        }

        public Employee AddEmployee(string Name, string Email, int Salary, int Golfclub_ID)
        {
            connection.Open();

            string employeeQuery = $"INSERT INTO Employee (Employee_Name, Employee_Email, Employee_Salary, Golfclub_ID) " +
                                        $"VALUES (\"{Name}\", \"{Email}\", {Salary}, {Golfclub_ID})";
            MySqlCommand employeeCommand = new MySqlCommand(employeeQuery, connection);
            employeeCommand.ExecuteNonQuery();

            string getIdQuery = "SELECT MAX(Employee_Id) AS \"id\" FROM Employee;";
            MySqlCommand getIdCommand = new MySqlCommand(getIdQuery, connection);
            MySqlDataReader reader = getIdCommand.ExecuteReader();
            reader.Read();

            int Employee_Id = (int)reader["id"];
            connection.Close();

            Employee employee = new Employee(Employee_Id, Name, Email, Salary);
            return employee;
        }

        public Golfclub AddGolfclub(string Name, string Country, string City)
        {
            connection.Open();

            string golfclubQuery = $"INSERT INTO Golfclub (Golfclub_Name, Golfclub_Country, Golfclub_City) " +
                                        $"VALUES (\"{Name}\", \"{Country}\", \"{City}\")";
            MySqlCommand golfclubCommand = new MySqlCommand(golfclubQuery, connection);
            golfclubCommand.ExecuteNonQuery();

            string getIdQuery = "SELECT MAX(Golfclub_Id) AS \"id\" FROM Golfclub;";
            MySqlCommand getIdCommand = new MySqlCommand(getIdQuery, connection);
            MySqlDataReader reader = getIdCommand.ExecuteReader();
            reader.Read();

            int Golfclub_Id = (int)reader["id"];
            connection.Close();

            Golfclub golfclub = new Golfclub(Golfclub_Id, Name, Country, City);
            return golfclub;
        }
        public void DeleteGolfer(int golferId)
        {
            connection.Open();

            string deleteLinkQuery = $"DELETE FROM Golfer_Has_Golfclub WHERE Golfer_ID = {golferId}";
            MySqlCommand deleteLinkCommand = new MySqlCommand(deleteLinkQuery, connection);
            deleteLinkCommand.ExecuteNonQuery();

            string deleteQuery = $"DELETE FROM Golfer WHERE Golfer_ID = {golferId}";
            MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
            deleteCommand.ExecuteNonQuery();

            connection.Close();
        }

        public void DeleteGolfcourse(int golfcourseId)
        {
            connection.Open();

            string deleteQuery = $"DELETE FROM Golfcourse WHERE Golfcourse_ID = {golfcourseId}";
            MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
            deleteCommand.ExecuteNonQuery();

            connection.Close();
        }

        public void DeleteEmployee(int employeeId)
        {
            connection.Open();

            string deleteQuery = $"DELETE FROM Employee WHERE Employee_ID = {employeeId}";
            MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
            deleteCommand.ExecuteNonQuery();

            connection.Close();
        }

        public void DeleteGolfclub(int golfclubId)
        {
            connection.Open();

            string deleteQuery = $"DELETE FROM Golfclub WHERE Golfclub_ID = {golfclubId}";
            MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection);
            deleteCommand.ExecuteNonQuery();

            connection.Close();
        }

        public List<object> SearchInDatabase(string searchText)
        {
            List<object> searchResults = new List<object>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    string golferQuery = $"SELECT * FROM Golfer_Golfclub_View WHERE Golfer_Name LIKE '%{searchText}%'";
                    using (MySqlCommand golferCommand = new MySqlCommand(golferQuery, connection))
                    using (MySqlDataReader golferReader = golferCommand.ExecuteReader())
                    {
                        while (golferReader.Read())
                        {
                            Golfer golfer = new Golfer(
                                (int)golferReader["Golfer_ID"],
                                (string)golferReader["Golfer_Name"],
                                (string)golferReader["Golfer_Email"],
                                (int)golferReader["Golfer_Handicap"]
                            );

                            golfer.GolfclubName = (string)golferReader["Golfclub_Name"];

                            searchResults.Add(golfer);
                        }
                    }
                    using (MySqlCommand golfclubCommand = new MySqlCommand($"SELECT * FROM Golfclub WHERE Golfclub_Name LIKE '%{searchText}%'", connection))
                    using (MySqlDataReader golfclubReader = golfclubCommand.ExecuteReader())
                    {
                        while (golfclubReader.Read())
                        {
                            Golfclub golfclub = new Golfclub(
                                (int)golfclubReader["Golfclub_ID"],
                                (string)golfclubReader["Golfclub_Name"],
                                (string)golfclubReader["Golfclub_Country"],
                                (string)golfclubReader["Golfclub_City"]
                            );
                            searchResults.Add(golfclub);
                        }
                    }
                    using (MySqlCommand golfcourseCommand = new MySqlCommand($"SELECT * FROM Golfcourse GC " +
                                                                             $"INNER JOIN Golfclub GClub ON GC.Golfclub_ID = GClub.Golfclub_ID " +
                                                                             $"WHERE GC.Golfcourse_Name LIKE '%{searchText}%'", connection))
                    using (MySqlDataReader golfcourseReader = golfcourseCommand.ExecuteReader())
                    {
                        while (golfcourseReader.Read())
                        {
                            Golfcourse golfcourse = new Golfcourse(
                                (int)golfcourseReader["Golfcourse_ID"],
                                (string)golfcourseReader["Golfcourse_Name"],
                                (int)golfcourseReader["Golfcourse_Holes"],
                                (int)golfcourseReader["Golfcourse_Greenfee"]
                            );
                            golfcourse.GolfclubName = (string)golfcourseReader["Golfclub_Name"];

                            searchResults.Add(golfcourse);
                        }
                    }
                    using (MySqlCommand employeeCommand = new MySqlCommand($"SELECT * FROM Employee E " +
                                                                           $"INNER JOIN Golfclub GClub ON E.Golfclub_ID = GClub.Golfclub_ID " +
                                                                           $"WHERE E.Employee_Name LIKE '%{searchText}%'", connection))
                    using (MySqlDataReader employeeReader = employeeCommand.ExecuteReader())
                    {
                        while (employeeReader.Read())
                        {
                            Employee employee = new Employee(
                                (int)employeeReader["Employee_ID"],
                                (string)employeeReader["Employee_Name"],
                                (string)employeeReader["Employee_Email"],
                                (int)employeeReader["Employee_Salary"]
                            );
                            employee.GolfclubName = (string)employeeReader["Golfclub_Name"];

                            searchResults.Add(employee);
                        }
                    }
                    return searchResults;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    return searchResults;
                }
            }
        }
        public void UpdateGolfclub(Golfclub updatedGolfclub)
        {
            connection.Open();
            string updateQuery = $"UPDATE Golfclub SET Golfclub_Name = '{updatedGolfclub.Name}', " +
                                 $"Golfclub_Country = '{updatedGolfclub.Country}', " +
                                 $"Golfclub_City = '{updatedGolfclub.City}' " +
                                 $"WHERE Golfclub_ID = {updatedGolfclub.Id}";

            MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
            updateCommand.ExecuteNonQuery();
            connection.Close();
        }
        public void UpdateEmployee(Employee updatedEmployee)
        {
            connection.Open();

            string updateQuery = $"UPDATE Employee SET Employee_Name = '{updatedEmployee.Name}', " +
                                 $"Employee_Email = '{updatedEmployee.Email}', " +
                                 $"Employee_Salary = {updatedEmployee.Salary} " +
                                 $"WHERE Employee_ID = {updatedEmployee.Id}";

            MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
            updateCommand.ExecuteNonQuery();
            connection.Close();
        }
        public void UpdateGolfcourse(Golfcourse updatedGolfcourse)
        {
            connection.Open();
            string updateQuery = $"UPDATE Golfcourse SET Golfcourse_Name = '{updatedGolfcourse.Name}', " +
                                 $"Golfcourse_Holes = {updatedGolfcourse.Holes}, " +
                                 $"Golfcourse_Greenfee = {updatedGolfcourse.Greenfee} " +
                                 $"WHERE Golfcourse_ID = {updatedGolfcourse.Id}";

            MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
            updateCommand.ExecuteNonQuery();
            connection.Close();
        }
        public void UpdateGolfer(Golfer updatedGolfer)
        {
            connection.Open();
            string updateQuery = $"UPDATE Golfer SET Golfer_Name = '{updatedGolfer.Name}', " +
                                 $"Golfer_Email = '{updatedGolfer.Email}', " +
                                 $"Golfer_Handicap = {updatedGolfer.Handicap} " +
                                 $"WHERE Golfer_ID = {updatedGolfer.Id}";

            MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection);
            updateCommand.ExecuteNonQuery();

            connection.Close();
        }





    }
}
