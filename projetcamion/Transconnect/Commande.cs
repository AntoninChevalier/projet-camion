using System.Formats.Asn1;

namespace projetcamion
{
    public class Commande
    {
        Client client;
        

        string villeDepart;
        string villeArrivee;

        string typeVehicule;
        double prix;
        Vehicule vehicule;
        Chauffeur chauffeur;
        DateTime date;



        public Commande(Client client,string villeDepart,string villeArrivee,Vehicule vehicule,Chauffeur chauffeur,DateTime date,int distance,string typeVehicule){
            this.typeVehicule=typeVehicule;
            this.client=client;
            this.villeDepart=villeDepart;
            this.villeArrivee=villeArrivee;
            this.vehicule=vehicule;
            this.chauffeur=chauffeur;
            this.date=date;
            CalculerPrixCommande(distance);


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
            set{this.client=value;}
        }

        public string VilleDepart{
            get{return this.villeDepart;}
            set{this.villeDepart=value;}
        }
        public string VilleArrivee{
            get{return this.villeArrivee;}
            set{this.villeArrivee=value;}
        }

        public Vehicule Vehicule{
            get{return this.vehicule;}
            set{this.vehicule=value;}
        }

        public Chauffeur Chauffeur{
            get{return this.chauffeur;}
            set{this.chauffeur=value;}
        }

        public DateTime Date{
            get{return this.date;}
            set{this.date=value;}
        }

        public double Prix{
            get{return this.prix;}
            set{this.prix=value;}
        }

        public void CalculerPrixCommande(int distance)
        {
            double coefficient = 1;
            if(this.typeVehicule == "Voiture") {coefficient = 0.8;}
            else if(this.typeVehicule == "Camion Frigorifique"){coefficient = 3;}
            else if(this.typeVehicule == "Camion Benne"){coefficient = 4;}
            else if(this.typeVehicule == "Camionnette"){coefficient = 1.5;}
            else if(this.typeVehicule == "Camion citerne"){coefficient = 5;}
            this.prix = (1-this.client.Remise*0.01) * (distance*coefficient);
        }

    }



}