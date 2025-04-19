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

        public void AjouterChauffeur(Chauffeur c)
        {
            if (!this.chauffeurs.Contains(c))
            {
                chauffeurs.Add(c);
                Console.WriteLine(c.ToString()+" a bien été ajouté à "+this.ToString());
            }
            else
            {
                Console.WriteLine("Ce salarié a déjà été ajouté");
            }
        }
        public void SupprimerChauffeur(string nom,string prenom)
        {
            bool suppression = false;
            foreach(Chauffeur c in this.chauffeurs)
            {
                if(c.Nom == nom & c.Prenom == prenom)
                {
                    this.chauffeurs.RemoveAll(cc => cc.Nom == nom && cc.Prenom == prenom);
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