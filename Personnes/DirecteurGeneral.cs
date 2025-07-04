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

        public void AjouterSalarie(Salarie c)
        {
            if (!this.sousDirecteurs.Exists(cc => cc.Nom == c.Nom && cc.Prenom == c.Prenom))
            {
                sousDirecteurs.Add(c);
                Console.WriteLine(c.ToString()+" a bien été ajouté à\n"+this.ToString());
            }
            else
            {
                Console.WriteLine(c.ToString()," a déjà été ajouté");
            }
        }
        public void SupprimerSalarie(string nom,string prenom)
        {
            bool suppression = false;
            foreach(Salarie c in new List<Salarie>(this.sousDirecteurs))
            {
                if(c.Nom == nom & c.Prenom == prenom)
                {
                    this.sousDirecteurs.RemoveAll(cc => cc.Nom == nom && cc.Prenom == prenom);
                    Console.WriteLine(prenom+" "+nom+" a bien été supprimé de\n"+this.ToString());
                    suppression = true;
                }
            }
            if (!suppression)
            {
                Console.WriteLine(prenom+" "+nom+ " n existe pas ici");
            }
        }
    }

}