namespace projetcamion
{
    public class DirecteurCommercial : Salarie
    {
        List<Commercial> commerciaux;

        public DirecteurCommercial(List<Commercial> commerciaux,DateTime dateEntree,float salaire,int nss, string nom, string prenom, DateTime naissance, string adresse, string mail, int numero):base(dateEntree, salaire,nss,nom,prenom,naissance,adresse,mail, numero){
            this.commerciaux=commerciaux;
        }

        public List<Commercial> Commerciaux{
            get{return this.commerciaux;}
        }
        public override void AfficherHierarchie(int indentation = 0)
        {
            base.AfficherHierarchie(indentation);
            foreach(Commercial c in this.commerciaux)
            {
                c.AfficherHierarchie(indentation+3);
            }
        }


    }

}