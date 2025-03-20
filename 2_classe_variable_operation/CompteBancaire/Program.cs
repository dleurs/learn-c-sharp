using System;

class CompteBancaire
{
    private string titulaire;
    private decimal solde;

    public CompteBancaire(string titulaire, decimal soldeInitial)
    {
        this.titulaire = titulaire;
        this.solde = soldeInitial;
    }

    public void Deposer(decimal montant)
    {
        if (montant > 0)
        {
            solde += montant;
            Console.WriteLine($"Dépôt de {montant:C} effectué. Nouveau solde: {solde:C}");
        }
        else
        {
            Console.WriteLine("Le montant du dépôt doit être positif.");
        }
    }

    public void Retirer(decimal montant)
    {
        if (montant > 0 && montant <= solde)
        {
            solde -= montant;
            Console.WriteLine($"Retrait de {montant:C} effectué. Nouveau solde: {solde:C}");
        }
        else if (montant <= 0)
        {
            Console.WriteLine("Le montant du retrait doit être positif.");
        }
        else
        {
            Console.WriteLine($"Fonds insuffisants pour effectuer ce retrait de {montant:C}.");
        }
    }

    public void AfficherSolde()
    {
        Console.WriteLine($"Solde actuel du compte de {titulaire}: {solde:C}");
    }
}

class Program
{
    static void Main()
    {
        CompteBancaire compte = new CompteBancaire("Jean Dupont", 1000);
        compte.AfficherSolde();
        compte.Deposer(500);
        compte.Retirer(200);
        compte.Retirer(2000); 
    }
}
