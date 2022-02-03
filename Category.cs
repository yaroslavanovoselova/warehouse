using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace warehouse
{
    /// <summary>
    /// Класс разделов.
    /// </summary>
    class Category
    {
        /// <summary>
        /// Название раздела.
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Список подразделов раздела.
        /// </summary>
        public List<Category> Subcategories { get; set; }
        
        /// <summary>
        /// Список главных разделов.
        /// </summary>
        public static List<Category> MainCategories { get; set; } = new List<Category>();
        
        /// <summary>
        /// Список товаров раздела.
        /// </summary>
        public List<Item> Items { get; set; }

        /// <summary>
        /// Конструктор раздела.
        /// </summary>
        /// <param name="name"> Имя раздела. </param>
        public Category(string name)
        {
            Name = name;
            Subcategories = new List<Category>();
            Items = new List<Item>();
        }
    }
}
