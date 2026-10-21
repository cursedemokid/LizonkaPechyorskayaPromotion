using ProjectSecurity;
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

        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        private void EnterBtn_Click(object sender, RoutedEventArgs e)
        {
            var user = App.context.User.FirstOrDefault(u => u.Login == LoginTb.Text);
            if (user != null && PasswordSecurity.Verify(PasswordPb.Password, user.Password))
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
