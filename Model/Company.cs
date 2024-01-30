using CargoTransactionDatabase.ExtraFiles;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CargoTransactionDatabase.Model
{
  public class Company
  {
    public int ID { get; set; }
    public string Name { get; set; } = null!;
    public string Country { get; set; } = null!;
    public string? GeneralDirector { get; set; }
    public ICollection<Ship> Ships { get; set; } = null!;

    /// <summary>
    /// Конструктор объекта по умолчанию
    /// </summary>
    public Company() { }

    /// <summary>
    /// Пользовательский конструктор класса транспортной компании
    /// </summary>
    /// <param name="isInterface">Флаг, указывающий на принадлежность конструктора пользовательскому интерфейсу</param>
    public Company(bool isInterface = true)
    {
      Ships = new List<Ship>();
      Supporting.highlightCurrentLineBySomeColor("Заполнение компании", ConsoleColor.Green, true);
      Console.Write("*Название: ");
      Name = Console.ReadLine();
      Console.Write("*Страна: ");
      Country = Console.ReadLine();
      Console.Write("*Имя генерального директора: ");
      GeneralDirector = Console.ReadLine();
    }

    /// <summary>
    /// Вывод информации о данном объекте в консоль
    /// </summary>
    public void Show()
    {
      Supporting.highlightCurrentLineBySomeColor(this.ToString(), ConsoleColor.DarkBlue, true);
    }

    /// <summary>
    /// Переопределение метода приведение объекта к строке
    /// </summary>
    /// <returns>Строка, содержащая информацию об объекте в удобочитаемом формате записи</returns>
    public override string ToString()
    {
      return String.Format($"{ID} | {Country} | {Name} | {GeneralDirector}");
    }

  }
}