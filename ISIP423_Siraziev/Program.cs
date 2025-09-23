using System;
using System.Collections.Generic;
using System.Linq;

class Product
{
    public string Code;
    public string Name;
    public decimal Price;
    public int kolvo;
    public bool InStock => kolvo > 0;
    public string Category;
}

class Program
{
    static List<Product> products = new List<Product>();
    static int counter = 1;
    static string[] categories = { "Электроника", "Одежда", "Продукты" };

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n1. Добавить товар\n2. Удалить товар\n3. Поставка\n4. Продажа\n5. Поиск\n6. Выход");
            Console.Write("Выбор: ");

            string choice = Console.ReadLine();

            if (choice == "1") AddP();
            else if (choice == "2") DeleteP();
            else if (choice == "3") postavkaP();
            else if (choice == "4") SellP();
            else if (choice == "5") poiskP();
            else if (choice == "6") break;
            else Console.WriteLine("Ошибка!");
        }
    }

    static void AddP()
    {
        Product p = new Product();
        p.Code = "1" + counter.ToString("00000");
        counter++;

        Console.Write("Название: ");
        p.Name = Console.ReadLine();

        Console.Write("Цена: ");
        p.Price = decimal.Parse(Console.ReadLine());

        Console.Write("Количество: ");
        p.kolvo = int.Parse(Console.ReadLine());

        Console.WriteLine("Категории: 1-Электроника, 2-Одежда, 3-Продукты");
        p.Category = categories[int.Parse(Console.ReadLine()) - 1];

        products.Add(p);
        Console.WriteLine("Товар добавлен!");
    }

    static void DeleteP()
    {
        Console.Write("Код товара: ");
        string code = Console.ReadLine();

        var product = products.FirstOrDefault(p => p.Code == code);
        if (product != null)
        {
            products.Remove(product);
            Console.WriteLine("Товар удален!");
        }
        else Console.WriteLine("Не найден!");
    }

    static void postavkaP()
    {
        Console.Write("Код товара: ");
        var product = products.FirstOrDefault(p => p.Code == Console.ReadLine());

        if (product != null)
        {
            Console.Write("Количество: ");
            product.kolvo += int.Parse(Console.ReadLine());
            Console.WriteLine("Поставка добавлена!");
        }
        else Console.WriteLine("Не найден!");
    }

    static void SellP()
    {
        Console.Write("Код товара: ");
        var product = products.FirstOrDefault(p => p.Code == Console.ReadLine());

        if (product != null)
        {
            Console.Write("Количество: ");
            int qty = int.Parse(Console.ReadLine());

            if (product.kolvo >= qty)
            {
                product.kolvo -= qty;
                Console.WriteLine("Продажа завершена!");
            }
            else Console.WriteLine("Недостаточно товара!");
        }
        else Console.WriteLine("Не найден!");
    }

    static void poiskP()
    {
        Console.Write("Поиск (код/название/категория): ");
        string search = Console.ReadLine().ToLower();

        var results = products.Where(p =>
            p.Code.ToLower().Contains(search) ||
            p.Name.ToLower().Contains(search) ||
            p.Category.ToLower().Contains(search));

        foreach (var p in results)
        {
            Console.WriteLine($"\nКод: {p.Code}");
            Console.WriteLine($"Название: {p.Name}");
            Console.WriteLine($"Цена: {p.Price} руб.");
            Console.WriteLine($"Количество: {p.kolvo}");
            Console.WriteLine($"В наличии: {(p.InStock ? "Да" : "Нет")}");
            Console.WriteLine($"Категория: {p.Category}");
        }

        if (!results.Any()) Console.WriteLine("Ничего не найдено");
    }
}