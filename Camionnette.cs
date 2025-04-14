namespace projetcamion{
    
    public class Camionnette : Vehicule
    {
        string usage;

        public Camionnette(string usage,string immatriculation, bool vehiculeDisponible, int categorieTarifaire):base(immatriculation, vehiculeDisponible, categorieTarifaire){
            this.usage=usage;
        }

        public string Usage{
            get{return this.usage;}
        }
    }



}