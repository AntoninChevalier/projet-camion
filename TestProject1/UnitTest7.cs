using projetcamion;

namespace TestProject1
{
    [TestClass]
    public class UnitTest7
    {
        [TestMethod]
        public void TestRemiseClient()
        {
            DirecteurGeneral dupond = Interface.CreationHierarchie();
            List<Client> clients = new List<Client>();
            Client camille = new Client(0, 0, 668, "Pape", "Camille", new DateTime(1995, 9, 9), "Lille avenue Hoche", "pape@gmail.fr", 0610203041);
            Client marie = new Client(2354, 0, 667, "Durant", "Marie", new DateTime(2000, 10, 10), "Versailles avenue Foch", "durant@gmail.fr", 0610203040);
            Client amandine = new Client(750.4, 0, 669, "Zaz", "Amandine", new DateTime(1992, 5, 3), "Paris rue mazarine", "zaz@yahoo.fr", 0610192562);
            Client chloe = new Client(0, 0, 669, "Melio", "Chloe", new DateTime(1989, 5, 3), "Strasbourg avenue du pape", "clo@yahoo.fr", 0610198888);
            clients.Add(camille);
            clients.Add(marie);
            clients.Add(amandine);
            clients.Add(chloe);
            Transconnect transconnect = new Transconnect(dupond,clients,null,null);
            
            transconnect.AppliquerRemises();
            Assert.AreEqual(transconnect.Clients[0], 0);
            Assert.AreEqual(transconnect.Clients[1], 10);
            Assert.AreEqual(transconnect.Clients[2], 8);
            Assert.AreEqual(transconnect.Clients[3], 0);

        }
    }
}
