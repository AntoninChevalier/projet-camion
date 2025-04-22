namespace projetcamion
{
    public class Transconnect
    {
        DirecteurGeneral directeurGeneral;
        List<Client> clients;
        List<Commande> commandes = new List<Commande>();
        Comparison<Client> comparaisonClient;

        public Transconnect(DirecteurGeneral d, List<Client> c,Comparison<Client> comp)
        {
            this.directeurGeneral = d;
            this.clients = c;
            this.comparaisonClient = comp;
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
        public void AfficherClients()
        {
            List<Client> clientsTries = this.clients.ToList();
            clientsTries.Sort(this.comparaisonClient);
            foreach(Client c in clientsTries)
            {
                Console.WriteLine(c.ToString());
            }
        }
    }
}