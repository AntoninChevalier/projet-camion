namespace projetcamion
{
    public class Commande
    {
        Client client;
        Livraison livraison;

        string typeVehicule;
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
            this.prix=vehicule.CategorieTarifaire;

        }

        public Commande(Client client,Livraison livraison,DateTime date,string typeVehicule){
            this.client=client;
            this.livraison=livraison;
            this.date=date;
            this.typeVehicule=typeVehicule;

        }


        public string TypeVehicule{
            get{return this.typeVehicule;}
            set{this.typeVehicule=value;}
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