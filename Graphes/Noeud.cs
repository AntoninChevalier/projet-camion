namespace projetcamion
{
    public class Noeud
    {      
        string ville;
        public List<Vehicule> listeVehicules;


        public Noeud (string ville)
        {
            this.ville = ville;
            listeVehicules = new List<Vehicule>{};
        }

        public string Ville
        {
            get { return this.ville; }
            set { this.ville = value; }
        }

        public List<Vehicule> ListeVehicules
        {
            get{return this.listeVehicules; }
            set{ this.listeVehicules = value;}
        }

        public void AjouterVehicule(Vehicule v1)
        {
            listeVehicules.Add(v1);
        }

        public void DaplacerVehicule(Vehicule v1)
        {
            listeVehicules.Remove(v1);
        }

        public override string ToString()
        {
        return Ville;
        }
    }
}