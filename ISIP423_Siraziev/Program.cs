


using System;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

Console.WriteLine("Введите количество операций: ");
int kol_op = Convert.ToInt32(Console.ReadLine());
Console.WriteLine("Шаблон: Название услуги или товара;Количество денег");
bool flag = true;
string[] massiv = new string[kol_op];
for (int i = 0; i < kol_op; i++)
{
    massiv[i] = Console.ReadLine();
}
Console.WriteLine("");
Console.WriteLine("Меню:");
Console.WriteLine("1. Вывод данных");
Console.WriteLine("2. Статистика");
Console.WriteLine("3. Сортировка по цене");
Console.WriteLine("4. Конвертация валюты");
Console.WriteLine("5. Поиск по названию");
Console.WriteLine("0. Выход");

void vyvod(string[] massiv)
{
    foreach (string element in  massiv)
    {
        Console.WriteLine(element);
    }
}
void statistika(string[] massiv)
{
    double sum = 0;
    double max = double.MinValue;
    double min = double.MaxValue;
    int count = 0;

    foreach (string element in massiv)
    {
        string[] word = element.Split(';');
        if (word.Length >= 2 && double.TryParse(word[1], out double price))
        {
            sum += price;
            max = Math.Max(max, price);
            min = Math.Min(min, price);
            count++;
        }
    }

    if (count > 0)
    {
        double sred = 0;
        sred = sum / count;
        Console.WriteLine($"Сумма: {sum}, Макс: {max}, Мин: {min}, Среднее: {sred}");
    }
    else
    {
        Console.WriteLine("Что-то пошло не так");
    }
}
void sortirovka(string[] massiv)
{
    int length = massiv.Length;
    for (int i = 0; i < length - 1; i++)
    {
        for (int j = 0; j < length - i - 1; j++)
        {
            string[] currentParts = massiv[j].Split(';');
            string[] nextParts = massiv[j + 1].Split(';');

            if (currentParts.Length >= 2 && nextParts.Length >= 2 &&
                double.TryParse(currentParts[1], out double currentPrice) &&
                double.TryParse(nextParts[1], out double nextPrice))
            {
                if (nextPrice < currentPrice)
                {
                    string temp = massiv[j];
                    massiv[j] = massiv[j + 1];
                    massiv[j + 1] = temp;
                }
            }
        }
    }
}

void convert(string[] massiv)
{
    Console.Write("Введите курс обмена: ");
    string kurs_vvod = Console.ReadLine();
    if (double.TryParse(kurs_vvod, out double kurs))
    {
        for(int i = 0; i < massiv.Length;i++)
        {
            string[] word = massiv[i].Split(";");
            if (word.Length >= 2 && double.TryParse(word[1], out double price))
            {
                massiv[i] = $"{word[0]};{(price * kurs)}";
            }
        }
        Console.WriteLine("Валюта успешно конвертирована");
    }
    else
    {
        Console.WriteLine("Некорректный ввод курса.");
    }

}
void poisk(string[] massiv)
{
    Console.Write("Введите название для поиска: ");
    string nazvaniye = Console.ReadLine();

    foreach (string element in massiv)
    {
        string[] parts = element.Split(';');
        if (parts.Length >= 1 && parts[0].IndexOf(nazvaniye, StringComparison.OrdinalIgnoreCase) >= 0)
        {
            Console.WriteLine(element);
        }
    }
}

while (flag != false) 
{
    Console.Write("Выберите операцию: ");
    int operation = Convert.ToInt32(Console.ReadLine());
    if (operation == 0)
    {
        flag = false;
        Console.WriteLine($"Выход завершен");
    }

    switch(operation)
    {
        case 1:
            vyvod(massiv);
            break;
        case 2:
            statistika(massiv);
            break;
        case 3:
            sortirovka(massiv);
            break;
        case 4:
            convert(massiv);
            break;
        case 5:
            poisk(massiv);
            break;
        default:
            Console.WriteLine("Неправильный ввод");
            break;

    }


}