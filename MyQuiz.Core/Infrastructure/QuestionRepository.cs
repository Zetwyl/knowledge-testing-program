using System;
using System.Collections.Generic;
using System.Linq;
using MyQuiz.Core.Domain;
using MyQuiz.Core.Strategies;

namespace MyQuiz.Core.Infrastructure
{
    public class QuestionRepository
    {
        public List<Question> GetRandomQuestions(int count)
        {
            var all = GetInterviewQuestions();
            return all.OrderBy(q => Guid.NewGuid()).Take(count).ToList();
        }

        public static List<Question> GetInterviewQuestions()
        {
            return new List<Question>
            {
                // 1. ПАМЯТЬ И ТИПЫ
                new Question {
                    Text = "Какие типы данных относятся к значимым (Value Types)?",
                    Options = new[] { "class", "struct", "interface", "enum" },
                    CorrectIndices = new[] { 1, 3 },
                    Strategy = new MultipleChoiceStrategy()
                },
                new Question {
                    Text = "Что такое Boxing?",
                    Options = new[] { "Упаковка значимого типа в ссылочный", "Распаковка объекта", "Метод сортировки", "Тип исключения" },
                    CorrectIndices = new[] { 0 },
                    Strategy = new SingleChoiceStrategy()
                },
                new Question {
                    Text = "В чем главное отличие struct от class?",
                    Options = new[] { "Класс — ссылочный тип, структура — значимый", "Структура не может иметь методов", "Класс всегда быстрее", "Нет отличий" },
                    CorrectIndices = new[] { 0 },
                    Strategy = new SingleChoiceStrategy()
                },

                // 2. ООП
                new Question {
                    Text = "Какое ключевое слово запрещает наследование от класса?",
                    Options = new[] { "static", "readonly", "sealed", "abstract" },
                    CorrectIndices = new[] { 2 },
                    Strategy = new SingleChoiceStrategy()
                },
                new Question {
                    Text = "Выберите верные утверждения об интерфейсах в C#:",
                    Options = new[] { "Нельзя создавать экземпляры", "Класс может реализовать несколько интерфейсов", "Могут содержать поля данных", "Все методы по умолчанию public" },
                    CorrectIndices = new[] { 0, 1, 3 },
                    Strategy = new MultipleChoiceStrategy()
                },
                new Question {
                    Text = "Что такое инкапсуляция?",
                    Options = new[] { "Возможность наследования", "Сокрытие внутренней реализации объекта", "Многообразие форм объекта", "Передача данных по сети" },
                    CorrectIndices = new[] { 1 },
                    Strategy = new SingleChoiceStrategy()
                },
                new Question {
                    Text = "Для чего нужен модификатор virtual?",
                    Options = new[] { "Для создания статического метода", "Чтобы разрешить переопределение метода в наследниках", "Чтобы скрыть метод", "Для асинхронности" },
                    CorrectIndices = new[] { 1 },
                    Strategy = new SingleChoiceStrategy()
                },

                // 3. СИНТАКСИС И КЛЮЧЕВЫЕ СЛОВА
                new Question {
                    Text = "Что делает оператор ?? (null-coalescing)?",
                    Options = new[] { "Сравнивает два числа", "Проверяет на равенство", "Возвращает левое значение, если оно не null", "Удаляет объект" },
                    CorrectIndices = new[] { 2 },
                    Strategy = new SingleChoiceStrategy()
                },
                new Question {
                    Text = "Какое ключевое слово используется для создания константы на уровне компиляции?",
                    Options = new[] { "readonly", "const", "static", "fixed" },
                    CorrectIndices = new[] { 1 },
                    Strategy = new SingleChoiceStrategy()
                },
                new Question {
                    Text = "Для чего используется блок finally?",
                    Options = new[] { "Для вызова исключения", "Для выполнения кода независимо от того, была ли ошибка", "Только если ошибка произошла", "Для остановки потока" },
                    CorrectIndices = new[] { 1 },
                    Strategy = new SingleChoiceStrategy()
                },

                // 4. .NET И CLR
                new Question {
                    Text = "Как расшифровывается CLR?",
                    Options = new[] { "Common Language Runtime", "Core Local Reader", "Central Logic Root", "C# Language Runner" },
                    CorrectIndices = new[] { 0 },
                    Strategy = new SingleChoiceStrategy()
                },
                new Question {
                    Text = "В какой код компилируется C# перед выполнением?",
                    Options = new[] { "Машинный код", "C++ код", "Intermediate Language (IL)", "Ассемблер" },
                    CorrectIndices = new[] { 2 },
                    Strategy = new SingleChoiceStrategy()
                },
                new Question {
                    Text = "Что такое JIT-компиляция?",
                    Options = new[] { "Компиляция перед установкой", "Компиляция IL в машинный код во время выполнения", "Сжатие кода", "Очистка памяти" },
                    CorrectIndices = new[] { 1 },
                    Strategy = new SingleChoiceStrategy()
                },

                // 5. КОЛЛЕКЦИИ И LINQ
                new Question {
                    Text = "Выберите Generic-коллекции (из System.Collections.Generic):",
                    Options = new[] { "ArrayList", "List<T>", "Dictionary<K,V>", "Hashtable" },
                    CorrectIndices = new[] { 1, 2 },
                    Strategy = new MultipleChoiceStrategy()
                },
                new Question {
                    Text = "Что такое LINQ?",
                    Options = new[] { "Библиотека для графики", "Язык интегрированных запросов", "Система управления БД", "Тип делегата" },
                    CorrectIndices = new[] { 1 },
                    Strategy = new SingleChoiceStrategy()
                },
                new Question {
                    Text = "В чем разница между IEnumerable и IQueryable?",
                    Options = new[] { "Нет разницы", "IQueryable выполняет запрос на стороне сервера/БД", "IEnumerable быстрее", "IQueryable только для массивов" },
                    CorrectIndices = new[] { 1 },
                    Strategy = new SingleChoiceStrategy()
                },

                // 6. ПРОДВИНУТЫЕ ТЕМЫ
                new Question {
                    Text = "Выберите ключевые слова для асинхронного программирования:",
                    Options = new[] { "wait", "async", "await", "task" },
                    CorrectIndices = new[] { 1, 2 },
                    Strategy = new MultipleChoiceStrategy()
                },
                new Question {
                    Text = "Что такое делегат?",
                    Options = new[] { "Ссылка на класс", "Типобезопасный указатель на метод", "Переменная цикла", "Событие" },
                    CorrectIndices = new[] { 1 },
                    Strategy = new SingleChoiceStrategy()
                },
                new Question {
                    Text = "Что делает yield return?",
                    Options = new[] { "Завершает программу", "Позволяет возвращать элементы коллекции по одному (лениво)", "Ускоряет вычисления", "Освобождает память" },
                    CorrectIndices = new[] { 1 },
                    Strategy = new SingleChoiceStrategy()
                },
                new Question {
                    Text = "Для чего нужен интерфейс IDisposable?",
                    Options = new[] { "Для клонирования объектов", "Для освобождения неуправляемых ресурсов", "Для сортировки", "Для сериализации" },
                    CorrectIndices = new[] { 1 },
                    Strategy = new SingleChoiceStrategy()
                },
                new Question {
                    Text = "В чем разница между 'throw' и 'throw ex'?",
                    Options = new[] { "'throw' сохраняет стек вызовов, 'throw ex' — сбрасывает его", "'throw ex' быстрее", "Разницы нет", "'throw' только для системных ошибок" },
                    CorrectIndices = new[] { 0 },
                    Strategy = new SingleChoiceStrategy()
                },
                new Question {
                    Text = "Можно ли использовать блок try без catch?",
                    Options = new[] { "Нет", "Да, если есть блок finally", "Да, всегда", "Только в асинхронных методах" },
                    CorrectIndices = new[] { 1 },
                    Strategy = new SingleChoiceStrategy()
                },
                new Question {
                    Text = "Какой базовый класс у всех исключений в .NET?",
                    Options = new[] { "Error", "Exception", "BaseException", "SystemError" },
                    CorrectIndices = new[] { 1 },
                    Strategy = new SingleChoiceStrategy()
                },

                // 8. ДЕЛЕГАТЫ И СОБЫТИЯ
                new Question {
                    Text = "Выберите встроенные типы делегатов в .NET:",
                    Options = new[] { "Action", "Func", "Predicate", "EventHandler" },
                    CorrectIndices = new[] { 0, 1, 2, 3 },
                    Strategy = new MultipleChoiceStrategy()
                },
                new Question {
                    Text = "Что такое анонимный метод?",
                    Options = new[] { "Метод без имени, определяемый через delegate", "Метод статического класса", "Метод интерфейса", "Приватный метод" },
                    CorrectIndices = new[] { 0 },
                    Strategy = new SingleChoiceStrategy()
                },
                new Question {
                    Text = "Чем отличается event от обычного делегата?",
                    Options = new[] { "Event нельзя вызвать извне класса, где он объявлен", "Event работает медленнее", "Event всегда статический", "Ничем" },
                    CorrectIndices = new[] { 0 },
                    Strategy = new SingleChoiceStrategy()
                },

                // 9. КЛАССЫ И СТРУКТУРЫ (УГЛУБЛЕННО)
                new Question {
                    Text = "Может ли структура иметь конструктор без параметров (C# ниже 10.0)?",
                    Options = new[] { "Да", "Нет", "Только если она static", "Только в WPF" },
                    CorrectIndices = new[] { 1 },
                    Strategy = new SingleChoiceStrategy()
                },
                new Question {
                    Text = "Что такое деструктор (финализатор) в C#?",
                    Options = new[] { "Метод для удаления объекта из памяти вручную", "Метод, вызываемый GC перед уничтожением объекта", "Оператор delete", "Блок кода в конце программы" },
                    CorrectIndices = new[] { 1 },
                    Strategy = new SingleChoiceStrategy()
                },
                new Question {
                    Text = "Что делает модификатор 'readonly' для поля?",
                    Options = new[] { "Разрешает изменение только в конструкторе", "Делает поле константой", "Запрещает чтение извне", "Ускоряет доступ к полю" },
                    CorrectIndices = new[] { 0 },
                    Strategy = new SingleChoiceStrategy()
                },

                // 10. СТРОКИ И МАССИВЫ
                new Question {
                    Text = "Почему string называют 'immutable' (неизменяемым)?",
                    Options = new[] { "Любое изменение создает новую строку в памяти", "Строку нельзя удалить", "У строк нет методов изменения", "Это значимый тип" },
                    CorrectIndices = new[] { 0 },
                    Strategy = new SingleChoiceStrategy()
                },
                new Question {
                    Text = "Для чего используется класс StringBuilder?",
                    Options = new[] { "Для эффективного изменения строк в циклах", "Для шифрования строк", "Для перевода строк в байты", "Для сравнения строк" },
                    CorrectIndices = new[] { 0 },
                    Strategy = new SingleChoiceStrategy()
                },
                new Question {
                    Text = "Что такое интернирование строк (String Interning)?",
                    Options = new[] { "Хранение одной копии одинаковых строк в пуле", "Перевод строк в Unicode", "Сжатие строк", "Объединение строк" },
                    CorrectIndices = new[] { 0 },
                    Strategy = new SingleChoiceStrategy()
                },

                // 11. ПАМЯТЬ (УГЛУБЛЕННО)
                new Question {
                    Text = "Где хранятся локальные переменные значимых типов?",
                    Options = new[] { "В стеке", "В куче", "В файле подкачки", "В регистрах" },
                    CorrectIndices = new[] { 0 },
                    Strategy = new SingleChoiceStrategy()
                },
                new Question {
                    Text = "Что такое LOH (Large Object Heap)?",
                    Options = new[] { "Куча для объектов более 85 000 байт", "Локальное хранилище потока", "Кэш процессора", "Область для статических данных" },
                    CorrectIndices = new[] { 0 },
                    Strategy = new SingleChoiceStrategy()
                },

                // 12. СИНТАКСИС C# 8.0 - 12.0
                new Question {
                    Text = "Что делает оператор '!' после переменной (null-forgiving)?",
                    Options = new[] { "Говорит компилятору, что значение не будет null", "Инвертирует bool", "Вызывает исключение", "Удаляет null" },
                    CorrectIndices = new[] { 0 },
                    Strategy = new SingleChoiceStrategy()
                },
                new Question {
                    Text = "Для чего нужен оператор 'nameof'?",
                    Options = new[] { "Возвращает имя переменной/типа в виде строки", "Создает новую переменную", "Меняет имя класса", "Проверяет тип" },
                    CorrectIndices = new[] { 0 },
                    Strategy = new SingleChoiceStrategy()
                },

                // 13. ДОПОЛНИТЕЛЬНЫЕ ВОПРОСЫ (36-50)
                new Question {
                    Text = "Что такое 'Extension Methods' (Методы расширения)?",
                    Options = new[] { "Методы, добавляющие функционал в существующие типы без изменения их кода", "Методы, увеличивающие размер памяти", "Приватные методы класса", "Методы для работы с файлами" },
                    CorrectIndices = new[] { 0 },
                    Strategy = new SingleChoiceStrategy()
                },
                new Question {
                    Text = "Какое ключевое слово используется для обращения к текущему экземпляру класса?",
                    Options = new[] { "base", "current", "this", "self" },
                    CorrectIndices = new[] { 2 },
                    Strategy = new SingleChoiceStrategy()
                },
                new Question {
                    Text = "Что такое 'управляемый код' (Managed Code)?",
                    Options = new[] { "Код, написанный на С++", "Код, выполняющийся под управлением CLR", "Код, который нельзя изменять", "Код для управления железом" },
                    CorrectIndices = new[] { 1 },
                    Strategy = new SingleChoiceStrategy()
                },
                new Question {
                    Text = "Выберите типы, которые могут быть элементами перечисления (enum):",
                    Options = new[] { "int", "byte", "string", "long" },
                    CorrectIndices = new[] { 0, 1, 3 },
                    Strategy = new MultipleChoiceStrategy()
                },
                new Question {
                    Text = "Для чего используется модификатор 'params' в параметрах метода?",
                    Options = new[] { "Для передачи параметров по ссылке", "Для передачи переменного количества аргументов", "Для защиты данных", "Для ускорения вызова" },
                    CorrectIndices = new[] { 1 },
                    Strategy = new SingleChoiceStrategy()
                },
                new Question {
                    Text = "Что такое 'Deadlock' (Взаимная блокировка)?",
                    Options = new[] { "Ошибка синтаксиса", "Ситуация, когда потоки бесконечно ждут друг друга", "Завершение программы", "Быстрая очистка памяти" },
                    CorrectIndices = new[] { 1 },
                    Strategy = new SingleChoiceStrategy()
                },
                new Question {
                    Text = "Что возвращает оператор 'is'?",
                    Options = new[] { "Ссылку на объект", "Логическое значение (true/false)", "Тип объекта", "Целое число" },
                    CorrectIndices = new[] { 1 },
                    Strategy = new SingleChoiceStrategy()
                },
                new Question {
                    Text = "Какое свойство интерфейса 'IEnumerable' является основным?",
                    Options = new[] { "Count", "GetEnumerator", "Add", "Clear" },
                    CorrectIndices = new[] { 1 },
                    Strategy = new SingleChoiceStrategy()
                },
                new Question {
                    Text = "Для чего нужен атрибут [Serializable]?",
                    Options = new[] { "Для защиты кода от взлома", "Для возможности сохранения состояния объекта", "Для ускорения работы программы", "Для автогенерации методов" },
                    CorrectIndices = new[] { 1 },
                    Strategy = new SingleChoiceStrategy()
                },
                new Question {
                    Text = "Что такое 'Nullable' типы?",
                    Options = new[] { "Типы, которые всегда равны 0", "Значимые типы, которые могут принимать значение null", "Классы без методов", "Ошибочные типы" },
                    CorrectIndices = new[] { 1 },
                    Strategy = new SingleChoiceStrategy()
                },
                new Question {
                    Text = "Выберите способы сравнения строк в C#:",
                    Options = new[] { "Оператор ==", "Метод Equals()", "Метод Compare()", "Оператор is" },
                    CorrectIndices = new[] { 0, 1, 2 },
                    Strategy = new MultipleChoiceStrategy()
                },
                new Question {
                    Text = "Что делает метод 'GC.Collect()'?",
                    Options = new[] { "Выключает компьютер", "Принудительно запускает сборку мусора", "Создает новый объект", "Очищает жесткий диск" },
                    CorrectIndices = new[] { 1 },
                    Strategy = new SingleChoiceStrategy()
                },
                new Question {
                    Text = "Какое ограничение 'where T : class' накладывает на обобщение (Generic)?",
                    Options = new[] { "Тип T должен быть структурой", "Тип T должен быть ссылочным типом", "Тип T должен быть статическим", "Ограничений нет" },
                    CorrectIndices = new[] { 1 },
                    Strategy = new SingleChoiceStrategy()
                },
                new Question {
                    Text = "Что такое 'Tuple' (Кортеж)?",
                    Options = new[] { "Набор из 8 бит", "Структура данных для хранения нескольких значений разных типов", "Метод удаления данных", "Тип исключения" },
                    CorrectIndices = new[] { 1 },
                    Strategy = new SingleChoiceStrategy()
                },
                new Question {
                    Text = "Что делает ключевое слово 'out' перед параметром метода?",
                    Options = new[] { "Удаляет параметр", "Позволяет методу вернуть значение через этот параметр", "Делает параметр константой", "Ничего не делает" },
                    CorrectIndices = new[] { 1 },
                    Strategy = new SingleChoiceStrategy()
                }
            };
        }
    }
}
