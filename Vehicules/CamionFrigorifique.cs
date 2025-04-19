namespace projetcamion{
    
    public class CamionFrigorifique : Vehicule
    {
        int nombreGroupesElec;

        public CamionFrigorifique(int nombreGroupesElec,string immatriculation, bool vehiculeDisponible, int categorieTarifaire):base(immatriculation, vehiculeDisponible, categorieTarifaire){
            this.nombreGroupesElec=nombreGroupesElec;
        }

        public int NombreGroupesElec{
            get{return this.nombreGroupesElec;}
        }
    }



}