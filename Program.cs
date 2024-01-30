using CargoTransactionDatabase.Model;
using CargoTransactionDatabase.Data;
using CargoTransactionDatabase.ExtraFiles;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Collections;
using System.Transactions;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;
using Microsoft.Data.SqlClient;

using DatabaseContext context = new DatabaseContext();

//Пользовательский интерфейс программы работы с базой данных морских грузоперевозок
while(true)
{
  Console.Clear();
  //Главное меню пользовательского интерфейса
  Supporting.highlightCurrentLineBySomeColor("База данных морских грузоперевозок", ConsoleColor.Green, true);
  Console.WriteLine("1. Добавить грузоперевозку\n2. Удалить грузоперевозку\n3. Вывести все прошедшие грузоперевозки\n" +
    "4. Выйти из программы");
  switch(Supporting.validAnswer(new int[] { 1, 2, 3, 4}))
  {
    case 1:
      //Формирование морской грузоперевозки
      addNewTransportationDetailToDatabase();
      context.SaveChanges();
      break;
    case 2:
      //Удаление морской грузоперевозки
      deleteTransportationDetailFromDatabase(); 
      context.SaveChanges();
      break;
    case 3:
      databaseLibrary();
      break;
    case 4:
      //Выход из меню. Завершение программы
      Console.WriteLine("Завершение работы программы...");
      Thread.Sleep(500);
      return;
  }
}

//Добавление новой грузоперевозци в базу данных
void addNewTransportationDetailToDatabase()
{
  Console.Clear();
  Supporting.highlightCurrentLineBySomeColor("Добавление новой грузоперевозки...", ConsoleColor.Green, true);
  TransportationDetail newAddingDetail = new TransportationDetail();
  Console.Write("Пункт отправления: ");
  newAddingDetail.FromWhere = Console.ReadLine();
  
  Console.Write("Пункт прибытия: ");
  newAddingDetail.ToWhere = Console.ReadLine();

  Company shipOwnerCompany = formingCompanyForTsDetail(); //Выбор компании, участвующей в грузоперевозке

  newAddingDetail.Ship = formingShipForTsDetail(shipOwnerCompany);  //Выбор корабля, осуществляющего грузоперевозку


  addGoodsToTsDetail(newAddingDetail); //Добавление перевозимых товаров

  context.Details.Add(newAddingDetail); //Добавление итоговой информации о грузоперевозке в БД
}

//Формирование подходящей компании ответственной за грузопоеревозку
Company formingCompanyForTsDetail()
{
  Company companyToTsDetail;
  if (context.Companies.Count() != 0)
  {
    Console.WriteLine("Компания, организующая перевозку:\n1. Существующая в базе компания\n2. Новая компания ");
    if (Supporting.validAnswer(new int[] { 1, 2 }) == 1)
    {
      //Выбор уже существующей в базе данных компании 
      int companyIdsChoice = Supporting.validAnswer(showAllSavedCompaniesFromDB().ToArray());
      companyToTsDetail = context.Companies.First(c => c.ID == companyIdsChoice);
    }
    else
    {
      Console.Clear();
      companyToTsDetail = new Company(true); //Формирование новой компании для досье грузоперевозки
      context.Companies.Add(companyToTsDetail);
    }
  }
  else
  {
    Console.Clear();
    companyToTsDetail = new Company(true); //Формирование новой компании для досье грузоперевозки
    context.Companies.Add(companyToTsDetail);
  }
  return companyToTsDetail;
}

//Формирования корабля, осуществляющей грузоперевозку
Ship formingShipForTsDetail(Company companyOwner)
{
  Console.Clear();
  Supporting.highlightCurrentLineBySomeColor($"Компания \"{companyOwner.Name}\"", ConsoleColor.Green, true);
  Ship shipToTsDetail;
  if (!(context.Ships.FromSqlRaw(" SELECT * FROM Ships").Where(s => s.TransCompany.ID == companyOwner.ID).IsNullOrEmpty())) //Добавление корабля в досье грузоперевозки
  {
    Console.WriteLine("Корабль транспортной компании, организующей грузоперевозку:\n" +
      "1. Уже существующий в базе данных корабль\n2. Новый корабль компании");
    if (Supporting.validAnswer(new int[] { 1, 2 }) == 1)
    {
      //Выбираем уже существующий в БД корабль
      int answerAboutShipAddingFromDatabase = Supporting.validAnswer(showAllSavedShipsFromDB(companyOwner).ToArray());
      shipToTsDetail = companyOwner.Ships.First(s => s.ID == answerAboutShipAddingFromDatabase);
    }
    else
    {
      shipToTsDetail = new Ship(companyOwner); //Создаем новый корабль
      companyOwner.Ships.Add(shipToTsDetail);
    }
  }
  else
  {
    shipToTsDetail = new Ship(companyOwner); //Создаем новый корабль
    companyOwner.Ships.Add(shipToTsDetail);
  }
  return shipToTsDetail;
}

//Добавление товаров в транзакцию
void addGoodsToTsDetail(TransportationDetail newAddingDetail)
{
  Console.Clear();
  bool keepAddingGoods = true;
  while (keepAddingGoods)
  {
    Console.SetCursorPosition(0, 6);
    foreach (Good good in newAddingDetail.Goods) good.Show();
    Console.SetCursorPosition(0, 0);
    Console.WriteLine("1. Добавить товар");
    Supporting.highlightCurrentLineBySomeColor("2. Прекратить добавление товаров на корабль", Supporting.setColorByValidator(newAddingDetail.Goods.Any()), true);
    switch (Supporting.validAnswer(new int[] { 1, 2}))
    {
      case 1:
        Good addingGood = new Good(true); //Добавление нового товара в базу данных
        newAddingDetail.Goods.Add(addingGood);
        Supporting.highlightCurrentLineBySomeColor("Груз добавлен. Сохраняем изменения...", ConsoleColor.Green);
        Thread.Sleep(120);
        break;
      case 2:
        if (newAddingDetail.Goods.Count != 0) keepAddingGoods = false;
        break;
    }
    Console.Clear();
  }
}

//Удаление деталей о грузоперевозке
void deleteTransportationDetailFromDatabase()
{
  if (!context.Details.IsNullOrEmpty())
  {
    int idOfRemovingTransaction = Supporting.validAnswer(showAllSavedDetailsFromDB().ToArray());
    TransportationDetail tsDetailToRemove = (TransportationDetail)context.Details.First(d => d.ID == idOfRemovingTransaction);
    Ship shipToDel = tsDetailToRemove.Ship;
    Company companyToDel = tsDetailToRemove.Ship.TransCompany;
    List<TransportationDetail> allDetails = context.Details.FromSqlRaw("Select * FROM Details").Include(g=>g.Goods).Include(s => s.Ship).Include(c => c.Ship.TransCompany).ToList();
    if (allDetails.Where(d => d.Ship.ID == shipToDel.ID).Count() == 1) context.Ships.Remove(shipToDel);
    if (allDetails.Where(d => d.Ship.TransCompany.ID == companyToDel.ID).Count() == 1) context.Companies.Remove(companyToDel);
    //Список товаров всегда уникален - это можно будет измменить в дальнейшем
    List<Good> goodsToDel = context.Details.First(d => d.ID == idOfRemovingTransaction).Goods.ToList();
    foreach(Good deletingGood in goodsToDel)
    {
      context.Goods.Remove(deletingGood);
    }
    context.Details.Remove(tsDetailToRemove);
  }
  else
  {
    Supporting.highlightCurrentLineBySomeColor("\t\tУдаление невозможно! База данных пуста!", ConsoleColor.Red);
    Thread.Sleep(1200);
  }
}


//Полная библиотека всех аспектов гргузоперевозок из базы данных
void databaseLibrary()
{
  bool showingcycle = true;
  while (showingcycle)
  {
    Console.Clear();
    Supporting.highlightCurrentLineBySomeColor("Библиотека базы данных морских грузоперевозок", ConsoleColor.Green, true);
    Console.WriteLine("1. Вывести все грузоперевозки\n2. Вывести все компании\n3. Вывести все корабли\n4. Вывести все товары\n5. Выход");
    switch (Supporting.validAnswer(new int[] { 1, 2, 3, 4, 5 }))
    {
      case 1:
        showAllSavedDetailsFromDB();
        break;
      case 2:
        showAllSavedCompaniesFromDB();
        break;
      case 3:
        showAllSavedShipsFromDB();
        break;
      case 4:
        showAllSavedGoodsFromDB();
        break;
      case 5:
        showingcycle = false;
        Console.WriteLine("Выход из библиотеки базы данных...");
        break;
    }
    Console.Write("Для продолжения нажмите на любую клавишу");
    Console.ReadKey();
  }
}

//Вывод всех деталей грузоперевозок из базы данных
List<int> showAllSavedDetailsFromDB()
{
  List<int> Ids = new List<int>();
  var answers = context.Details.FromSqlRaw(" SELECT * FROM Details").Include(s=> s.Ship).Include(c=>c.Ship.TransCompany).Include(g => g.Goods).ToList(); 
  if (answers.Count()!=0)
  {
    Console.WriteLine(new String('-', Console.BufferWidth));
    foreach (var detail in answers)
    {
      detail.Show();
      Ids.Add(detail.ID);
      foreach (Good good in detail.Goods)
      {
        Console.SetCursorPosition(6, Console.CursorTop);
        Supporting.highlightCurrentLineBySomeColor($"{good.ID}. " + good.ToString(), ConsoleColor.DarkCyan, true);
      }
      Console.SetCursorPosition(0,Console.CursorTop);
    }
    Console.WriteLine(new String('-', Console.BufferWidth));
  }
  else
  {
    Supporting.highlightCurrentLineBySomeColor("База грузоперевозок пуста!", ConsoleColor.Red);
    Thread.Sleep(1000);
  }
  return Ids;
}

//Вывод всех сохраненных в базе данных компаний, ответственных за грузоперевозки
List<int> showAllSavedCompaniesFromDB()
{
  List<int> Ids = new List<int>();
  var answers = context.Companies.FromSqlRaw(" SELECT * FROM Companies").ToList();
  if (answers.Count() != 0)
  {
    Console.WriteLine(new String('-', Console.BufferWidth));
    foreach (Company company in answers)
    {
      company.Show();
      Ids.Add(company.ID);
    }
    Console.WriteLine(new String('-', Console.BufferWidth));
  }
  else
  {
    Supporting.highlightCurrentLineBySomeColor("База компаний пуста!", ConsoleColor.Red);
    Thread.Sleep(1000);
  }
  return Ids;
}

//Вывод всех(или определенной компании) кораблей, участвующих в грузоперевозках
List<int> showAllSavedShipsFromDB(Company companyOwner = null)
{
  List<int> Ids = new List<int>();
  List<Ship> answers;
  if (companyOwner != null) answers = context.Ships.FromSqlRaw(" SELECT * FROM Ships").Where(s => s.TransCompany.ID == companyOwner.ID).ToList();
  else answers = context.Ships.FromSqlRaw(" SELECT * FROM Ships").ToList();
  if (answers.Count() != 0)
  {
    Console.WriteLine(new String('-', Console.BufferWidth));
    foreach (Ship ship in answers)
    {
      Ids.Add(ship.ID);
      ship.Show();
    }
    Console.WriteLine(new String('-', Console.BufferWidth));
  }
  else
  {
    Supporting.highlightCurrentLineBySomeColor("База кораблей пуста!", ConsoleColor.Red);
    Thread.Sleep(1000);
  }
  return Ids;
}

//Вывод всех сохраненных в базе данных товаров из грузоперевозок
List<int> showAllSavedGoodsFromDB()
{
  List<int> Ids = new List<int>();
  var answers = context.Goods.FromSqlRaw(" SELECT * FROM Goods").ToList();
  if (answers.Count() != 0)
  {
    Console.WriteLine(new String('-', Console.BufferWidth));
    foreach (Good good in answers)
    {
      Ids.Add(good.ID);
      Supporting.highlightCurrentLineBySomeColor($"{good.ID}. " + good.ToString(), ConsoleColor.DarkCyan, true);
    }
    Console.WriteLine(new String('-', Console.BufferWidth));
  }
  else
  {
    Supporting.highlightCurrentLineBySomeColor("База товаров пуста!", ConsoleColor.Red);
    Thread.Sleep(1000);
  }
  return Ids;
}
