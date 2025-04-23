namespace projetcamion
{
    public class Client : Personne
    {
        double montantAchatCumule;
        double remise;

        public Client(double montantAchatCumule,double remise,int nss, string nom, string prenom, DateTime naissance, string adresse, string mail, int numero) : base(nss, nom, prenom, naissance, adresse, mail, numero)
        {
            this.montantAchatCumule = montantAchatCumule;
            this.remise = remise;
        }
        public double MontantAchatCumule
        {
            get{return this.montantAchatCumule;}
            set{this.montantAchatCumule=value;}
        }
        public double Remise
        {
            get{return this.remise;}
            set{this.remise = value;}
        }
        public override string ToString()
        {
            return "Client: "+nom+" "+prenom+",montant accumulé : "+montantAchatCumule+", remise : "+remise+" NSS: "+nss+", Naissance: "+naissance.ToShortDateString()+", Adresse: "+adresse+", Mail: "+mail+", Numero: "+numero;
        }

    }


    }