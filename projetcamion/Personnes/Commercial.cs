namespace projetcamion
{
    public class Commercial : Salarie, IAfficherHierarchie
    {
        int nombreContrat;

        public Commercial(int nombreContrat,DateTime dateEntree,float salaire,int nss, string nom, string prenom, DateTime naissance, string adresse, string mail, int numero):base(dateEntree, salaire,nss,nom,prenom,naissance,adresse,mail, numero){
            this.nombreContrat=nombreContrat;
        }

        public int NombreContrat{
            get{return this.nombreContrat;}
        }
    }
}