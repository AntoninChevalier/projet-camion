namespace projetcamion{
    
    public class Camionnette : Vehicule
    {
        string usage;

        public Camionnette(string usage,string immatriculation, int categorieTarifaire,Chauffeur chauffeur):base(immatriculation, categorieTarifaire, chauffeur){
            this.usage=usage;
        }

        public string Usage{
            get{return this.usage;}
        }
    }



}