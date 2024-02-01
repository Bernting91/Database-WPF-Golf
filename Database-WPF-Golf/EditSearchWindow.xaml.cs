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
    /// Interaction logic for EditSearchWindow.xaml
    /// </summary>
    public partial class EditSearchWindow : Window
    {
        Databaseconnection databaseconnection;
        public Dictionary<int, Golfclub> golfclubDictionary;
        public Dictionary<int, Golfer> golferDictionary;
        public Dictionary<int, Golfcourse> golfcourseDictionary;
        public Dictionary<int, Employee> employeeDictionary;
        public Golfclub selectedGolfclub;
        public EditSearchWindow(Dictionary<int, Golfclub> golfclubs)
        {
            InitializeComponent();
            databaseconnection = new Databaseconnection("localhost", "InlämningDatabaser", "root", "");
            golfclubDictionary = golfclubs;
            golferDictionary = new Dictionary<int, Golfer>();
            employeeDictionary = new Dictionary<int, Employee>();
            golfcourseDictionary = new Dictionary<int, Golfcourse>();
            EditGolfclubListBox.ItemsSource = golfclubs.Values.ToList();
            EditGolfclubListBox.Items.Refresh();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = SearchBar.Text;

            if (string.IsNullOrWhiteSpace(searchText))
            {
                return;
            }
            SearchListBox.Items.Clear();
            List<object> searchResults = databaseconnection.SearchInDatabase(searchText);
            foreach (object result in searchResults)
            {
                SearchListBox.Items.Add(result);
            }
        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("EditButton_Click method executed!");
            if (SearchListBox.SelectedItem != null)
            {
                dynamic selectedItem = SearchListBox.SelectedItem;
                MessageBox.Show($"Selected item type: {selectedItem.GetType().Name}");
                if (selectedItem is Golfclub)
                {
                    Golfclub selectedGolfclub = (Golfclub)selectedItem;
                    selectedGolfclub.Name = TopTextBox.Text.Trim();
                    selectedGolfclub.Country = MiddleTextbox.Text.Trim();
                    selectedGolfclub.City = BottomTextBox.Text.Trim();
                    databaseconnection.UpdateGolfclub(selectedGolfclub);
                }
                else if (selectedItem is Employee)
                {
                    Employee selectedEmployee = (Employee)selectedItem;
                    selectedEmployee.Name = TopTextBox.Text.Trim();
                    selectedEmployee.Email = MiddleTextbox.Text.Trim();
                    selectedEmployee.Salary = int.Parse(BottomTextBox.Text.Trim());
                    databaseconnection.UpdateEmployee(selectedEmployee);
                }
                else if (selectedItem is Golfcourse)
                {
                    Golfcourse selectedGolfcourse = (Golfcourse)selectedItem;
                    selectedGolfcourse.Name = TopTextBox.Text.Trim();
                    selectedGolfcourse.Holes = int.Parse(MiddleTextbox.Text.Trim());
                    selectedGolfcourse.Greenfee = int.Parse(BottomTextBox.Text.Trim());
                    databaseconnection.UpdateGolfcourse(selectedGolfcourse);
                }
                else if (selectedItem is Golfer)
                {
                    Golfer selectedGolfer = (Golfer)selectedItem;
                    selectedGolfer.Name = TopTextBox.Text.Trim();
                    selectedGolfer.Email = MiddleTextbox.Text.Trim();
                    selectedGolfer.Handicap = int.Parse(BottomTextBox.Text.Trim());
                    databaseconnection.UpdateGolfer(selectedGolfer);
                }
                SearchListBox.SelectedItem = null;
                RefreshSearchResults();

            }
        }

        private void RefreshSearchResults()
        {
            string searchText = SearchBar.Text;
            SearchListBox.Items.Clear();

            List<object> searchResults = databaseconnection.SearchInDatabase(searchText);
            foreach (object result in searchResults)
            {
                SearchListBox.Items.Add(result);
            }
        }


        private void SearchListBox_Selectionchanged(object sender, SelectionChangedEventArgs e)
        {
            if (SearchListBox.SelectedItem != null)
            {
                dynamic selectedResult = SearchListBox.SelectedItem;

                if (selectedResult is Golfer)
                {
                    Golfer selectedGolfer = (Golfer)selectedResult;
                    TopTextBox.Text = selectedGolfer.Name;
                    MiddleTextbox.Text = selectedGolfer.Email;
                    BottomTextBox.Text = selectedGolfer.Handicap.ToString();
                }
                else if (selectedResult is Golfclub)
                {
                    Golfclub selectedGolfclub = (Golfclub)selectedResult;
                    TopTextBox.Text = selectedGolfclub.Name;
                    MiddleTextbox.Text = selectedGolfclub.Country;
                    BottomTextBox.Text = selectedGolfclub.City;
                }
                else if (selectedResult is Golfcourse)
                {
                    Golfcourse selectedGolfcourse = (Golfcourse)selectedResult;
                    TopTextBox.Text = selectedGolfcourse.Name;
                    MiddleTextbox.Text = selectedGolfcourse.Holes.ToString();
                    BottomTextBox.Text = selectedGolfcourse.Greenfee.ToString();
                }
                else if (selectedResult is Employee)
                {
                    Employee selectedEmployee = (Employee)selectedResult;
                    TopTextBox.Text = selectedEmployee.Name;
                    MiddleTextbox.Text = selectedEmployee.Email;
                    BottomTextBox.Text = selectedEmployee.Salary.ToString();
                }
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            InformationPage informationPage = new InformationPage();
            this.Close();
            informationPage.Show();
        }
    }
}

