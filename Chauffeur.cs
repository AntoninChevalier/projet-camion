namespace projetcamion
{
    public class Chauffeur : Salarie
    {
        bool chauffeurDisponible;

        public Chauffeur(bool chauffeurDisponible,DateTime dateEntree,float salaire,int nss, string nom, string prenom, DateTime naissance, string adresse, string mail, int numero):base(dateEntree, salaire,nss,nom,prenom,naissance,adresse,mail, numero){
            this.chauffeurDisponible=chauffeurDisponible;
        }

        public bool ChauffeurDisponible{
            get{return this.chauffeurDisponible;}
        }


    }

}