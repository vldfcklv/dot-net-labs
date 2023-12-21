using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace lab3
{
    class Program
    {
        class ProductInfo
        {
            private string _name;
            private string _type;
            private short _price;

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

            public ProductInfo()
            {
                Name = string.Empty;
                Type = string.Empty;
                Price = default;
            }

            public override string ToString()
            {
                return $"Тип:\t{Type}\n"+
                    $"Назва:\t{Name}\n" +
                $"Ціна:\t{Price} грн.\n";
            }
        }

        class StoreHouseInfo
        {
            private string _address;

            public string Address
            {
                get { return _address; }
                set { _address = value; }
            }

            public StoreHouseInfo()
            {
                Address = string.Empty;
            }

            public override string ToString()
            {
                return $"Місто:\t{Address}\n";
            }
        }

        class DeliveryOfGoods
        {
            private DateTime _deliveryData;
            private ProductInfo _product;
            private StoreHouseInfo _store;

            public DateTime DeliveryData
            {
                get { return _deliveryData; }
                set { _deliveryData = value; }
            }

            public ProductInfo Product
            {
                get { return _product; }
                set { _product = value; }
            }
            public StoreHouseInfo Store
            {
                get { return _store; }
                set { _store = value; }
            }

            public DeliveryOfGoods()
            {
                DeliveryData = DateTime.Now;
                Product = new ProductInfo();
                Store = new StoreHouseInfo();
            }

            public override string ToString()
            {
                return $"Дата доставки:\t{DeliveryData}\n" +
                $"{Product}" +
                $"{Store}";
            }
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            DeliveryOfGoods delivery = new DeliveryOfGoods();
            ProductInfo product = new ProductInfo();
            StoreHouseInfo store = new StoreHouseInfo();

            Console.WriteLine($"Введіть тип товару:");
            product.Type = Console.ReadLine();
            Console.WriteLine($"Введіть назву товару:");
            product.Name = Console.ReadLine();
            Console.WriteLine($"Введіть ціну товару:");
            product.Price = short.Parse(Console.ReadLine());
            Console.WriteLine($"Введіть місто в якому знаходиться склад:");
            store.Address = Console.ReadLine();

            delivery.Product = product;
            delivery.Store = store;
            Console.WriteLine($"\n{delivery}");
        }
    }
    //Система обліку товарів на складі

}
