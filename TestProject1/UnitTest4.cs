using projetcamion;

namespace TestProject1
{
    [TestClass]
    public class UnitTest4
    {
        [TestMethod]
        public void TestSuppresionSalarie()
        {
            DirecteurGeneral dupond = Interface.CreationHierarchie();
            DirecteurCommercial victorHugo = new DirecteurCommercial(null, new DateTime(2015, 5, 6), 88000, 66885000, "Hugo", "Victor", new DateTime(1968, 3, 2), "Avenue Hoche Paris", "vh@gmail.fr", 0712345611);
            dupond.AjouterSalarie(victorHugo);
            Assert.AreEqual(dupond.RerchercheSalarie("Hugo", "Victor"), victorHugo);

            dupond.SupprimerSalarie("Hugo","Victor");
            Assert.AreEqual(dupond.RerchercheSalarie("Hugo", "Victor"), null);

        }
    }
}