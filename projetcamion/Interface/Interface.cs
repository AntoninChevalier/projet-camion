using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Security.Cryptography;

namespace projetcamion
{
    public class Interface
    {
        public static Transconnect transconnect = CreationTransconnect();
        public static DirecteurGeneral dg = CreationHierarchie();
        public static Graphe graphe = CreationGrapheExemple();

        
        static int nss_cpt = 100;

        public static void Demarrer()
        {
            bool continuer = true;
            while (continuer)
            {
                Console.WriteLine("\n--- Menu Principal ---");
                Console.WriteLine("1. Afficher la hiérarchie");
                Console.WriteLine("2. Ajouter un salarié");
                Console.WriteLine("3. Supprimer un salarié");
                Console.WriteLine("4. Rechercher un salarié");
                Console.WriteLine("5. Trier sous-directeurs par salaire");
                Console.WriteLine("0. Quitter");
                Console.Write("Votre choix : ");
                
                string choix = Console.ReadLine();

                switch (choix)
                {
                    case "1":
                        Console.WriteLine();
                        dg.AfficherHierarchie();
                        break;
                    case "2":
                        AjouterSalarie();
                        break;
                    case "3":
                        SupprimerSalarie();
                        break;
                    case "4":
                        RechercherSalarie();
                        break;
                    case "5":
                        TrierSousDirecteurs();
                        break;
                    case "0":
                        continuer = false;
                        break;
                    default:
                        Console.WriteLine("Choix invalide, veuillez réessayer.");
                        break;
                }
            }
        }

        static void AjouterSalarie()
        {
            Console.WriteLine("\n--- Ajout d'un salarié ---");
            Salarie superieur = ChoixSuperieur();
            if(superieur is null)
            {
                return;
            }

            Console.WriteLine("Type de salarié à ajouter :");
            Console.WriteLine("1. Directeur d'Opérations");
            Console.WriteLine("2. Chef d'Équipe");
            Console.WriteLine("3. Chauffeur");
            Console.WriteLine("4. Directeur Commercial");
            Console.WriteLine("5. Commercial");
            Console.Write("Votre choix : ");
            string type = Console.ReadLine();

            Console.Write("Nom : ");
            string nom = Console.ReadLine();
            Console.Write("Prénom : ");
            string prenom = Console.ReadLine();
            Console.Write("Salaire : ");
            float salaire = float.Parse(Console.ReadLine());
            Console.Write("Téléphone : ");
            int telephone = int.Parse(Console.ReadLine());
            Console.Write("Date de naissance (ex : 21/03/2012) : ");
            string date_naissance = Console.ReadLine();
            DateTime naissance = DateTime.Parse(date_naissance);
            Console.Write("Adresse : ");
            string adresse = Console.ReadLine();
            Console.Write("Mail : ");
            string mail = Console.ReadLine();
            Console.WriteLine();

            Salarie nouveau;
            DateTime dateEmbauche = DateTime.Now;
            
            switch (type)
            {
                case "1":
                    List<ChefEquipe> l1 = new List<ChefEquipe>();
                    nouveau = new DirecteurOperation(l1,dateEmbauche, salaire,nss_cpt++, nom, prenom,naissance,adresse,mail,telephone);
                    ((DirecteurGeneral)superieur).AjouterSalarie((DirecteurOperation)nouveau);
                    break;
                case "2":
                    List<Chauffeur> l2 = new List<Chauffeur>();
                    nouveau = new ChefEquipe(l2,dateEmbauche, salaire,nss_cpt++, nom, prenom,naissance,adresse,mail,telephone);
                    ((DirecteurOperation)superieur).AjouterChefEquipe((ChefEquipe)nouveau);
                    break;
                case "3":
                    nouveau = new Chauffeur(true,dateEmbauche, salaire,nss_cpt++, nom, prenom,naissance,adresse,mail,telephone);
                    ((ChefEquipe)superieur).AjouterChauffeur((Chauffeur)nouveau);
                    break;
                case "4":
                    List<Commercial> l4 = new List<Commercial>();
                    nouveau = new DirecteurCommercial(l4,dateEmbauche, salaire,nss_cpt++, nom, prenom,naissance,adresse,mail,telephone);
                    ((DirecteurGeneral)superieur).AjouterSalarie((DirecteurCommercial)nouveau);
                    break;
                case "5":
                    nouveau = new Commercial(0,dateEmbauche, salaire,nss_cpt++, nom, prenom,naissance,adresse,mail,telephone);
                    ((DirecteurCommercial)superieur).AjouterCommercial((Commercial)nouveau);
                    break;
                default:
                    Console.WriteLine("Type invalide.");
                    return;
            }
            Console.WriteLine("Salarié ajouté avec succès !");
        }
        static Salarie ChoixSuperieur()
        {
            Console.Write("Nom du supérieur hiérarchique : ");
            string nomSup = Console.ReadLine();
            Console.Write("Prénom du supérieur : ");
            string prenomSup = Console.ReadLine();

            Salarie superieur = null;
    
            superieur = Interface.dg.RerchercheSalarie(nomSup, prenomSup);
            if (superieur == null)
            {
                Console.WriteLine("Supérieur non trouvé !");
                return null;
            }
            return superieur;
        }
        static void SupprimerSalarie()
        {
            Console.WriteLine("\n--- Suppression d'un salarié ---");
            Salarie superieur = ChoixSuperieur();
            if(superieur is null)
            {
                return;
            }
            
            Console.WriteLine("Type de salarié à supprimer :");
            Console.WriteLine("1. Directeur d'Opérations");
            Console.WriteLine("2. Chef d'Équipe");
            Console.WriteLine("3. Chauffeur");
            Console.WriteLine("4. Directeur Commercial");
            Console.WriteLine("5. Commercial");
            Console.Write("Votre choix : ");
            string type = Console.ReadLine();
            Console.Write("Nom du salarié à supprimer : ");
            string nom = Console.ReadLine();
            Console.Write("Prénom : ");
            string prenom = Console.ReadLine();

            switch (type)
            {
                case "1":
                    Interface.dg.SupprimerSalarie(nom,prenom);
                    break;
                case "2":
                    ((DirecteurOperation)superieur).SupprimerChefEquipe(nom,prenom);
                    break;
                case "3":
                    ((ChefEquipe)superieur).SupprimerChauffeur(nom,prenom);
                    break;
                case "4":
                    Interface.dg.SupprimerSalarie(nom,prenom);
                    break;
                case "5":
                    ((DirecteurCommercial)superieur).SupprimerCommercial(nom,prenom);
                    break;
                default:
                    Console.WriteLine("Type invalide.");
                    return;
            }
        }
        static void RechercherSalarie()
        {
            Console.WriteLine("\n--- Recherche d'un salarié ---");
            Console.Write("Nom : ");
            string nom = Console.ReadLine();
            Console.Write("Prénom : ");
            string prenom = Console.ReadLine();

            Salarie salarie = Interface.dg.RerchercheSalarie(nom, prenom);

            if (salarie != null)
                Console.WriteLine(salarie.Nom+" "+salarie.Prenom+" trouvé");
            else
                Console.WriteLine("Salarié non trouvé.");
        }
        static void TrierSousDirecteurs()
        {
            Console.WriteLine("\n--- Tri des sous-directeurs ---");
            Interface.dg.SousDirecteurs.Sort();
            Console.WriteLine("Sous-directeurs triés par salaire !");
        }
        public static bool estCorretMDP(string mdp)
        {
            string vraiHash = "591843df2c4cfefdb70e85ae547ecfc13e8288581d2d7037b82eb3af8abca2f0";
            string mdpHashé = HashPassword(mdp);
            return mdpHashé == vraiHash;
        }
        static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
        public static DirecteurGeneral CreationHierarchie()
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

        public static Graphe CreationGrapheExemple()
        {
              
             (List<string> pointA, List<string> pointB, List<int> distance) = LireCsv.LireFichierCsv("distances_villes_france.csv");

                Graphe graphe = new Graphe();

                for (int i = 0; i < pointA.Count; i++)
                {
                    graphe.AjouterLien(pointA[i], pointB[i], distance[i]);

                }

                graphe.ConstructionMatriceAdjacence();

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
                


                Voiture vtest1 = new Voiture(5,"123",10,ch1);
                Voiture vtest2 = new Voiture(5,"12",10,ch2);
                Voiture vtest3 = new Voiture(5,"1234",10,ch3);
                Voiture vtest4 = new Voiture(5,"1235",10,ch4);
                Voiture vtest5 = new Voiture(5,"1236",10,ch5);
                Camionnette vtest6 = new Camionnette("transport","1237",50,ch6);
                Camionnette vtest7 = new Camionnette("transport","1238",50,ch7);
                Camionnette vtest8 = new Camionnette("transport","1239",50,ch8);
                CamionCiterne vtest9 = new CamionCiterne("eau",1000,"12310",100,ch9);
                CamionCiterne vtest10 = new CamionCiterne("lave",1000,"12311",100,ch10);
                
                
                graphe.Noeuds["Toulouse"].AjouterVehicule(vtest1);
                graphe.Noeuds["Paris"].AjouterVehicule(vtest2);
                graphe.Noeuds["Bordeaux"].AjouterVehicule(vtest3);
                graphe.Noeuds["Pau"].AjouterVehicule(vtest4);
                graphe.Noeuds["Lille"].AjouterVehicule(vtest5);
                graphe.Noeuds["Dax"].AjouterVehicule(vtest6);
                graphe.Noeuds["Brest"].AjouterVehicule(vtest7);
                graphe.Noeuds["Troyes"].AjouterVehicule(vtest8);
                graphe.Noeuds["Nantes"].AjouterVehicule(vtest9);
                graphe.Noeuds["Nice"].AjouterVehicule(vtest10);


               return graphe;
                
                
        }

        static void TestAffichageEtTriClient()
        {
            
            DirecteurGeneral dupond = CreationHierarchie();
            List<Client> clients = new List<Client>();
            Graphe graphe = CreationGrapheExemple();
            clients.Add(new Client(0,0,667,"Durant","Marie",new DateTime(2000,10,10),"Versailles avenue Foch","durant@gmail.fr",0610203040));
            clients.Add(new Client(0,0,668,"Pape","Camille",new DateTime(1995,9,9),"Lille avenue Hoche","pape@gmail.fr",0610203041));
            
            List<Commande> listeCommandesFuture = new List<Commande>();
            Transconnect transconnect = new Transconnect(dupond,clients,graphe,listeCommandesFuture);
            transconnect.AfficherClients();
            transconnect.AjouterClient(new Client(750.4,0,669,"Zaz","Amandine",new DateTime(1992,5,3),"Paris rue mazarine","zaz@yahoo.fr",0610192562));
            
            transconnect.AjouterClient(new Client(750.4,0,669,"Zaz","Amandine",new DateTime(1992,5,3),"Paris rue mazarine","zaz@yahoo.fr",0610192562));
            transconnect.AjouterClient(new Client(157,0,669,"Abar","Zoé",new DateTime(1994,3,7),"Marseille rue de la porte","zozo@yahoo.com",0610197777));
          


        }


        public static Transconnect CreationTransconnect()
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
            Chauffeur ch6 = new Chauffeur(true,new DateTime(2019,10,10),41800,66889955, "Dujardin", "Luc", new DateTime(1983,10,2), "Boulevard de Paris", "dujardin@gmail.fr", 0610457825);
            Chauffeur ch7 = new Chauffeur(true,new DateTime(2022,1,1),43000,66889977, "Latours", "Paul", new DateTime(1978,1,9), "Boulevard de la place", "latours@gmail.com", 0610457825);
            Chauffeur ch8 = new Chauffeur(false, new DateTime(2016, 3, 15), 37500, 66991122, "Lefevre", "Sophie", new DateTime(1985, 6, 20), "Avenue des Lilas Clamart", "sophie.l@orange.fr", 0611223344);
            Chauffeur ch9 = new Chauffeur(true, new DateTime(2024, 9, 22), 42200, 66991133, "Garcia", "Pierre", new DateTime(1972, 11, 5), "Rue du Château Sèvres", "p.garcia@sfr.fr", 0788990011);
            Chauffeur ch10 = new Chauffeur(true, new DateTime(2020, 6, 1), 39800, 66991144, "Martin", "Isabelle", new DateTime(1979, 4, 12), "Place de la Mairie Issy-les-Moulineaux", "i.martin@gmail.com", 0655443322);
            Chauffeur ch11 = new Chauffeur(false, new DateTime(2015, 12, 18), 36000, 66991155, "Bernard", "Antoine", new DateTime(1988, 8, 1), "Rue de la Gare Meudon", "a.bernard@yahoo.fr", 0711223344);
            Chauffeur ch12 = new Chauffeur(true, new DateTime(2023, 2, 28), 41500, 66991166, "Dubois", "Catherine", new DateTime(1974, 7, 25), "Boulevard Gambetta Levallois-Perret", "c.dubois@hotmail.com", 0699887766);
            Chauffeur ch13 = new Chauffeur(true, new DateTime(2021, 5, 10), 40200, 66991177, "Thomas", "François", new DateTime(1981, 3, 10), "Avenue Victor Hugo Neuilly-sur-Seine", "f.thomas@gmail.com", 0722334455);
            Chauffeur ch14 = new Chauffeur(false, new DateTime(2017, 9, 5), 38500, 66991188, "Robert", "Nathalie", new DateTime(1986, 9, 18), "Rue de Paris Saint-Cloud", "n.robert@orange.fr", 0633445566);
            Chauffeur ch15 = new Chauffeur(true, new DateTime(2022, 7, 15), 42800, 66991199, "Richard", "Laurent", new DateTime(1973, 12, 3), "Place du Général Leclerc Rueil-Malmaison", "l.richard@sfr.fr", 0744556677);
            Chauffeur ch16 = new Chauffeur(true, new DateTime(2019, 1, 20), 41200, 66992200, "Petit", "Sandrine", new DateTime(1980, 5, 30), "Rue de Versailles Suresnes", "s.petit@gmail.com", 0666778899);
            Chauffeur ch17 = new Chauffeur(false, new DateTime(2024, 4, 1), 37000, 66992211, "Leroy", "Stéphane", new DateTime(1987, 2, 7), "Boulevard de la République Puteaux", "s.leroy@yahoo.fr", 0777889900);
            Chauffeur ch18 = new Chauffeur(true, new DateTime(2018, 6, 5), 40100, 66992222, "Moreau", "Martine", new DateTime(1975, 10, 15), "Avenue Foch Courbevoie", "m.moreau@hotmail.com", 0622334455);
            Chauffeur ch19 = new Chauffeur(false, new DateTime(2023, 11, 28), 39500, 66992233, "Fournier", "Philippe", new DateTime(1982, 1, 22), "Rue de Bezons Colombes", "p.fournier@sfr.fr", 0799001122);
            Chauffeur ch20 = new Chauffeur(true, new DateTime(2021, 3, 12), 41300, 66992244, "Girard", "Sylvie", new DateTime(1979, 7, 8), "Place de la Liberté Bois-Colombes", "s.girard@gmail.com", 0644556677);
            Chauffeur ch21 = new Chauffeur(true, new DateTime(2019, 9, 1), 42500, 66992255, "Dupont", "Alain", new DateTime(1976, 4, 3), "Boulevard de Verdun La Garenne-Colombes", "a.dupont@orange.fr", 0755667788);
            Chauffeur ch22 = new Chauffeur(false, new DateTime(2024, 1, 17), 38800, 66992266, "Lambert", "Valérie", new DateTime(1984, 9, 29), "Rue Voltaire Nanterre", "v.lambert@yahoo.fr", 0677889900);
            Chauffeur ch23 = new Chauffeur(true, new DateTime(2020, 7, 30), 40800, 66992277, "Rousseau", "Didier", new DateTime(1971, 11, 11), "Avenue Joffre Suresnes", "d.rousseau@hotmail.com", 0711223344);
            Chauffeur ch24 = new Chauffeur(true, new DateTime(2022, 4, 5), 43200, 66992288, "Laurent", "Céline", new DateTime(1983, 6, 17), "Place du Marché Puteaux", "c.laurent@sfr.fr", 0633445566);
            Chauffeur ch25 = new Chauffeur(false, new DateTime(2017, 12, 20), 37800, 66992299, "Lefort", "Pascal", new DateTime(1978, 2, 5), "Rue des Chanteraines Gennevilliers", "p.lefort@gmail.com", 0744556677);
            Chauffeur ch26 = new Chauffeur(true, new DateTime(2023, 8, 8), 41800, 66993300, "Blanc", "Sophie", new DateTime(1985, 8, 21), "Boulevard de la Seine Villeneuve-la-Garenne", "s.blanc@orange.fr", 0655667788);
            Chauffeur ch27 = new Chauffeur(true, new DateTime(2019, 5, 15), 42100, 66993311, "Chevalier", "Eric", new DateTime(1973, 12, 9), "Rue de la Mairie Clichy", "e.chevalier@yahoo.fr", 0766778899);



            List<Chauffeur> ch100 = new List<Chauffeur>{ch1,ch2,ch3,ch4,ch5};
            List<Chauffeur> ch200 = new List<Chauffeur>{ch6,ch7,ch8,ch9,ch10};
            List<Chauffeur> ch300 = new List<Chauffeur>{ch11,ch12,ch13,ch14,ch15};
            List<Chauffeur> ch400 = new List<Chauffeur>{ch16,ch17,ch18,ch19,ch20};
            List<Chauffeur> ch500 = new List<Chauffeur>{ch21,ch22,ch23,ch24,ch25,ch26,ch27};
            ChefEquipe ce1 = new ChefEquipe(ch100,new DateTime(2015,4,8),51000,66885577, "Royal", "Benjamin", new DateTime(1968,5,7), "Rue du roi Paris", "royal@gmail.com", 0610457899);
            ChefEquipe ce2 = new ChefEquipe(ch200,new DateTime(2014,4,8),53500,66885566, "Prince", "Jules", new DateTime(1966,9,7), "Boulevard Hoche Levallois-Perret", "prince@gmail.com", 0789564124);
            ChefEquipe ce3 = new ChefEquipe(ch300,new DateTime(2017,4,8),53100,64585566, "Gros", "Louis", new DateTime(1990,3,3), "Boulevard de fer Levallois-Perret", "gros@gamil.com", 0789578124);
            ChefEquipe ce4 = new ChefEquipe(ch400,new DateTime(2018,4,8),53000,64585111, "Petit", "Luc", new DateTime(1970,1,1), "Boulevard de feu Levallois-Perret", "petit@gamil", 0789578133);
            ChefEquipe ce5 = new ChefEquipe(ch500,new DateTime(2019,4,8),53000,64585996, "Léger", "Jean", new DateTime(1990,8,3), "Boulevard de l'eau Levallois-Perret", "leger@gamil", 0789578155);
            
            List<ChefEquipe> ce11 = new List<ChefEquipe>{ce1,ce2};
            List<ChefEquipe> ce22 = new List<ChefEquipe>{ce3,ce4,ce5};
            DirecteurOperation do1 = new DirecteurOperation(ce11,new DateTime(2013,4,8),72500,66885500, "Fetard", "Alban", new DateTime(1963,5,7), "Boulevard du faubourg Paris", "fetard@gmail.com", 0711111111);
            DirecteurOperation do2 = new DirecteurOperation(ce22,new DateTime(2014,4,8),72500,66885511,"Leloup", "Marie", new DateTime(1963,5,7), "Boulevard du faubourg Paris", "leloup@gmail.com", 0711111112);
            List<Salarie> p = new List<Salarie>{dc1,do1,do2};
            DirecteurGeneral dg = new DirecteurGeneral(p,new DateTime(2012,4,8),117800,69874521, "Dupond", "Clotaire", new DateTime(1965,5,7), "Ile Saint-Louis Paris", "dupond-pdg@gmail.com", 0700000099);

            (List<string> pointA, List<string> pointB, List<int> distance) = LireCsv.LireFichierCsv("distances_villes_france.csv");

            Graphe graphe = new Graphe();

            for (int i = 0; i < pointA.Count; i++)
            {
                graphe.AjouterLien(pointA[i], pointB[i], distance[i]);

            }

            

            Voiture vtest1 = new Voiture(5,"33123",10,ch1);
            Voiture vtest2 = new Voiture(5,"12896",10,ch2);
            Voiture vtest3 = new Voiture(5,"15234",10,ch3);
            Voiture vtest4 = new Voiture(5,"91235",10,ch4);
            Voiture vtest5 = new Voiture(5,"41236",10,ch5);
            Camionnette vtest6 = new Camionnette("transport","16237",50,ch6);
            Camionnette vtest7 = new Camionnette("transport","12368",50,ch7);
            Camionnette vtest8 = new Camionnette("transport","61239",50,ch8);
            CamionCiterne vtest9 = new CamionCiterne("eau",1000,"12310",100,ch9);
            CamionCiterne vtest10 = new CamionCiterne("essence",1000,"12311",100,ch10);
            CamionBenne vtest11 = new CamionBenne(1,false,1000,"12312",100,ch11);
            CamionBenne vtest12 = new CamionBenne(2,true,1000,"12313",100,ch12);
            CamionBenne vtest13 = new CamionBenne(1,true,1000,"12314",100,ch13);
            CamionFrigorifique vtest14 = new CamionFrigorifique(2,1000,"12315",100,ch14);
            CamionFrigorifique vtest15 = new CamionFrigorifique(1,1000,"12316",100,ch15);
            CamionFrigorifique vtest16 = new CamionFrigorifique(8,1000,"12317",100,ch16);
            Voiture vtest17 = new Voiture(4, "78945", 15, ch17);
            Voiture vtest18 = new Voiture(5, "23456", 8, ch18);
            Camionnette vtest19 = new Camionnette("livraison", "87654", 60, ch19);
            Camionnette vtest20 = new Camionnette("déménagement", "34567", 45, ch20);
            CamionCiterne vtest21 = new CamionCiterne("lait", 1500, "98765", 120, ch21);
            CamionCiterne vtest22 = new CamionCiterne("produits chimiques", 800, "45678", 90, ch22);
            CamionBenne vtest23 = new CamionBenne(3, false, 1200, "65432", 110, ch23);
            CamionBenne vtest24 = new CamionBenne(1, false, 900, "56789", 95, ch24);
            CamionFrigorifique vtest25 = new CamionFrigorifique(3, 1200, "21098", 105, ch25);
            CamionFrigorifique vtest26 = new CamionFrigorifique(4, 950, "76543", 115, ch26);
            CamionFrigorifique vtest27 = new CamionFrigorifique(2, 1100, "89012", 100, ch27);

            graphe.Noeuds["Toulouse"].AjouterVehicule(vtest1);
            graphe.Noeuds["Paris"].AjouterVehicule(vtest2);
            graphe.Noeuds["Bordeaux"].AjouterVehicule(vtest3);
            graphe.Noeuds["Pau"].AjouterVehicule(vtest4);
            graphe.Noeuds["Dax"].AjouterVehicule(vtest5);
            graphe.Noeuds["Dax"].AjouterVehicule(vtest6);
            graphe.Noeuds["Brest"].AjouterVehicule(vtest7);
            graphe.Noeuds["Troyes"].AjouterVehicule(vtest8);
            graphe.Noeuds["Nantes"].AjouterVehicule(vtest9);
            graphe.Noeuds["Nice"].AjouterVehicule(vtest10);
            graphe.Noeuds["Lyon"].AjouterVehicule(vtest11);
            graphe.Noeuds["Marseille"].AjouterVehicule(vtest12);
            graphe.Noeuds["Strasbourg"].AjouterVehicule(vtest13);
            graphe.Noeuds["Strasbourg"].AjouterVehicule(vtest14);
            graphe.Noeuds["Lyon"].AjouterVehicule(vtest15);
            graphe.Noeuds["Grenoble"].AjouterVehicule(vtest16);
            graphe.Noeuds["Nice"].AjouterVehicule(vtest17);
            graphe.Noeuds["Montpellier"].AjouterVehicule(vtest18);
            graphe.Noeuds["Rennes"].AjouterVehicule(vtest19);
            graphe.Noeuds["Dax"].AjouterVehicule(vtest20);
            graphe.Noeuds["Nantes"].AjouterVehicule(vtest21);
            graphe.Noeuds["Paris"].AjouterVehicule(vtest22);
            graphe.Noeuds["Paris"].AjouterVehicule(vtest23);
            graphe.Noeuds["Marseille"].AjouterVehicule(vtest24);
            graphe.Noeuds["Marseille"].AjouterVehicule(vtest25);
            graphe.Noeuds["Strasbourg"].AjouterVehicule(vtest26);
            graphe.Noeuds["Lyon"].AjouterVehicule(vtest27);

            List<Client> clients = new List<Client>();

        
            Client client1 = new Client(0,0,667,"Durant","Marie",new DateTime(2000,10,10),"Versailles avenue Foch","durant@gmail.fr",0610203040);
            Client client2 =new Client(0,0,668,"Pape","Camille",new DateTime(1995,9,9),"Lille avenue Hoche","pape@gmail.fr",0610203041);
            Client client3 =new Client(750.4,0,649,"Zaz","Amandine",new DateTime(1992,5,3),"Paris rue mazarine","zaz@yahoo.fr",0610192562);
            
            Client client4 =new Client(70.4,0,619,"Moulin","Louis",new DateTime(1952,3,3),"Paris rue rouge","moulin@yahoo.fr",0610442562);
            Client client5 =new Client(157,0,639,"Abar","Zoé",new DateTime(1994,3,7),"Marseille rue de la porte","zozo@yahoo.com",0610197777);
            clients.Add(client1);
            clients.Add(client2);
            clients.Add(client3);
            clients.Add(client4);
            clients.Add(client5);

            

            List<Commande> commandes = new List<Commande>();

            Commande commande1 = new Commande(client1,"Paris", "Lille",new DateTime(2025, 5, 5),"Voiture");
            Commande commande2 = new Commande(client2,"Lille", "Marseille",new DateTime(2025, 5, 5),"Voiture");
            Commande commande3 = new Commande(client3,"Nice", "Lyon",new DateTime(2025, 5, 5),"Camion Frigorifique");
            Commande commande4 = new Commande(client4,"Grenoble", "Strasbourg",new DateTime(2025, 5, 4),"Camion Benne");
            Commande commande5 = new Commande(client5,"Marseille", "Toulouse",new DateTime(2025, 5, 5),"Camionnette");
            Commande commande6 = new Commande(client1,"Paris", "Nice",new DateTime(2025, 5, 12),"Camionnette");
            Commande commande7 = new Commande(client2,"Lille", "Bordeaux",new DateTime(2025, 5, 22),"Camion Citerne");
            Commande commande8 = new Commande(client3,"Toulouse", "Lille",new DateTime(2025, 5, 13),"Camion Frigorifique");
            Commande commande9 = new Commande(client4,"Lille", "Marseille",new DateTime(2025, 5, 14),"Camion Benne");
            Commande commande10 = new Commande(client5,"Marseille", "Toulouse",new DateTime(2025, 5, 15),"Camionnette");

            commandes.Add(commande1);
            commandes.Add(commande2);
            commandes.Add(commande3);
            commandes.Add(commande4);
            commandes.Add(commande5);
            commandes.Add(commande6);
            commandes.Add(commande7);
            commandes.Add(commande8);
            commandes.Add(commande9);
            

            Transconnect transconnect = new Transconnect(dg,clients,graphe,commandes);
            return transconnect;
        }


    }
}