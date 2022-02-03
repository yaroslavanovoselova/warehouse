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
    /// Логика взаимодействия для SetMinAmountOfItems.xaml
    /// </summary>
    public partial class SetMinAmountOfItems : Window
    {
        /// <summary>
        /// Минимальное количество товаров на складе.
        /// </summary>
        public int MinAmountOfItems {get;set;}
        public SetMinAmountOfItems()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Проверка введенных пользователем данных и их сохранение.
        /// </summary>
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MinAmountTextBox.Text.Replace(" ", "").Length == 0)
                    throw new Exception("Вы не заполнили поле. \nПопробуйте еще раз.");
                if (!int.TryParse(MinAmountTextBox.Text, out var minAmount))
                    throw new Exception("Введенный текст имеет неверный формат. \nПопробуйте еще раз.");
                if (minAmount<0)
                    throw new Exception("Количество товаров не может быть меньше нуля. \nПопробуйте еще раз.");
                MinAmountOfItems = minAmount;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Произошла ошибка!");
            }
        }
    }
}
