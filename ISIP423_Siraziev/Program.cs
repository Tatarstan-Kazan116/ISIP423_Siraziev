using System;
using System.Collections.Generic;
using System.Text;

class Program
{
    // Класс для хранения статистики
    class Stats
    {
        public int Words;
        public string ShortWord;
        public int Sentences;
        public int Vowels;
        public int Consonants;
        public string LongWord;
        public Dictionary<char, int> Letters = new Dictionary<char, int>();
    }

    static List<Stats> allStats = new List<Stats>();
    static Stats current;

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\nМеню:");
            Console.WriteLine("1 - Новый текст");
            Console.WriteLine("2 - Статистика");
            Console.WriteLine("3 - Сохранить");
            Console.WriteLine("4 - История");
            Console.WriteLine("5 - Выход");

            string input = Console.ReadLine();

            if (input == "1") NewText();
            else if (input == "2") ShowStats();
            else if (input == "3") SaveStats();
            else if (input == "4") ShowHistory();
            else if (input == "5") break;
            else Console.WriteLine("Ошибка!");
        }
    }

    static void NewText()
    {
        Console.WriteLine("Введите текст (от 100 символов):");
        string text = Console.ReadLine();

        if (text.Length < 100)
        {
            Console.WriteLine("Мало символов!");
            return;
        }

        current = CheckText(text);
        Console.WriteLine("Готово!");
    }

    static Stats CheckText(string text)
    {
        Stats s = new Stats();
        StringBuilder word = new StringBuilder();
        bool inSentence = false;

        // Списки букв
        string vowelLetters = "аеёиоуыэюяaeiou";
        string consonantLetters = "бвгджзйклмнпрстфхцчшщbcdfghjklmnpqrstvwxyz";

        for (int i = 0; i < text.Length; i++)
        {
            char c = text[i];
            char lower = char.ToLower(c);

            if (char.IsLetter(c))
            {
                // Частота букв
                if (!s.Letters.ContainsKey(lower))
                {
                    s.Letters[lower] = 0;
                }    
                s.Letters[lower]++;

                // Гласные/согласные
                if (vowelLetters.IndexOf(lower) >= 0) s.Vowels++;
                else if (consonantLetters.IndexOf(lower) >= 0) s.Consonants++;

                word.Append(c);
            }
            else
            {
                // Конец слова
                if (word.Length > 0)
                {
                    string w = word.ToString();
                    s.Words++;

                    if (s.ShortWord == null || w.Length < s.ShortWord.Length)
                        s.ShortWord = w;

                    if (s.LongWord == null || w.Length > s.LongWord.Length)
                        s.LongWord = w;

                    word.Clear();
                }

                // Конец предложения
                if (c == '.' || c == '!' || c == '?')
                {
                    if (inSentence)
                    {
                        s.Sentences++;
                        inSentence = false;
                    }
                }
                else if (!char.IsWhiteSpace(c))
                {
                    inSentence = true;
                }
            }
        }

        // Последнее слово
        if (word.Length > 0)
        {
            string w = word.ToString();
            s.Words++;

            if (s.ShortWord == null || w.Length < s.ShortWord.Length)
                s.ShortWord = w;

            if (s.LongWord == null || w.Length > s.LongWord.Length)
                s.LongWord = w;
        }

        // Последнее предложение
        if (inSentence) s.Sentences++;

        return s;
    }

    static void ShowStats()
    {
        if (current == null)
        {
            Console.WriteLine("Сначала введите текст!");
            return;
        }

        Console.WriteLine("\nСтатистика:");
        Console.WriteLine($"Слов: {current.Words}");
        Console.WriteLine($"Короткое: {current.ShortWord}");
        Console.WriteLine($"Предложений: {current.Sentences}");
        Console.WriteLine($"Гласных: {current.Vowels}");
        Console.WriteLine($"Согласных: {current.Consonants}");
        Console.WriteLine($"Длинное: {current.LongWord}");

        Console.WriteLine("Буквы:");
        foreach (var item in current.Letters)
        {
            Console.WriteLine($"{item.Key}: {item.Value}");
        }
    }

    static void SaveStats()
    {
        if (current == null)
        {
            Console.WriteLine("Нет статистики!");
            return;
        }

        allStats.Add(current);
        Console.WriteLine("Сохранено!");
    }

    static void ShowHistory()
    {
        if (allStats.Count == 0)
        {
            Console.WriteLine("История пуста!");
            return;
        }

        Console.WriteLine("\nИстория:");
        for (int i = 0; i < allStats.Count; i++)
        {
            Console.WriteLine($"#{i + 1}: {allStats[i].Words} слов, {allStats[i].Sentences} предл.");
        }
    }
}