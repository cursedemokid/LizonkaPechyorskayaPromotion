using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace LizonkaPechyorskayaPromotion.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для PromoOrdersPage.xaml
    /// </summary>
    public partial class PromoOrdersPage : Page
    {
        public PromoOrdersPage()
        {
            InitializeComponent();

            LoadAvailableShares();
        }

        private void FeedbackBtn_Click(object sender, RoutedEventArgs e)
        {
            var share = OrdersLb.SelectedItem as Share;

            var isAlreadyRegistered = App.context.UserShare
                    .Any(us => us.UserId == App.currentUser.Id && us.ShareId == share.Id);

            if (isAlreadyRegistered)
            {
                MessageBox.Show("Вы уже зарегистрированы на эту акцию",
                    "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            App.context.UserShare.Add(new UserShare
            {
                ShareId = share.Id,
                UserId = App.currentUser.Id,
                PromoterShift = 8
            });
            App.context.SaveChanges();
            MessageBox.Show("Вы успешно откликнулись на акцию");
        }

        private void LoadAvailableShares()
        {
            var currentDate = DateTime.Now.Date;

            var allShares = App.context.Share.ToList();
            var availableShares = new List<Share>();

            foreach (var share in allShares)
            {
                if (share.StartDate > currentDate)
                {
                    int registeredCount = App.context.UserShare
                        .Count(us => us.ShareId == share.Id);

                    if (registeredCount < share.QuantityPromoters)
                    {
                        availableShares.Add(share);
                    }
                }
            }

            OrdersLb.ItemsSource = availableShares
                .OrderBy(s => s.StartDate)
                .ToList();
        }
    }
}
