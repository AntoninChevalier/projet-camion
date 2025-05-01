namespace projetcamion
{
    abstract public class Vehicule
    {
        protected string immatriculation;
       
        protected Chauffeur chauffeur;
        protected int categorieTarifaire;

        public Vehicule(string immatriculation, int categorieTarifaire, Chauffeur chauffeur)
        {
            this.immatriculation = immatriculation;
            this.categorieTarifaire = categorieTarifaire;
            this.chauffeur = chauffeur;
        }
       
        public string Immatriculation
        {
            get { return this.immatriculation; }
            set { this.immatriculation = value; }
        }
        

        public int CategorieTarifaire{
            get{return this.categorieTarifaire;}
        }
        public override string ToString()
        {
            return "Vehicule: "+immatriculation;
        }
    }
}