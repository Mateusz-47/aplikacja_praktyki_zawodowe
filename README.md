# System Rejestracji na Praktyki Zawodowe

Aplikacja konsolowa w języku C# służąca do zarządzania uczniami i firmami w kontekście praktyk zawodowych. Projekt łączy się z bazą danych SQL (hostowaną na platformie Azure) i umożliwia m.in. dodawanie uczniów, przeglądanie firm, rejestrowanie uczniów na praktyki oraz generowanie prostych umów.

## 🛠 Technologie

- .NET / C# (aplikacja konsolowa)
- Azure SQL Database
- Microsoft.Data.SqlClient

## 📦 Funkcjonalności

1. **Dodawanie uczniów**  
   Pozwala na wprowadzenie danych ucznia do bazy (`Students`).

2. **Wyświetlanie listy firm**  
   Pokazuje wszystkie dostępne firmy z tabeli `Companies`.

3. **Rejestracja ucznia na praktyki**  
   Umożliwia zapisanie ucznia do firmy, jeśli dostępne są wolne miejsca (`Registrations`).

4. **Lista uczniów zapisanych do firmy**  
   Wyświetla uczniów przypisanych do danej firmy, wraz z datą rejestracji.

5. **Lista wszystkich uczniów i firm**  
   Drukuje osobno wszystkie wpisy z tabel `Students` i `Companies`.

6. **Generowanie umowy**  
   Tworzy plik tekstowy z umową praktyk na podstawie wpisu rejestracji.

---

## 🔧 Konfiguracja

Przed uruchomieniem aplikacji:

1. Upewnij się, że masz dostęp do bazy danych na Azure SQL.
2. Zmień ciąg połączenia (`ConnectionString`) w pliku `Program.cs` na własny, jeśli to konieczne:
   ```csharp
   private const string ConnectionString = "Server=...;Database=...;User ID=...;Password=...;";
   ```

---

## 📁 Struktura bazy danych (SQL)

Poniżej przedstawiono definicje tabel wymaganych do działania aplikacji:

```sql
CREATE TABLE Students (
    Id INT IDENTITY PRIMARY KEY,
    FirstName NVARCHAR(50),
    LastName NVARCHAR(50),
    Class NVARCHAR(20),
    Phone NVARCHAR(20),
    Email NVARCHAR(100)
);

CREATE TABLE Companies (
    Id INT IDENTITY PRIMARY KEY,
    Name NVARCHAR(100),
    Supervisor NVARCHAR(100),
    Address NVARCHAR(150),
    MaxPlaces INT
);

CREATE TABLE Registrations (
    Id INT IDENTITY PRIMARY KEY,
    StudentId INT FOREIGN KEY REFERENCES Students(Id),
    CompanyId INT FOREIGN KEY REFERENCES Companies(Id),
    RegisteredAt DATETIME
);
```

---

## ▶️ Uruchomienie

1. Otwórz projekt w Visual Studio lub innym środowisku zgodnym z .NET.
2. Zbuduj projekt (`Build`).
3. Uruchom aplikację (`Start` lub `F5`).
4. Korzystaj z menu tekstowego w konsoli.

---

## 📄 Przykładowy plik umowy

Aplikacja generuje plik `.txt` z umową praktyk zawodowych w formacie:

```
UMOWA O PRAKTYKI ZAWODOWE
========================

Uczeń: Jan Kowalski
Firma: ABC Sp. z o.o.
Data: 2025-05-01 09:30

Podpisy:
____________________        ____________________
     Uczeń                Pracodawca
```

---

## 🧾 Licencja

Projekt stworzony do celów edukacyjnych – brak ograniczeń licencyjnych.

---

## ✍️ Autor

Projekt stworzony w ramach praktyk zawodowych.  
Autor: *Twoje imię i nazwisko*  
Szkoła: *Zespół Szkół Technicznych w Radomiu*
