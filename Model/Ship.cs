using System;
using CargoTransactionDatabase.ExtraFiles;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CargoTransactionDatabase.Model
{
  public class Ship
  {
    public int ID { get; set; }
    public string Name { get; set; } = null!;
    public string Captain { get; set; } = null!;
    public string? Flag { get; set; }
    public Company TransCompany { get; set; } = null!;
    
    /// <summary>
    /// Конструктор объекта по умолчанию
    /// </summary>
    public Ship() { }

    /// <summary>
    /// Пользовательский конструктор класса транспортной компании
    /// </summary>
    /// <param name="companyOwner">Транспортная компания, несущая отвественность за данный корабль</param>
    /// <remarks>Наличие параметра компании при вызове констуктора объекта дает понять, что будет вызван конструктор пользовательского интерфейса</remarks>
    public Ship(Company companyOwner)
    {
      Console.Write("*Введите название корабля: ");
      Name = Console.ReadLine();
      Console.Write("*Введите имя капитана: ");
      Captain = Console.ReadLine();
      Console.Write("*Флаг страны, под которым ходит корабль: ");
      Flag = Console.ReadLine();
    }


    /// <summary>
    /// Вывод информации о данном объекте в консоль
    /// </summary>
    public void Show()
    {
      Supporting.highlightCurrentLineBySomeColor(this.ToString(), ConsoleColor.Blue, true);
    }

    /// <summary>
    /// Переопределение метода приведение объекта к строке
    /// </summary>
    /// <returns>Строка, содержащая информацию об объекте в удобочитаемом формате записи</returns>
    public override string ToString()
    {
      return String.Format($"{ID} | {Flag} | {Name} | {Captain}");
    }

  }
}
