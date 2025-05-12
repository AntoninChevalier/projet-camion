using System.IO;

namespace projetcamion
{
    public class LireCsv
    {
        public static (List<string> pointA, List<string> pointB,List<int> distance) LireFichierCsv(string nomFichier)
        {
            string cheminCsv = Path.Combine(AppContext.BaseDirectory, nomFichier); //chemin relatif
            string[] lignesCsv = System.IO.File.ReadAllLines(cheminCsv);

            var pointA = new List<string>();
            var pointB = new List<string>();
            var distance = new List<int>();

            for (int i = 1; i < lignesCsv.Length; i++)
            {
                string[] colonnesCsv = lignesCsv[i].Split(';');

                pointA.Add(colonnesCsv[0]);
                pointB.Add(colonnesCsv[1]);
                int f = Convert.ToInt32(colonnesCsv[2]);
                distance.Add(f);
            }
            return (pointA,pointB, distance);
        }
        

    }



}
