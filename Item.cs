using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace warehouse
{
    /// <summary>
    /// Класс товаров.
    /// </summary>
    class Item
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
        /// Оставшееся количество товара.
        /// </summary>
        public int? AmountLeft { get; set; }

        /// <summary>
        /// Стомость товара.
        /// </summary>
        public double? Price { get; set; }

        /// <summary>
        /// Описание товара.
        /// </summary>
        public string AdditionalInfo { get; set; }

        /// <summary>
        /// Фотография товара.
        /// </summary>
        [JsonIgnore]
        public ImageSource Image { get; set; }

        /// <summary>
        /// Раздел, к которому относится товар.
        /// </summary>
        [JsonIgnore]
        public Category ParentCategory { get; set; }

        /// <summary>
        /// Конструктор товара.
        /// </summary>
        /// <param name="name"> Название. </param>
        /// <param name="articleNumber"> Артикул. </param>
        /// <param name="amount"> Оставшееся количество. </param>
        /// <param name="price"> Стоимость. </param>
        /// <param name="info"> Дополнительная информация. </param>
        /// <param name="parent"> Родительский раздел. </param>
        public Item (string name, string articleNumber, int? amount, double? price, string info, Category parent)
        {
            Name = name;
            ArticleNumber = articleNumber;
            AmountLeft = amount;
            Price = price;
            AdditionalInfo = info;
            ParentCategory = parent;
        }

        /// <summary>
        /// Сериализация и дезериализация фотографии товара.
        /// </summary>
        [JsonProperty("Image")]
        public byte[] SerializedImage
        {
            get
            {
                if (Image == null)
                    return null;
                using(var memStream = new MemoryStream())
                {
                    var encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create((BitmapSource)Image));
                    encoder.Save(memStream);
                    return memStream.ToArray();
                }
            }
            set
            {
                if (value == null) 
                    Image = null;
                else
                {
                    using (var stream = new MemoryStream(value))
                    {
                        var decoder = BitmapDecoder.Create(stream, BitmapCreateOptions.None, BitmapCacheOption.OnLoad);
                        Image = decoder.Frames[0];
                    }
                }
            }
        }
    }
}
