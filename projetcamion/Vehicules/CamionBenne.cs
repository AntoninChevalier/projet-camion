namespace projetcamion{
    
    public class CamionBenne : Vehicule
    {
        int nombreBennes;
        bool grue;
        double volumeParCuve;

        public CamionBenne(int nombreBennes,bool grue,double volumeParCuve,string immatriculation, bool vehiculeDisponible, int categorieTarifaire):base(immatriculation, vehiculeDisponible, categorieTarifaire){
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