namespace projetcamion
{
    public class Commande
    {
        Client client;
        Livraison livraison;
        float prix;
        Vehicule vehicule;
        Chauffeur chauffeur;
        DateTime date;

        public Commande(Client client,Livraison livraison,Vehicule vehicule,Chauffeur chauffeur,DateTime date){
            this.client=client;
            this.livraison=livraison;
            this.vehicule=vehicule;
            this.chauffeur=chauffeur;
            this.date=date;
            this.prix=vehicule.CategorieTarifaire*livraison.Distance;

        }

        public Client Client{
            get{return this.client;}
        }

        public Livraison Livraison{
            get{return this.livraison;}
        }

        public Vehicule Vehicule{
            get{return this.vehicule;}
        }

        public Chauffeur Chauffeur{
            get{return this.chauffeur;}
        }

        public DateTime Date{
            get{return this.date;}
        }

        public float Prix{
            get{return this.prix;}
        }


    }



}