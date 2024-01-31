# GlobalCargoOperationsHandler
### The project based on C# code which linked with SQL database by Entity Framework. Database contains 4 tables each of which describes any of aspects cargo transportations. Tables linked among themselves by foreign keys to create a kind of database architecture. Let's see what this program is capable of. 
# Contents
## [Program sections:](#program-sections)
- ### [Add new detail](#adding)
- ### [Delete detail](#deleting)
- ### [Display all details](#displaying)
- ### [Close the program](#ending)
## [Conclusion:](#conclusion)
- ### [Gained experience](#experience)
- ### [Problems](#problems)
- ### [Code features](#features)
## [SQL Screenshots](#sql-screenshots)

# Main menu

![MainMenu_screenshot](https://github.com/atomforevertoxic/GlobalCargoOperationsHandler/blob/main/Screenshots/MainMenu.jpg)
**So, how we can see, the program has some commands to do.**
## <a name = "program-sections"><a/>Program sections:
### <a name = "adding"><a/>Add new detail

First action, what we can inject, is "Добавить грузоперевозку" that means adding a new cargo transportation detail to database.

This command leads to two outcomes:
  ### **Empty database**
  In this way the program will offer you to form a new company... 

![MainMenu_screenshot](https://github.com/atomforevertoxic/GlobalCargoOperationsHandler/blob/main/Screenshots/formingNewCompany.jpg)

  and later a new ship for transportation.
  
![MainMenu_screenshot](https://github.com/atomforevertoxic/GlobalCargoOperationsHandler/blob/main/Screenshots/formingNewShip.jpg)

 ### **Not empty database**

If the database is not empty, the user will have a choice between creating a new transport company and using an existing one.
_**If you select the item adding an aspect that already exists in the database to the details of cargo transportation, the program will display a list of all currently existing similar objects**_

![MainMenu_screenshot](https://github.com/atomforevertoxic/GlobalCargoOperationsHandler/blob/main/Screenshots/startAddingDetailWithExistingCompany.jpg)

Further, a similar choice happens when adding a ship.

![MainMenu_screenshot](https://github.com/atomforevertoxic/GlobalCargoOperationsHandler/blob/main/Screenshots/formingExistingShip.jpg)

It is worth noting that adding goods to the ship happens a little differently. The project does not support adding products that already exist in the database, because this action destroys some relationships between database tables.

![MainMenu_screenshot](https://github.com/atomforevertoxic/GlobalCargoOperationsHandler/blob/main/Screenshots/formingNewGood.jpg)

After adding user can see list of all added goods

![MainMenu_screenshot](https://github.com/atomforevertoxic/GlobalCargoOperationsHandler/blob/main/Screenshots/goodMenuAfterAdding.jpg)

_Command "Прекратить добавление товаров на корабль" stops adding goods and saves cargo transportation detail._
### <a name = "deleting"><a/>Delete detail

Second action is "Удалить грузоперевозку". That means deleting transportation detail

![MainMenu_screenshot](https://github.com/atomforevertoxic/GlobalCargoOperationsHandler/blob/main/Screenshots/deletingDetail.jpg)

_If the cargo transportation detail contains a company or ship that is not involved in other transportations, they will be also deleted from the database. Goods transported as a result of cargo transportation are removed in any case_

### <a name = "ending"><a/>Close the program
The last action closes the program. You shouldn't worry about saving, because it occurs immediately after adding information

### <a name = "displaying"><a/>Display all details
- Third action of this program is "Вывести все прошедшие грузоперевозки" that means displaying all past shipments and providing a library of all aspects of the database in particular.

![MainMenu_screenshot](https://github.com/atomforevertoxic/GlobalCargoOperationsHandler/blob/main/Screenshots/databaseLibrary.jpg)

Every action is a library of separate type of objects of database:

![MainMenu_screenshot](https://github.com/atomforevertoxic/GlobalCargoOperationsHandler/blob/main/Screenshots/dboLibrary1.jpg)

![MainMenu_screenshot](https://github.com/atomforevertoxic/GlobalCargoOperationsHandler/blob/main/Screenshots/dboLibrary2.jpg)

![MainMenu_screenshot](https://github.com/atomforevertoxic/GlobalCargoOperationsHandler/blob/main/Screenshots/dboLibrary3.jpg)

![MainMenu_screenshot](https://github.com/atomforevertoxic/GlobalCargoOperationsHandler/blob/main/Screenshots/dboLibrary4.jpg)

## <a name="conclusion"><a/> Conclusion
### <a name="experience"><a/> Gained experience
The section of the experience gained will sometimes be intertwined with the topic of the problems that had to be faced during development, since it was in solving these problems that that precious experience was accumulated. This project is based on the connection of an SQL database and a "server" acting as my PC. The connection was provided by the Entity Framework, which I studied in Microsoft courses and articles with Metanit. Of course, we also had to study the database itself, its configuration, linking using EF core (Migrations, Snapshots, etc.), the sql language, which was used to generate requests for previously saved data from the database and more.
### <a name="problems"><a/> Problems
I expected to encounter a lot of problems when I decided to delve into a topic I didn't understand much about. The database setup itself, its connection with C#, was already problematic, but it was interesting to figure it out. Problems arose when designing the application architecture. I wanted to make the program as user-friendly as possible without breaking the relationship between the tables. It is for this reason that I abandoned the idea of re-adding existing products. This idea can be implemented, but it would require a complete change of architecture and therefore even more time and effort, which is no longer available for this pet project. I also often encountered errors when sending requests for data, but after understanding the types of data that I receive, as well as reading several articles and discussions, all the problems were fixed.

### <a name="features"><a/> Code features
While acquiring the skills to write readable and high-quality code at university, I decided to transfer the experience I gained into my own projects. Thus, the names of the methods have become longer and clearer. Now they describe in an expanded form what this function does. I tried to create a universal and competent function, avoid unnecessary code duplication and enable the programmer to use the method for as many tasks as possible.

Based on the principles of OOP, I divided the project into files with classes, each of which performs a separate part of the program. I took into account the scope of methods and variables, where it might be required, and added XML comments to my project, which increased the readability of the code and improved coding.

## <a name = "sql-screenshots"><a/>SQL Screenshots
At the end, you can make sure that all the data is actually stored in the database, and the ids are self-incremented: 

![MainMenu_screenshot](https://github.com/atomforevertoxic/GlobalCargoOperationsHandler/blob/main/Screenshots/dboDetails.jpg)

![MainMenu_screenshot](https://github.com/atomforevertoxic/GlobalCargoOperationsHandler/blob/main/Screenshots/dboCompanies.jpg)

![MainMenu_screenshot](https://github.com/atomforevertoxic/GlobalCargoOperationsHandler/blob/main/Screenshots/dboShips.jpg)

![MainMenu_screenshot](https://github.com/atomforevertoxic/GlobalCargoOperationsHandler/blob/main/Screenshots/dboGoods.jpg)

