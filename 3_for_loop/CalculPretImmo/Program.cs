class Program
{
    static void Main()
    {
        Console.Write("Montant emprunté en euros (exemple '100000') : ");
        string? montantEmprunteEurosString = Console.ReadLine();
        double montantEmprunteEuros = Convert.ToDouble(montantEmprunteEurosString);

        Console.Write("Durée du prêt en année (exemple '15') : ");
        string? dureePretAnneeString = Console.ReadLine();
        double dureePretAnnee = Convert.ToDouble(dureePretAnneeString);
        double dureePretMois = dureePretAnnee * 12;

        Console.Write("Taux d'intérêt (exemple '3') : ");
        string? tauxInteretString = Console.ReadLine();
        double tauxInteretAnnuel = Convert.ToDouble(tauxInteretString) / 100;
        double tauxInteretMensuel = tauxInteretAnnuel / 12;

        Console.WriteLine("=========================================================");
        Console.WriteLine($"Montant emprunté : {montantEmprunteEuros} euros");
        Console.WriteLine($"Durée du prêt : {dureePretAnnee} ans");
        Console.WriteLine($"Taux d'intérêt : {tauxInteretAnnuel * 100}%");
        Console.WriteLine("=========================================================");

        double unPlusT = 1 + tauxInteretMensuel;
        double unPlusTPuissanceN = 1 + tauxInteretMensuel;
        for (int i = 1; i <= dureePretMois; i++)
        {
            unPlusTPuissanceN = unPlusTPuissanceN * unPlusT;
        }
        double mensualite = (montantEmprunteEuros * tauxInteretMensuel * unPlusTPuissanceN) / (unPlusTPuissanceN - 1);
        double coutTotalDuPret = mensualite * dureePretMois;
        double coutInteret = coutTotalDuPret - montantEmprunteEuros;

        Console.WriteLine($"Mensualité : {Math.Round(mensualite, 2)} euros");
        Console.WriteLine($"Coût total du prêt : {Math.Round(coutTotalDuPret, 2)} euros");
        Console.WriteLine($"Coût des intérêts : {Math.Round(coutInteret, 2)} euros");
    }
}

