namespace projetcamion
{
    abstract public class Vehicule : IComparable<Vehicule>
    {
        protected string immatriculation;
       
        protected Chauffeur chauffeur;
        protected int categorieTarifaire;
        protected int distanceParcourue;

        public Vehicule(string immatriculation, int categorieTarifaire, Chauffeur chauffeur, int distanceParcourue=0)
        {
            this.immatriculation = immatriculation;
            this.categorieTarifaire = categorieTarifaire;
            this.chauffeur = chauffeur;
            this.distanceParcourue = distanceParcourue;
        }
       
        public string Immatriculation
        {
            get { return this.immatriculation; }
            set { this.immatriculation = value; }
        }
        public Chauffeur Chauffeur
        {
            get { return this.chauffeur; }
            set { this.chauffeur = value; }
        }
        public int CategorieTarifaire
        {
            get { return this.categorieTarifaire; }
            set { this.categorieTarifaire = value; }
        }
        public int DistanceParcourue
        {
            get { return this.distanceParcourue; }
            set { this.distanceParcourue = value; }
        }
        
        public int CompareTo(Vehicule v)
        {
            if(v == null)
            {
                return 1;
            }
            return v.DistanceParcourue.CompareTo(this.distanceParcourue);
        }
        
        public override string ToString()
        {
            return "Vehicule: "+immatriculation;
        }
    }
}