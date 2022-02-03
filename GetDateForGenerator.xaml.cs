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
    /// Логика взаимодействия для GetDateForGenerator.xaml
    /// </summary>

    // Я не успела доделать этот пункт, поэтому ничего не работает.
    public partial class GetDateForGenerator : Window
    {
        public int AmountOfCategories { get; set; } = -1;
        public int AmountOfItems { get; set; } = -1;
        public GetDateForGenerator()
        {
            InitializeComponent();
        }

        private void GenerateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AmountOfItemsTextBox.Text.Replace(" ", "").Length == 0 || AmountOfCategoriesTextBox.Text.Replace(" ", "").Length == 0)
                    throw new Exception("Вы не заполнили обязательные поля. \nПопробуйте еще раз.");
                if (!int.TryParse(AmountOfItemsTextBox.Text, out var amountOfItems) || !int.TryParse(AmountOfCategoriesTextBox.Text, out var amountOfCategories)
                    || amountOfItems < 0 || amountOfCategories < 0)
                    throw new Exception("Количество должно быть натуральным числом. \nПопробуйте ввести еще раз.");
                if (amountOfItems  >0 && amountOfCategories == 0 )
                    throw new Exception("Создание товаров без разделов невозможно. \nПовторите ввод.");
                if (amountOfItems > 100 || amountOfCategories > 100)
                    throw new Exception("Для адекватной генерации количество раздело и товаров должно быть не больше 100. \nПовторите ввод.");
                AmountOfItems = amountOfItems;
                AmountOfCategories = amountOfCategories;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Произошла ошибка!");
            }
        }
    }
}
