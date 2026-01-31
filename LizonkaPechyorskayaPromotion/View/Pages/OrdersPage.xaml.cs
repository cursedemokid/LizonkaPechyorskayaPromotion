using LizonkaPechyorskayaPromotion.View.Windows;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace LizonkaPechyorskayaPromotion.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        public OrdersPage()
        {
            InitializeComponent();

            OrdersLb.ItemsSource = App.context.Share.ToList();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            AddShareWindow addShareWindow = new AddShareWindow();
            if (addShareWindow.ShowDialog() == true)
            {
                OrdersLb.ItemsSource = App.context.Share.ToList();
            }
        }

        private void ChangeBtn_Click(object sender, RoutedEventArgs e)
        {
            var share = OrdersLb.SelectedItem as Share;
            if (share != null)
            {
                AddShareWindow addShareWindow = new AddShareWindow(share.Id);
                if (addShareWindow.ShowDialog() == true)
                {
                    OrdersLb.ItemsSource = App.context.Share.ToList();
                }
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var share = OrdersLb.SelectedItem as Share;
            if (share != null)
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить акцию?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    App.context.Share.Remove(share);
                    App.context.SaveChanges();
                    OrdersLb.ItemsSource = App.context.Share.ToList();
                }
            }
        }
    }
}
