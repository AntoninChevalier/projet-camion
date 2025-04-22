namespace projetcamion
{
    public class Client : Personne
    {
        double montantAchatCumule;

        public Client(double montantAchatCumule,int nss, string nom, string prenom, DateTime naissance, string adresse, string mail, int numero) : base(nss, nom, prenom, naissance, adresse, mail, numero)
        {
            this.montantAchatCumule = montantAchatCumule;
        }
        public double MontantAchatCumule
        {
            get{return this.montantAchatCumule;}
            set{this.montantAchatCumule=value;}
        }
        public override string ToString()
        {
            return "Client: "+nom+" "+prenom+",montant accumulé : "+montantAchatCumule+" NSS: "+nss+", Naissance: "+naissance.ToShortDateString()+", Adresse: "+adresse+", Mail: "+mail+", Numero: "+numero;
        }

    }


    }