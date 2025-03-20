using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Globalization;

class ValidationUtil
{
    // Vérifie si l'email est valide
    public static bool ValiderEmail(string email)
    {
        string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        return Regex.IsMatch(email, pattern);
    }

    // Vérifie si le numéro de téléphone est valide (Format : +XX XXXXXXXXXX)
    public static bool ValiderTelephone(string telephone)
    {
        string pattern = @"^\+\d{1,3} \d{7,12}$";
        return Regex.IsMatch(telephone, pattern);
    }

    // Vérifie si la date de naissance est valide et si la personne a au moins 18 ans
    public static bool ValiderDateNaissance(string dateNaissance, out int age)
    {
        age = 0;

        // Vérifie si la date est bien au format dd-MM-yyyy
        if (!Regex.IsMatch(dateNaissance, @"^\d{2}-\d{2}-\d{4}$"))
        {
            return false; // Format incorrect
        }

        // Conversion en objet DateTime en respectant le format exact
        if (DateTime.TryParseExact(dateNaissance, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
        {
            age = DateTime.Now.Year - date.Year;
            if (date > DateTime.Now.AddYears(-age)) age--; // Ajuste si l'anniversaire n'est pas encore passé
            return age >= 18;
        }

        return false; // Date invalide
    }
}

class Client
{
    public string Nom { get; set; }
    public string Email { get; set; }
    public string Telephone { get; set; }
    public string DateNaissance { get; set; }

    public void AfficherValidation()
    {
        Console.WriteLine($"\n🟢 Validation pour {Nom}:");
        Console.WriteLine($"- Email : {Email} → {(ValidationUtil.ValiderEmail(Email) ? "✅ Valide" : "❌ Invalide")}");
        Console.WriteLine($"- Téléphone : {Telephone} → {(ValidationUtil.ValiderTelephone(Telephone) ? "✅ Valide" : "❌ Invalide")}");

        if (!Regex.IsMatch(DateNaissance, @"^\d{2}-\d{2}-\d{4}$"))
        {
            Console.WriteLine($"- Date de naissance : {DateNaissance} → ❌ Format invalide (Utilisez dd-MM-yyyy)");
        }
        else if (ValidationUtil.ValiderDateNaissance(DateNaissance, out int age))
        {
            Console.WriteLine($"- Date de naissance : {DateNaissance} (Âge : {age} ans) → ✅ Valide");
        }
        else
        {
            Console.WriteLine($"- Date de naissance : {DateNaissance} → ❌ Invalide ou âge < 18 ans");
        }
    }
}

class Program
{
    static void Main()
    {
        List<Client> clients = new List<Client>
        {
            new Client { Nom = "Alice", Email = "alice@example.com", Telephone = "+33 612345678", DateNaissance = "12-05-2000" },
            new Client { Nom = "Bob", Email = "bob@invalid", Telephone = "+1 12345", DateNaissance = "20/06/2010" },  // Mauvais format
            new Client { Nom = "Charlie", Email = "charlie.email.com", Telephone = "+49 987654321", DateNaissance = "01-12-1995" },
            new Client { Nom = "David", Email = "david@company.org", Telephone = "+44 7123456789", DateNaissance = "15-08-2005" },
            new Client { Nom = "Emma", Email = "emma@valid.net", Telephone = "+33 699887766", DateNaissance = "30-11-1987" }
        };

        Console.WriteLine("🔍 Résultats de validation des clients :");
        foreach (var client in clients)
        {
            client.AfficherValidation();
        }
    }
}
