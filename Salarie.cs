namespace projetcamion
{
    public class Salarie : Personne
    {
        DateTime dateEntree;
        string poste;
        float salaire;

        public Salarie(DateTime dateEntree,string poste,float salaire,int nss, string nom, string prenom, DateTime naissance, string adresse, string mail, int numero):base(nss,nom,prenom,naissance,adresse,mail, numero){
            this.dateEntree=dateEntree;
            this.poste=poste;
            this.salaire=salaire;
        }

        public string Poste{
            get{return this.poste;}
            set{this.poste=value;}
        }

        public float Salaire{
            get{return this.salaire;}
            set{this.salaire=value;}
        }




    }






}