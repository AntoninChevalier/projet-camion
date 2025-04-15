namespace projetcamion
{
    public class Graphe
{
    public Dictionary<string, Noeud> Noeuds { get; private set; }
    public List<Lien> Liens { get; private set; }

    // liste d'adjacence
    public Dictionary<Noeud, List<Lien>> ListeAdjacence { get; private set; }

    // matrice d'adjacence
    public int[,] MatriceAdjacence { get; private set; }

    public Graphe()
    {
        Noeuds = new Dictionary<string, Noeud>();
        Liens = new List<Lien>();
        ListeAdjacence = new Dictionary<Noeud, List<Lien>>();
    }

    public void AjouterLien(string ville1, string ville2, int distance)
    {
        if (!Noeuds.ContainsKey(ville1))
        {
            Noeuds[ville1] = new Noeud(ville1);
        }
            
        if (!Noeuds.ContainsKey(ville2))
        {
            Noeuds[ville2] = new Noeud(ville2);
        }
            

        var n1 = Noeuds[ville1];
        var n2 = Noeuds[ville2];

        var lien = new Lien(n1, n2, distance);
        Liens.Add(lien);

        if (!ListeAdjacence.ContainsKey(n1))
        {
            ListeAdjacence[n1] = new List<Lien>();
        }
           
        ListeAdjacence[n1].Add(lien);

    }

    public void AfficherListeAdjacence()
    {
        Console.WriteLine("Liste d'adjacence :");
        foreach (var kvp in ListeAdjacence)
        {
            Console.Write(kvp.Key.ToString() + ": ");
            foreach (var lien in kvp.Value)
            {
                    Console.Write(lien.VilleArr+" ("+lien.Distance +" km) ");
            }
                
            Console.WriteLine();
        }
    }


    public void ConstructionMatriceAdjacence()
    {
        int[,] matAdj = new int[ListeAdjacence.Count, ListeAdjacence.Count];

        int cpt = 0;
        foreach (var kvp in ListeAdjacence)
        {
            int cpt2 = 0;
            foreach (var lien in kvp.Value)
            {
                if (cpt == cpt2)
                {
                    matAdj[cpt, cpt2] = 0;
                    cpt2++;
                }
                matAdj[cpt, cpt2] = lien.Distance;
                cpt2++;
            }
            cpt++;
            
        }
        MatriceAdjacence = matAdj;
    }

    public void AfficheMatriceAdjacence()
    {
        Console.WriteLine("Matrice d'adjacence : ");
        if (MatriceAdjacence != null)
        {
            for(int i = 0; i < MatriceAdjacence.GetLength(0); i++)
            {
                for(int j = 0;j< MatriceAdjacence.GetLength(1); j++)
                {
                    
                    if (i == j)
                    {
                        Console.Write(MatriceAdjacence[i, j] + "00 ");
                    }
                    else
                    {
                        Console.Write(MatriceAdjacence[i, j] + " ");
                    }
                }
                Console.WriteLine();
            }
        }
    }

}
}