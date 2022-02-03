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
    /// Логика взаимодействия для CreateClassifier.xaml
    /// </summary>
    public partial class CreateCategory : Window
    {
        /// <summary>
        /// Поле-имя раздела.
        /// </summary>
        string categoryName = null;

        /// <summary>
        /// Свойство, возвращающее введенное пользователем название раздела.
        /// </summary>
        public string CategoryName { get => categoryName; }

        public CreateCategory()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Проверка введенных пользователем данных и их сохранение.
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!(NameOfCategoryTextBox.Text.Replace(" ", "").Length > 0))
                MessageBox.Show("Введенное название имеет неверный формат. \nПопробуйте ввести еще раз.");
            else
            {
                categoryName = NameOfCategoryTextBox.Text.Trim();
                Close();
            }
        }
    }
}
