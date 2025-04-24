namespace projetcamion
{
    public class Noeud
    {      
         string ville;
        


        public Noeud (string ville)
        {
            this.ville = ville;
        }

        public string Ville
        {
            get { return this.ville; }
            set { this.ville = value; }
        }
        public override string ToString()
        {
        return Ville;
        }
    }
}