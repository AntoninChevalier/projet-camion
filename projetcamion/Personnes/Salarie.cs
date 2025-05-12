namespace projetcamion
{
    public class Salarie : Personne,IComparable<Salarie>, IAfficherHierarchie
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
        public DateTime DateEntree
        {
            get { return this.dateEntree; }
            set { this.dateEntree = value; }
        }


        public virtual void AfficherHierarchie(int indentation = 0)
        {
            Console.WriteLine(new string(' ',indentation)+"-"+this.Nom+" "+this.Prenom+" ("+this.GetType().Name+")");
        }
        /// <summary>
        /// Recherche un salarié parmi l’ensemble des subordonnés et des subordonnés des subordonnés.
        /// </summary>
        /// <param name="nomm">Nom du salarié à rechercher.</param>
        /// <param name="prenomm">Prénom du salarié à rechercher.</param>
        /// <returns>Le salarié recherché (ou null si introuvable).</returns>
        public Salarie RerchercheSalarie(string nomm,string prenomm)
        {
            if (this.Nom == nomm && this.Prenom == prenomm)
            {
                return this;
            }
            switch(this)
            {
                case DirecteurGeneral dg:
                foreach (Salarie s in dg.SousDirecteurs)
                {
                    Salarie retour = s.RerchercheSalarie(nomm, prenomm);
                    if (retour != null) return retour;
                }
                break;

            case DirecteurOperation dop:
                foreach (ChefEquipe ce in dop.Chefs)
                {
                    Salarie retour = ce.RerchercheSalarie(nomm, prenomm);
                    if (retour != null) return retour;
                }
                break;

            case DirecteurCommercial dcom:
                foreach (Commercial com in dcom.Commerciaux)
                {
                    if (com.Nom == nomm && com.Prenom == prenomm)
                        return com;
                }
                break;

            case ChefEquipe ce:
                foreach (Chauffeur ch in ce.Chauffeurs)
                {
                    if (ch.Nom == nomm && ch.Prenom == prenomm)
                        return ch;
                }
                break;
            }
        return null;
        }
        /// <summary>
        /// Tri les salariés par salaire décroissant (par défaut)
        /// </summary>
        /// <param name="s">Salarié à comparer.</param>
        public int CompareTo(Salarie s)
        {
            int retour = 0;
            if (this.salaire > s.Salaire)
            {
                retour = -1;
            }
            else if (this.Salaire < s.Salaire)
            {
                retour = 1;
            }
            return retour;
        }
        public override string ToString()
        {
            return nom + " " + prenom + ", NSS: " + nss + ", Naissance: " + naissance.ToShortDateString() + ", Adresse: " + adresse + ", Mail: " + mail + ", Numero: " + numero+", Salaire:"+salaire+", date entr�e: "+Convert.ToString(dateEntree);
        }

    }
}