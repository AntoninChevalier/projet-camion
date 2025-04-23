namespace projetcamion{
    
    public class CamionFrigorifique : Vehicule
    {
        int nombreGroupesElec;
        double volumeMax;

        public CamionFrigorifique(int nombreGroupesElec,double volumeMax,string immatriculation, bool vehiculeDisponible, int categorieTarifaire):base(immatriculation, vehiculeDisponible, categorieTarifaire){
            this.nombreGroupesElec=nombreGroupesElec;
            this.volumeMax = volumeMax;
        }

        public int NombreGroupesElec{
            get{return this.nombreGroupesElec;}
        }
        public double VolumeMax
        {
            get{return this.volumeMax;}
        }
    }



}