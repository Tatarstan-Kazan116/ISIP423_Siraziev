using System;
using System.Collections.Generic;
using System.Linq;

class Book
{
    public int Id;
    public string Title;
    public string Author;
    public string Genre;
    public int Year;
    public decimal Price;
}

class Program
{
    static List<Book> books = new List<Book>();
    static int nextId = 0;

    static void Main()
    {
        // Добавляем тестовые книги
        books.Add(new Book { Id = nextId++, Title = "Властелин Колец", Author = "Толкин", Genre = "Фэнтези", Year = 1954, Price = 1500 });
        books.Add(new Book { Id = nextId++, Title = "1984", Author = "Оруэлл", Genre = "Антиутопия", Year = 1949, Price = 800 });
        books.Add(new Book { Id = nextId++, Title = "Убийство в Восточном экспрессе", Author = "Кристи", Genre = "Детектив", Year = 1934, Price = 700 });
        books.Add(new Book { Id = nextId++, Title = "Дюна", Author = "Герберт", Genre = "Фантастика", Year = 1965, Price = 950 });
        books.Add(new Book { Id = nextId++, Title = "Дракула", Author = "Стокер", Genre = "Ужасы", Year = 1897, Price = 650 });

        while (true)
        {
            Console.WriteLine("\n1. Добавить книгу\n2. Удалить книгу\n3. Найти книги\n4. Сортировать\n5. Цены\n6. Авторы\n7. Выход");
            Console.Write("Выбор: ");
            string choice = Console.ReadLine();

            if (choice == "1") AddBook();
            else if (choice == "2") DeleteBook();
            else if (choice == "3") FindBooks();
            else if (choice == "4") SortBooks();
            else if (choice == "5") ShowPrices();
            else if (choice == "6") ShowAuthors();
            else if (choice == "7") break;
            else Console.WriteLine("Неверно");
        }
    }

    static void AddBook()
    {
        Console.Write("Название: ");
        string title = Console.ReadLine();
        Console.Write("Автор: ");
        string author = Console.ReadLine();
        Console.Write("Жанр: ");
        string genre = Console.ReadLine();
        Console.Write("Год: ");
        int year = int.Parse(Console.ReadLine());
        Console.Write("Цена: ");
        decimal price = decimal.Parse(Console.ReadLine());

        if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(author) && price >= 0)
        {
            books.Add(new Book { Id = nextId++, Title = title, Author = author, Genre = genre, Year = year, Price = price });
            Console.WriteLine("Добавлено");
        }
        else Console.WriteLine("Ошибка");
    }

    static void DeleteBook()
    {
        Console.Write("ID книги: ");
        int id = int.Parse(Console.ReadLine());
        var book = books.FirstOrDefault(b => b.Id == id);
        if (book != null)
        {
            books.Remove(book);
            Console.WriteLine("Удалено");
        }
        else Console.WriteLine("Не найдено");
    }

    static void FindBooks()
    {
        Console.Write("Искать по (название/автор/жанр): ");
        string type = Console.ReadLine();
        Console.Write("Запрос: ");
        string query = Console.ReadLine();

        var results = books.Where(b =>
            (type == "название" && b.Title.Contains(query)) ||
            (type == "автор" && b.Author.Contains(query)) ||
            (type == "жанр" && b.Genre.Contains(query)));

        foreach (var book in results)
            Console.WriteLine($"ID: {book.Id}, {book.Title} - {book.Author}, {book.Genre}, {book.Year}, {book.Price}р");
    }

    static void SortBooks()
    {
        Console.Write("Сортировать по (название/год): ");
        string type = Console.ReadLine();

        var sorted = type == "название" ? books.OrderBy(b => b.Title) : books.OrderBy(b => b.Year); //условие ? значение_если_true : значение_если_false

        foreach (var book in sorted)
            Console.WriteLine($"{book.Title} - {book.Year}");
    }

    static void ShowPrices()
    {
        var expensive = books.OrderByDescending(b => b.Price).First();
        var cheap = books.OrderBy(b => b.Price).First();

        Console.WriteLine($"Дорогая: {expensive.Title} - {expensive.Price}р");
        Console.WriteLine($"Дешевая: {cheap.Title} - {cheap.Price}р");
    }

    static void ShowAuthors()
    {
        var groups = books.GroupBy(b => b.Author);
        foreach (var group in groups)
            Console.WriteLine($"{group.Key}: {group.Count()} книг");
    }
}