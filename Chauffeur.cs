namespace projetcamion
{
    public class Chauffeur : Poste
    {
        bool chauffeurDisponible;

        public Chauffeur(bool chauffeurDisponible,string titre):base(titre){
            this.chauffeurDisponible=chauffeurDisponible;
        }

        public bool ChauffeurDisponible{
            get{return this.chauffeurDisponible;}
        }



    }

}