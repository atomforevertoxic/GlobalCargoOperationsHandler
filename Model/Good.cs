using CargoTransactionDatabase.ExtraFiles;

namespace CargoTransactionDatabase.Model
{
  public class Good
  {
    public int ID { get; set; }
    public string Title { get; set; } = null!;
    public string GoodType { get; set; } = null!;
    public double Weight { get; set; }
    public int ExpirationTime { get; set; }

    /// <summary>
    /// Конструктор объекта по умолчанию
    /// </summary>
    public Good() { }

    /// <summary>
    /// Пользовательский конструктор класса корабля
    /// </summary>
    /// <param name="isInterface">Флаг, указывающий на принадлежность конструктора пользовательскому интерфейсу</param>
    public Good(bool isInterface = true)
    {
      Console.Clear();
      Supporting.highlightCurrentLineBySomeColor("Добавление нового товара в базу данных", ConsoleColor.Green, true);
      Console.Write("Название товара: ");
      Title = Console.ReadLine();
      Console.Write("Тип товара: ");
      GoodType = Console.ReadLine();
      Console.Write("Вес контейнера с товаром(кг): ");
      Weight = Supporting.validIntegerAnswer("Введите корректное значение веса контейнера: ");
      Console.Write("Допустимый срок хранения (кол-во месяцев): ");
      ExpirationTime = Supporting.validIntegerAnswer("Введите корректное значения срока хранения: ");
    }


    /// <summary>
    /// Вывод информации о данном объекте в консоль
    /// </summary>
    public void Show()
    {
      Supporting.highlightCurrentLineBySomeColor(this.ToString(), ConsoleColor.DarkCyan, true);
    }

    /// <summary>
    /// Переопределение метода приведение объекта к строке
    /// </summary>
    /// <returns>Строка, содержащая информацию об объекте в удобочитаемом формате записи</returns>
    public override string ToString()
    {
      return String.Format($"{Title} | {GoodType} | {ExpirationTime} месяц(ев) | {Weight} кг");
    }

  }
}