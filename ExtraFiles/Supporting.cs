using CargoTransactionDatabase.Data;
using CargoTransactionDatabase.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**Здесь прописаны вспомогательные функции, которые не влияют напрямую на ход работы программы
        Но участвуют в реализации удобочитаемого и приятного консольного интерфейса**/
namespace CargoTransactionDatabase.ExtraFiles
{
  public static class Supporting
  {

    /// <summary>
    /// Проверка корректности ответа пользователя на вопрос с выбором ответа при помощи введенного числа
    /// </summary>
    /// <param name="choices">Допустимые варианты ответа пользователя</param> 
    /// <param name="errorMessage">Сообщение об ошибке в случае некорректного ввода</param>
    /// <returns>Ответ пользователя, прошедший валидацию</returns>
    public static int validAnswer(int[] choices, string errorMessage = "Введите корректный номер команды: ")
    {
      int answer;
      Console.Write("Ввод пользователя: ");
      //Если ответ не удовлетворяет критериям проверки, строка с ответом затирается
      while (!int.TryParse(Console.ReadLine(), out answer) || (!choices.Contains(answer)))
      {
        Console.SetCursorPosition(0, Console.CursorTop - 1);
        Console.Write(new String(' ', Console.BufferWidth));
        Console.SetCursorPosition(0, Console.CursorTop);
        highlightCurrentLineBySomeColor(errorMessage, ConsoleColor.Red, false);
      }
      return answer;
    }

    /// <summary>
    /// Валидация числовый ответов пользователя
    /// </summary>
    /// <param name="errorMessage">Cообщение об ошибке в случае некорректного ввода</param>
    /// <returns>Ответ пользователя, прошедший валидацию</returns>
    public static int validIntegerAnswer(string errorMessage)
    {
      int intToValid;
      while (!int.TryParse(Console.ReadLine(), out intToValid) || intToValid <= 0)
      {
        Console.SetCursorPosition(0, Console.CursorTop - 1);
        Console.Write(new String(' ', Console.BufferWidth));
        Console.SetCursorPosition(0, Console.CursorTop);
        highlightCurrentLineBySomeColor(errorMessage, ConsoleColor.Red, false);
      }
      return intToValid;
    }
    /// <summary>
    /// Установка цветного квадрата в зависимост от выполнения условия
    /// </summary>
    /// <param name="conditionResult">Логическая операция, результат которой определяет цвет текста</param>
    /// <returns>Цвет, соответствующий результату логической операции</returns>
    public static ConsoleColor setColorByValidator(bool conditionResult)
    {
      if (conditionResult) return ConsoleColor.Green;
      return ConsoleColor.Red;
    }

    /// <summary>
    /// Выделеине текста определенным цветом
    /// </summary>
    /// <param name="currentString">Строка, которая подлежит выделению цветом</param>
    /// <param name="highlightColor">Цвет для выделения</param>
    /// <param name="moveToNextLine">Флаг перехода на следующую строку после выделения</param>
    public static void highlightCurrentLineBySomeColor(string currentString, ConsoleColor highlightColor, bool moveToNextLine = false)
    {
      Console.Write(currentString, Console.ForegroundColor = highlightColor);
      Console.ResetColor();
      if (moveToNextLine) Console.WriteLine();
    }
  }
}
