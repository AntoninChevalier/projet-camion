using projetcamion;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestDijkstra()
        {
            Transconnect transconnect = Interface.CreationTransconnect();
            var (distance, chemin) = transconnect.Graphe.DijkstraDistance("Lille", "Dax");
            string strChemin = "";
            foreach(Noeud c in chemin)
            {
                strChemin = strChemin + c.Ville + " ";
            }

            Assert.AreEqual(distance, 204 + 499 + 154);
            Assert.AreEqual(strChemin, "Lille Paris Bordeaux Dax ");
        }
    }
}