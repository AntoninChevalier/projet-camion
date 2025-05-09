using System.Runtime.InteropServices;

namespace projetcamion
{
    public class DirecteurOperation : Salarie, IAfficherHierarchie
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
        public void AjouterChefEquipe(ChefEquipe c)
        {
            if (!this.chefs.Exists(cc => cc.Nom == c.Nom && cc.Prenom == c.Prenom))
            {
                chefs.Add(c);
                Console.WriteLine(c.ToString()+" a bien été ajouté à\n"+this.ToString());
            }
            else
            {
                Console.WriteLine(c.ToString()+" a déjà été ajouté");
            }
        }
        public void SupprimerChefEquipe(string nom,string prenom)
        {
            bool suppression = false;
            foreach(ChefEquipe c in new List<ChefEquipe>(this.chefs))
            {
                if(c.Nom == nom & c.Prenom == prenom)
                {
                    this.chefs.RemoveAll(cc => cc.Nom == nom && cc.Prenom == prenom);
                    Console.WriteLine(prenom+" "+nom+" a bien été supprimé de\n"+this.ToString());
                    suppression = true;
                }
            }
            if (!suppression)
            {
                Console.WriteLine(prenom+" "+nom+" n existe pas ici");
            }
        }
    }

}