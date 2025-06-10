using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Mail;
using System;

namespace Projekt_Praktyki_Zawodowe.Managers
{
    public static class EmailSender
    {
        public static void SendAgreement(string studentEmail, string filePath)
        {
            var from = "praktyki@outlook.com"; // Twój adres
            var password = "R7p#xW2eQ";       // Hasło do konta
            var subject = "Umowa praktyk zawodowych";
            var body = "W załączniku znajduje się umowa praktyk.";

            var smtp = new SmtpClient("smtp.office365.com", 587)
            {
                Credentials = new NetworkCredential(from, password),
                EnableSsl = true
            };

            var message = new MailMessage(from, studentEmail, subject, body);

            if (!File.Exists(filePath))
            {
                Console.WriteLine("❌ Plik umowy nie istnieje: " + filePath);
                return;
            }

            message.Attachments.Add(new Attachment(filePath));
            Console.WriteLine("Ścieżka do załącznika: " + filePath);

            try
            {
                smtp.Send(message);
                Console.WriteLine("\n📧 Wysłano umowę na adres e-mail studenta.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ Błąd podczas wysyłania maila: {ex.Message}");
            }
        }
    }
}

