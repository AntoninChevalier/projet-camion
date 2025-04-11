namespace projetcamion
{
    abstract public class Poste
    {
        protected string titre;

        public Poste(string titre){
            this.titre=titre;
        }

        public string Titre{
            get{return this.titre;}
        }



    }

}