using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace warehouse
{
    /// <summary>
    /// Логика взаимодействия для ItemImage.xaml
    /// </summary>
    public partial class ItemImage : Window
    {
        /// <summary>
        /// Загрузка фотографии товара.
        /// </summary>
        public ItemImage(ImageSource source)
        {
            InitializeComponent();
            ProductImage.Source = source;
        }
    }
}
