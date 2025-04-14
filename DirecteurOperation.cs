namespace projetcamion
{
    public class DirecteurOperation : Salarie
    {
        List<ChefEquipe> chefs;

        public DirecteurOperation(List<ChefEquipe> chefs,DateTime dateEntree,float salaire,int nss, string nom, string prenom, DateTime naissance, string adresse, string mail, int numero):base(dateEntree, salaire,nss,nom,prenom,naissance,adresse,mail, numero){
            this.chefs=chefs;
        }

        public List<ChefEquipe> Chefs{
            get{return this.chefs;}
        }

        public override void AfficherHierarchie(int indentation = 0)
        {
            base.AfficherHierarchie(indentation);
            foreach(ChefEquipe c in this.chefs)
            {
                c.AfficherHierarchie(indentation+3);
            }
        }


    }

}