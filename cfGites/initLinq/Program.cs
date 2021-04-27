using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using cfGites;

namespace cfGites
{
    class Program
    {
        static void Main(string[] args)
        {
            ModelGites bd = new ModelGites();

            char choix = ' ';
            while (choix != '9')
            {
                choix = Menu();
                Traiter(choix, bd);
                Console.ReadLine();
                Console.Clear();
            }
        }
        private static char Menu()
        {
            char choix;
            Console.WriteLine("|| 0 : Liste des nombres > 5 sans linq");
            Console.WriteLine("|| 1 : Liste des nombres > 5 avec linq");
            Console.WriteLine("|| 2 : Liste des nombres > 5 triée en une seule requête");
            Console.WriteLine("|| 3 : Liste des gîtes qui ont plus de 2 épis classé par ordre alphabétique");
            Console.WriteLine("|| 4 : Liste des gîtes qui ont plus de 2 épis classé par ordre alphabétique(objet anonyme)");
            Console.WriteLine("|| 5 : Liste des gîtes réservés par le client CAMUS");
            Console.WriteLine("|| 6 : Liste des gîtes réservés par le client CAMUS (jointure traditionelle)");
            Console.WriteLine("|| 7 : Nombre de gîtes réservés par contrat");
            Console.WriteLine("|| 8 : Chiffres d'affaires générépar client");
            Console.WriteLine("|| 9 : Liste des clients \n");
            Console.WriteLine("|| 10 : Fin du traitement \n");
            Console.Write("|| Votre choix : ");

            choix = Console.ReadLine()[0];
            return choix;
        }
        private static void Traiter(char choix, ModelGites bd)
        {
            Console.WriteLine("");
            switch (choix)
            {
                case '0':
                    Requete_Un();
                    break;
                case '1':
                    Requete_Deux(bd);
                    break;
                case '2':
                    Requete_Deux(bd);
                    break;
                case '3':
                    Requete_Trois(bd);
                    break;
                case '4':
                    Requete_Trois(bd);
                    break;
                case '5':
                    Requete_Six(bd);
                    break;
                case '6':
                    Requete_Cinq(bd);
                    break;
                case '7':
                    Requete_Sept(bd);
                    break;
                case '8':
                    Requete_Huit(bd);
                    break;
                case '9':
                    allClient(bd);
                    break;
            }
            if (choix != '9')
            {
                Console.Write("continuer...");
            }
        }
        private static List<int> Requete_Un()
        {
            List<int> liste = new List<int>() { 4, 6, 1, 9, 5, 15, 8, 3 };
            List<int> newliste = new List<int>();
            foreach (int item in liste)
            {
                if (item > 5)
                {
                    newliste.Add(item);
                    Console.WriteLine(item);
                }
            }
            return newliste;
        }
        private static List<int> Requete_Deux(ModelGites bd)
        {
            List<int> liste = new List<int>() { 4, 6, 1, 9, 5, 15, 8, 3 };
            List<int> newliste = new List<int>();
            IEnumerable<int> requeteFiltree = liste.Where(i => i > 5);
            foreach (int i in requeteFiltree)
            {
                newliste.Add(i);
                Console.WriteLine(i);
            }
            return newliste;
        }
        private static void Requete_Trois(ModelGites bd)
        {
            var req = from gite in bd.gites
                      where gite.nbEpis > 2
                      orderby gite.nomGite
                      select new
                      {
                          gite.nomGite,
                          gite.villeGite
                      };
            foreach (var g in req)
                Console.WriteLine("Nom du gite : " + g.nomGite + " ,Ville :  " + g.villeGite);
        }
        private static void allClient(ModelGites bd)
        {
            foreach  (client cl in bd.clients)
            {
                Console.WriteLine(cl.nomClient);
            }
        }
        private static void Requete_Quatre(ModelGites bd)
        {
            var req = from gite in bd.gites
                      where gite.nbEpis > 2
                      orderby gite.nomGite
                      select new
                      {
                          gite.nomGite,
                          gite.villeGite
                      };
            foreach (var g in req)
                Console.WriteLine("Nom du gite : " + g.nomGite + " ,Ville :  " + g.villeGite);
        }
        private static void Requete_Cinq(ModelGites bd)
        {
            var r3 = from git in bd.gites
                     join plan in bd.plannings on git.numGite equals plan.numGite
                     join ctr in bd.contrats on plan.numContrat equals ctr.numContrat
                     join cli in bd.clients on ctr.numClient equals cli.numClient
                     where cli.nomClient == "CAMUS"
                     select new { git.nomGite, git.villeGite, ctr.numContrat, plan.numSemaine };
            foreach (var obj in r3)
            {
                Console.WriteLine("Gites loués par CAMUS {0} {1} contrat n° {2} pour la semaine N°{3}",
                    obj.nomGite, obj.villeGite, obj.numContrat, obj.numSemaine);
            }
        }
        private static void Requete_Six(ModelGites bd)
        {
            var r4 = from git in bd.gites
                     from plan in bd.plannings
                     from ctr in bd.contrats
                     from cli in bd.clients
                     where git.numGite == plan.numGite && plan.numContrat == ctr.numContrat && ctr.numClient == cli.numClient && cli.nomClient == "CAMUS"
                     select new
                     {
                         git.nomGite,
                         git.villeGite,
                         ctr.numContrat,
                         plan.numSemaine
                     };
            foreach (var obj in r4)
                Console.WriteLine("Gites loués par CAMUS {0} {1} contrat n° {2} pour la semaine N°{3}",
                     obj.nomGite, obj.villeGite, obj.numContrat, obj.numSemaine);
        }
        private static void Requete_Sept(ModelGites bd)
        {
            var req5 = from ctr in bd.contrats
                       from plan in bd.plannings
                       from cli in bd.clients
                       where ctr.numContrat == plan.numContrat && ctr.numClient == cli.numClient
                       group ctr by new
                       {
                           ctr.numContrat,
                           cli.nomClient
                       } into nbGitesContrat
                       select new
                       {
                           nbGitesContrat.Key.numContrat,
                           nbGitesContrat.Key.nomClient,
                           nbreGites = nbGitesContrat.Count()
                       };
            foreach (var obj in req5)
            {
                Console.WriteLine("le client : {0} a loué {1} gite(s) avec le contrat n° {2}"
                    , obj.nomClient, obj.nbreGites, obj.numContrat);
            }
        }
        private static void Requete_Huit(ModelGites bd)
        {
            var req6 = from ctr in bd.contrats
                       from plan in bd.plannings
                       from cli in bd.clients
                       from tf in bd.tarifs
                       from sem in bd.semaines
                       where ctr.numContrat == plan.numContrat && ctr.numClient == cli.numClient && plan.numGite == tf.numGite && plan.numSemaine == sem.numsemaine && sem.numPeriode == tf.numPeriode
                       group tf by new
                       {
                           cli.nomClient
                       } into CaClient
                       let CA = CaClient.Sum(i => i.prixSemaine)
                       select new
                       {
                           CaClient.Key.nomClient,
                           CA
                       };
            foreach (var obj in req6)
            {
                Console.WriteLine("le client : {0} a généré un chiffre d'affaires de {1} euros"
                    , obj.nomClient, obj.CA);
            }
        }


    }
}