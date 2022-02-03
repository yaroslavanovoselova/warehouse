using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace warehouse
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary> 
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Контекстное меню, появляющееся при клике на раздел.
        /// </summary>
        ContextMenu CategoryContextMenu = new ContextMenu();

        /// <summary>
        /// Путь для сериализации.
        /// </summary>
        string PathToSavedInfo;

        /// <summary>
        /// Минимальное кол-во товаров, задаваемое пользователем.
        /// </summary>
        int MinAmountOfItems = -1;

        /// <summary>
        /// Артикулы всех созданных товаров.
        /// </summary>
        public static List<string> ArticleNumbersOfAllItems;

        public MainWindow()
        {
            InitializeComponent();
            InitializeCategoryContextMenu();
            ArticleNumbersOfAllItems = new List<string>();

            var getNameDialog = new GetNameOfWarehouse();
            getNameDialog.ShowDialog();
            if (getNameDialog.Name == null)
                return;

            // Загрузка склада, если пользователь выбрал путь.
            if (getNameDialog.Path == null)
            {
                Title = getNameDialog.Name;
                var pathToDir = Directory.GetCurrentDirectory();
                PathToSavedInfo = System.IO.Path.Combine(pathToDir, $"{getNameDialog.Name}.json");
            }
            else
                LoadPreviousData(getNameDialog.Path);


        }

        /// <summary>
        /// Инициализация контексного меню для TreeView.
        /// </summary>
        public void InitializeCategoryContextMenu()
        {
            var createSubcategoryMenuItem = new MenuItem { Header = "Добавить подраздел" };
            createSubcategoryMenuItem.Click += CreateSubcategoryMenuItem_Click;
            var createItemMenuItem = new MenuItem { Header = "Добавить товар" };
            createItemMenuItem.Click += CreateItemMenuItem_Click;
            var deleteCategoryMenuItem = new MenuItem { Header = "Удалить раздел" };
            deleteCategoryMenuItem.Click += DeleteCategoryMenuItem_Click;
            var changeCategoryMenuItem = new MenuItem { Header = "Изменить имя раздела" };
            changeCategoryMenuItem.Click += ChangeCategoryMenuItem_Click;
            CategoryContextMenu.Items.Add(createSubcategoryMenuItem);
            CategoryContextMenu.Items.Add(deleteCategoryMenuItem);
            CategoryContextMenu.Items.Add(changeCategoryMenuItem);
            CategoryContextMenu.Items.Add(createItemMenuItem);
            CategoriesTreeView.ContextMenuOpening += CategoryContextMenu_ContextMenuOpening;
        }

        /// <summary>
        /// Создание главных разделов.
        /// </summary>
        private void CreateCategoryMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var createCategoryDialog = new CreateCategory();
                createCategoryDialog.ShowDialog();
                if (createCategoryDialog.CategoryName == null)
                    return;
                var category = new Category(createCategoryDialog.CategoryName);
                if (Category.MainCategories.Any(mainCategory => mainCategory.Name == category.Name))
                    throw new Exception("Раздел с таким названием уже существует. \nПопробуйте придумать новое.");
                Category.MainCategories.Add(category);
                var newTVI = new TreeViewItem { Header = createCategoryDialog.CategoryName, ContextMenu = CategoryContextMenu, Tag = category };
                newTVI.MouseDoubleClick += TVI_MouseDoubleClick;
                CategoriesTreeView.Items.Add(newTVI);
                Serialize();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Произошла ошибка!");
            }
        }

        /// <summary>
        /// Создание подразделов.
        /// </summary>
        private void CreateSubcategoryMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var parentTVI = (TreeViewItem)CategoriesTreeView.SelectedItem;
                var createCategoryDialog = new CreateCategory();
                createCategoryDialog.ShowDialog();
                if (createCategoryDialog.CategoryName == null)
                    return;
                var parentCategory = (Category)parentTVI.Tag;
                if (parentCategory.Subcategories.Any(category => category.Name == createCategoryDialog.CategoryName))
                    throw new Exception("У этого раздела уже существует дочерний раздел с таким же именем.\nПопробуйте придумать что-то другое.");
                var newSubcategory = new Category(createCategoryDialog.CategoryName);
                parentCategory.Subcategories.Add(newSubcategory);
                var newTVI = new TreeViewItem { Header = createCategoryDialog.CategoryName, ContextMenu = CategoryContextMenu, Tag = newSubcategory };
                newTVI.MouseDoubleClick += TVI_MouseDoubleClick;
                parentTVI.Items.Add(newTVI);
                Serialize();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Произошла ошибка!");
            }
        }

        /// <summary>
        /// Удаление раздела.
        /// </summary>
        private void DeleteCategoryMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var TVIToDelete = (TreeViewItem)CategoriesTreeView.SelectedItem;
                var categoryToDelete = (Category)TVIToDelete.Tag;
                if (categoryToDelete.Subcategories.Count != 0 || categoryToDelete.Items.Count != 0)
                    throw new Exception("Этот раздел пока содерджит другие подразделы и товары. \nЧтобы его удалить, для начала удалите все его содержимое.");
                var parentTVI = TVIToDelete.Parent;
                if (parentTVI is TreeViewItem TVI)
                {
                    TVI.Items.Remove(TVIToDelete);
                    ((Category)TVI.Tag).Subcategories.Remove(categoryToDelete);
                }
                else
                {
                    ((TreeView)parentTVI).Items.Remove(TVIToDelete);
                    Category.MainCategories.Remove(categoryToDelete);
                }
                Serialize();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Произошла ошибка!");
            }
        }

        /// <summary>
        /// Изменение названия раздела.
        /// </summary>
        private void ChangeCategoryMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var TVIToChange = (TreeViewItem)CategoriesTreeView.SelectedItem;
                var categoryToChange = (Category)TVIToChange.Tag;
                var changeCategoryDialog = new CreateCategory();
                changeCategoryDialog.NameOfCategoryTextBox.Text = categoryToChange.Name;
                changeCategoryDialog.ShowDialog();
                if (changeCategoryDialog.CategoryName == null)
                    return;
                var parentTVI = TVIToChange.Parent;
                if (parentTVI is TreeViewItem TVI)
                {
                    if (((Category)TVI.Tag).Subcategories.Any(category => category.Name == changeCategoryDialog.CategoryName))
                        throw new Exception("У этого раздела уже существует дочерний раздел с таким же именем.\nПопробуйте придумать что-то другое.");

                }
                else
                {
                    if (Category.MainCategories.Any(category => category.Name == changeCategoryDialog.CategoryName))
                        throw new Exception("У этого раздела уже существует дочерний раздел с таким же именем.\nПопробуйте придумать что-то другое.");
                }
                categoryToChange.Name = changeCategoryDialog.CategoryName;
                TVIToChange.Header = changeCategoryDialog.CategoryName;
                TVIToChange.Tag = categoryToChange;
                Serialize();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Произошла ошибка!");
            }
        }

        /// <summary>
        /// Создание товара.
        /// </summary>
        private void CreateItemMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var parentTVI = (TreeViewItem)CategoriesTreeView.SelectedItem;
                var createItemDialog = new CreateItem();
                createItemDialog.ShowDialog();
                if (createItemDialog.Name == null || createItemDialog.ArticleNumber == null || createItemDialog.AmountLeft == -1 || createItemDialog.Price == -1)
                    return;
                var parentCategory = (Category)parentTVI.Tag;
                var newItem = new Item(createItemDialog.Name, createItemDialog.ArticleNumber, createItemDialog.AmountLeft, createItemDialog.Price, createItemDialog.Info, parentCategory);
                newItem.Image = createItemDialog.ItemImage.Source;
                parentCategory.Items.Add(newItem);
                ArticleNumbersOfAllItems.Add(newItem.ArticleNumber);
                ItemsDataGrid.Items.Clear();
                foreach (var item in parentCategory.Items)
                    ItemsDataGrid.Items.Add(item);
                MakeListOfSubcategoryItems(parentCategory);
                if (AllItemsOfSubcategory.Count != 0)
                {
                    ItemsDataGrid.Items.Add(new Item("Товары подразделов:", "", AllItemsOfSubcategory.Count, null, null, null));
                    foreach (var item in AllItemsOfSubcategory)
                        ItemsDataGrid.Items.Add(item);
                }
                if (MinAmountOfItems != -1)
                    GetCSVMenuItem.Visibility = Visibility.Visible;

                Serialize();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Произошла ошибка!");
            }
        }

        /// <summary>
        /// Выбранная категория.
        /// </summary>
        Category SelectedCategory;

        /// <summary>
        /// Отражение списка товаров при двойном нажатии на раздел.
        /// </summary>
        private void TVI_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ItemsDataGrid.Items.Clear();
                var parentTVI = (TreeViewItem)CategoriesTreeView.SelectedItem;
                if (parentTVI == null)
                {
                    MessageBox.Show("Вы не выбрали категорию, поэтому эта операция невозможна. \nПопробуйте еще раз.");
                    return;
                }
                var category = (Category)parentTVI.Tag;
                SelectedCategory = category;
                foreach (var item in category.Items)
                {
                    ItemsDataGrid.Items.Add(item);
                }

                MakeListOfSubcategoryItems(category);
                if (AllItemsOfSubcategory.Count != 0)
                {
                    ItemsDataGrid.Items.Add(new Item("Товары подразделов:", "", AllItemsOfSubcategory.Count, null, null, null));
                    foreach (var item in AllItemsOfSubcategory)
                        ItemsDataGrid.Items.Add(item);
                }
                Serialize();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Произошла ошибка!");
            }

        }

        /// <summary>
        /// Удаление товара.
        /// </summary>
        private void RemoveItemMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedRow = (Item)ItemsDataGrid.SelectedItem;

                if (!SelectedCategory.Items.Contains(selectedRow))
                {
                    MessageBox.Show("Этот товар не принадлежит выбранному разделу, поэтому не может быть удален здесь. " +
                        "\nОткройте подраздел, к которому он относится, и попробуйте это сделать там.");
                    return;
                }
                ArticleNumbersOfAllItems.Remove(selectedRow.ArticleNumber);
                selectedRow.ParentCategory.Items.Remove(selectedRow);
                ItemsDataGrid.Items.Clear();
                foreach (var item in selectedRow.ParentCategory.Items)
                    ItemsDataGrid.Items.Add(item);
                MakeListOfSubcategoryItems(selectedRow.ParentCategory);
                if (AllItemsOfSubcategory.Count != 0)
                {
                    ItemsDataGrid.Items.Add(new Item("Товары подразделов:", "", AllItemsOfSubcategory.Count, null, null, null));
                    foreach (var item in AllItemsOfSubcategory)
                        ItemsDataGrid.Items.Add(item);
                }
                Serialize();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Произошла ошибка!");
            }
        }

        /// <summary>
        /// Изменение параметров товара.
        /// </summary>
        private void ChangeItemMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedRow = (Item)ItemsDataGrid.SelectedItem;
                var index = selectedRow.ParentCategory.Items.IndexOf(selectedRow);
                var changeItemDialog = new CreateItem();

                changeItemDialog.NameOfItemTextBox.Text = selectedRow.Name;
                changeItemDialog.ArticleNumberTextBox.Text = selectedRow.ArticleNumber;
                changeItemDialog.PriceTextBox.Text = selectedRow.Price.ToString();
                changeItemDialog.AmountLeftTextBox.Text = selectedRow.AmountLeft.ToString();
                changeItemDialog.DescriptionTextBox.Text = selectedRow.AdditionalInfo;
                changeItemDialog.ItemImage.Source = selectedRow.Image;

                ArticleNumbersOfAllItems.Remove(selectedRow.ArticleNumber);

                changeItemDialog.ShowDialog();

                if (changeItemDialog.Name == null || changeItemDialog.ArticleNumber == null || changeItemDialog.AmountLeft == -1 || changeItemDialog.Price == -1)
                    return;

                selectedRow.Name = changeItemDialog.Name;
                selectedRow.ArticleNumber = changeItemDialog.ArticleNumber;
                selectedRow.Price = changeItemDialog.Price;
                selectedRow.AmountLeft = changeItemDialog.AmountLeft;
                selectedRow.AdditionalInfo = changeItemDialog.Info;

                var changedItem = new Item(changeItemDialog.Name, changeItemDialog.ArticleNumber, changeItemDialog.AmountLeft, changeItemDialog.Price, changeItemDialog.Info, selectedRow.ParentCategory);
                changedItem.Image = changeItemDialog.ItemImage.Source;
                selectedRow.ParentCategory.Items[index] = changedItem;

                ArticleNumbersOfAllItems.Add(changedItem.ArticleNumber);

                ItemsDataGrid.Items.Clear();
                foreach (var item in selectedRow.ParentCategory.Items)
                    ItemsDataGrid.Items.Add(item);
                MakeListOfSubcategoryItems(selectedRow.ParentCategory);
                if (AllItemsOfSubcategory.Count != 0)
                {
                    ItemsDataGrid.Items.Add(new Item("Товары подразделов:", "", AllItemsOfSubcategory.Count, null, null, null));
                    foreach (var item in AllItemsOfSubcategory)
                        ItemsDataGrid.Items.Add(item);
                }
                Serialize();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Произошла ошибка!");
            }
        }

        /// <summary>
        /// Создание нового склада.
        /// </summary>
        private void NewWarehouseMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ArticleNumbersOfAllItems = new List<string>();
                var getNameDialog = new GetNameOfWarehouse();
                getNameDialog.LoadWarehouseButton.Visibility = Visibility.Hidden;
                getNameDialog.LoadLabel.Visibility = Visibility.Hidden;
                getNameDialog.ShowDialog();
                if (getNameDialog.Name == null)
                    return;
                Title = getNameDialog.Name;
                var pathToDir = Directory.GetCurrentDirectory();
                PathToSavedInfo = System.IO.Path.Combine(pathToDir, $"{getNameDialog.Name}.json");
                Category.MainCategories.Clear();
                CategoriesTreeView.Items.Clear();
                ItemsDataGrid.Items.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Произошла ошибка!");
            }
        }

        /// <summary>
        /// Получение пути к складу и его загрузка..
        /// </summary>
        private void LoadWarehouseMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
                dialog.Filter = "Файл JSON (*.json)|*.json";
                Nullable<bool> result = dialog.ShowDialog();
                if (result == true)
                {
                    LoadPreviousData(dialog.FileName);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Произошла ошибка!");
            }
        }

        /// <summary>
        /// Загрузка выбранного пользователем склада.
        /// </summary>
        /// <param name="path"> Путь к сериализованному файлу. </param>
        private void LoadPreviousData(string path)
        {
            try
            {
                PathToSavedInfo = path;
                Title = System.IO.Path.GetFileName(path).Replace(".json", "");
                JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All };
                Category.MainCategories = JsonConvert.DeserializeObject<List<Category>>(File.ReadAllText(PathToSavedInfo), settings);
                CategoriesTreeView.Items.Clear();
                ItemsDataGrid.Items.Clear();
                ArticleNumbersOfAllItems = new List<string>();
                foreach (var category in Category.MainCategories)
                {
                    var newTVI = new TreeViewItem { Header = category.Name, ContextMenu = CategoryContextMenu, Tag = category };
                    newTVI.MouseDoubleClick += TVI_MouseDoubleClick;
                    CategoriesTreeView.Items.Add(newTVI);
                    foreach (var item in category.Items)
                    {
                        item.ParentCategory = category;
                        ArticleNumbersOfAllItems.Add(item.ArticleNumber);
                    }
                    RestoreCategories(category, newTVI);
                }
                GetCSVMenuItem.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Произошла ошибка десериализации! Будет создан новый пустой склад.");
                Title = "newStore";
                var pathToDir = Directory.GetCurrentDirectory();
                PathToSavedInfo = System.IO.Path.Combine(pathToDir, "newStore.json");
            }
        }

        /// <summary>
        /// Рекурсивный метод для восстановления разделов из файла.
        /// </summary>
        /// <param name="parentCategory"></param>
        /// <param name="parentTVI"></param>
        private void RestoreCategories(Category parentCategory, TreeViewItem parentTVI)
        {
            foreach (var subcategory in parentCategory.Subcategories)
            {
                var newTVI = new TreeViewItem { Header = subcategory.Name, ContextMenu = CategoryContextMenu, Tag = subcategory };
                newTVI.MouseDoubleClick += TVI_MouseDoubleClick;
                parentTVI.Items.Add(newTVI);
                foreach (var item in subcategory.Items)
                {
                    item.ParentCategory = subcategory;
                    ArticleNumbersOfAllItems.Add(item.ArticleNumber);
                }
                RestoreCategories(subcategory, newTVI);
            }
        }

        /// <summary>
        /// Установка пользователем минимального кол-ва товаров, не требуещего дозакупки.
        /// </summary>
        private void ChoseMinAmountMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var setItemDialog = new SetMinAmountOfItems();
            setItemDialog.MinAmountOfItems = MinAmountOfItems == -1 ? 0 : MinAmountOfItems;
            setItemDialog.ShowDialog();
            if (setItemDialog.MinAmountOfItems == 0)
                return;
            MinAmountOfItems = setItemDialog.MinAmountOfItems;
            GetCSVMenuItem.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Список названий товаров, требующих дозакупки.
        /// </summary>
        List<string> ItemsToRestore;

        /// <summary>
        /// Сохдание CSV-отчета.
        /// </summary>
        private void GetCSVMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MinAmountOfItems == -1)
                    return;
                ItemsToRestore = new List<string>();
                foreach (var category in Category.MainCategories)
                {
                    var pathToItem = $"{category.Name} /";
                    CheckItems(pathToItem, category);
                }
                File.WriteAllText("csv-report.csv", "путь классификатора;артикул;название товара;количество на складе");
                File.AppendAllLines("csv-report.csv", ItemsToRestore);
                MessageBox.Show("CSV-отчет успешно создан. \nОн находится в файле csv-report.csv");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Произошла ошибка!");
            }
        }

        /// <summary>
        /// Рекурсивный метод проверки, нуждается ли товар в дозакупке.
        /// </summary>
        /// <param name="pathToItem"> Полный путь классификатора к товару. </param>
        /// <param name="category"> Родительский раздел. </param>
        private void CheckItems(string pathToItem, Category category)
        {
            foreach (var item in category.Items)
            {
                if (item.AmountLeft < MinAmountOfItems)
                {
                    ItemsToRestore.Add($"{pathToItem.Remove(pathToItem.Length - 1).Trim()};{item.ArticleNumber.Trim()};{item.Name.Trim()};{item.AmountLeft}");
                }
            }
            foreach (var subcategory in category.Subcategories)
            {
                pathToItem += $" {subcategory.Name} /";
                CheckItems(pathToItem, subcategory);
            }
        }

        /// <summary>
        /// Список всех товаров из подразделов.
        /// </summary>
        List<Item> AllItemsOfSubcategory;

        /// <summary>
        /// Получение всех товаров из разделов.
        /// </summary>
        /// <param name="selectedCategory"></param>
        private void MakeListOfSubcategoryItems(Category selectedCategory)
        {
            AllItemsOfSubcategory = new List<Item>();
            foreach (var subcategory in selectedCategory.Subcategories)
                GetAllItems(subcategory);
        }

        /// <summary>
        /// Рекурсивный метод получения списка товаров раздела.
        /// </summary>
        /// <param name="category"> Родительский раздел. </param>
        private void GetAllItems(Category category)
        {
            foreach (var item in category.Items)
                AllItemsOfSubcategory.Add(item);
            foreach (var subcategory in category.Subcategories)
                GetAllItems(subcategory);
        }

        /// <summary>
        /// Просмотр изображения товара.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowImageMenuItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedRow = (Item)ItemsDataGrid.SelectedItem;
                new ItemImage(selectedRow.Image).Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Произошла ошибка!");
            }
        }

        /// <summary>
        /// Установка доступности элементов контекстного меню TreeView.
        /// </summary>
        private void CategoryContextMenu_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            var selectedTVI = (TreeViewItem)CategoriesTreeView.SelectedItem;

            if (selectedTVI != null)
            {
                var selectedCategory = (Category)selectedTVI.Tag;
                foreach (MenuItem item in CategoryContextMenu.Items)
                {
                    item.IsEnabled = true;
                    if ((selectedCategory.Subcategories.Count != 0 || selectedCategory.Items.Count != 0) && item.Header == "Удалить раздел")
                        item.IsEnabled = false;

                }
            }
            else
                foreach (MenuItem item in CategoryContextMenu.Items)
                    item.IsEnabled = false;
        }

        /// <summary>
        /// Установка доступности элементов контекстного меню DataGrid.
        /// </summary>
        private void ItemsContextMenu_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            var selectedRow = (Item)ItemsDataGrid.SelectedItem;
            if (selectedRow != null && SelectedCategory.Items.Contains(selectedRow))
            {
                RemoveItemMenuItem.IsEnabled = true;
                ChangeItemMenuItem.IsEnabled = true;
            }
            else
            {
                RemoveItemMenuItem.IsEnabled = false;
                ChangeItemMenuItem.IsEnabled = false;
                ShowImageMenuItem.IsEnabled = false;
            }

            if (selectedRow != null && selectedRow.Image != null)
                ShowImageMenuItem.IsEnabled = true;
        }

        /// <summary>
        /// Автосохранение склада.
        /// </summary>
        private void Serialize()
        {
            JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All };
            var json = JsonConvert.SerializeObject(Category.MainCategories, settings);
            File.WriteAllText(PathToSavedInfo, json);
        }

        /// <summary>
        /// Справка о программе.
        /// </summary>
        private void HelpMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("\nДля добавления главых разделов кликните правой кнопкой мыши на области справа." +
                "\nДля работы с разделом и добавление товара выберите раздел, кликнув на него, и нажмите правой кнопкой мыши." +
                "\nДля работы с товарами выделите строку, нажав на нее, и кликните правой кнопкой мыши." +
                "\nДля просмотра списка товаров дважды нажмите на раздел." +
                "\nЧтобы создать CSV-отчет, для начала необходимо создать хотя бы один товар и установить минимальное количество." +
                "\nВсе остальное, вроде бы, интуитивно понятно." +
                "\n\nНадеюсь, работа с программой не покажется сложной для Вас. \nДо скорых встреч!", "Добро пожаловать в программу!");
        }
    }
}
