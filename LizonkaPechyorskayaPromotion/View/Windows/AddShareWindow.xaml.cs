using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace LizonkaPechyorskayaPromotion.View.Windows
{
    public partial class AddShareWindow : Window
    {
        private Share _currentShare;

        public AddShareWindow()
        {
            InitializeComponent();

            ClientCmb.ItemsSource = App.context.User
                .Where(u => u.PassportData == null)
                .ToList();

            TypeCmb.ItemsSource = App.context.ShareType
                .ToList();
        }

        public AddShareWindow(int id)
        {
            InitializeComponent();

            _currentShare = App.context.Share
                .FirstOrDefault(s => s.Id == id);

            ClientCmb.ItemsSource = App.context.User
                .Where(u => u.PassportData == null)
                .ToList();

            TypeCmb.ItemsSource = App.context.ShareType
                .ToList();

            if (_currentShare != null)
            {
                NameTb.Text = _currentShare.Name;
                HourlyPayUserTb.Text = _currentShare.HourlyPayUser.ToString();
                PriceTb.Text = _currentShare.Price.ToString();
                QuantityPromotersTb.Text = _currentShare.QuantityPromoters.ToString();
                ClientCmb.SelectedValue = _currentShare.ClientId;
                TypeCmb.SelectedValue = _currentShare.TypeId;
                StartDateDp.SelectedDate = _currentShare.StartDate;
                EndDateDp.SelectedDate = _currentShare.EndDate;
            }
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            // Валидация
            if (string.IsNullOrWhiteSpace(NameTb.Text) ||
                ClientCmb.SelectedItem == null ||
                TypeCmb.SelectedItem == null ||
                string.IsNullOrWhiteSpace(PriceTb.Text) ||
                StartDateDp.SelectedDate == null ||
                EndDateDp.SelectedDate == null ||
                string.IsNullOrWhiteSpace(QuantityPromotersTb.Text) ||
                string.IsNullOrWhiteSpace(HourlyPayUserTb.Text))
            {
                MessageBox.Show("Заполните все поля", "Ошибка", 
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                if (_currentShare == null) // Добавление
                {
                    var share = new Share
                    {
                        Name = NameTb.Text,
                        TypeId = (int)TypeCmb.SelectedValue,
                        ClientId = (int)ClientCmb.SelectedValue,
                        Price = Convert.ToDecimal(PriceTb.Text),
                        StartDate = (DateTime)StartDateDp.SelectedDate,
                        EndDate = (DateTime)EndDateDp.SelectedDate,
                        QuantityPromoters = Convert.ToInt32(QuantityPromotersTb.Text),
                        StatusId = 4,
                        ManegerId = App.currentUser.Id,
                        HourlyPayUser = Convert.ToDecimal(HourlyPayUserTb.Text)
                    };
                    App.context.Share.Add(share);
                }
                else // Редактирование
                {
                    _currentShare.Name = NameTb.Text;
                    _currentShare.TypeId = (int)TypeCmb.SelectedValue;
                    _currentShare.ClientId = (int)ClientCmb.SelectedValue;
                    _currentShare.Price = Convert.ToDecimal(PriceTb.Text);
                    _currentShare.StartDate = (DateTime)StartDateDp.SelectedDate;
                    _currentShare.EndDate = (DateTime)EndDateDp.SelectedDate;
                    _currentShare.QuantityPromoters = Convert.ToInt32(QuantityPromotersTb.Text);
                    _currentShare.HourlyPayUser = Convert.ToDecimal(HourlyPayUserTb.Text);
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