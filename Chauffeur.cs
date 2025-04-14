namespace projetcamion
{
    public class Chauffeur : Salarie
    {
        bool chauffeurDisponible;

        public Chauffeur(bool chauffeurDisponible,string titre,DateTime dateEntree,string poste,float salaire,int nss, string nom, string prenom, DateTime naissance, string adresse, string mail, int numero):base(dateEntree, poste, salaire,nss,nom,prenom,naissance,adresse,mail, numero){
            this.chauffeurDisponible=chauffeurDisponible;
        }

        public bool ChauffeurDisponible{
            get{return this.chauffeurDisponible;}
        }



    }

}