using System;
using System.Collections.Generic;
using System.Linq; // Nécessaire pour le tri

class Client
{
    public string Nom { get; set; }
    public double TransactionsMensuelles { get; set; }
    public string Pays { get; set; }
    public bool ActiviteSuspecte { get; set; }
    public string TypeCompte { get; set; }

    public string EvaluerRisque()
    {
        int score = 0;

        // Critère 1 : Montant des transactions
        if (TransactionsMensuelles > 10000) score += 2;
        else if (TransactionsMensuelles > 5000) score += 1;

        // Critère 2 : Pays à risque
        string[] paysRisque = { "CorruptionLand", "Fraudistan" };
        if (Array.Exists(paysRisque, p => p == Pays)) score += 2;

        // Critère 3 : Activité suspecte
        if (ActiviteSuspecte) score += 2;

        // Critère 4 : Type de compte (Offshore → Risque élevé)
        if (TypeCompte == "Offshore") score += 1;

        // Détermination du niveau de risque
        if (score >= 4) return "Élevé";
        if (score >= 2) return "Moyen";
        return "Faible";
    }

    public int NiveauRisque()
    {
        return EvaluerRisque() switch
        {
            "Élevé" => 3,
            "Moyen" => 2,
            _ => 1 // "Faible"
        };
    }
}

class Program
{
    static void Main()
    {
        List<Client> clients = new List<Client>
        {
            new Client { Nom = "Alice", TransactionsMensuelles = 3000, Pays = "France", ActiviteSuspecte = false, TypeCompte = "Standard" },
            new Client { Nom = "Bob", TransactionsMensuelles = 12000, Pays = "Fraudistan", ActiviteSuspecte = false, TypeCompte = "Offshore" },
            new Client { Nom = "Charlie", TransactionsMensuelles = 7000, Pays = "Allemagne", ActiviteSuspecte = true, TypeCompte = "Premium" },
            new Client { Nom = "David", TransactionsMensuelles = 1500, Pays = "CorruptionLand", ActiviteSuspecte = false, TypeCompte = "Standard" },
            new Client { Nom = "Emma", TransactionsMensuelles = 20000, Pays = "USA", ActiviteSuspecte = true, TypeCompte = "VIP" }
        };

        // Trier les clients du plus risqué au moins risqué
        var clientsTries = clients.OrderByDescending(c => c.NiveauRisque()).ToList();

        Console.WriteLine("Évaluation du risque KYC pour les clients (du plus risqué au moins risqué) :\n");
        foreach (var client in clientsTries)
        {
            Console.WriteLine($"Client: {client.Nom} | Risque: {client.EvaluerRisque()}");
        }
    }
}
