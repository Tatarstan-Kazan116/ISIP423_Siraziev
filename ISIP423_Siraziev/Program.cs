using System;
using System.Collections.Generic;
using System.Linq;

// Базовый класс для всех людей
public abstract class Person
{
    public int Id { get; }
    public string Name { get; }
    public int Age { get; }

    public Person(int id, string name, int age)
    {
        Id = id;
        Name = name;
        Age = age;
    }

    public abstract void ShowInfo();
}

// Студент
public class Student : Person
{
    private List<Course> _myCourses;

    public Student(int id, string name, int age) : base(id, name, age)
    {
        _myCourses = new List<Course>();
    }

    public void AddCourse(Course course)
    {
        if (!_myCourses.Contains(course))
        {
            _myCourses.Add(course);
            course.AddStudent(this);
        }
    }

    public override void ShowInfo()
    {
        Console.WriteLine($"Студент: {Name} (ID: {Id}), Возраст: {Age}");
        Console.WriteLine($"Курсов: {_myCourses.Count}");
    }

    public void ShowCourses()
    {
        Console.WriteLine($"\nКурсы студента {Name}:");
        foreach (var course in _myCourses)
        {
            Console.WriteLine($"- {course.Name}");
        }
    }
}

// Преподаватель
public class Teacher : Person
{
    private List<Course> _myCourses;

    public Teacher(int id, string name, int age) : base(id, name, age)
    {
        _myCourses = new List<Course>();
    }

    public void AddCourse(Course course)
    {
        if (!_myCourses.Contains(course))
        {
            _myCourses.Add(course);
            course.SetTeacher(this);
        }
    }

    public override void ShowInfo()
    {
        Console.WriteLine($"Преподаватель: {Name} (ID: {Id}), Возраст: {Age}");
        Console.WriteLine($"Курсов: {_myCourses.Count}");
    }
}

// Курс
public class Course
{
    public int Id { get; }
    public string Name { get; }
    private Teacher _teacher;
    private List<Student> _students;

    public Course(int id, string name)
    {
        Id = id;
        Name = name;
        _students = new List<Student>();
    }

    public void SetTeacher(Teacher teacher)
    {
        _teacher = teacher;
    }

    public void AddStudent(Student student)
    {
        if (!_students.Contains(student))
        {
            _students.Add(student);
        }
    }

    public void ShowInfo()
    {
        Console.WriteLine($"Курс: {Name} (ID: {Id})");
        Console.WriteLine($"Преподаватель: {_teacher?.Name ?? "Нет"}");
        Console.WriteLine($"Студентов: {_students.Count}");
    }

    public void ShowStudents()
    {
        Console.WriteLine($"\nСтуденты курса {Name}:");
        foreach (var student in _students)
        {
            Console.WriteLine($"- {student.Name}");
        }
    }
}

// Управление университетом
public class UniManager
{
    private List<Student> _students;
    private List<Teacher> _teachers;
    private List<Course> _courses;
    private int _nextSId;
    private int _nextTId;
    private int _nextCId;

    public UniManager()
    {
        _students = new List<Student>();
        _teachers = new List<Teacher>();
        _courses = new List<Course>();
        _nextSId = 1;
        _nextTId = 1;
        _nextCId = 1;
    }

    // Студенты
    public void AddStudent(string name, int age)
    {
        var student = new Student(_nextSId++, name, age);
        _students.Add(student);
        Console.WriteLine($"Добавлен студент: {name}");
    }

    public void ShowStudents()
    {
        Console.WriteLine("\nВсе студенты:");
        foreach (var s in _students)
        {
            s.ShowInfo();
        }
    }

    // Преподаватели
    public void AddTeacher(string name, int age)
    {
        var teacher = new Teacher(_nextTId++, name, age);
        _teachers.Add(teacher);
        Console.WriteLine($"Добавлен преподаватель: {name}");
    }

    public void ShowTeachers()
    {
        Console.WriteLine("\nВсе преподаватели:");
        foreach (var t in _teachers)
        {
            t.ShowInfo();
        }
    }

    // Курсы
    public void AddCourse(string name)
    {
        var course = new Course(_nextCId++, name);
        _courses.Add(course);
        Console.WriteLine($"Добавлен курс: {name}");
    }

    public void ShowCourses()
    {
        Console.WriteLine("\nВсе курсы:");
        foreach (var c in _courses)
        {
            c.ShowInfo();
        }
    }

    // Связи
    public void StudentToCourse(int sId, int cId)
    {
        var student = _students.FirstOrDefault(s => s.Id == sId);
        var course = _courses.FirstOrDefault(c => c.Id == cId);

        if (student == null || course == null)
        {
            Console.WriteLine("Ошибка: не найден студент или курс");
            return;
        }

        student.AddCourse(course);
        Console.WriteLine($"Студент {student.Name} записан на курс {course.Name}");
    }

    public void TeacherToCourse(int tId, int cId)
    {
        var teacher = _teachers.FirstOrDefault(t => t.Id == tId);
        var course = _courses.FirstOrDefault(c => c.Id == cId);

        if (teacher == null || course == null)
        {
            Console.WriteLine("Ошибка: не найден преподаватель или курс");
            return;
        }

        teacher.AddCourse(course);
        Console.WriteLine($"Преподаватель {teacher.Name} назначен на курс {course.Name}");
    }

    // Поиск
    public Student FindStudent(int id)
    {
        return _students.FirstOrDefault(s => s.Id == id);
    }

    public Teacher FindTeacher(int id)
    {
        return _teachers.FirstOrDefault(t => t.Id == id);
    }

    public Course FindCourse(int id)
    {
        return _courses.FirstOrDefault(c => c.Id == id);
    }
}

// Главная программа
public class Program
{
    private static UniManager _uni = new UniManager();

    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Тестовые данные
        AddTestData();

        while (true)
        {
            ShowMenu();
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1": StudentMenu(); break;
                case "2": TeacherMenu(); break;
                case "3": CourseMenu(); break;
                case "4": ShowAll(); break;
                case "0": return;
                default: Console.WriteLine("Неверный выбор"); break;
            }
        }
    }

    private static void ShowMenu()
    {
        Console.WriteLine("\n=== УНИВЕРСИТЕТ ===");
        Console.WriteLine("1. Студенты");
        Console.WriteLine("2. Преподаватели");
        Console.WriteLine("3. Курсы");
        Console.WriteLine("4. Вся информация");
        Console.WriteLine("0. Выход");
        Console.Write("Выбор: ");
    }

    private static void StudentMenu()
    {
        while (true)
        {
            Console.WriteLine("\n=== СТУДЕНТЫ ===");
            Console.WriteLine("1. Добавить");
            Console.WriteLine("2. Список");
            Console.WriteLine("3. Записать на курс");
            Console.WriteLine("4. Показать курсы студента");
            Console.WriteLine("0. Назад");
            Console.Write("Выбор: ");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1": AddStudent(); break;
                case "2": _uni.ShowStudents(); break;
                case "3": AddStudentToCourse(); break;
                case "4": ShowStudentCourses(); break;
                case "0": return;
                default: Console.WriteLine("Неверный выбор"); break;
            }
        }
    }

    private static void TeacherMenu()
    {
        while (true)
        {
            Console.WriteLine("\n=== ПРЕПОДАВАТЕЛИ ===");
            Console.WriteLine("1. Добавить");
            Console.WriteLine("2. Список");
            Console.WriteLine("3. Назначить на курс");
            Console.WriteLine("0. Назад");
            Console.Write("Выбор: ");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1": AddTeacher(); break;
                case "2": _uni.ShowTeachers(); break;
                case "3": AddTeacherToCourse(); break;
                case "0": return;
                default: Console.WriteLine("Неверный выбор"); break;
            }
        }
    }

    private static void CourseMenu()
    {
        while (true)
        {
            Console.WriteLine("\n=== КУРСЫ ===");
            Console.WriteLine("1. Добавить");
            Console.WriteLine("2. Список");
            Console.WriteLine("3. Показать студентов курса");
            Console.WriteLine("0. Назад");
            Console.Write("Выбор: ");

            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1": AddCourse(); break;
                case "2": _uni.ShowCourses(); break;
                case "3": ShowCourseStudents(); break;
                case "0": return;
                default: Console.WriteLine("Неверный выбор"); break;
            }
        }
    }

    private static void ShowAll()
    {
        _uni.ShowStudents();
        _uni.ShowTeachers();
        _uni.ShowCourses();
    }

    // Методы ввода
    private static void AddStudent()
    {
        Console.Write("Имя: ");
        var name = Console.ReadLine();
        Console.Write("Возраст: ");
        var age = int.Parse(Console.ReadLine());
        _uni.AddStudent(name, age);
    }

    private static void AddTeacher()
    {
        Console.Write("Имя: ");
        var name = Console.ReadLine();
        Console.Write("Возраст: ");
        var age = int.Parse(Console.ReadLine());
        _uni.AddTeacher(name, age);
    }

    private static void AddCourse()
    {
        Console.Write("Название: ");
        var name = Console.ReadLine();
        _uni.AddCourse(name);
    }

    private static void AddStudentToCourse()
    {
        Console.Write("ID студента: ");
        var sId = int.Parse(Console.ReadLine());
        Console.Write("ID курса: ");
        var cId = int.Parse(Console.ReadLine());
        _uni.StudentToCourse(sId, cId);
    }

    private static void AddTeacherToCourse()
    {
        Console.Write("ID преподавателя: ");
        var tId = int.Parse(Console.ReadLine());
        Console.Write("ID курса: ");
        var cId = int.Parse(Console.ReadLine());
        _uni.TeacherToCourse(tId, cId);
    }

    private static void ShowStudentCourses()
    {
        Console.Write("ID студента: ");
        var id = int.Parse(Console.ReadLine());
        var student = _uni.FindStudent(id);
        student?.ShowCourses();
    }

    private static void ShowCourseStudents()
    {
        Console.Write("ID курса: ");
        var id = int.Parse(Console.ReadLine());
        var course = _uni.FindCourse(id);
        course?.ShowStudents();
    }

    private static void AddTestData()
    {
        _uni.AddStudent("Иван", 20);
        _uni.AddStudent("Мария", 19);
        _uni.AddStudent("Петр", 21);

        _uni.AddTeacher("Смирнов", 45);
        _uni.AddTeacher("Козлова", 38);

        _uni.AddCourse("Программирование");
        _uni.AddCourse("Математика");
        _uni.AddCourse("Физика");

        _uni.StudentToCourse(1, 1);
        _uni.StudentToCourse(1, 2);
        _uni.StudentToCourse(2, 1);
        _uni.StudentToCourse(3, 3);

        _uni.TeacherToCourse(1, 1);
        _uni.TeacherToCourse(2, 2);
        _uni.TeacherToCourse(1, 3);
    }
}