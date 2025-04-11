namespace projetcamion
{
    abstract public class Vehicule
    {
        protected string immatriculation;
        protected bool disponible; 

        public Vehicule(string immatriculation, bool disponible)
        {
            this.immatriculation = immatriculation;
            this.disponible = disponible;
        }
        public string Immatriculation
        {
            get { return this.immatriculation; }
            set { this.immatriculation = value; }
        }
        public bool Disponible
        {
            get { return this.disponible; }
            set { this.disponible = value; }
        }
        public override string ToString()
        {
            return $"Vehicule: {immatriculation}, Disponible: {disponible}";
        }
    }
    }