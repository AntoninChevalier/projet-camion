namespace projetcamion{
    
    public class Voiture : Vehicule
    {
        int nombrePlaces;

        public Voiture(int nombrePlaces,string immatriculation, int categorieTarifaire,Chauffeur chauffeur):base(immatriculation, categorieTarifaire, chauffeur){
            this.nombrePlaces=nombrePlaces;
        }

        public int NombrePlaces{
            get{return this.nombrePlaces;}
        }

        public override string ToString()
        {
            return base.ToString()+", Nombre de places : "+NombrePlaces;
        }
    }



}