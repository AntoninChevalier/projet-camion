namespace projetcamion{
    
    public class CamionFrigorifique : Vehicule
    {
        int nombreGroupesElec;
        double volumeMax;

        public CamionFrigorifique(int nombreGroupesElec,double volumeMax,string immatriculation, int categorieTarifaire,Chauffeur chauffeur):base(immatriculation, categorieTarifaire, chauffeur){
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