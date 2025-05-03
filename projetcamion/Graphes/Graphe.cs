using System.Data.SqlTypes;
using System.Text.RegularExpressions;

namespace projetcamion
{
public class Graphe
{
    public Dictionary<string, Noeud> Noeuds { get; private set; }
    public List<Lien> Liens { get; private set; }

    // liste d'adjacence
    public Dictionary<Noeud, List<Lien>> ListeAdjacence { get; private set; }

    public Dictionary<int, Noeud> ListeNoeuds { get; private set; }
    // matrice d'adjacence
    public int[,] MatriceAdjacence { get; private set; }

    public Graphe()
    {
        Noeuds = new Dictionary<string, Noeud>();
        Liens = new List<Lien>();
        ListeAdjacence = new Dictionary<Noeud, List<Lien>>();
        
        MatriceAdjacence = new int[0,0];
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


    public void ConstruireListeNoeuds()
    {
    ListeNoeuds = new Dictionary<int, Noeud>();
    int index = 0;
    foreach (var noeud in Noeuds.Values)
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
            var noeudDepart = ListeNoeuds[i];
            if (ListeAdjacence.ContainsKey(noeudDepart))
            {
                foreach (var lien in ListeAdjacence[noeudDepart])
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


    public void ParcoursEnLargeur(string villeDepart)
    {
        if (!Noeuds.ContainsKey(villeDepart))
        {
            return;
        }

        var origine = Noeuds[villeDepart];
        var queue = new Queue<Noeud>();
        var noeudsVisites = new HashSet<Noeud>();
        var ordreVisite = new List<Noeud>();
        queue.Enqueue(origine);
        noeudsVisites.Add(origine);

        while (queue.Count > 0)
        {
            var noeudActuel = queue.Dequeue();
            ordreVisite.Add(noeudActuel);

            if (!ListeAdjacence.ContainsKey(noeudActuel))
                continue;

            foreach (var lien in ListeAdjacence[noeudActuel])
            {
                var voisin = lien.VilleArr;
                if (!noeudsVisites.Contains(voisin))
                {
                    noeudsVisites.Add(voisin);
                    queue.Enqueue(voisin);
                }
            }
        }

        Console.WriteLine("Parcours en largeur depuis "+villeDepart+": ");
        foreach (var noeud in ordreVisite)
        {
            Console.Write("-->"+noeud.Ville);
        }
        Console.WriteLine();
    }


    public void ParcoursEnProfondeur(string villeDepart)
    {
        if (!Noeuds.ContainsKey(villeDepart))
        {
            return;
        }

        var origine = Noeuds[villeDepart];
        var stack = new Stack<Noeud>();
        var noeudsVisites = new HashSet<Noeud>();
        var ordreVisite = new List<Noeud>();
        stack.Push(origine);

        while (stack.Count > 0)
        {
            var noeudActuel = stack.Pop();

            if (noeudsVisites.Contains(noeudActuel))
                continue;

            noeudsVisites.Add(noeudActuel);
            ordreVisite.Add(noeudActuel);

            if (!ListeAdjacence.ContainsKey(noeudActuel))
                continue;

            foreach (var lien in ListeAdjacence[noeudActuel])
            {
                var voisin = lien.VilleArr;
                if (!noeudsVisites.Contains(voisin))
                {
                    stack.Push(voisin);
                }
            }
        }

        Console.WriteLine("Parcours en profondeur depuis "+villeDepart+": ");
        foreach (var noeud in ordreVisite)
        {
            Console.Write("-->"+noeud.Ville);
        }
        Console.WriteLine();
    }

    public void Connexe()
    {
        bool b=true;
        foreach(var noeud in Noeuds.Values){
            int cpt =0;
            foreach(var lien in ListeAdjacence[noeud]){
                cpt++;
            }
            if(cpt!=Noeuds.Count-1){
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
    if (!Noeuds.ContainsKey(villeDepart))
    {
    Console.WriteLine("Ville de départ non trouvée !");
    return;
    }

    var source = Noeuds[villeDepart];

    // Initialisation des distances : infini sauf pour la source
    Dictionary<Noeud, int> distances = new Dictionary<Noeud, int>();
    Dictionary<Noeud, Noeud> precedents = new Dictionary<Noeud, Noeud>();
    HashSet<Noeud> visites = new HashSet<Noeud>();

    foreach (var noeud in Noeuds.Values)
    {
    distances[noeud] = int.MaxValue;
    precedents[noeud] = null;
    }

    distances[source] = 0;

    // Simuler une file de priorité (valeur minimale toujours en premier)
    var queue = new List<Noeud>(Noeuds.Values);

    while (queue.Count > 0)
    {
    // Sélectionner le noeud avec la plus petite distance
    queue.Sort((a, b) => distances[a].CompareTo(distances[b]));
    var noeudActuel = queue[0];
    queue.RemoveAt(0);

    visites.Add(noeudActuel);

    if (!ListeAdjacence.ContainsKey(noeudActuel))
        continue;

    foreach (var lien in ListeAdjacence[noeudActuel])
    {
        var voisin = lien.VilleArr;
        if (visites.Contains(voisin))
            continue;

        int nouvelleDistance = distances[noeudActuel] + lien.Distance;

        if (nouvelleDistance < distances[voisin])
        {
            distances[voisin] = nouvelleDistance;
            precedents[voisin] = noeudActuel;
        }
    }
    }

    // Affichage des résultats
    Console.WriteLine($"\nPlus courts chemins depuis {villeDepart} :");
    foreach (var kvp in distances)
    {
    Console.Write($"Vers {kvp.Key.Ville} : {kvp.Value} km - Chemin : ");
    AfficherChemin(precedents, kvp.Key);
    Console.WriteLine();
    }
    }


    public (Noeud villeVehicule, Vehicule vehiculeUtilise, int distance_ville_vehicule) DijkstraRechercheCamion(string villeDepart, string typeVehicule)
    {
        Noeud villeVehicule = null;
        Vehicule premierVehicule = null;
        int distance_ville_vehicule = 0;

        if (!Noeuds.ContainsKey(villeDepart))
        {
            Console.WriteLine("Ville de départ non trouvée !");
            return (villeVehicule, premierVehicule, distance_ville_vehicule);
        }

        var source = Noeuds[villeDepart];

        // Initialisation des distances
        Dictionary<Noeud, int> distances = new Dictionary<Noeud, int>();
        Dictionary<Noeud, Noeud> precedents = new Dictionary<Noeud, Noeud>();
        HashSet<Noeud> visites = new HashSet<Noeud>();

        foreach (var noeud in Noeuds.Values)
        {
            distances[noeud] = int.MaxValue;
            precedents[noeud] = null;
        }

        distances[source] = 0;
        var queue = new List<Noeud>(Noeuds.Values);

        while (queue.Count > 0)
        {
            queue.Sort((a, b) => distances[a].CompareTo(distances[b]));
            var noeudActuel = queue[0];
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
                Console.WriteLine($"Le {typeVehicule.ToLower()} le plus proche de {villeDepart} est dans la ville : {noeudActuel.Ville}");
                Console.WriteLine($"Le chauffeur fais donc {distance_ville_vehicule} km entre {villeDepart} et {noeudActuel.Ville}");
                break;
            }

            if (!ListeAdjacence.ContainsKey(noeudActuel))
                continue;

            foreach (var lien in ListeAdjacence[noeudActuel])
            {
                var voisin = lien.VilleArr;
                if (visites.Contains(voisin))
                    continue;

                int nouvelleDistance = distances[noeudActuel] + lien.Distance;

                if (nouvelleDistance < distances[voisin])
                {
                    distances[voisin] = nouvelleDistance;
                    precedents[voisin] = noeudActuel;
                }
            }
        }

        return (villeVehicule, premierVehicule, distance_ville_vehicule);
    }

    // static void floydWarshall(int[,] matriceAdjacence)
    // {
    //     int longueur = matriceAdjacence.GetLength(0);

    //     for (int k = 0; k < longueur; k++)
    //     {
    //         for (int i = 0; i < longueur; i++)
    //         {
    //             for (int j = 0; j < longueur; j++)
    //             {
    //                 if(matriceAdjacence[i,k] != int.MaxValue && matriceAdjacence[k, j]!= int.MaxValue)
    //                 {
    //                     matriceAdjacence[i,j] = Math.Min(matriceAdjacence[i, j], matriceAdjacence[i, k] + matriceAdjacence[k, j]);
    //                 }
    //             }
    //         }
    //     }

    //     for(int i=0;i<matriceAdjacence.GetLength(0);i++){
    //         for(int j=0;j<matriceAdjacence.GetLength(1);j++){
    //             Console.Write(matriceAdjacence[i,j]);
    //         }
    //         Console.WriteLine();
    //     }
    // }





    public void BellmanFord(string villeDepart)
    {
    if (!Noeuds.ContainsKey(villeDepart))
    {
        Console.WriteLine("Ville de départ non trouvée !");
        return;
    }

    var source = Noeuds[villeDepart];

    // Initialisation des distances : infini sauf pour la source
    Dictionary<Noeud, int> distances = new Dictionary<Noeud, int>();
    Dictionary<Noeud, Noeud> precedents = new Dictionary<Noeud, Noeud>();

    foreach (var noeud in Noeuds.Values)
    {
        distances[noeud] = int.MaxValue;
        precedents[noeud] = null;
    }

    distances[source] = 0;

    // Relaxation des arcs
    int n = Noeuds.Count;
    for (int i = 1; i <= n - 1; i++) // On répète le processus n-1 fois
    {
        foreach (var lien in Liens)
        {
            var u = lien.VilleDep;
            var v = lien.VilleArr;
            int poids = lien.Distance;

            if (distances[u] != int.MaxValue && distances[u] + poids < distances[v])
            {
                distances[v] = distances[u] + poids;
                precedents[v] = u;
            }
        }
    }

    // Vérification des cycles négatifs
    foreach (var lien in Liens)
    {
        var u = lien.VilleDep;
        var v = lien.VilleArr;
        int poids = lien.Distance;

        if (distances[u] != int.MaxValue && distances[u] + poids < distances[v])
        {
            Console.WriteLine("Le graphe contient un cycle négatif.");
            return;
        }
    }

    // Affichage des résultats
    Console.WriteLine($"\nPlus courts chemins depuis {villeDepart} :");
    foreach (var kvp in distances)
    {
        Console.Write($"Vers {kvp.Key.Ville} : {kvp.Value} km - Chemin : ");
        AfficherChemin(precedents, kvp.Key);
        Console.WriteLine();
    }
    }

    public void floydWarshall(int[,] matriceAdjacence)
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

        for(int i=0;i<matriceAdjacence.GetLength(0);i++){
            for(int j=0;j<matriceAdjacence.GetLength(1);j++){
                
                Console.Write(matriceAdjacence[i,j]+ "  ");
                
            }
            Console.WriteLine();
        }
    }

    private void AfficherChemin(Dictionary<Noeud, Noeud> precedents, Noeud destination)
    {
        Stack<Noeud> chemin = new Stack<Noeud>();
        var courant = destination;
        while (courant != null)
        {
            chemin.Push(courant);
            courant = precedents[courant];
        }

        while (chemin.Count > 0)
        {
            Console.Write(chemin.Pop().Ville);
            if (chemin.Count > 0)
            Console.Write(" -> ");
        }
    }


    public (Noeud villeVehicule,Vehicule vehiculeUtilise,int distance_ville_vehicule) BellmanFordRechercheCamion(string villeDepart,string typeVehicule)
    {
        Noeud villeVehicule = null;
        Vehicule premierVehicule = null;
        int distance_ville_vehicule = 0;
        
        if (!Noeuds.ContainsKey(villeDepart))
        {
            Console.WriteLine("Ville de départ non trouvée !");
            return(villeVehicule,premierVehicule,distance_ville_vehicule);
        }

        var source = Noeuds[villeDepart];

        // Initialisation des distances : infini sauf pour la source
        Dictionary<Noeud, int> distances = new Dictionary<Noeud, int>();
        Dictionary<Noeud, Noeud> precedents = new Dictionary<Noeud, Noeud>();

        foreach (var noeud in Noeuds.Values)
        {
            distances[noeud] = int.MaxValue;
            precedents[noeud] = null;
        }

        distances[source] = 0;

        // Relaxation des arcs
        int n = Noeuds.Count;
        for (int i = 1; i <= n - 1; i++) // On répète le processus n-1 fois
        {
            foreach (var lien in Liens)
            {
                var u = lien.VilleDep;
                var v = lien.VilleArr;
                int poids = lien.Distance;

                if (distances[u] != int.MaxValue && distances[u] + poids < distances[v])
                {
                    distances[v] = distances[u] + poids;
                    precedents[v] = u;
                }
            }
        }

        // Vérification des cycles négatifs
        foreach (var lien in Liens)
        {
            var u = lien.VilleDep;
            var v = lien.VilleArr;
            int poids = lien.Distance;

            if (distances[u] != int.MaxValue && distances[u] + poids < distances[v])
            {
                Console.WriteLine("Le graphe contient un cycle négatif.");
                return(villeVehicule,premierVehicule,distance_ville_vehicule);
            }
        }

        // Affichage des résultats
        
        
        var distancesTrie = distances.OrderBy(x => x.Value);

        foreach (var kvp in distancesTrie)
        {
            bool contientVehicule = false;
            
            if(typeVehicule == "Voiture"){
                contientVehicule = kvp.Key.ListeVehicules.Any(vehicule => vehicule is  Voiture);
                if (contientVehicule == true)
                {
                    Console.WriteLine($"La voiture le plus proche de {villeDepart} est dans la ville: {kvp.Key.Ville}");
                    premierVehicule = kvp.Key.ListeVehicules.FirstOrDefault(vehicule => vehicule is Voiture);
                    villeVehicule = kvp.Key;
                    distance_ville_vehicule = kvp.Value;
                    Console.WriteLine($"Le chauffeur fais donc {distance_ville_vehicule} km entre {villeDepart} et {kvp.Key.Ville}");
                }
            }
            if(typeVehicule == "Camion Benne"){
                contientVehicule = kvp.Key.ListeVehicules.Any(vehicule => vehicule is  CamionBenne);
                if (contientVehicule == true)
                {
                    Console.WriteLine($"Le camion benne le plus proche de {villeDepart} est dans la ville: {kvp.Key.Ville}");
                    premierVehicule = kvp.Key.ListeVehicules.FirstOrDefault(vehicule => vehicule is CamionBenne);
                    villeVehicule = kvp.Key;
                    distance_ville_vehicule = kvp.Value;
                    Console.WriteLine($"Le chauffeur fais donc {distance_ville_vehicule} km entre {villeDepart} et {kvp.Key.Ville}");
                }
            }
            if(typeVehicule == "Camion Citerne"){
                contientVehicule = kvp.Key.ListeVehicules.Any(vehicule => vehicule is  CamionCiterne);
                if (contientVehicule == true)
                {
                    Console.WriteLine($"Le camion citerne le plus proche de {villeDepart} est dans la ville: {kvp.Key.Ville}");
                    premierVehicule = kvp.Key.ListeVehicules.FirstOrDefault(vehicule => vehicule is CamionCiterne);
                    villeVehicule = kvp.Key;
                    distance_ville_vehicule = kvp.Value;
                    Console.WriteLine($"Le chauffeur fais donc {distance_ville_vehicule} km entre {villeDepart} et {kvp.Key.Ville}");
                }
            }
            if(typeVehicule == "Camion Frigorifique"){
                contientVehicule = kvp.Key.ListeVehicules.Any(vehicule => vehicule is  CamionFrigorifique);
                if (contientVehicule == true)
                {
                    Console.WriteLine($"Le camion frigorifique le plus proche de {villeDepart} est dans la ville: {kvp.Key.Ville}");
                    premierVehicule = kvp.Key.ListeVehicules.FirstOrDefault(vehicule => vehicule is CamionFrigorifique);
                    villeVehicule = kvp.Key;
                    distance_ville_vehicule = kvp.Value;
                    Console.WriteLine($"Le chauffeur fais donc {distance_ville_vehicule} km entre {villeDepart} et {kvp.Key.Ville}");
                }
            }

            if(typeVehicule == "Camionnette"){
                contientVehicule = kvp.Key.ListeVehicules.Any(vehicule => vehicule is  Camionnette);
                if (contientVehicule == true)
                {
                    Console.WriteLine($"La camionnette le plus proche de {villeDepart} est dans la ville: {kvp.Key.Ville}");
                    premierVehicule = kvp.Key.ListeVehicules.FirstOrDefault(vehicule => vehicule is Camionnette);
                    villeVehicule = kvp.Key;
                    distance_ville_vehicule = kvp.Value;
                    Console.WriteLine($"Le chauffeur fais donc {distance_ville_vehicule} km entre {villeDepart} et {kvp.Key.Ville}");
                }
            }
            

            if(contientVehicule == true)
            {

                
                break;
            }

            
        }
        return(villeVehicule,premierVehicule,distance_ville_vehicule);

    }
    public int  BellmanFordDistance(string villeDepart, string villeArrivee)
    {
        if (!Noeuds.ContainsKey(villeDepart) || !Noeuds.ContainsKey(villeArrivee))
        {
            Console.WriteLine("Ville de départ ou d'arrivée non trouvée !");
            return 0;
        }

        var source = Noeuds[villeDepart];
        var destination = Noeuds[villeArrivee];

        Dictionary<Noeud, int> distances = new Dictionary<Noeud, int>();
        Dictionary<Noeud, Noeud> precedents = new Dictionary<Noeud, Noeud>();

        foreach (var noeud in Noeuds.Values)
        {
            distances[noeud] = int.MaxValue;
            precedents[noeud] = null;
        }

        distances[source] = 0;

        int n = Noeuds.Count;
        for (int i = 1; i <= n - 1; i++)
        {
            foreach (var lien in Liens)
            {
                var u = lien.VilleDep;
                var v = lien.VilleArr;
                int poids = lien.Distance;

                if (distances[u] != int.MaxValue && distances[u] + poids < distances[v])
                {
                    distances[v] = distances[u] + poids;
                    precedents[v] = u;
                }
            }
        }

        foreach (var lien in Liens)
        {
            var u = lien.VilleDep;
            var v = lien.VilleArr;
            int poids = lien.Distance;

            if (distances[u] != int.MaxValue && distances[u] + poids < distances[v])
            {
                Console.WriteLine("Le graphe contient un cycle négatif.");
                return 0;
            }
        }

        Console.WriteLine($"\nDistance entre {villeDepart} et {villeArrivee} : {distances[destination]} km");

        return distances[destination];
    }


    public (int distance, List<Noeud> chemin) BellmanFordDistance2(string villeDepart, string villeArrivee)
    {
        List<Noeud> cheminListe = new List<Noeud>();

        if (!Noeuds.ContainsKey(villeDepart) || !Noeuds.ContainsKey(villeArrivee))
        {
            Console.WriteLine("Ville de départ ou d'arrivée non trouvée !");
            return (0, cheminListe);
        }

        var source = Noeuds[villeDepart];
        var destination = Noeuds[villeArrivee];

        Dictionary<Noeud, int> distances = new Dictionary<Noeud, int>();
        Dictionary<Noeud, Noeud> precedents = new Dictionary<Noeud, Noeud>();

        foreach (var noeud in Noeuds.Values)
        {
            distances[noeud] = int.MaxValue;
            precedents[noeud] = null;
        }

        distances[source] = 0;

        int n = Noeuds.Count;
        for (int i = 1; i <= n - 1; i++)
        {
            foreach (var lien in Liens)
            {
                var u = lien.VilleDep;
                var v = lien.VilleArr;
                int poids = lien.Distance;

                if (distances[u] != int.MaxValue && distances[u] + poids < distances[v])
                {
                    distances[v] = distances[u] + poids;
                    precedents[v] = u;
                }
            }
        }

        foreach (var lien in Liens)
        {
            var u = lien.VilleDep;
            var v = lien.VilleArr;
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
            cheminListe.Insert(0, courant); // Insère en tête pour garder l'ordre
            courant = precedents[courant];
        }

        Console.WriteLine($"\nDistance entre {villeDepart} et {villeArrivee} : {distances[destination]} km");
        Console.WriteLine("Chemin : " + string.Join(" -> ", cheminListe.Select(n => n.Ville)));

        return (distances[destination], cheminListe);
    }

    public (int distance, List<Noeud> chemin) DijkstraDistance(string villeDepart, string villeArrivee)
    {
        List<Noeud> cheminListe = new List<Noeud>();

        if (!Noeuds.ContainsKey(villeDepart) || !Noeuds.ContainsKey(villeArrivee))
        {
            Console.WriteLine("Ville de départ ou d'arrivée non trouvée !");
            return (0, cheminListe);
        }

        var source = Noeuds[villeDepart];
        var destination = Noeuds[villeArrivee];

        Dictionary<Noeud, int> distances = new Dictionary<Noeud, int>();
        Dictionary<Noeud, Noeud> precedents = new Dictionary<Noeud, Noeud>();
        HashSet<Noeud> visites = new HashSet<Noeud>();

        foreach (var noeud in Noeuds.Values)
        {
            distances[noeud] = int.MaxValue;
            precedents[noeud] = null;
        }

        distances[source] = 0;

        var queue = new List<Noeud>(Noeuds.Values);

        while (queue.Count > 0)
        {
            queue.Sort((a, b) => distances[a].CompareTo(distances[b]));
            var noeudActuel = queue[0];
            queue.RemoveAt(0);

            if (noeudActuel == destination)
                break;

            visites.Add(noeudActuel);

            if (!ListeAdjacence.ContainsKey(noeudActuel))
                continue;

            foreach (var lien in ListeAdjacence[noeudActuel])
            {
                var voisin = lien.VilleArr;
                if (visites.Contains(voisin))
                    continue;

                int nouvelleDistance = distances[noeudActuel] + lien.Distance;

                if (nouvelleDistance < distances[voisin])
                {
                    distances[voisin] = nouvelleDistance;
                    precedents[voisin] = noeudActuel;
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

        Console.WriteLine($"\nDistance entre {villeDepart} et {villeArrivee} : {distances[destination]} km");
        Console.WriteLine("Chemin : " + string.Join(" -> ", cheminListe.Select(n => n.Ville)));

        return (distances[destination], cheminListe);
    }



    public (Vehicule v,Noeud villeVehicule,int distanceTotal,List<Noeud> chemin) CommandeGraphe(string villeDepart, string villeArrivee,string typeVehicule)
    {
        (Noeud villeVehicule,Vehicule vehiculeUtilise,int distance_ville_vehicule) =  DijkstraRechercheCamion(villeDepart,typeVehicule);
        Console.WriteLine(villeVehicule.Ville);
        (int distance_livraison,List<Noeud> chemin) = DijkstraDistance(villeDepart,villeArrivee);
        int distanceTotal = distance_ville_vehicule + distance_livraison;
        Noeuds[villeArrivee].AjouterVehicule(vehiculeUtilise);
        Noeuds[villeVehicule.Ville].DaplacerVehicule(vehiculeUtilise);
        vehiculeUtilise.DistanceParcourue += distanceTotal;
        Console.WriteLine();
        Console.WriteLine($"Pour la livraison entre {villeDepart} et {villeArrivee}");
        Console.WriteLine($"Le chauffeur fais donc {distanceTotal} km entre {villeVehicule.Ville} et {villeArrivee} en passant par {villeDepart}");
        Console.WriteLine($"Le véhicule {vehiculeUtilise.Immatriculation} est donc dans la ville {villeArrivee}");
        return (vehiculeUtilise,villeVehicule,distanceTotal,chemin);
    }

    
    public Vehicule ContientVehicule(List<Vehicule> ListeVehicules,string typeVehicule)
    {
        bool contientVehicule = false;
            
        if(typeVehicule == "Voiture"){
            contientVehicule = ListeVehicules.Any(vehicule => vehicule is  Voiture);
            if (contientVehicule == true)
            {
                
                Vehicule premierVehicule = ListeVehicules.FirstOrDefault(vehicule => vehicule is Voiture);
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
                
                Vehicule premierVehicule = ListeVehicules.FirstOrDefault(vehicule => vehicule is CamionBenne);
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
                
                Vehicule premierVehicule = ListeVehicules.FirstOrDefault(vehicule => vehicule is CamionCiterne);
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
                
                Vehicule premierVehicule = ListeVehicules.FirstOrDefault(vehicule => vehicule is CamionFrigorifique);
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
                
                Vehicule premierVehicule = ListeVehicules.FirstOrDefault(vehicule => vehicule is Camionnette);
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