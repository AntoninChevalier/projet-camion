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

        public string Nom
        {
            get {return this.nom;}
            set {this.nom = value;}
        }
        public string Prenom
        {
            get {return this.prenom;}
            set {this.prenom = value;}
        }
        public string Adresse
        {
            get {return this.adresse;}
            set {this.adresse = value;}
        }
        public string Mail
        {
            get {return this.mail;}
            set {this.mail = value;}
        }
        public int Numero
        {
            get {return this.numero;}
            set {this.numero = value;}
        }
        public int Nss
        {
            get { return this.nss; }
            set { this.nss = value; }
        }
        public DateTime Naissance
        {
            get {return this.naissance;}
            set {this.naissance = value;}
        }
        public override string ToString()
        {
            return nom+" "+prenom+", NSS: "+nss+", Naissance: "+naissance.ToShortDateString()+", Adresse: "+adresse+", Mail: "+mail+", Numero: "+numero;
        }

    }
}