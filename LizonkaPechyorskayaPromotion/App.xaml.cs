using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace LizonkaPechyorskayaPromotion
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static User currentUser { get; set; }
        public static PechyorskayaPromotionEntities context = new PechyorskayaPromotionEntities();
    }
}
