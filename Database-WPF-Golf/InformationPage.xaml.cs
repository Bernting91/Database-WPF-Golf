using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
    /// Interaction logic for InformationPage.xaml
    /// </summary>
    public partial class InformationPage : Window
    {
        Databaseconnection databaseconnection;
        Dictionary<int, Golfclub> golfclubDictionary;

        public InformationPage()
        {
            InitializeComponent();
            databaseconnection = new Databaseconnection("localhost", "InlämningDatabaser", "root", "");
            golfclubDictionary = databaseconnection.GetGolfClubs();
            GolfclubsListBox.ItemsSource = golfclubDictionary.Values.ToList();
            GolfclubsListBox.Items.Refresh();

        }

        private void Golfclub_select(object sender, SelectionChangedEventArgs e)
        {
            Golfclub selectedGolfclub = GolfclubsListBox.SelectedItem as Golfclub;
            if (selectedGolfclub != null)
            {
                GCVisible();
                UpdateEmployeeListBox(selectedGolfclub.Id);
                UpdateGolfcourseListBox(selectedGolfclub.Id);
                UpdateGolferListBox(selectedGolfclub.Id);
                GolfclubCountryTextBlock.Text = selectedGolfclub.Country;
                GolfClubCityTextBlock.Text = selectedGolfclub.City;
            }
            else
            {
                GCCollapsed();
            }
        }

        public void GCVisible()
        {
            EmployeeListBox.Visibility = Visibility.Visible;
            EmployeeTextBlock.Visibility = Visibility.Visible;
            GolfclubInfoTextBlock.Visibility = Visibility.Visible;
            golfcourseListBox.Visibility = Visibility.Visible;
            GolfcourseTextBlock.Visibility = Visibility.Visible;
            GolfClubMembers.Visibility = Visibility.Visible;
            GolfclubCountryTextBlock.Visibility = Visibility.Visible;
            GolfClubCityTextBlock.Visibility = Visibility.Visible;
            BackgroundGolfcourse.Visibility = Visibility.Visible;
            GolferListBox.Visibility = Visibility.Visible;
        }
        public void GCCollapsed()
        {
            EmployeeListBox.Visibility = Visibility.Collapsed;
            EmployeeTextBlock.Visibility = Visibility.Collapsed;
            GolfclubInfoTextBlock.Visibility = Visibility.Collapsed;
            golfcourseListBox.Visibility = Visibility.Collapsed;
            GolfcourseTextBlock.Visibility = Visibility.Collapsed;
            GolfClubMembers.Visibility = Visibility.Collapsed;
            GolfclubCountryTextBlock.Visibility = Visibility.Collapsed;
            GolfClubCityTextBlock.Visibility = Visibility.Collapsed;
            BackgroundGolfcourse.Visibility = Visibility.Collapsed;
            GolferListBox.Visibility = Visibility.Collapsed;
        }
        private void UpdateEmployeeListBox(int golfclub)
        {
            Dictionary<int, Employee> employeeDictionary = databaseconnection.GetEmployeesForGolfclub(golfclub);
            EmployeeListBox.ItemsSource = employeeDictionary.Values.ToList();
            EmployeeListBox.Items.Refresh();
        }

        private void UpdateGolfcourseListBox(int golfclub)
        {
            Dictionary<int, Golfcourse> golfcourseDictionary = databaseconnection.GetGolfcoursesForGolfclub(golfclub);
            golfcourseListBox.ItemsSource = golfcourseDictionary.Values.ToList();
            golfcourseListBox.Items.Refresh();
        }

        private void UpdateGolferListBox(int golfclub)
        {
        Dictionary<int, Golfer> golferDictionary = databaseconnection.GetGolfersForGolfclub(golfclub);
            GolferListBox.ItemsSource = golferDictionary.Values.ToList();
            GolferListBox.Items.Refresh();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddWindow addwindow = new AddWindow(golfclubDictionary);
            this.Close();
            addwindow.Show();
           
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (GolferListBox.SelectedItem != null)
            {
                Golfer selectedGolfer = GolferListBox.SelectedItem as Golfer;
                databaseconnection.DeleteGolfer(selectedGolfer.Id);
                UpdateGolferListBox(selectedGolfer.Id);
            }
            else if (golfcourseListBox.SelectedItem != null)
            {
                Golfcourse selectedGolfcourse = golfcourseListBox.SelectedItem as Golfcourse;
                databaseconnection.DeleteGolfcourse(selectedGolfcourse.Id);
                UpdateGolfcourseListBox(selectedGolfcourse.Id);
            }
            else if (EmployeeListBox.SelectedItem != null)
            {
                Employee selectedEmployee = EmployeeListBox.SelectedItem as Employee;
                databaseconnection.DeleteEmployee(selectedEmployee.Id);
                UpdateEmployeeListBox(selectedEmployee.Id);
            }
            else if (GolfclubsListBox.SelectedItem != null)
            {
                Golfclub selectedGolfclub = GolfclubsListBox.SelectedItem as Golfclub;
                databaseconnection.DeleteGolfclub(selectedGolfclub.Id);
                golfclubDictionary.Remove(selectedGolfclub.Id);
                GolfclubsListBox.ItemsSource = golfclubDictionary.Values.ToList();
                GolfclubsListBox.Items.Refresh();
                GCCollapsed();
            }
            else
            {
                MessageBox.Show("Please select an item to delete.");
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            EditSearchWindow editSearchWindow = new EditSearchWindow(golfclubDictionary);
            this.Close();
            editSearchWindow.Show();
        }
    }
}








