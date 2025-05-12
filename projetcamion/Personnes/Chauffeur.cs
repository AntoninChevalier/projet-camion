namespace projetcamion
{
    public class Chauffeur : Salarie, IAfficherHierarchie
    {
        bool chauffeurDisponible;

        int nombreLivraisonEffectuee=0;

        public Chauffeur(bool chauffeurDisponible,DateTime dateEntree,float salaire,int nss, string nom, string prenom, DateTime naissance, string adresse, string mail, int numero):base(dateEntree, salaire,nss,nom,prenom,naissance,adresse,mail, numero){
            this.chauffeurDisponible=chauffeurDisponible;
        }

        public bool ChauffeurDisponible{
            get{return this.chauffeurDisponible;}
        }

        public int NombreLivraisonEffectuee{
            get{return this.nombreLivraisonEffectuee;}
            set{this.nombreLivraisonEffectuee=value;}
        }
        /// <summary>
        /// Ajout d’une prime salariale à chaque commande effectuée par un chauffeur
        /// </summary>
        /// <param name="prime">Le montant de la prime salariale</param>
        public void AjoutPrime(float prime)
        {
            this.Salaire = this.Salaire + prime;
        }


    }

}