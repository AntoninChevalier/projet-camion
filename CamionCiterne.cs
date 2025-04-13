namespace projetcamion{
    
    public class CamionCiterne : Vehicule
    {
        string contenu;

        public CamionCiterne(string contenu,string immatriculation, bool vehiculeDisponible, int categorieTarifaire):base(immatriculation, vehiculeDisponible, categorieTarifaire){
            this.contenu=contenu;
        }

        public string Contenu{
            get{return this.contenu;}
        }
    }



}