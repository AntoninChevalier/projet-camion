using projetcamion;

namespace TestProject1
{
    [TestClass]
    public class UnitTest6
    {
        [TestMethod]
        public void TestBellmanFord()
        {
            Transconnect transconnect = Interface.CreationTransconnect();
            var (distance, chemin) = transconnect.Graphe.BellmanFordDistance2("Nimes", "Brest");
            string strChemin = "";
            foreach (Noeud c in chemin)
            {
                strChemin = strChemin + c.Ville + " ";
            }

            Assert.AreEqual(distance, 1061);
            Assert.AreEqual(strChemin, "Nimes Montpellier Toulouse Nantes Brest ");
        }
    }
}