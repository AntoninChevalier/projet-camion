namespace projetcamion{
    
    public class CamionBenne : Vehicule
    {
        int nombreBennes;
        bool grue;
        double volumeParCuve;

        public CamionBenne(int nombreBennes,bool grue,double volumeParCuve,string immatriculation, int categorieTarifaire,Chauffeur chauffeur):base(immatriculation, categorieTarifaire, chauffeur){
            this.nombreBennes=nombreBennes;
            this.grue=grue;
            this.volumeParCuve=volumeParCuve;
        }

        public int NombreBennes{
            get{return this.nombreBennes;}
        }

        public bool Grue{
            get{return this.grue;}
        }
        public double VolumeParCuve
        {
            get{return this.volumeParCuve;}
        }
    }



}