using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace projetcamion
{
    public class Program
    {
        
        
        static void Main(string[] args)
        {
            
            Interface.Demarrer();

            //TestGrapheRecherche();

            //TestCreationEtModificationHierarchie();
            //TestAffichageEtTriClient();
            

            //Graphe g1 = new Graphe();
            /*Noeud n1 = new Noeud("1");
            Noeud n2 = new Noeud("2");
            Noeud n3 = new Noeud("3");
            Noeud n4 = new Noeud("4");
            Noeud n5 = new Noeud("5");
            Noeud n6 = new Noeud("6");
            Noeud n7 = new Noeud("7");*/

            /*g1.AjouterLien("1","2",2);
            g1.AjouterLien("1","3",2);
            g1.AjouterLien("1","4",2);
            g1.AjouterLien("2","5",2);
            g1.AjouterLien("2","6",2);
            g1.AjouterLien("3","7",2);*/

            //g1.AfficherListeAdjacence();
            

            /*Console.WriteLine("Hello World!");
            Console.WriteLine("This is a test of the projet-camion program.");
            Console.ReadLine();*/

            /*var villes = LireCsv.LireFichierCsv(@"..\..\..\distances_villes_france.csv");
            foreach(string s in villes.pointA){
                Console.WriteLine(s);
            }
            Console.WriteLine(" ");
            foreach(string s in villes.pointB){
                Console.WriteLine(s);
            }
            Console.WriteLine(" ");
            foreach(float f in villes.distance){
                Console.WriteLine(f);
            }
            Console.WriteLine(" ");*/

            /*Livraison livraison = new Livraison("Paris","Lyon");
            Console.WriteLine(livraison.Distance);*/
            
        }

        /*
        static List<Salarie> CreerExempleSalarie()
        {
            List<Salarie> liste = new List<Salarie>();
            Salarie s1 = new Salarie(new DateTime(2024,4,8), poste,float salaire,int nss, string nom, string prenom, DateTime naissance, string adresse, string mail, int numero)
            return liste;
        }
        */

        static DirecteurGeneral CreationHierarchie()
        {
            Commercial c1 = new Commercial(5,new DateTime(2024,4,8),45000,66885544, "Forge", "Guy", new DateTime(1974,5,7), "Rue du moulin Paris", "forge@gmail.com", 0610457814);
            Commercial c2 = new Commercial(7,new DateTime(2023,10,11),52000,66885543, "Fermi", "Carinne", new DateTime(1971,2,3), "Rue de la mode Paris", "fermi@gmail.fr", 0600110011);
            List<Commercial> c = new List<Commercial>{c1,c2};
            DirecteurCommercial dc1 = new DirecteurCommercial(c,new DateTime(2019,5,6),69000,66885542, "Fiesta", "Jeanne", new DateTime(1969,3,1), "Avenue Hoche Paris", "fiesta444@gmail.fr", 0712345678);
            //Directeur dc1 = new Directeur(c,"Directeur Commercial",new DateTime(2019,5,6),69000,66885542, "Fiesta", "Jeanne", new DateTime(1969,3,1), "Avenue Hoche Paris", "fiesta444@gmail.fr", 0712345678);

            Chauffeur ch1 = new Chauffeur(true,new DateTime(2020,4,8),39000,66885541, "Romu", "Jean", new DateTime(1975,5,7), "Rue Carnot Boulogne", "romu@gmail.com", 0610457811);
            Chauffeur ch2 = new Chauffeur(true,new DateTime(2017,5,12),41000,66885540, "Romi", "Jean-Pierre", new DateTime(1971,5,7), "Rue Laplace Bagneux", "romi@gmail.com", 0610457810);
            Chauffeur ch3 = new Chauffeur(true,new DateTime(2021,7,10),40500,66885549, "Roma", "Jean-Yves", new DateTime(1976,5,7), "Rue de la boule Créteil", "roma@gmail.fr", 0610457817);
            Chauffeur ch4 = new Chauffeur(true,new DateTime(2023,4,8),40000,66885545, "Rome", "Jean-Marc", new DateTime(1980,5,7), "Rue civil Antony", "rome@gmail.com", 0610457820);
            Chauffeur ch5 = new Chauffeur(true,new DateTime(2018,11,10),43800,66885555, "Rimou", "Jacques", new DateTime(1983,9,2), "Boulevard de la Reine Versaille", "rimou@gmail.fr", 0610457825);
            List<Chauffeur> ch11 = new List<Chauffeur>{ch1,ch2,ch3};
            List<Chauffeur> ch22 = new List<Chauffeur>{ch4,ch5};
            ChefEquipe ce1 = new ChefEquipe(ch11,new DateTime(2015,4,8),51000,66885577, "Royal", "Benjamin", new DateTime(1968,5,7), "Rue du roi Paris", "royal@gmail.com", 0610457899);
            ChefEquipe ce2 = new ChefEquipe(ch22,new DateTime(2014,4,8),53500,66885566, "Prince", "Jules", new DateTime(1966,5,7), "Boulevard Hoche Levallois-Perret", "prince@gmail.com", 0789564124);
            List<ChefEquipe> ce11 = new List<ChefEquipe>{ce1,ce2};
            DirecteurOperation do1 = new DirecteurOperation(ce11,new DateTime(2013,4,8),72500,66885500, "Fetard", "Alban", new DateTime(1963,5,7), "Boulevard du faubourg Paris", "fetard@gmail.com", 0711111111);
            List<Salarie> p = new List<Salarie>{dc1,do1};
            DirecteurGeneral dg = new DirecteurGeneral(p,new DateTime(2012,4,8),117800,69874521, "Dupond", "Clotaire", new DateTime(1965,5,7), "Ile Saint-Louis Paris", "dupond-pdg@gmail.com", 0700000099);
            return dg;
        }

       
            //Creation d'un graphe et affichage 

        public static void TestGraphe()
        {
              
             (List<string> pointA, List<string> pointB, List<int> distance) = LireCsv.LireFichierCsv("distances_villes_france.csv");

                Graphe graphe = new Graphe();

                for (int i = 0; i < pointA.Count; i++)
                {
                    graphe.AjouterLien(pointA[i], pointB[i], distance[i]);

                }

                graphe.ConstructionMatriceAdjacence();

                graphe.AfficheMatriceAdjacence();

                Console.WriteLine();

                graphe.AfficherListeAdjacence();


                //graphe.Dijkstra("Dax");

                //Console.WriteLine();

                graphe.BellmanFord("Dax");
                
                

                

                graphe.BellmanFordRechercheCamion("Dax","Voiture");

                var visualiseur = new VisualiseurGrapheSkia();
                visualiseur.Visualiser(graphe, "graphe_france.png");

                
                
        }



        public static void TestGrapheRecherche()
        {
              
             (List<string> pointA, List<string> pointB, List<int> distance) = LireCsv.LireFichierCsv("distances_villes_france.csv");

                Graphe graphe = new Graphe();

                for (int i = 0; i < pointA.Count; i++)
                {
                    graphe.AjouterLien(pointA[i], pointB[i], distance[i]);

                }

                graphe.ConstructionMatriceAdjacence();

                //graphe.Dijkstra("Dax");

                //Console.WriteLine();

               // graphe.BellmanFord("Dax");
                Chauffeur ch1 = new Chauffeur(true,new DateTime(2020,4,8),39000,66885541, "Romu", "Jean", new DateTime(1975,5,7), "Rue Carnot Boulogne", "romu@gmail.com", 0610457811);
                Chauffeur ch2 = new Chauffeur(true,new DateTime(2017,5,12),41000,66885540, "Romi", "Jean-Pierre", new DateTime(1971,5,7), "Rue Laplace Bagneux", "romi@gmail.com", 0610457810);
                Chauffeur ch3 = new Chauffeur(true,new DateTime(2021,7,10),40500,66885549, "Roma", "Jean-Yves", new DateTime(1976,5,7), "Rue de la boule Créteil", "roma@gmail.fr", 0610457817);
                Chauffeur ch4 = new Chauffeur(true,new DateTime(2023,4,8),40000,66885545, "Rome", "Jean-Marc", new DateTime(1980,5,7), "Rue civil Antony", "rome@gmail.com", 0610457820);
                Chauffeur ch5 = new Chauffeur(true,new DateTime(2018,11,10),43800,66885555, "Rimou", "Jacques", new DateTime(1983,9,2), "Boulevard de la Reine Versaille", "rimou@gmail.fr", 0610457825);
                Chauffeur ch6 = new Chauffeur(true,new DateTime(2019,10,10),41800,66889955, "Dujardin", "Luc", new DateTime(1983,10,2), "Boulevard de Paris", "dujardin@gmail.fr", 0610457825);
                Chauffeur ch7 = new Chauffeur(true,new DateTime(2022,1,1),43000,66889977, "Latours", "Paul", new DateTime(1978,1,9), "Boulevard de la place", "latours@gmail.com", 0610457825);
                Chauffeur ch8 = new Chauffeur(false, new DateTime(2016, 3, 15), 37500, 66991122, "Lefevre", "Sophie", new DateTime(1985, 6, 20), "Avenue des Lilas Clamart", "sophie.l@orange.fr", 0611223344);
                Chauffeur ch9 = new Chauffeur(true, new DateTime(2024, 9, 22), 42200, 66991133, "Garcia", "Pierre", new DateTime(1972, 11, 5), "Rue du Château Sèvres", "p.garcia@sfr.fr", 0788990011);
                Chauffeur ch10 = new Chauffeur(true, new DateTime(2020, 6, 1), 39800, 66991144, "Martin", "Isabelle", new DateTime(1979, 4, 12), "Place de la Mairie Issy-les-Moulineaux", "i.martin@gmail.com", 0655443322);
                


                Voiture vtest0 = new Voiture(5,"123",10,ch1);
                Voiture vtest1 = new Voiture(5,"12",10,ch2);
                Voiture vtest2 = new Voiture(5,"1234",10,ch3);
                Voiture vtest3 = new Voiture(5,"1235",10,ch4);
                Voiture vtest4 = new Voiture(5,"1236",10,ch5);
                Camionnette vtest5 = new Camionnette("transport","1237",50,ch6);
                Camionnette vtest6 = new Camionnette("transport","1238",50,ch7);
                Camionnette vtest7 = new Camionnette("transport","1239",50,ch8);
                CamionCiterne vtest8 = new CamionCiterne("eau",1000,"12310",100,ch9);
                CamionCiterne vtest9 = new CamionCiterne("lave",1000,"12311",100,ch10);
                
                graphe.Noeuds["Pau"].AjouterVehicule(vtest4);
                graphe.Noeuds["Pau"].AjouterVehicule(vtest0);
                graphe.Noeuds["Toulouse"].AjouterVehicule(vtest1);
                graphe.Noeuds["Paris"].AjouterVehicule(vtest2);
                graphe.Noeuds["Bordeaux"].AjouterVehicule(vtest3);
                
                graphe.Noeuds["Toulouse"].AjouterVehicule(vtest5);
                graphe.Noeuds["Paris"].AjouterVehicule(vtest6);
                graphe.Noeuds["Bordeaux"].AjouterVehicule(vtest7);
                graphe.Noeuds["Pau"].AjouterVehicule(vtest8);
                graphe.Noeuds["Toulouse"].AjouterVehicule(vtest9);

                

                graphe.CommandeGraphe("Dax","Lille","Voiture");

                
                var visualiseur = new VisualiseurGrapheSkia();
                visualiseur.Visualiser(graphe, "graphe_france.png");
                
                
        }

        
        static void TestCreationEtModificationHierarchie()
        {
            DirecteurGeneral dupond = CreationHierarchie();
            Console.WriteLine("\nHiérarchie initiale :");
            dupond.AfficherHierarchie();

            Console.WriteLine("\nTentative ajout impossible :");
            ChefEquipe benjaminRoyal = (ChefEquipe)dupond.RerchercheSalarie("Royal","Benjamin");
            benjaminRoyal.AjouterChauffeur(new Chauffeur(true,new DateTime(2020,4,8),39000,66885541, "Romu", "Jean", new DateTime(1975,5,7), "Rue Carnot Boulogne", "romu@gmail.com", 0610457811));

            Console.WriteLine("\nTentative suppression impossible :");
            dupond.SupprimerSalarie("Hugo","Victor");

            Console.WriteLine("\nNouvelle hiérarchie après ajout d un nouveau chef d équipe :\n");
            DirecteurOperation albanFetard = (DirecteurOperation)dupond.RerchercheSalarie("Fetard","Alban");
            albanFetard.AjouterChefEquipe(new ChefEquipe(new List<Chauffeur>(),new DateTime(2011,5,7),51200,66885564, "Mazarin", "Norbert", new DateTime(1979,6,7), "Passage Choiseul Paris", "mazarin@gmail.com", 0789564199));
            dupond.AfficherHierarchie();

            Console.WriteLine("\nNouvelle hiérarchie après ajout d un nouveau chauffeur:\n");
            ChefEquipe norbertMazarin = (ChefEquipe)dupond.RerchercheSalarie("Mazarin","Norbert");
            norbertMazarin.AjouterChauffeur(new Chauffeur(true,new DateTime(2021,9,10),37000,66885777, "Danton", "Melchior", new DateTime(1964,1,2), "Place de la concorde", "danton@gmail.com", 0610458888));
            dupond.AfficherHierarchie();

            Console.WriteLine("\nNouvelle hiérarchie suppression d un chef d equipe :\n");
            albanFetard.SupprimerChefEquipe("Mazarin","Norbert");
            dupond.AfficherHierarchie();

            Console.WriteLine("\nAffichage Hiérarchie avec les sous directeurs triés par salaire :\n");
            dupond.SousDirecteurs.Sort();
            dupond.AfficherHierarchie();
        }
        // static void TestAffichageEtTriClient()
        // {
        //     Console.WriteLine("\nAffichage des clients :");
        //     DirecteurGeneral dupond = CreationHierarchie();
        //     List<Client> clients = new List<Client>();
        //     clients.Add(new Client(0,0,667,"Durant","Marie",new DateTime(2000,10,10),"Versailles avenue Foch","durant@gmail.fr",0610203040));
        //     clients.Add(new Client(0,0,668,"Pape","Camille",new DateTime(1995,9,9),"Lille avenue Hoche","pape@gmail.fr",0610203041));
        //     Comparison<Client> comparaisonNomCroi = (a,b) => a.Nom.CompareTo(b.Nom);
        //     Transconnect transconnect = new Transconnect(dupond,clients,comparaisonNomCroi);
        //     transconnect.AfficherClients();
        //     transconnect.AjouterClient(new Client(750.4,0,669,"Zaz","Amandine",new DateTime(1992,5,3),"Paris rue mazarine","zaz@yahoo.fr",0610192562));
        //     Console.WriteLine("\nTentative ajout client en double :");
        //     transconnect.AjouterClient(new Client(750.4,0,669,"Zaz","Amandine",new DateTime(1992,5,3),"Paris rue mazarine","zaz@yahoo.fr",0610192562));
        //     transconnect.AjouterClient(new Client(157,0,669,"Abar","Zoé",new DateTime(1994,3,7),"Marseille rue de la porte","zozo@yahoo.com",0610197777));
        //     Console.WriteLine("\nAffichage client par nom alphabétique:");
        //     transconnect.AfficherClients();
        //     Console.WriteLine("\nAffichage client par ville inversement alphabétique:");
        //     transconnect.ComparaisonClient = (a,b) => b.Adresse.CompareTo(a.Adresse);
        //     transconnect.AfficherClients();
        //     Console.WriteLine("\nAffichage client par montant croissant, et par prénom alphabétique en cas de montant égal :");
        //     transconnect.ComparaisonClient = (a,b) => 
        //     {
        //         int retour = a.MontantAchatCumule.CompareTo(b.MontantAchatCumule);
        //         if(retour != 0)
        //         {
        //             return retour;
        //         }
        //         return a.Prenom.CompareTo(b.Prenom);
        //     };
        //     transconnect.AfficherClients();
        //     Console.WriteLine("\nApplication des remises :");
        //     transconnect.AppliquerRemises();
        //     transconnect.AfficherClients();


        // }
    }

}
