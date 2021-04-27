using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using cfGites;

namespace gitesForm
{
    public partial class FGites : Form
    {
        List<string> requetes;
        ModelGites bd = new ModelGites();

        public FGites()
        {
            InitializeComponent();
            this.requetes = new List<string>() {
                "Liste des gîtes",
                "Tarifs des gîtes",
                "Liste des contrats annulés",
                "Montant du chiffre d'affaires réalisé par client", "Montant total de chaque contrat",
                "Liste des clients (nom, prénom) qui ont loué au moins un gîte",
                "Liste des clients  qui ont loué plus de de 3 gîtes",
                "Liste des clients  qui ont loué à la fois en période 1 et en période 2 ",
                "nom et prénom du (des) client(s) qui a (ont) signé le plus de contrat",
                "liste des gites qui n'ont jamais été loués",
                "donner le contrat (n°et nom client) qui représente le plus gros montant",
                "donner les contrats qui repésente le plus gros montant",
                "donner les villes dont le CA réalisé est supérieur à celui de la ville de Sommières"
            };
            this.cb_requetes.DataSource = this.requetes;
        }
        public void lancerRequete(string requete)
        {
            this.rb_resultat.Text = String.Empty;
            switch (requete)
            {
                case "Liste des gîtes":
                    var listeGites = from gite in bd.gites select new { gite.numGite, gite.nomGite, gite.villeGite };
                    foreach (var o in listeGites)
                    {
                        this.rb_resultat.Text += String.Format("Numéro de gîte: {0}  Nom du gîte: {1}  Ville: {2}\n", o.numGite, o.nomGite, o.villeGite);
                    }
                    break;
                case "Tarifs des gîtes":
                    var tarifsGites = from gite in bd.gites join tarif in bd.tarifs on gite.numGite equals tarif.numGite select new { gite.numGite, gite.nomGite, tarif.prixSemaine };
                    foreach (var o in tarifsGites)
                    {
                        this.rb_resultat.Text += String.Format("Numéro de gîte: {0}  Nom du gîte: {1}  Tarif par semaine: {2}\n", o.numGite, o.nomGite, o.prixSemaine);
                    }
                    break;
                case "Liste des contrats annulés":
                    var contratsAnnules = from contrat in bd.contrats where contrat.annule == 1 select new { contrat.numContrat };
                    foreach (var o in contratsAnnules)
                    {
                        this.rb_resultat.Text += String.Format("Numéro du contrat: {0}\n", o.numContrat);
                    }
                    break;
                case "Montant du chiffre d'affaires réalisé par client":
                    var montantChiffreAffaireParClient = from ctr in bd.contrats
                                                         from pl in bd.plannings
                                                         from cli in bd.clients
                                                         from tf in bd.tarifs
                                                         from sem in bd.semaines
                                                         where ctr.numContrat == pl.numContrat
                                                         && ctr.numClient == cli.numClient
                                                         && pl.numGite == tf.numGite
                                                         && pl.numSemaine == sem.numsemaine
                                                         && sem.numPeriode == tf.numPeriode
                                                         group tf by new { cli.nomClient }
                                                         into CaClient
                                                         let CA = CaClient.Sum(i => i.prixSemaine)
                                                         select new { CaClient.Key.nomClient, CA };
                    foreach (var o in montantChiffreAffaireParClient)
                    {
                        this.rb_resultat.Text += String.Format("Le client {0} a généré un chiffre d'affaires de {1} euros.\n", o.nomClient, o.CA);
                    }
                    break;
                case "Montant total de chaque contrat":
                    var montantTotalChaqueContrat = from ctr in bd.contrats
                                                    from plan in bd.plannings
                                                    from tarif in bd.tarifs
                                                    from semaine in bd.semaines
                                                    where ctr.numContrat == plan.numContrat
                                                    && plan.numSemaine == semaine.numsemaine
                                                    && semaine.numPeriode == tarif.numPeriode
                                                    && plan.numGite == tarif.numGite
                                                    group tarif by new { ctr.numContrat }
                                                    into Con
                                                    let CON = Con.Sum(i => i.prixSemaine)
                                                    select new { Con.Key.numContrat, CON };
                    foreach (var o in montantTotalChaqueContrat)
                    {
                        this.rb_resultat.Text += String.Format("Le contrat {0} a généré un chiffre d'affaires de {1} euros.\n", o.numContrat, o.CON);
                    }
                    break;
                case "Liste des clients (nom, prénom) qui ont loué au moins un gîte":
                    var listeClientsLouesUnGite = from git in bd.gites
                                                  from cli in bd.clients
                                                  from ctr in bd.contrats
                                                  from pl in bd.plannings
                                                  where pl.numContrat == ctr.numContrat
                                                  && ctr.numClient == cli.numClient
                                                  && git.numGite == pl.numGite
                                                  select new { cli.nomClient };
                    foreach (var o in listeClientsLouesUnGite)
                    {
                        this.rb_resultat.Text += String.Format("Le client {0} a loué au moins un gîte.\n", o.nomClient);
                    }
                    break;

                case "Liste des clients  qui ont loué à la fois en période 1 et en période 2 ":
                    var Listeclientslouéenperiode12 = (from ctr in bd.contrats
                                                       from plan in bd.plannings
                                                       from cli in bd.clients
                                                       from per in bd.periodes
                                                       where (ctr.numContrat == plan.numContrat) && (ctr.numClient == cli.numClient) && (per.numPeriode == 1 || per.numPeriode == 2)
                                                       select new
                                                       {
                                                           cli.nomClient,
                                                           cli.numClient,
                                                       }).Distinct().ToList();
                    foreach (var o in Listeclientslouéenperiode12)
                    {
                        this.rb_resultat.Text += String.Format("le client : {0} n°{1} \n", o.nomClient, o.numClient);
                    }
                    
                    break;
                case "nom et prénom du (des) client(s) qui a (ont) signé le plus de contrat":
                    var nomPrenomsPlusContrat = (from ctr in bd.clients
                                                 from c in bd.contrats
                                                 where ctr.numClient == c.numClient
                                                 group c by new
                                                 {
                                                     ctr.numClient,
                                                     ctr.nomClient
                                                 } into nbGitesContrat
                                                 select new
                                                 {
                                                     nbGitesContrat.Key.numClient,
                                                     nbGitesContrat.Key.nomClient,
                                                     nbrsContrat = nbGitesContrat.Count()

                                                 });
                    List<int> nbcontrat = new List<int>();
                    foreach (var obj in nomPrenomsPlusContrat)
                    {

                        nbcontrat.Add(obj.nbrsContrat);
                    }
                    foreach (var o in nomPrenomsPlusContrat)
                    {
                        if (nbcontrat.Max() == o.nbrsContrat)
                        {
                            this.rb_resultat.Text += String.Format("{0} a signé le plus de contrat {1}. \n", o.nomClient, o.nbrsContrat);
                        }
                    }
                    break;
                case "liste des gites qui n'ont jamais été loués": 
                    var listegitesnontjamaisloués = (from c in bd.contrats
                                    from p in bd.plannings
                                    where c.numContrat == p.numContrat && c.valide == 1 && c.annule == 0
                                    select new
                                    {
                                        p.numGite,
                                    }).ToList();

                    var ire = (from g in bd.gites
                                      select new
                                      {
                                          g.numGite,
                                      }).ToList();

                    var interQuerya = ire.Except(listegitesnontjamaisloués);

                    var result = (from z in interQuerya
                                    from g in bd.gites
                                    where z.numGite == g.numGite
                                    select new
                                    {
                                        g.nomGite,
                                    }).ToList();

                    foreach (var gite in result)
                    {
                        this.rb_resultat.Text += String.Format("Les gites : {0}\n", gite.nomGite);
                    }
                    break;

                case "donner les contrats qui repésente le plus gros montant":
                    var contratsquirepésenteplusgrosmontant = from c in bd.contrats
                               join cl in bd.clients on c.numClient equals cl.numClient
                               join p in bd.plannings on c.numContrat equals p.numContrat
                               join s in bd.semaines on p.numSemaine equals s.numsemaine
                               join g in bd.gites on p.numGite equals g.numGite
                               join t in bd.tarifs on g.numGite equals t.numGite
                               where t.numPeriode == s.numPeriode && c.valide == 1
                               group t by new
                               {
                                   c.numContrat
                               } into CaContrat
                               let CA = CaContrat.Sum(i => i.prixSemaine)
                               select new
                               {
                                   
                                   CaContrat.Key.numContrat,
                                   CA
                               };
                    foreach (var item in contratsquirepésenteplusgrosmontant)
                    {

                        this.rb_resultat.Text += String.Format("le contrat {0} vaut {1}$ .", item.numContrat,  item.CA);
                    }
                    break;
                case "donner les villes dont le CA réalisé est supérieur à celui de la ville de Sommières":

                    var req = from c in bd.contrats
                              join cl in bd.clients on c.numClient equals cl.numClient
                              join p in bd.plannings on c.numContrat equals p.numContrat
                              join s in bd.semaines on p.numSemaine equals s.numsemaine
                              join g in bd.gites on p.numGite equals g.numGite
                              join t in bd.tarifs on g.numGite equals t.numGite
                              where t.numPeriode == s.numPeriode && c.valide == 1
                              group t by new
                              {
                                  g.numGite,
                                  g.villeGite
                              } into CaContrat
                              let CA = CaContrat.Sum(i => i.prixSemaine)
                              select new
                              {
                                  CaContrat.Key.numGite,
                                  CaContrat.Key.villeGite,
                                  CA
                              };
                    decimal a = 0;
                    foreach (var item in req)
                    {
                        if (item.villeGite.Contains("Gap"))
                        {
                            a = (decimal)item.CA;
                        }
                    }
                    foreach (var item in req)
                    {
                        if (item.CA > a)
                        {
                            this.rb_resultat.Text += String.Format("le gite {0} a rapporté {1}$ pour la ville de {2} \n", item.numGite, item.CA, item.villeGite);
                        }

                    }

                    break;

                default:
                    break;
            }
        }
        private void btn_lancerRequetes_Click(object sender, EventArgs e)
        {
            this.lancerRequete(this.cb_requetes.Text);
        }
    }
}
