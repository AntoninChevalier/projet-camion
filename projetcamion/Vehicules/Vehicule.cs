namespace projetcamion
{
    abstract public class Vehicule
    {
        protected string immatriculation;
        protected bool vehiculeDisponible; 

        protected int categorieTarifaire;

        public Vehicule(string immatriculation, bool vehiculeDisponible, int categorieTarifaire)
        {
            this.immatriculation = immatriculation;
            this.vehiculeDisponible = vehiculeDisponible;
            this.categorieTarifaire=categorieTarifaire;
        }
        public string Immatriculation
        {
            get { return this.immatriculation; }
            set { this.immatriculation = value; }
        }
        public bool VehiculeDisponible
        {
            get { return this.vehiculeDisponible; }
            set { this.vehiculeDisponible = value; }
        }

        public int CategorieTarifaire{
            get{return this.categorieTarifaire;}
        }
        public override string ToString()
        {
            return "Vehicule: "+immatriculation+", Disponible: "+vehiculeDisponible;
        }
    }
}