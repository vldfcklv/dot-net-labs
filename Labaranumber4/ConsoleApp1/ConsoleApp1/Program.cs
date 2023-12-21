

using System.Diagnostics;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;

namespace lab4
{
    class Program
    {
        enum ProductOptions
        {
            AddProduct = 1,
            DeleteProduct,
            UpdateProductPrice,
            OutputProducts,
        }
        enum MenuOptions : byte
        {

            Execute = 1,

            Exit,
        }

        class Product
        {
            private string _name;
            private string _type;
            private short _price;
            private DateTime _deliveryData;

            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }

            public string Type
            {
                get { return _type; }
                set { _type = value; }
            }

            public short Price
            {
                get { return _price; }
                set { _price = value; }
            }

            public DateTime DeliveryData
            {
                get { return _deliveryData; }
                set { _deliveryData = value; }
            }

            public Product()
            {
                Name = string.Empty;
                Type = string.Empty;
                Price = default;
                DeliveryData = new DateTime();
            }

            public override string ToString()
            {
                return $"Назва:\t{Name}\n" +
                $"Тип:\t{Type}\n" +
                $"Ціна:\t{Price} грн.\n" +
                $"Дата доставки:\t{DeliveryData}\n";
            }
        }

        class ProviderCompany
        {
            private string _nameCompany;

            public string Name
            {
                get { return _nameCompany; }
                set { _nameCompany = value; }
            }

            public override string ToString()
            {
                return $"Постачальник:\t{Name}\n";
            }
        }

        private Dictionary<Product, ProviderCompany> ProductProviders { get; set; }


        private List<Product> DeliveredGoods { get; set; }

        public Program()
        {
            ProductProviders = new Dictionary<Product, ProviderCompany>();
            DeliveredGoods = new List<Product>();
        }
        private void AddProduct()
        {
            var newProduct = new Product();
            var newProductProvider = new ProviderCompany();

            newProduct.DeliveryData = DateTime.Now;
            Console.WriteLine($"Введіть назву товару:");
            newProduct.Name = Console.ReadLine();
            Console.WriteLine($"Введіть тип товару:");
            newProduct.Type = Console.ReadLine();
            Console.WriteLine($"Введіть ціну товару:");
            newProduct.Price = short.Parse(Console.ReadLine());
            Console.WriteLine($"Введіть компанію постачальника:");
            newProductProvider.Name = Console.ReadLine();

            DeliveredGoods.Add(newProduct);
            ProductProviders.TryAdd(newProduct, newProductProvider);
            Console.WriteLine("Товар успішно додано.");
        }

        private Product SelectProduct()
        {
            Console.WriteLine($"Оберіть потрібний документ:");
            OutputProducts();
            int selectedDocIndex;
            while (!int.TryParse(Console.ReadLine(), out selectedDocIndex))
            {
                Console.WriteLine($"Введено неправильні дані");
            }
            // Якщо користувач обрав неіснуючий документ
            if (selectedDocIndex > ProductProviders.Keys.Count)
            {
                Console.WriteLine($"Цього документу не існує.");
                return null;
            }
            return ProductProviders.Keys.ElementAt(selectedDocIndex - 1);
        }
        private void DeleteProduct()
        {
            Product productToBeRemoved = SelectProduct();
            Console.WriteLine($"{productToBeRemoved}");
            if (DeliveredGoods.Remove(productToBeRemoved) && ProductProviders.Remove(productToBeRemoved))
                Console.WriteLine("Продукт вилучено");
        }

        private void OutputProducts()
        {
            if (ProductProviders.Count > 0)
            {
                foreach (var product in ProductProviders)
                {
                    Console.WriteLine($"1.)\n{product.Key}{product.Value}\n");
                }
            }
            else
            {
                Console.WriteLine("Ви не додали товар");
            }
        }

        private void UpdateProductPrice()
        {
            Product product = DeliveredGoods.Find(item => item == SelectProduct());
            Console.WriteLine($"Введіть нову ціну для товару:");
            short newPrice;
            while (!short.TryParse(Console.ReadLine(), out newPrice))
                Console.WriteLine($"Введіть коректне значення\n");
            product.Price = newPrice;
            Console.WriteLine("Цінц товару було змінено.");
        }

        private void Execute()
        {
            Console.WriteLine($"Оберіть дію з товарами:");
            Console.WriteLine($"1.) Додати товар;");
            Console.WriteLine($"2.) Видалити товар;");
            Console.WriteLine($"3.) Оновити ціну товару;");
            Console.WriteLine($"4.) Вивести товари.");
            Byte docOption;
            while (!Byte.TryParse(Console.ReadLine(), out docOption))
            {
                Console.WriteLine($"Введено неправильні дані, перевірте та спробуйте знову");
            }
            ProductOptions selectedOption = (ProductOptions)docOption;
            Console.WriteLine(Environment.NewLine);
            switch (selectedOption)
            {
                case ProductOptions.AddProduct:
                    AddProduct();
                    break;
                case ProductOptions.DeleteProduct:
                    if (ProductProviders.Count > 0) DeleteProduct();
                    else Console.WriteLine($"Ви не можете видалити товар. Жодного товару не знайдено");
                    break;
                case ProductOptions.UpdateProductPrice:
                    if (ProductProviders.Count > 0) UpdateProductPrice();
                    else Console.WriteLine($"Ви не можете оновити ціну товару. Жодного товару не знайдено");
                    break;
                case ProductOptions.OutputProducts:
                    OutputProducts();
                    break;
                default:
                    break;
            }
        }
        private void Menu()
        {
            Console.WriteLine($"Оберіть пункт меню:");
            Console.WriteLine($"1.) Виконати програму;");
            Console.WriteLine($"2.) Завершити роботу;");
            Byte menuOption;
            while (!Byte.TryParse(Console.ReadLine(), out menuOption))
            {
                Console.WriteLine($"Введено неправильні дані");
            }
            MenuOptions selectedOption = (MenuOptions)menuOption;
            switch (selectedOption)
            {
                case MenuOptions.Execute:
                    Execute();
                    Console.WriteLine("\n");
                    break;
                case MenuOptions.Exit:
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }
        }
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Program program = new Program();
            while (true)
            {
                program.Menu();
            }
        }
    }

}

