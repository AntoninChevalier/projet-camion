namespace projetcamion{
    
    public class CamionBenne : Vehicule
    {
        int nombreBennes;
        bool grue;

        public CamionBenne(int nombreBennes,bool grue,string immatriculation, bool vehiculeDisponible, int categorieTarifaire):base(immatriculation, vehiculeDisponible, categorieTarifaire){
            this.nombreBennes=nombreBennes;
            this.grue=grue;
        }

        public int NombreBennes{
            get{return this.nombreBennes;}
        }

        public bool Grue{
            get{return this.grue;}
        }
    }



}