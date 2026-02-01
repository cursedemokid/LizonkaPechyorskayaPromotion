using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace LizonkaPechyorskayaPromotion.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        List<User> _users = App.context.User.ToList();
        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        private void EnterBtn_Click(object sender, RoutedEventArgs e)
        {
            var user = _users
                .FirstOrDefault(u => u.Login == LoginTb.Text
            && u.Password == PasswordPb.Password);
            if (user != null)
            {
                App.currentUser = user;
                MessageBox.Show($"Добро пожаловать, {user.FullName}");

                if (user.RoleId == 1)
                {
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                }
                else if (user.RoleId == 3)
                {
                    PromotionWindow promotionWindow = new PromotionWindow();
                    promotionWindow.Show();
                }
                    Close();
            }
            else
            {
                MessageBox.Show($"Вы неправильно ввели логин или пароль! Введите снова и повторите попытку", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
