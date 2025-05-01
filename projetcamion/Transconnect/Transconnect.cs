using System.Text.RegularExpressions;

namespace projetcamion
{
    public class Transconnect
    {
        DirecteurGeneral directeurGeneral;
        List<Client> clients;
        List<Commande> listeCommandesFuture = new List<Commande>();
        List<Commande> listeCommandesPasse = new List<Commande>();

        Graphe graphe = new Graphe();
        Comparison<Client> comparaisonClient;

        public Transconnect(DirecteurGeneral directeurGeneral, List<Client> clients,Graphe graphe,List<Commande> listeCommandesFuture)
        {
            this.directeurGeneral = directeurGeneral;
            this.clients = clients;
            this.graphe = graphe;
            this.listeCommandesFuture = listeCommandesFuture;
            
        }

        public DirecteurGeneral DirecteurGeneral
        {
            get{return this.directeurGeneral;}
            set{this.directeurGeneral = value;}
        }
        public List<Client> Clients
        {
            get{return this.clients;}
            set{this.clients = value;}
        }
        public List<Commande> ListeCommandesFuture
        {
            get{return this.listeCommandesFuture;}
            set{this.listeCommandesFuture = value;}
        }
        public List<Commande> ListeCommandesPasse
        {
            get{return this.listeCommandesPasse;}
            set{this.listeCommandesPasse = value;}
        }
        public Graphe Graphe
        {
            get{return this.graphe;}
            set{this.graphe = value;}
        }
        public Comparison<Client> ComparaisonClient
        {
            get{return comparaisonClient;}
            set{this.comparaisonClient = value;}
        }

        public void AjouterClient(Client c)
        {
            if(!this.clients.Exists(cc => cc.Nom == c.Nom && cc.Prenom == c.Prenom))
            {
                clients.Add(c);
                Console.WriteLine("\nAjout du client : "+c.ToString());
            }
            else
            {
                Console.WriteLine("\nLe client "+ c.ToString()+" existe déjà");
            }
        }
        public void SupprimerClient(string nomm,string prenomm)
        {
            bool suppression = false;
            foreach(Client c in new List<Client>(this.clients))
            {
                this.clients.RemoveAll(cc => cc.Nom == nomm && cc.Prenom == prenomm);
                Console.WriteLine("\n"+c.ToString()+" a bien été supprimé");
                suppression = true;
            }
            if(!suppression)
            {
                Console.WriteLine("\nLe client existe déjà");
            }
        }
        /*
        public void ModifierMontant(string nomm,string prenomm,double nouveauMontant)
        {
            bool modificationMontant = false;
            foreach(Client c in this.clients)
            {
                if(c.Nom == nomm && c.Prenom == prenomm)
                {
                    c.MontantAchatCumule = nouveauMontant;
                    Console.WriteLine("Le montant a bien été modifié");
                    modificationMontant = true;
                }
            }
            if(!modificationMontant)
            {
                Console.WriteLine("Le client est introuvable");
            }
        }
        */
        public void AfficherClients()
        {
            List<Client> clientsTries = this.clients.ToList();
            clientsTries.Sort(this.comparaisonClient);
            foreach(Client c in clientsTries)
            {
                Console.WriteLine(c.ToString());
            }
        }
        public void AppliquerRemises()
        {
            List<Client> clientsTries = this.clients.ToList();
            Comparison<Client> comparaisonRemise = (a,b) => b.MontantAchatCumule.CompareTo(a.MontantAchatCumule);
            clientsTries.Sort(comparaisonRemise);
            int compteurRemise = 10;
            foreach(Client c in clientsTries)
            {
                if(c.MontantAchatCumule != 0)
                {
                    c.Remise = compteurRemise;
                }
                if (compteurRemise != 0)
                {
                    compteurRemise -= 2;
                }
            }
        }
    }
}