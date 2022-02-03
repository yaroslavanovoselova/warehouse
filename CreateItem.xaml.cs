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
    /// Логика взаимодействия для CreateItem.xaml
    /// </summary>
    public partial class CreateItem : Window
    {
        /// <summary>
        /// Название товара.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Артикул товара.
        /// </summary>
        public string ArticleNumber { get; set; }

        /// <summary>
        /// Количество товара, оставшееся на складе.
        /// </summary>
        public int AmountLeft { get; set; } = -1;

        /// <summary>
        /// Стоимость товара.
        /// </summary>
        public double Price { get; set; } = -1;

        /// <summary>
        /// Дополнительная информация об товаре
        /// </summary>
        public string Info { get; set; }

        public CreateItem()
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
                if (NameOfItemTextBox.Text.Replace(" ", "").Length == 0 || ArticleNumberTextBox.Text.Replace(" ", "").Length == 0 ||
                    AmountLeftTextBox.Text.Replace(" ", "").Length == 0 || PriceTextBox.Text.Replace(" ", "").Length == 0)
                    throw new Exception("Вы не заполнили обязательные поля. \nПопробуйте еще раз.");
                if (!int.TryParse(AmountLeftTextBox.Text, out var amountLeft)||!double.TryParse(PriceTextBox.Text, out var price))
                    throw new Exception("Одно или несколько полей имеют неверный формат. \nПопробуйте еще раз.");
                if (amountLeft<0 || price<0)
                    throw new Exception("Цена и количество должны быть не меньше нуля. \nПопробуйте ввести еще раз.");
                if(MainWindow.ArticleNumbersOfAllItems.Contains(ArticleNumberTextBox.Text))
                    throw new Exception("Товар с таким артикулом уже добавлен в какой-то из разделов. \nПопробуйте ввести еще раз.");
                Name = NameOfItemTextBox.Text.Trim();
                ArticleNumber = ArticleNumberTextBox.Text.Trim();
                AmountLeft = amountLeft;
                Price = price;
                Info = DescriptionTextBox.Text.Trim();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Произошла ошибка!");
            }
        }

        /// <summary>
        /// Выбор и сохранение картинки.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DownloadImageButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dialog = new Microsoft.Win32.OpenFileDialog();
                dialog.Filter = "Image files (*.jpg,*.png;*.jpeg)|*.png;*.jpeg;*.jpg";
                Nullable<bool> result = dialog.ShowDialog();
                if (result == true)
                {
                    var converter = new ImageSourceConverter();
                    ItemImage.Source = (ImageSource)converter.ConvertFromString(dialog.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Произошла ошибка!");
            }
        }
    }
}
