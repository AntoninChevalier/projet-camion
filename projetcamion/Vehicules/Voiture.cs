namespace projetcamion{
    
    public class Voiture : Vehicule
    {
        int nombrePlaces;

        public Voiture(int nombrePlaces,string immatriculation, bool vehiculeDisponible, int categorieTarifaire):base(immatriculation, vehiculeDisponible, categorieTarifaire){
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