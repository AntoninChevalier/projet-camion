namespace projetcamion{
    
    public class CamionCiterne : Vehicule
    {
        string contenu;
        double volumeMax;

        public CamionCiterne(string contenu,double volumeMax,string immatriculation, int categorieTarifaire,Chauffeur chauffeur):base(immatriculation,  categorieTarifaire, chauffeur){
            this.contenu=contenu;
            this.volumeMax = volumeMax;
        }

        public string Contenu{
            get{return this.contenu;}
        }
        public double VolumeMax{
            get{return this.volumeMax;}
        }
    }



}