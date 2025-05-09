namespace projetcamion
{
    public class ChefEquipe : Salarie, IAfficherHierarchie, ITriSubordonnesSalaire
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

        public void AjouterChauffeur(Chauffeur c)
        {
            if (!this.chauffeurs.Exists(cc => cc.Nom == c.Nom && cc.Prenom == c.Prenom))
            {
                chauffeurs.Add(c);
                Console.WriteLine(c.ToString()+" a bien été ajouté à\n"+this.ToString());
            }
            else
            {
                Console.WriteLine(c.ToString()+" a déjà été ajouté");
            }
        }
        public void SupprimerChauffeur(string nom,string prenom)
        {
            bool suppression = false;
            foreach(Chauffeur c in new List<Chauffeur>(this.chauffeurs))
            {
                if(c.Nom == nom & c.Prenom == prenom)
                {
                    this.chauffeurs.RemoveAll(cc => cc.Nom == nom && cc.Prenom == prenom);
                    Console.WriteLine(c.ToString()+" a bien été supprimé de\n"+this.ToString());
                    suppression = true;
                }
            }
            if (!suppression)
            {
                Console.WriteLine(prenom+" "+nom+" n existe pas ici");
            }
        }
        public void TriSubordonneSalaire()
        {
            this.chauffeurs.Sort((x,y) => y.Salaire.CompareTo(x.Salaire));
        }

    }

}