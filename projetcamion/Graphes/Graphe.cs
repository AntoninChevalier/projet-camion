using System.Data.SqlTypes;
using System.Dynamic;
using System.Text.RegularExpressions;

namespace projetcamion
{
public class Graphe
{
    Dictionary<string, Noeud> noeuds;
    List<Lien> liens;

    // liste d'adjacence
    Dictionary<Noeud, List<Lien>> listeAdjacence;

    Dictionary<int, Noeud> listeNoeuds;
    // matrice d'adjacence
    int[,] matriceAdjacence;

    public Graphe()
    {
        this.noeuds = new Dictionary<string, Noeud>();
        this.liens = new List<Lien>();
        this.listeAdjacence = new Dictionary<Noeud, List<Lien>>();
        
        this.matriceAdjacence = new int[0,0];
    }

    public Dictionary<string, Noeud> Noeuds 
    { 
        get{return this.noeuds;}
        set{this.noeuds = value;}
    }
    public List<Lien> Liens
    {
        get{return this.liens;}
        set{this.liens= value;}
    }
    public Dictionary<Noeud, List<Lien>> ListeAdjacence
    {
        get{return this.listeAdjacence;}
        set{this.listeAdjacence = value;}
    }
    public Dictionary<int, Noeud> ListeNoeuds
    {
        get{return this.listeNoeuds;}
        set{this.listeNoeuds =value;}
    }
    public int[,] MatriceAdjacence
    {
        get{return this.matriceAdjacence;}
        set{this.matriceAdjacence = value;}
    }



    public void AjouterLien(string ville1, string ville2, int distance)
    {
        if (!noeuds.ContainsKey(ville1))
        {
            noeuds[ville1] = new Noeud(ville1);
        }
            
        if (!noeuds.ContainsKey(ville2))
        {
            noeuds[ville2] = new Noeud(ville2);
        }
            

        Noeud n1 = noeuds[ville1];
        Noeud n2 = noeuds[ville2];

        Lien lien = new Lien(n1, n2, distance);
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
            foreach (Lien lien in kvp.Value)
            {
                    Console.Write(lien.VilleArr+" ("+lien.Distance +" km) ");
            }
                
            Console.WriteLine();
        }
    }


    public void ConstruireListeNoeuds()
    {
        ListeNoeuds = new Dictionary<int, Noeud>();
        int index = 0;
        foreach (Noeud noeud in noeuds.Values)
        {
            ListeNoeuds[index] = noeud;
            index++;
        }
    }

    public void ConstructionMatriceAdjacence()
    {
        ConstruireListeNoeuds();

        int taille = ListeNoeuds.Count;
        int[,] matAdj = new int[taille, taille];

        for (int i = 0; i < taille; i++)
        {
            for (int j = 0; j < taille; j++)
            {
                matAdj[i, j] = int.MaxValue; // initialisation à l'infini (pas de lien)
            }
        }
        for(int i=0;i<taille;i++){
            matAdj[i,i]=0;
        }

        for (int i = 0; i < taille; i++)
        {
            Noeud noeudDepart = ListeNoeuds[i];
            if (ListeAdjacence.ContainsKey(noeudDepart))
            {
                foreach (Lien lien in ListeAdjacence[noeudDepart])
                {
                    // Trouver l'indice du noeud d'arrivée
                    int indexArrivee = -1;
                    foreach (var kvp in ListeNoeuds)
                    {
                        if (kvp.Value == lien.VilleArr)
                        {
                            indexArrivee = kvp.Key;
                            break;
                        }
                    }

                    if (indexArrivee != -1)
                    {
                        matAdj[i, indexArrivee] = lien.Distance;
                    }
                }
            }
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
                    
                    if (MatriceAdjacence[i, j] == int.MaxValue)
                    
                    {
                        Console.Write("inf  "); 
                        
                    }
                    else
                    {
                        if(MatriceAdjacence[i, j] < 100)
                        {
                            Console.Write(MatriceAdjacence[i, j] + "0 ");
                        }
                        else
                        {
                        Console.Write(MatriceAdjacence[i, j] + " ");
                        }
                    }
                }
                Console.WriteLine();
            }
        }
    }


    public string ParcoursEnLargeur(string villeDepart)
    {
        if (!noeuds.ContainsKey(villeDepart))
        {
            return "";
        }

        Noeud origine = noeuds[villeDepart];
        Queue<Noeud> queue = new Queue<Noeud>();
        HashSet<Noeud> noeudsVisites = new HashSet<Noeud>();
        List<Noeud> ordreVisite = new List<Noeud>();
        queue.Enqueue(origine);
        noeudsVisites.Add(origine);

        while (queue.Count > 0)
        {
            Noeud noeudActuel = queue.Dequeue();
            ordreVisite.Add(noeudActuel);

            if (ListeAdjacence.ContainsKey(noeudActuel))
            {
                foreach (Lien lien in ListeAdjacence[noeudActuel])
                {
                    Noeud voisin = lien.VilleArr;
                    if (!noeudsVisites.Contains(voisin))
                    {
                        noeudsVisites.Add(voisin);
                        queue.Enqueue(voisin);
                    }
                }
            }
        }
            
        string renvoi = "";
        Console.WriteLine("Parcours en largeur depuis "+villeDepart+": ");
        foreach (Noeud noeud in ordreVisite)
        {
            Console.Write("-->"+noeud.Ville);
                renvoi = renvoi + noeud.Ville + " ";
        }
        Console.WriteLine();
            return renvoi;
    }


    public void ParcoursEnProfondeur(string villeDepart)
    {
        if (!noeuds.ContainsKey(villeDepart))
        {
            return;
        }

        Noeud origine = noeuds[villeDepart];
        Stack<Noeud> stack = new Stack<Noeud>();
        HashSet<Noeud> noeudsVisites = new HashSet<Noeud>();
        List<Noeud> ordreVisite = new List<Noeud>();
        stack.Push(origine);

        while (stack.Count > 0)
        {
            Noeud noeudActuel = stack.Pop();

            if (!noeudsVisites.Contains(noeudActuel))
            {
                noeudsVisites.Add(noeudActuel);
                ordreVisite.Add(noeudActuel);
                if (ListeAdjacence.ContainsKey(noeudActuel))
                {
                    foreach (Lien lien in ListeAdjacence[noeudActuel])
                    {
                        Noeud voisin = lien.VilleArr;
                        if (!noeudsVisites.Contains(voisin))
                        {
                            stack.Push(voisin);
                        }
                    }
                }
            }
        }

        Console.WriteLine("Parcours en profondeur depuis "+villeDepart+": ");
        foreach (Noeud noeud in ordreVisite)
        {
            Console.Write("-->"+noeud.Ville);
        }
        Console.WriteLine();
    }

    public void Connexe()
    {
        bool b=true;
        foreach(Noeud noeud in noeuds.Values){
            int cpt =0;
            foreach(Lien lien in ListeAdjacence[noeud]){
                cpt++;
            }
            if(cpt!=noeuds.Count-1){
                b=false;
            }
        }
        if(b==true){
            Console.WriteLine("Le graphe est connexe");
        }
        else{
            Console.WriteLine("Le graphe n'est pas connexe");
        }
    }

    public void Dijkstra(string villeDepart)
    {
        if (!noeuds.ContainsKey(villeDepart))
        {
            Console.WriteLine("Ville de départ non trouvée !");
            return;
        }

        Noeud source = noeuds[villeDepart];

        // Initialisation des distances : infini sauf pour la source
        Dictionary<Noeud, int> distances = new Dictionary<Noeud, int>();
        Dictionary<Noeud, Noeud> precedents = new Dictionary<Noeud, Noeud>();
        HashSet<Noeud> visites = new HashSet<Noeud>();

        foreach (Noeud noeud in noeuds.Values)
        {
            distances[noeud] = int.MaxValue;
            precedents[noeud] = null;
        }

        distances[source] = 0;

        // Simuler une file de priorité (valeur minimale toujours en premier)
        List<Noeud> queue = new List<Noeud>(noeuds.Values);

        while (queue.Count > 0)
        {
            // Sélectionner le noeud avec la plus petite distance
            queue.Sort((a, b) => distances[a].CompareTo(distances[b]));
            Noeud noeudActuel = queue[0];
            queue.RemoveAt(0);

            visites.Add(noeudActuel);

            if (ListeAdjacence.ContainsKey(noeudActuel))
            {
                foreach (Lien lien in ListeAdjacence[noeudActuel])
                {
                    Noeud voisin = lien.VilleArr;
                    if (!visites.Contains(voisin))
                    {
                        int nouvelleDistance = distances[noeudActuel] + lien.Distance;

                        if (nouvelleDistance < distances[voisin])
                        {
                            distances[voisin] = nouvelleDistance;
                            precedents[voisin] = noeudActuel;
                        }
                    }
                }
            }
        }

        // Affichage des résultats
        Console.WriteLine("\nPlus courts chemins depuis "+villeDepart+" :");
        foreach (var kvp in distances)
        {
            Console.Write("Vers "+kvp.Key.Ville+" : "+kvp.Value+" km et chemin: ");
            AfficherChemin(precedents, kvp.Key);
            Console.WriteLine();
        }
    }


    public (Noeud villeVehicule, Vehicule vehiculeUtilise, int distance_ville_vehicule) DijkstraRechercheCamion(string villeDepart, string typeVehicule)
    {
        Noeud villeVehicule = null;
        Vehicule premierVehicule = null;
        int distance_ville_vehicule = 0;

        if (!noeuds.ContainsKey(villeDepart))
        {
            Console.WriteLine("Ville de départ non trouvée !");
            return (villeVehicule, premierVehicule, distance_ville_vehicule);
        }

        Noeud source = noeuds[villeDepart];

        // Initialisation des distances
        Dictionary<Noeud, int> distances = new Dictionary<Noeud, int>();
        Dictionary<Noeud, Noeud> precedents = new Dictionary<Noeud, Noeud>();
        HashSet<Noeud> visites = new HashSet<Noeud>();

        foreach (Noeud noeud in noeuds.Values)
        {
            distances[noeud] = int.MaxValue;
            precedents[noeud] = null;
        }

        distances[source] = 0;
        List<Noeud> queue = new List<Noeud>(noeuds.Values);

        while (queue.Count > 0)
        {
            queue.Sort((a, b) => distances[a].CompareTo(distances[b]));
            Noeud noeudActuel = queue[0];
            queue.RemoveAt(0);

            visites.Add(noeudActuel);

            // Vérifie s’il y a un véhicule du type demandé dans cette ville
            bool contientVehicule = false;

            switch (typeVehicule)
            {
                case "Voiture":
                    contientVehicule = noeudActuel.ListeVehicules.Any(v => v is Voiture);
                    if (contientVehicule)
                        premierVehicule = noeudActuel.ListeVehicules.First(v => v is Voiture);
                    break;
                case "Camion Benne":
                    contientVehicule = noeudActuel.ListeVehicules.Any(v => v is CamionBenne);
                    if (contientVehicule)
                        premierVehicule = noeudActuel.ListeVehicules.First(v => v is CamionBenne);
                    break;
                case "Camion Citerne":
                    contientVehicule = noeudActuel.ListeVehicules.Any(v => v is CamionCiterne);
                    if (contientVehicule)
                        premierVehicule = noeudActuel.ListeVehicules.First(v => v is CamionCiterne);
                    break;
                case "Camion Frigorifique":
                    contientVehicule = noeudActuel.ListeVehicules.Any(v => v is CamionFrigorifique);
                    if (contientVehicule)
                        premierVehicule = noeudActuel.ListeVehicules.First(v => v is CamionFrigorifique);
                    break;
                case "Camionnette":
                    contientVehicule = noeudActuel.ListeVehicules.Any(v => v is Camionnette);
                    if (contientVehicule)
                        premierVehicule = noeudActuel.ListeVehicules.First(v => v is Camionnette);
                    break;
            }

            if (contientVehicule)
            {
                villeVehicule = noeudActuel;
                distance_ville_vehicule = distances[noeudActuel];
                Console.WriteLine("Le "+typeVehicule.ToLower()+" le plus proche de "+villeDepart+" est dans la ville : "+noeudActuel.Ville);
                Console.WriteLine("Le chauffeur fais donc "+distance_ville_vehicule+" km entre "+villeDepart+" et "+noeudActuel.Ville);
                break;
            }

            if (ListeAdjacence.ContainsKey(noeudActuel))
            {
                foreach (Lien lien in ListeAdjacence[noeudActuel])
                {
                    Noeud voisin = lien.VilleArr;
                    if (!visites.Contains(voisin))
                    {
                        int nouvelleDistance = distances[noeudActuel] + lien.Distance;

                        if (nouvelleDistance < distances[voisin])
                        {
                            distances[voisin] = nouvelleDistance;
                            precedents[voisin] = noeudActuel;
                        }
                    }
                }
            }
        }

        return (villeVehicule, premierVehicule, distance_ville_vehicule);
    }


    public (Noeud villeVehicule, Vehicule vehiculeUtilise, int distance_ville_vehicule) DijkstraRechercheCamionDisponible(string villeDepart, string typeVehicule,List<Vehicule> vehiculesIndispo)
    {
        Noeud villeVehicule = null;
        Vehicule premierVehicule = null;
        int distance_ville_vehicule = 0;

        if (!noeuds.ContainsKey(villeDepart))
        {
            Console.WriteLine("Ville de départ non trouvée !");
            return (villeVehicule, premierVehicule, distance_ville_vehicule);
        }

        Noeud source = noeuds[villeDepart];

        // Initialisation des distances
        Dictionary<Noeud, int> distances = new Dictionary<Noeud, int>();
        Dictionary<Noeud, Noeud> precedents = new Dictionary<Noeud, Noeud>();
        HashSet<Noeud> visites = new HashSet<Noeud>();

        foreach (Noeud noeud in noeuds.Values)
        {
            distances[noeud] = int.MaxValue;
            precedents[noeud] = null;
        }

        distances[source] = 0;
        List<Noeud> queue = new List<Noeud>(noeuds.Values);

        while (queue.Count > 0)
        {
            queue.Sort((a, b) => distances[a].CompareTo(distances[b]));
            Noeud noeudActuel = queue[0];
            queue.RemoveAt(0);

            visites.Add(noeudActuel);

            // Vérifie s’il y a un véhicule du type demandé dans cette ville
            bool contientVehicule = false;

            switch (typeVehicule)
            {
                case "Voiture":
                    premierVehicule = noeudActuel.ListeVehicules.Find(v => v is Voiture && !vehiculesIndispo.Contains(v));
                    break;
                case "Camion Benne":
                    premierVehicule = noeudActuel.ListeVehicules.Find(v => v is CamionBenne && !vehiculesIndispo.Contains(v));
                    break;
                case "Camion Citerne":
                    premierVehicule = noeudActuel.ListeVehicules.Find(v => v is CamionCiterne && !vehiculesIndispo.Contains(v));
                    break;
                case "Camion Frigorifique":
                    premierVehicule = noeudActuel.ListeVehicules.Find(v => v is CamionFrigorifique && !vehiculesIndispo.Contains(v));
                    break;
                case "Camionnette":
                    premierVehicule = noeudActuel.ListeVehicules.Find(v => v is Camionnette && !vehiculesIndispo.Contains(v));
                    break;
            }
            contientVehicule = premierVehicule != null;


            if (contientVehicule)
            {
                villeVehicule = noeudActuel;
                distance_ville_vehicule = distances[noeudActuel];
                Console.WriteLine("Le "+typeVehicule.ToLower()+" le plus proche de "+villeDepart+" est dans la ville : "+noeudActuel.Ville);
                Console.WriteLine("Le chauffeur fais donc "+distance_ville_vehicule+" km entre "+villeDepart+" et "+noeudActuel.Ville);
                break;
            }

            if (ListeAdjacence.ContainsKey(noeudActuel))
            {

                foreach (Lien lien in ListeAdjacence[noeudActuel])
                {
                    Noeud voisin = lien.VilleArr;
                    if (!visites.Contains(voisin))
                    {
                        int nouvelleDistance = distances[noeudActuel] + lien.Distance;

                        if (nouvelleDistance < distances[voisin])
                        {
                            distances[voisin] = nouvelleDistance;
                            precedents[voisin] = noeudActuel;
                        }
                    }
                }
            }
        }

        return (villeVehicule, premierVehicule, distance_ville_vehicule);
    }






    public void BellmanFord(string villeDepart)
    {
    if (!noeuds.ContainsKey(villeDepart))
    {
        Console.WriteLine("Ville de départ non trouvée !");
        return;
    }

    Noeud source = noeuds[villeDepart];

    // Initialisation des distances : infini sauf pour la source
    Dictionary<Noeud, int> distances = new Dictionary<Noeud, int>();
    Dictionary<Noeud, Noeud> precedents = new Dictionary<Noeud, Noeud>();

    foreach (Noeud noeud in noeuds.Values)
    {
        distances[noeud] = int.MaxValue;
        precedents[noeud] = null;
    }

    distances[source] = 0;

    // Relaxation des arcs
    int n = noeuds.Count;
    for (int i = 1; i <= n - 1; i++) // On répète le processus n-1 fois
    {
        foreach (Lien lien in Liens)
        {
            Noeud u = lien.VilleDep;
            Noeud v = lien.VilleArr;
            int poids = lien.Distance;

            if (distances[u] != int.MaxValue && distances[u] + poids < distances[v])
            {
                distances[v] = distances[u] + poids;
                precedents[v] = u;
            }
        }
    }

    // Vérification des cycles négatifs
    foreach (Lien lien in Liens)
    {
        Noeud u = lien.VilleDep;
        Noeud v = lien.VilleArr;
        int poids = lien.Distance;

        if (distances[u] != int.MaxValue && distances[u] + poids < distances[v])
        {
            Console.WriteLine("Le graphe contient un cycle négatif.");
            return;
        }
    }

    // Affichage des résultats
    Console.WriteLine("\nPlus courts chemins depuis "+villeDepart+" :");
    foreach (var kvp in distances)
    {
        Console.Write("Vers "+kvp.Key.Ville+" : "+kvp.Value+" km. Chemin : ");
        AfficherChemin(precedents, kvp.Key);
        Console.WriteLine();
    }
    }

    public string floydWarshall(int[,] matriceAdjacence)
    {
        int longueur = matriceAdjacence.GetLength(0);

        for (int k = 0; k < longueur; k++)
        {
            for (int i = 0; i < longueur; i++)
            {
                for (int j = 0; j < longueur; j++)
                {
                    if(matriceAdjacence[i,k] != int.MaxValue && matriceAdjacence[k, j]!= int.MaxValue)
                    {
                        matriceAdjacence[i,j] = Math.Min(matriceAdjacence[i, j], matriceAdjacence[i, k] + matriceAdjacence[k, j]);
                    }
                }
            }
        }
        string renvoi = ""; 
        for(int i=0;i<matriceAdjacence.GetLength(0);i++)
        {
            for(int j=0;j<matriceAdjacence.GetLength(1);j++)
            {
                
                Console.Write(matriceAdjacence[i,j]+ "  ");
                renvoi = renvoi + matriceAdjacence[i, j] + "  ";
            }
            Console.WriteLine();
        }
        return renvoi;
    }

    private void AfficherChemin(Dictionary<Noeud, Noeud> precedents, Noeud destination)
    {
        Stack<Noeud> chemin = new Stack<Noeud>();
        Noeud courant = destination;
        while (courant != null)
        {
            chemin.Push(courant);
            courant = precedents[courant];
        }

        while (chemin.Count > 0)
        {
            Console.Write(chemin.Pop().Ville);
            if (chemin.Count > 0)
            {
                Console.Write(" -> ");
            }
        }
    }


    public (int distance, List<Noeud> chemin) BellmanFordDistance2(string villeDepart, string villeArrivee)
    {
        List<Noeud> cheminListe = new List<Noeud>();

        if (!noeuds.ContainsKey(villeDepart) || !noeuds.ContainsKey(villeArrivee))
        {
            Console.WriteLine("Ville de départ ou d'arrivée non trouvée !");
            return (0, cheminListe);
        }

        Noeud source = noeuds[villeDepart];
        Noeud destination = noeuds[villeArrivee];

        Dictionary<Noeud, int> distances = new Dictionary<Noeud, int>();
        Dictionary<Noeud, Noeud> precedents = new Dictionary<Noeud, Noeud>();

        foreach (Noeud noeud in noeuds.Values)
        {
            distances[noeud] = int.MaxValue;
            precedents[noeud] = null;
        }

        distances[source] = 0;

        int n = noeuds.Count;
        for (int i = 1; i <= n - 1; i++)
        {
            foreach (Lien lien in Liens)
            {
                Noeud u = lien.VilleDep;
                Noeud v = lien.VilleArr;
                int poids = lien.Distance;

                if (distances[u] != int.MaxValue && distances[u] + poids < distances[v])
                {
                    distances[v] = distances[u] + poids;
                    precedents[v] = u;
                }
            }
        }

        foreach (Lien lien in Liens)
        {
            Noeud u = lien.VilleDep;
            Noeud v = lien.VilleArr;
            int poids = lien.Distance;

            if (distances[u] != int.MaxValue && distances[u] + poids < distances[v])
            {
                Console.WriteLine("Le graphe contient un cycle négatif.");
                return (0, cheminListe);
            }
        }

        // Reconstruire le chemin
        Noeud courant = destination;
        while (courant != null)
        {
            cheminListe.Insert(0, courant); // Insère en tête pour garder l ordre
            courant = precedents[courant];
        }

        return (distances[destination], cheminListe);
    }

    public (int distance, List<Noeud> chemin) DijkstraDistance(string villeDepart, string villeArrivee)
    {
        List<Noeud> cheminListe = new List<Noeud>();

        if (!noeuds.ContainsKey(villeDepart) || !noeuds.ContainsKey(villeArrivee))
        {
            Console.WriteLine("Ville de départ ou d'arrivée non trouvée !");
            return (0, cheminListe);
        }

        Noeud source = noeuds[villeDepart];
        Noeud destination = noeuds[villeArrivee];

        Dictionary<Noeud, int> distances = new Dictionary<Noeud, int>();
        Dictionary<Noeud, Noeud> precedents = new Dictionary<Noeud, Noeud>();
        HashSet<Noeud> visites = new HashSet<Noeud>();

        foreach (Noeud noeud in noeuds.Values)
        {
            distances[noeud] = int.MaxValue;
            precedents[noeud] = null;
        }

        distances[source] = 0;

        List<Noeud> queue = new List<Noeud>(noeuds.Values);

        while (queue.Count > 0)
        {
            queue.Sort((a, b) => distances[a].CompareTo(distances[b]));
            Noeud noeudActuel = queue[0];
            queue.RemoveAt(0);

            if (noeudActuel == destination)
                break;

            visites.Add(noeudActuel);

            if (ListeAdjacence.ContainsKey(noeudActuel))
            {
                foreach (Lien lien in ListeAdjacence[noeudActuel])
                {
                    Noeud voisin = lien.VilleArr;
                    if (!visites.Contains(voisin))
                    {
                        int nouvelleDistance = distances[noeudActuel] + lien.Distance;

                        if (nouvelleDistance < distances[voisin])
                        {
                            distances[voisin] = nouvelleDistance;
                            precedents[voisin] = noeudActuel;
                        }
                    }
                }
            }
        }

        // Reconstruire le chemin
        Noeud courant = destination;
        while (courant != null)
        {
            cheminListe.Insert(0, courant);
            courant = precedents[courant];
        }

        Console.WriteLine("\nDistance entre "+villeDepart+" et "+villeArrivee+" est "+distances[destination]+"km");
        Console.WriteLine("Chemin : " + string.Join(" -> ", cheminListe.Select(n => n.Ville)));

        return (distances[destination], cheminListe);
    }



    public (Vehicule v,Noeud villeVehicule,int distanceTotal,List<Noeud> chemin) CommandeGraphe(string villeDepart, string villeArrivee,string typeVehicule)
    {
        (Noeud villeVehicule,Vehicule vehiculeUtilise,int distance_ville_vehicule) =  DijkstraRechercheCamion(villeDepart,typeVehicule);
        Console.WriteLine(villeVehicule.Ville);
        (int distance_livraison,List<Noeud> chemin) = DijkstraDistance(villeDepart,villeArrivee);
        int distanceTotal = distance_ville_vehicule + distance_livraison;
        noeuds[villeArrivee].AjouterVehicule(vehiculeUtilise);
        noeuds[villeVehicule.Ville].DaplacerVehicule(vehiculeUtilise);
        vehiculeUtilise.DistanceParcourue += distanceTotal;
        Console.WriteLine();
        Console.WriteLine("Pour la livraison entre "+villeDepart+" et "+villeArrivee);
        Console.WriteLine("Le chauffeur fais donc "+distanceTotal+" km entre "+villeVehicule.Ville+" et "+villeArrivee+" en passant par "+villeDepart);
        Console.WriteLine("Le véhicule "+vehiculeUtilise.Immatriculation+" est donc dans la ville "+villeArrivee);
        return (vehiculeUtilise,villeVehicule,distanceTotal,chemin);
    }

    public (Vehicule v,Noeud villeVehicule,int distanceTotal,List<Noeud> chemin) CommandeGrapheDisponible(string villeDepart, string villeArrivee,string typeVehicule,List<Vehicule> vehiculesIndispo)
    {
        (Noeud villeVehicule,Vehicule vehiculeUtilise,int distance_ville_vehicule) =  DijkstraRechercheCamionDisponible(villeDepart,typeVehicule,vehiculesIndispo);
        Console.WriteLine(villeVehicule.Ville);
        (int distance_livraison,List<Noeud> chemin) = DijkstraDistance(villeDepart,villeArrivee);
        int distanceTotal = distance_ville_vehicule + distance_livraison;
        noeuds[villeArrivee].AjouterVehicule(vehiculeUtilise);
        noeuds[villeVehicule.Ville].DaplacerVehicule(vehiculeUtilise);
        vehiculeUtilise.DistanceParcourue += distanceTotal;
        Console.WriteLine();

        Console.WriteLine("Pour la livraison entre "+villeDepart+" et "+villeArrivee);
        Console.WriteLine("Le chauffeur fais donc "+distanceTotal+" km entre "+villeVehicule.Ville+" et "+villeArrivee+" en passant par "+villeDepart);
        Console.WriteLine("Le véhicule "+vehiculeUtilise.Immatriculation+" est donc dans la ville "+villeArrivee);
        return (vehiculeUtilise,villeVehicule,distanceTotal,chemin);
    }
    

    
    public Vehicule ContientVehicule(List<Vehicule> ListeVehicules,string typeVehicule)
    {
        bool contientVehicule = false;
            
        if(typeVehicule == "Voiture"){
            contientVehicule = ListeVehicules.Any(vehicule => vehicule is  Voiture);
            if (contientVehicule == true)
            {
                
                Vehicule premierVehicule = ListeVehicules.Find(vehicule => vehicule is Voiture);
                return premierVehicule;
            }
            else
            {
                return null;
            }
        }
        if(typeVehicule == "Camion Benne"){
            contientVehicule = ListeVehicules.Any(vehicule => vehicule is  CamionBenne);
            if (contientVehicule == true)
            {
                
                Vehicule premierVehicule = ListeVehicules.Find(vehicule => vehicule is CamionBenne);
                return premierVehicule;
            }
            else
            {
                return null;
            }
        }
        if(typeVehicule == "Camion Citerne"){
            contientVehicule = ListeVehicules.Any(vehicule => vehicule is  CamionCiterne);
            if (contientVehicule == true)
            {
                
                Vehicule premierVehicule = ListeVehicules.Find(vehicule => vehicule is CamionCiterne);
                return premierVehicule;
            }
            else
            {
                return null;
            }
        }
        if(typeVehicule == "Camion Frigorifique"){
            contientVehicule = ListeVehicules.Any(vehicule => vehicule is  CamionFrigorifique);
            if (contientVehicule == true)
            {
                
                Vehicule premierVehicule = ListeVehicules.Find(vehicule => vehicule is CamionFrigorifique);
                return premierVehicule;
            }
            else
            {
                return null;
            }
        }

        if(typeVehicule == "Camionnette"){
            contientVehicule = ListeVehicules.Any(vehicule => vehicule is  Camionnette);
            if (contientVehicule == true)
            {
                
                Vehicule premierVehicule = ListeVehicules.Find(vehicule => vehicule is Camionnette);
                return premierVehicule;
            }
            else
            {
                return null;
            }
        }
        return null;
    }


}
}