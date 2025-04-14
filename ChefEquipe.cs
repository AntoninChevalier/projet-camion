namespace projetcamion
{
    public class ChefEquipe : Salarie
    {
        List<Chauffeur> chauffeurs;

        public ChefEquipe(List<Chauffeur> chauffeurs,DateTime dateEntree,float salaire,int nss, string nom, string prenom, DateTime naissance, string adresse, string mail, int numero):base(dateEntree, salaire,nss,nom,prenom,naissance,adresse,mail, numero){
            this.chauffeurs=chauffeurs;
        }

        public List<Chauffeur> Chauffeurs{
            get{return this.chauffeurs;}
        }
        public override void AfficherHierarchie(int indentation = 0)
        {
            base.AfficherHierarchie(indentation);
            foreach(Chauffeur c in this.chauffeurs)
            {
                c.AfficherHierarchie(indentation+3);
            }
        }



    }

}