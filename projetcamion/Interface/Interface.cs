using System.ComponentModel;
using System.Reflection;

namespace projetcamion
{
    public class Interface
    {
        static DirecteurGeneral dg = CreationHierarchie();
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
    }
}