namespace projetcamion
{
    public class Commande
    {
        Client client;
        

        string villeDepart;
        string villeArrivee;

        string typeVehicule;
        float prix;
        Vehicule vehicule;
        Chauffeur chauffeur;
        DateTime date;



        public Commande(Client client,string villeDepart,string villeArrivee,Vehicule vehicule,Chauffeur chauffeur,DateTime date){
            this.client=client;
            this.villeDepart=villeDepart;
            this.villeArrivee=villeArrivee;
            this.vehicule=vehicule;
            this.chauffeur=chauffeur;
            this.date=date;
            this.prix=vehicule.CategorieTarifaire;

        }

        public Commande(Client client,string villeDepart,string villeArrivee,DateTime date,string typeVehicule){
            this.client=client;
            this.villeDepart=villeDepart;
            this.villeArrivee=villeArrivee;
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

        public string VilleDepart{
            get{return this.villeDepart;}
        }
        public string VilleArrivee{
            get{return this.villeArrivee;}
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