using LizonkaPechyorskayaPromotion.View.Windows;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace LizonkaPechyorskayaPromotion.View.Pages
{
    public partial class EmployeesPage : Page
    {
        public EmployeesPage()
        {
            InitializeComponent();
            LoadEmployees();
        }

        private void LoadEmployees()
        {
            EmployeesLv.ItemsSource = App.context.User.ToList();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            AddEmployeeWindow addEmployeeWindow = new AddEmployeeWindow();
            if (addEmployeeWindow.ShowDialog() == true)
            {
                LoadEmployees();
            }
        }

        private void ChangeBtn_Click(object sender, RoutedEventArgs e)
        {
            var user = EmployeesLv.SelectedItem as User;
            if (user != null)
            {
                AddEmployeeWindow addEmployeeWindow = new AddEmployeeWindow(user.Id);
                if (addEmployeeWindow.ShowDialog() == true)
                {
                    LoadEmployees();
                }
            }
            else
            {
                MessageBox.Show("Выберите сотрудника", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var user = EmployeesLv.SelectedItem as User;
            if (user != null)
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить сотрудника?", "Внимание",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    App.context.User.Remove(user);
                    App.context.SaveChanges();
                    LoadEmployees();
                }
            }
            else
            {
                MessageBox.Show("Выберите сотрудника", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}