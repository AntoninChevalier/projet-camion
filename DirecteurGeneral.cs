namespace projetcamion
{
    public class DirecteurGeneral : Salarie
    {
        List<Salarie> sousDirecteurs;

        public DirecteurGeneral(List<Salarie> sousDirecteurs,DateTime dateEntree,float salaire,int nss, string nom, string prenom, DateTime naissance, string adresse, string mail, int numero):base(dateEntree, salaire,nss,nom,prenom,naissance,adresse,mail, numero){
            this.sousDirecteurs=sousDirecteurs;
        }

        public List<Salarie> SousDirecteurs{
            get{return this.sousDirecteurs;}
        }

        public override void AfficherHierarchie(int indentation = 0)
        {
            base.AfficherHierarchie(indentation);
            foreach(Salarie c in this.sousDirecteurs)
            {
                c.AfficherHierarchie(indentation+3);
            }
        }


    }

}