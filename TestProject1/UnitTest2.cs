using projetcamion;

namespace TestProject1
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestParcoursLargeur()
        {
            Transconnect transconnect = Interface.CreationTransconnect();
            Assert.AreEqual(transconnect.Graphe.ParcoursEnLargeur("Brest"), "Brest Nantes Rennes Lyon Bordeaux Toulouse Lille Paris Le Havre Marseille Grenoble Limoges Mulhouse Dax Pau Montpellier Perpignan Strasbourg Dunkerque Troyes Nice Nimes ");

        }
    }
}