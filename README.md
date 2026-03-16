## Зміст
- [Опис](#опис)
- [Вимоги](#вимоги)
- [Встановлення](#встановлення)
- [Використання](#використання)
- [Міграції](#міграції)

## Опис
У проекті є можливість підключити різні БД
Два рядка підключення присутні в appsettings.json
Треба уважно переглянути секцію LibraryDbContext там костиль з датою
В Program.cs також підключення треба уважно дивитись

## Вимоги

- .NET 8 SDK
- MySQL 8.0+ or MSSQL SERVER
- Visual Studio 2022
- EF Core 8.0


## Міграції
- Створення Add-Migration NameOfMigration -Project Books.Infrastructure -StartupProject Books.Api
- Накат Update-Database -Project Books.Infrastructure -StartupProject Books.Api
- Видалення Drop-Database

