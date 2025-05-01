

namespace projetcamion
{
    public class Livraison
    {
        string pointDepart;
        string pointArrivee;
        

        public Livraison(string pointDepart,string pointArrivee){
            this.pointDepart=pointDepart;
            this.pointArrivee=pointArrivee;
            
        }

        public string PointDepart{
            get{return this.pointDepart;}
            
        }

        public string PointArrivee{
            get{return this.pointArrivee;}
        }

        


        


    }



}