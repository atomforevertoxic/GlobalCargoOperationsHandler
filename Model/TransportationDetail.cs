using System;
using System.Collections.Generic;
using CargoTransactionDatabase.ExtraFiles;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace CargoTransactionDatabase.Model
{
  public class TransportationDetail
  {
    public int ID { get; set; }
    public string FromWhere { get; set; } = null!;
    public string ToWhere { get; set; } = null!;
    public Ship Ship { get; set; } = null!;
    public ICollection<Good> Goods { get; set; } = null!;

    /// <summary>
    /// Конструктор объекта по умолчанию
    /// </summary>
    public TransportationDetail()
    {
      Goods = new List<Good>();
    }


    /// <summary>
    /// Вывод информации о данном объекте в консоль
    /// </summary>
    public void Show()
    {
      Supporting.highlightCurrentLineBySomeColor(this.ToString(), ConsoleColor.Cyan, true);
    }


    /// <summary>
    /// Переопределение метода приведение объекта к строке
    /// </summary>
    /// <returns>Строка, содержащая информацию об объекте в удобочитаемом формате записи</returns>
    public override string ToString()
    {
      return String.Format($"{ID} | {Ship.TransCompany.Name} | \"{Ship.Name}\" : {FromWhere} - {ToWhere}");
    }
  }
}
