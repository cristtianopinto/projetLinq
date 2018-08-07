using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace projetLinq
{

    class Program
    {
        private static readonly int[] numeros = new int[] { 1, 3, 4, 6, 7, 8, 11, 15 };
        private static readonly int[] scores = new int[] { 97, 92, 81, 60};
        private static readonly string valeur = "mammouth";

        private static readonly string[] prenoms = new string[] {
            "Thierry", "Alexina", "Alexandre", "Frédéric",
            "Youcef", "Stéphane", "Laura", "Chi Trung", "Julien",
            "Cristtiano", "Hatem", "Laynet", "Anne", "Thi Tuong" };

        private static readonly List<Ville> villes = new List<Ville> {
            new Ville { Nom = "Paris", CodePostal = "75013" },
            new Ville { Nom = "Bordeaux", CodePostal = "33000" },
            new Ville { Nom = "Sarlat-La-Canéda", CodePostal = "24200" },
            new Ville { Nom = "Marseille", CodePostal = "13000" },
        };

        private static readonly List<Departement> departements = new List<Departement> {
            new Departement { Nom = "Paris", Numero = "75" },
            new Departement { Nom = "Dordogne", Numero = "24" },
            new Departement { Nom = "Charente", Numero = "16" },
            new Departement { Nom = "Gironde", Numero = "33" },
        };
        private static void RequeteAvecJointure()
        {
            var requete = from ville in villes
                          join departement in departements
                          on ville.Departement equals departement.Numero
                          select new
                          {
                              Ville = ville.Nom,
                              NomDepartement = departement.Nom
                          };
            foreach(var r in requete)
            {
                Console.Write("ville " + r.Ville);Console.WriteLine("\tdep. " + r.NomDepartement);
            }
        }
        static void EcrireFichier()
        {
            var cheminFichier = @"C:\\Temp\ville.txt";

            var contenuFichier = new StringBuilder();
            foreach (var ville in villes)
            {
                contenuFichier.AppendLine($"{ville.Nom};{ville.CodePostal}");
                contenuFichier.AppendLine(string.Join(";", ville.Nom, ville.CodePostal));
            }
            File.WriteAllText(cheminFichier, contenuFichier.ToString());
        }
        static void LireFichier()
        {
            var cheminFichier = @"C:\\Temp\ville.txt";
            if (File.Exists(cheminFichier))
            {
                IEnumerable<string> lignesFichier = File.ReadLines(cheminFichier);
                var villesDansFichier = new List<Ville>();
                foreach (var ligneFichier in lignesFichier)
                {
                    string[] champs = ligneFichier.Split(';');
                    var ville = new Ville();
                    ville.Nom = champs[0];
                    ville.CodePostal = champs[1];

                    villesDansFichier.Add(ville);
                }
            }
        }

        static void Main(string[] args)
        {
            /*
            int[] linsteNumeros = new int[5] { 5, 8, 2, 3, 4 };

            var requete = from num in linsteNumeros
                          where (num % 2 ) == 0
                          orderby num ascending // order le resultat
                          select num;
            foreach(int numero in requete)
            {
                Console.Write($"{numero}");
            }
            //Interessent
            Console.SetCursorPosition(Console.CursorLeft + 15, 0);
            Console.ReadLine();
            Console.Write("teste");
            Console.SetCursorPosition(Console.CursorLeft + 30, 0);
            Console.ReadLine();*/
            //Console.WriteLine(Console.CursorLeft);
            //Console.ReadKey();
            //RequeteSimple();
            //RequeteAvecGroupement();
            //AfficherScore(1);
            //Console.WriteLine();
            //AfficherScore(2);
            //Console.ReadKey();
            //RequeteMammouth();
            //RequeteAvecJointure();
            LireFichier();
            Console.ReadKey();
        }
        private static void AfficherScore(int op)
        {
            if (op == 1)
            {
                foreach (int score in RequeteScores1())
                {
                    Console.Write($"\t - {score} ");
                }
            }
            else if (op == 2)
            {
                foreach (int score in RequeteScores2())
                {
                    Console.Write($"\t - {score} ");
                }
            }

        }
        private static void RequeteMammouth()
        {
            char[] arrayChar = valeur.ToArray<Char>() ;
            var requete = from lettre in arrayChar
                          group lettre by lettre into groupe
                          select groupe;
            foreach (var mot in requete)
            {
                Console.WriteLine($"\t - {mot.Key}  / {mot.Count()}");
            };
            
        }

        private static IEnumerable<int> RequeteScores1()
        {
            var requete = from score in scores
                          where score > 80
                          select score;
            return requete;
        }
        private static IEnumerable<int> RequeteScores2()
        {
            var requete = from score in scores
                          orderby score ascending
                          select score;
            return requete;
        }
        private static void RequeteAvecGroupement()
        {
            AfficherEntete();

            // Syntaxe de requête
            //prenoms = TaskCompletionSource de donnez
            var requete = from prenom in prenoms
                          group prenom by prenom[0] into groupe
                          let lettre = groupe.Key
                          orderby lettre
                          select new
                          {
                              //Lettre = groupe.Key,
                              Lettre = lettre,
                              Prenoms = groupe.ToList()
                          };
            foreach (var resultat in requete)
            {
                Console.WriteLine(resultat.Lettre);
                foreach (var prenom in resultat.Prenoms)
                    Console.WriteLine($"\t{prenom}");
            }
                
            Console.ReadKey();
            // Syntaxe de méthode


        }

        private static void RequeteSimple()
        {
            AfficherEntete();

            // Syntaxe de requête
            var requete = from prenom in prenoms
                          where prenom.StartsWith("A")
                          select prenom;
            foreach (var resultat in requete)
                Console.WriteLine(resultat);
            Console.ReadKey();
            // Syntaxe de méthode
        }
        private static void RequeteAvecTri()
        {
            AfficherEntete();

            // Syntaxe de requête
            var requete = from prenom in prenoms
                          where prenom.StartsWith("A")
                          select prenom;

            // Syntaxe de méthode
        }

        private static void AfficherEntete([CallerMemberName]string nomMethodeAppelant = null)
        {
            Console.WriteLine();
            Console.WriteLine(nomMethodeAppelant);
            Console.WriteLine(new string('-', 60));
        }
    }
}
