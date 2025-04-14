

namespace projetcamion
{
    public class Livraison
    {
        string pointDepart;
        string pointArrivee;
        int distance;

        public Livraison(string pointDepart,string pointArrivee){
            this.pointDepart=pointDepart;
            this.pointArrivee=pointArrivee;
            string cheminCsv = Path.Combine(AppContext.BaseDirectory, "distances_villes_france.csv");
            var villes = LireCsv.LireFichierCsv(cheminCsv);
            int distanceTemp=0;
            for(int i=0;i<villes.pointA.Count;i++){
                if(villes.pointA[i]==pointDepart && villes.pointB[i]==pointArrivee){
                    distanceTemp=villes.distance[i];
                }
            }
            this.distance=distanceTemp;
        }

        public string PointDepart{
            get{return this.pointDepart;}
        }

        public string PointArrivee{
            get{return this.pointArrivee;}
        }

        public int Distance{
            get{return this.distance;}
        }


        


    }



}