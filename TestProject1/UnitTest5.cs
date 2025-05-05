using projetcamion;

namespace TestProject1
{
    [TestClass]
    public class UnitTest5
    {
        [TestMethod]
        public void TestTriClient()
        {
            DirecteurGeneral dupond = Interface.CreationHierarchie();
            List<Client> clients = new List<Client>();
            Client camille = new Client(0, 0, 668, "Pape", "Camille", new DateTime(1995, 9, 9), "Lille avenue Hoche", "pape@gmail.fr", 0610203041);
            Client marie = new Client(0, 0, 667, "Durant", "Marie", new DateTime(2000, 10, 10), "Versailles avenue Foch", "durant@gmail.fr", 0610203040);
            Client amandine = new Client(750.4, 0, 669, "Zaz", "Amandine", new DateTime(1992, 5, 3), "Paris rue mazarine", "zaz@yahoo.fr", 0610192562);
            clients.Add(camille);
            clients.Add(marie);
            Transconnect transconnect = new Transconnect(dupond,clients,null,null);
            transconnect.AjouterClient(amandine);
            transconnect.AjouterClient(amandine);

            Assert.AreEqual(transconnect.Clients.Count, 3); //vérifie qu’on ne peut pas ajouter un client en double
            
            
            transconnect.AfficherClients();   //Affiche mais surtout trie, le tri par défaut étant par nom ordre alphabétique
            Assert.AreEqual(transconnect.Clients[0], marie);

            Console.WriteLine("Tri du plus vieux au plus jeune");
            transconnect.ComparaisonClient = (a,b) => a.Naissance.CompareTo(b.Naissance);  //tri par date de naissance
            transconnect.AfficherClients();
            Assert.AreEqual(transconnect.Clients[0], amandine);

        }
    }
}
