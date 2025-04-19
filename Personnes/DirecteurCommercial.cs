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

        public void AjouterCommercial(Commercial c)
        {
            if (!this.commerciaux.Contains(c))
            {
                commerciaux.Add(c);
                Console.WriteLine(c.ToString()+" a bien été ajouté à "+this.ToString());
            }
            else
            {
                Console.WriteLine("Ce salarié a déjà été ajouté");
            }
        }
        public void SupprimerCommercial(string nom,string prenom)
        {
            bool suppression = false;
            foreach(Commercial c in this.commerciaux)
            {
                if(c.Nom == nom & c.Prenom == prenom)
                {
                    this.commerciaux.RemoveAll(cc => cc.Nom == nom && cc.Prenom == prenom);
                    Console.WriteLine(c.ToString()+" a bien été supprimé de "+this.ToString());
                    suppression = true;
                }
            }
            if (!suppression)
            {
                Console.WriteLine("Ce salarié n existe pas ici");
            }
        }
    }

}