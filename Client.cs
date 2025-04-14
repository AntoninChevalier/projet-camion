namespace projetcamion
{
    public class Client : Personne
    {

        public Client(int nss, string nom, string prenom, DateTime naissance, string adresse, string mail, int numero) : base(nss, nom, prenom, naissance, adresse, mail, numero)
        {
            
        }
        public override string ToString()
        {
            return "Client: "+nom+" "+prenom+", NSS: "+nss+", Naissance: "+naissance.ToShortDateString()+", Adresse: "+adresse+", Mail: "+mail+", Numero: "+numero;
        }

    }


    }