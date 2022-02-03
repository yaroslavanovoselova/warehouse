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
    /// Логика взаимодействия для GetNameOfWarehouse.xaml
    /// </summary>
    public partial class GetNameOfWarehouse : Window
    {
        /// <summary>
        /// Введенное пользователем название склада.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Путь к складу, который нужно загрузить.
        /// </summary>
        public string Path { get; set; }

        public GetNameOfWarehouse()
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
                if (NameOfWarehouseTextBox.Text.Replace(" ", "").Length == 0)
                    throw new Exception("Вы не заполнили поле или не загрузили склад. \nПопробуйте еще раз.");
                if (NameOfWarehouseTextBox.Text.Contains("\\")|| NameOfWarehouseTextBox.Text.Contains("/")) 
                    throw new Exception("Имя склада не может содержать \"\\\" или \"/\". \nПопробуйте еще раз.");
                Name = NameOfWarehouseTextBox.Text.Trim();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Произошла ошибка!");
            }
        }

        /// <summary>
        /// Получение пути к складу который нужно загрузить.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadWarehouse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
                dialog.Filter = "Файл JSON (*.json)|*.json";

                Nullable<bool> result = dialog.ShowDialog();

                if (result == true)
                {
                    Path = dialog.FileName;
                    NameOfWarehouseTextBox.Text = System.IO.Path.GetFileName(Path).Replace(".json", "");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Произошла ошибка!");
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (Name == null)
                {
                    e.Cancel = true;
                    throw new Exception("Введите имя склада, без этого нельзя продолжить.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Произошла ошибка!");
            }
        }
    }
}
