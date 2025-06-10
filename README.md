

# 📚 Projekt Praktyki Zawodowe

Aplikacja konsolowa w języku C# służąca do zarządzania praktykami zawodowymi uczniów. Pozwala na dodawanie uczniów i firm, przypisywanie uczniów do firm, generowanie umów oraz automatyczne wysyłanie ich na e-mail studenta.

---

## 🧩 Funkcjonalności

- ✅ Dodawanie, przeglądanie i usuwanie uczniów
- ✅ Dodawanie, przeglądanie i usuwanie firm
- ✅ Przypisywanie uczniów do firm
- ✅ Generowanie pliku umowy `.txt`
- ✅ Automatyczne wysyłanie umowy na e-mail studenta (Outlook SMTP)

---

## 📁 Struktura projektu

```markdown
Projekt_Praktyki_Zawodowe/
│
├── Program.cs
├── Projekt_Praktyki_Zawodowe.db          ← baza danych SQLite (tworzona automatycznie)
├── Umowy/                                ← folder z wygenerowanymi umowami (.txt)
│
├── Helpers/
│   └── DbHelper.cs                       ← obsługa połączenia z bazą danych
│
├── Managers/
│   ├── StudentManager.cs                ← zarządzanie uczniami
│   ├── CompanyManager.cs                ← zarządzanie firmami
│   ├── RegistrationManager.cs           ← przypisywanie uczniów do firm
│   ├── AgreementManager.cs              ← generowanie i zapis umowy
│   └── EmailSender.cs                   ← wysyłanie umowy mailem do ucznia (nie działa)
```
## 📁 Struktura bazy danych (SQL)

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

## 💡 Jak uruchomić projekt
Zmień ciąg połączenia (`ConnectionString`) w pliku `Program.cs` na własny, jeśli to konieczne:
   ```csharp
   private const string ConnectionString = "Server=...;Database=...;User ID=...;Password=...;";
   ```

### ✅ Wymagania

- .NET SDK (7.0 lub wyższy)
- Visual Studio lub dowolny edytor obsługujący C#
- Połączenie internetowe (jeśli chcesz wysyłać e-maile)

### ▶️ Uruchomienie

```bash
git clone https://github.com/Mateusz-47/aplikacja_praktyki_zawodowe.git
cd aplikacja_praktyki_zawodowe
dotnet build
dotnet run
````

---

##📨 Konfiguracja wysyłania e-maili (Outlook SMTP) 

Aplikacja automatycznie wysyła wygenerowaną umowę `.txt` na e-mail przypisany do studenta.


### 📌 Co musisz zrobić:

1. Mieć konto Outlook.com lub Microsoft 365
2. Włączyć **uwierzytelnianie dwuskładnikowe (2FA)**
3. Wygenerować **hasło aplikacji (App Password)**:

   * Zaloguj się na: [https://account.live.com/proofs/manage](https://account.live.com/proofs/manage)
   * Wybierz „Hasła aplikacji” → „Utwórz nowe hasło aplikacji”
4. W pliku `EmailSender.cs` wstaw swoje dane logowania:

```csharp
var fromAddress = new MailAddress("twojemail@outlook.com", "Nazwa nadawcy");
string fromPassword = "twoje_haslo_aplikacji"; // App password
```

### 🛠 Domyślne ustawienia SMTP:

```csharp
smtp.Host = "smtp.office365.com";
smtp.Port = 587;
smtp.EnableSsl = true;
smtp.Credentials = new NetworkCredential(fromAddress.Address, fromPassword);
```

---

## 🗂️ Generowanie i wysyłanie umów

* Umowy są zapisywane automatycznie w folderze `Umowy` (obok pliku EXE).
* Nazwa pliku: `Umowa_Imie_Nazwisko.txt`
* Jeżeli e-mail studenta istnieje — umowa zostanie automatycznie do niego wysłana jako załącznik. [❌ NIE DZIAŁA]

