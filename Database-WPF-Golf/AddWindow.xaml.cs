using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Database_WPF_Golf
{
    /// <summary>
    /// Interaction logic for AddWindow.xaml
    /// </summary>
    public partial class AddWindow : Window
    {
        Databaseconnection databaseconnection;
        public Dictionary<int, Golfclub> golfclubDictionary;
        public Dictionary<int, Golfer> golferDictionary;
        public Dictionary<int, Golfcourse> golfcourseDictionary;
        public Dictionary<int, Employee> employeeDictionary;
        public Golfclub selectedGolfclub;
        public AddWindow(Dictionary<int, Golfclub> golfclubs)
        {
            InitializeComponent();
            databaseconnection = new Databaseconnection("localhost", "InlämningDatabaser", "root", "");
            golfclubDictionary = golfclubs;
            golferDictionary = new Dictionary<int, Golfer>();
            employeeDictionary = new Dictionary<int, Employee>();
            golfcourseDictionary = new Dictionary<int, Golfcourse>();
            GolfClubListBox.ItemsSource = golfclubs.Values.ToList();
            GolfClubListBox.Items.Refresh();
        }

        private void Golfer_Checked(object sender, RoutedEventArgs e)
        {
            AddGolferGrid.Visibility = Visibility.Visible;
            BackButton.Visibility = Visibility.Visible;
            GolfClubListBox.Visibility = Visibility.Visible;
        }

        private void Employee_Checked(object sender, RoutedEventArgs e)
        {
            AddEmployeeGrid.Visibility = Visibility.Visible;
            BackButton.Visibility = Visibility.Visible;
            GolfClubListBox.Visibility = Visibility.Visible;
        }

        private void Golfclub_Checked(object sender, RoutedEventArgs e)
        {
            AddGolfclubGrid.Visibility = Visibility.Visible;
            BackButton.Visibility = Visibility.Visible;
            GolfClubListBox.Visibility = Visibility.Collapsed;
        }

        private void Golfcourse_Checked(object sender, RoutedEventArgs e)
        {
            AddGolfcourseGrid.Visibility = Visibility.Visible;
            BackButton.Visibility = Visibility.Visible;
            GolfClubListBox.Visibility = Visibility.Visible;
        }

        private void GolferAdd_Click(object sender, RoutedEventArgs e)
        {
            AddGolferGrid.Visibility = Visibility.Collapsed;
            BackButton.Visibility = Visibility.Collapsed;
            Golfer_Checkbox.IsChecked = false;
            GolfClubListBox.Visibility = Visibility.Collapsed;
            if (selectedGolfclub != null)
            {
                string GolferName = GolferNameTextbox.Text;
                string GolferEmail = GolferEmailTextbox.Text;
                int GolferHandicap;

                if (int.TryParse(GolferHandicapTextbox.Text, out GolferHandicap))
                {
                    Golfer golfer = databaseconnection.AddGolfer(GolferName, GolferEmail, GolferHandicap, selectedGolfclub.Id);
                    golferDictionary.Add(golfer.Id, golfer);
                }
                else
                {
                    MessageBox.Show("Please enter number for Golfer Handicap.");
                }
            }
            else
            {
                MessageBox.Show("Please select a golf club.");
            }
        }

        private void GolfclubListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedGolfclub = GolfClubListBox.SelectedItem as Golfclub;
        }

        private void GolfcourseAdd_Click(object sender, RoutedEventArgs e)
        {
            AddGolfcourseGrid.Visibility = Visibility.Collapsed;
            BackButton.Visibility = Visibility.Collapsed;
            Golfcourse_Checkbox.IsChecked = false;
            GolfClubListBox.Visibility = Visibility.Collapsed;
            if (selectedGolfclub != null)
            {
                string GolfcourseName = GolfcourseNameTextbox.Text;
                int GolfcourseHoles;
                int GolfcourseGreenfee;

                if (int.TryParse(GolfcourseHolesTextbox.Text, out GolfcourseHoles) &&
                    int.TryParse(GolfcourseGreenfeeTextbox.Text, out GolfcourseGreenfee))
                {
                    Golfcourse golfcourse = databaseconnection.AddGolfcourse(GolfcourseName, GolfcourseHoles, GolfcourseGreenfee, selectedGolfclub.Id);
                    golfcourseDictionary.Add(golfcourse.Id, golfcourse);
                }
                else
                {
                    MessageBox.Show("Please enter holes for golfcourse and greenfee cost.");
                }
            }
            else
            {
                MessageBox.Show("Please select a golf club.");
            }
        }

        private void EmployeeAdd_Click(object sender, RoutedEventArgs e)
        {
            AddEmployeeGrid.Visibility = Visibility.Collapsed;
            BackButton.Visibility = Visibility.Collapsed;
            Employee_Checkbox.IsChecked = false;
            GolfClubListBox.Visibility = Visibility.Collapsed;
            if (selectedGolfclub != null)
            {
                string EmployeeName = EmployeeNameTextbox.Text;
                string EmployeeEmail = EmployeeEmailTextbox.Text;
                int EmployeeSalary;

                if (int.TryParse(EmployeeSalaryTextbox.Text, out EmployeeSalary))
                {
                    Employee employee = databaseconnection.AddEmployee(EmployeeName, EmployeeEmail, EmployeeSalary, selectedGolfclub.Id);
                    employeeDictionary.Add(employee.Id, employee);
                }
                else
                {
                    MessageBox.Show("Please enter a reasonable salary.");
                }
            }
            else
            {
                MessageBox.Show("Please select a golf club.");
            }
        }

         private void GolfclubAdd_Click(object sender, RoutedEventArgs e)
         {

                AddGolfclubGrid.Visibility = Visibility.Collapsed;
                BackButton.Visibility = Visibility.Collapsed;
                Golfclub_Checkbox.IsChecked = false;
                GolfClubListBox.Visibility = Visibility.Collapsed;

                string GolfclubName = GolfclubNameTextbox.Text;
                string GolfclubCountry = GolfclubCountryTextbox.Text;
                string GolfclubCity = GolfclubCityTextbox.Text;

                Golfclub golfclub = databaseconnection.AddGolfclub(GolfclubName, GolfclubCountry, GolfclubCity);
                golfclubDictionary.Add(golfclub.Id, golfclub);
                GolfClubListBox.Items.Refresh();

         }

            private void BackButton_Click(object sender, RoutedEventArgs e)
            {
                AddGolferGrid.Visibility = Visibility.Collapsed;
                AddGolfcourseGrid.Visibility = Visibility.Collapsed;
                AddGolfclubGrid.Visibility = Visibility.Collapsed;
                AddEmployeeGrid.Visibility = Visibility.Collapsed;
                BackButton.Visibility = Visibility.Collapsed;
                GolfClubListBox.Visibility = Visibility.Collapsed;
                Golfer_Checkbox.IsChecked = false;
                Employee_Checkbox.IsChecked = false;
                Golfclub_Checkbox.IsChecked = false;
                Golfcourse_Checkbox.IsChecked = false;
            InformationPage informationpage = new InformationPage();
            this.Close();
            informationpage.Show();

        }

        private void BackButton2_Click(object sender, RoutedEventArgs e)
        {

                AddGolferGrid.Visibility = Visibility.Collapsed;
                AddGolfcourseGrid.Visibility = Visibility.Collapsed;
                AddGolfclubGrid.Visibility = Visibility.Collapsed;
                AddEmployeeGrid.Visibility = Visibility.Collapsed;
                BackButton.Visibility = Visibility.Collapsed;
                GolfClubListBox.Visibility = Visibility.Collapsed;
                Golfer_Checkbox.IsChecked = false;
                Employee_Checkbox.IsChecked = false;
                Golfclub_Checkbox.IsChecked = false;
                Golfcourse_Checkbox.IsChecked = false;
                InformationPage informationpage = new InformationPage();
                this.Close();
                informationpage.Show();

            
        }
    }
    }
