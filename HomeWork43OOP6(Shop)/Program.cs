using System;
using System.Collections.Generic;

namespace HomeWork43OOP6_Shop_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Seller seller = new Seller();
            Buyer buyer = new Buyer();
            Shop shop = new Shop();

            shop.Trade(seller, buyer);
        }
    }

    class Shop
    {
        public void Trade(Seller seller, Buyer buyer)
        {
            const string ShowSellerProducts = "1";
            const string BuyProduct = "2";
            const string ShowBuyerProducts = "3";
            const string StopShopping = "4";

            bool IsWorking = true;

            while (IsWorking)
            {
                Console.WriteLine(new string('_', 35));
                Console.WriteLine();
                Console.WriteLine("    Добро пожаловать в магазин    ");
                Console.WriteLine(new string('_', 35));
                Console.WriteLine();
                Console.WriteLine($"Показать все товары в магазине - {ShowSellerProducts}");
                Console.WriteLine($"Купить товар                   - {BuyProduct}");
                Console.WriteLine($"Показать продукты покупателя   - {ShowBuyerProducts}");
                Console.WriteLine($"Уйти из магазина               - {StopShopping}");
                Console.WriteLine(new string('_', 35));
                Console.WriteLine();
                Console.WriteLine($"Деньги продавца {seller.Money} рублей.");
                Console.WriteLine($"Деньги покупателя {buyer.Money} рублей.");
                Console.WriteLine(new string('_', 35));

                Console.Write("\nВаш выбор: ");
                string userInput = Console.ReadLine();
                Console.WriteLine();

                switch (userInput)
                {
                    case ShowSellerProducts:
                        seller.ShowInfo();
                        break;

                    case BuyProduct:
                        TradeProduct(seller, buyer);
                        break;

                    case ShowBuyerProducts:
                        buyer.ShowInfo();
                        break;

                    case StopShopping:
                        IsWorking = false;
                        break;

                    default:
                        Console.WriteLine("Такого пункта меню нет, попробуйте снова!");
                        break;
                }
            }
        }

        public void TradeProduct(Seller seller, Buyer buyer)
        {
            if (seller.TryGetProduct(out Product product))
            {
                if (buyer.CanPay(product.Price))
                {
                    seller.Sell(product);

                    buyer.BuyProduct(product);

                    Console.WriteLine();
                    Console.WriteLine("!!!Покупка совершена успешно!!!");
                }
            }
        }
    }

    class Human
    {
        protected List<Product> Products = new List<Product>();

        public Human(int minMoney, int maxMoney)
        {
            Money = Utilities.GetRandomValue(minMoney, maxMoney);
        }

        public int Money { get; protected set; }

        public void ShowInfo()
        {
            for (int i = 0; i < Products.Count; i++)
            {
                Console.Write($"{i + 1} ");

                Products[i].ShowInfo();
            }
        }
    }

    class Seller : Human
    {
        public Seller() : base(0, 0)
        {
            CreateProductList();
        }

        public bool TryGetProduct(out Product product)
        {
            product = null;

            Console.Write("Введите номер продукта: ");

            if (int.TryParse(Console.ReadLine(), out int number) == false)
            {
                Console.WriteLine("Ошибка ввода данных");

                return false;
            }

            if (number < 1 || number > Products.Count)
            {
                Console.WriteLine("Такого товара нет");

                return false;
            }

            product = Products[number - 1];

            return true;
        }

        public void Sell(Product product)
        {
            Money += product.Price;

            Products.Remove(product);
        }

        private void CreateProductList()
        {
            Products.Add(new Product("Хлеб", 15));
            Products.Add(new Product("Молоко", 30));
            Products.Add(new Product("Сыр", 40));
            Products.Add(new Product("Помидоры", 30));
            Products.Add(new Product("Огурцы", 45));
        }
    }

    class Buyer : Human
    {
        public Buyer() : base(60, 100)
        {
        }

        public bool CanPay(int price)
        {
            if (Money >= price)
                return true;

            Console.WriteLine("У вас недостаточно денег!");

            return false;
        }

        public void BuyProduct(Product product)
        {
            Money -= product.Price;

            Products.Add(product);
        }
    }

    class Product
    {
        public Product(string title, int price)
        {
            Title = title;
            Price = price;
        }

        public string Title { get; private set; }
        public int Price { get; private set; }

        public void ShowInfo()
        {
            Console.WriteLine($"товар - {Title}, цена - {Price}.");
        }
    }

    class Utilities
    {
        private static Random s_random = new Random();

        public static int GetRandomValue(int minValue, int maxValue)
        {
            return s_random.Next(minValue, maxValue);
        }
    }
}
