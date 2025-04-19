namespace projetcamion
{
    public class Salarie : Personne
    {
        DateTime dateEntree;
        float salaire;

        public Salarie(DateTime dateEntree,float salaire,int nss, string nom, string prenom, DateTime naissance, string adresse, string mail, int numero):base(nss,nom,prenom,naissance,adresse,mail, numero){
            this.dateEntree=dateEntree;
            this.salaire=salaire;
        }


        public float Salaire{
            get{return this.salaire;}
            set{this.salaire=value;}
        }

        public virtual void AfficherHierarchie(int indentation = 0)
        {
            Console.WriteLine(new string(' ',indentation)+"-"+this.Nom+" ("+this.GetType().Name+")");
        }

        
    }






}