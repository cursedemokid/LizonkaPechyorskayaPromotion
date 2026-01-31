using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace LizonkaPechyorskayaPromotion.View.Windows
{
    public partial class AddEmployeeWindow : Window
    {
        private User _currentUser;

        public AddEmployeeWindow()
        {
            InitializeComponent();

            RoleCmb.ItemsSource = App.context.Role.ToList();
        }

        public AddEmployeeWindow(int id)
        {
            InitializeComponent();

            _currentUser = App.context.User
                .FirstOrDefault(u => u.Id == id);

            RoleCmb.ItemsSource = App.context.Role.ToList();

            if (_currentUser != null)
            {
                FullNameTb.Text = _currentUser.FullName;
                LoginTb.Text = _currentUser.Login;
                PasswordTb.Text = _currentUser.Password;
                PassportDataTb.Text = _currentUser.PassportData;
                RoleCmb.SelectedValue = _currentUser.RoleId;
            }
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            // Валидация
            if (string.IsNullOrWhiteSpace(FullNameTb.Text) ||
                string.IsNullOrWhiteSpace(LoginTb.Text) ||
                string.IsNullOrWhiteSpace(PasswordTb.Text) ||
                RoleCmb.SelectedItem == null)
            {
                MessageBox.Show("Заполните все обязательные поля", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                if (_currentUser == null) // Добавление
                {
                    var user = new User
                    {
                        FullName = FullNameTb.Text,
                        Login = LoginTb.Text,
                        Password = PasswordTb.Text,
                        RoleId = (int?)RoleCmb.SelectedValue,
                        PassportData = PassportDataTb.Text
                    };
                    App.context.User.Add(user);
                }
                else // Редактирование
                {
                    _currentUser.FullName = FullNameTb.Text;
                    _currentUser.Login = LoginTb.Text;
                    _currentUser.Password = PasswordTb.Text;
                    _currentUser.RoleId = (int?)RoleCmb.SelectedValue;
                    _currentUser.PassportData = PassportDataTb.Text;
                }

                App.context.SaveChanges();
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}