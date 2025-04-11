namespace projetcamion
{
    abstract public class Personne
    {
        protected int nss;
        protected string nom;
        protected string prenom;
        protected DateTime naissance;
        protected string adresse;
        protected string mail;
        protected int numero;

        public Personne(int nss, string nom, string prenom, DateTime naissance, string adresse, string mail, int numero)
        {
            this.nss = nss;
            this.nom = nom;
            this.prenom = prenom;
            this.naissance = naissance;
            this.adresse = adresse;
            this.mail = mail;
            this.numero = numero;
        }

    }
}