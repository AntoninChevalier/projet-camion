using projetcamion;

namespace TestProject1
{
    [TestClass]
    public class UnitTest3
    {
        [TestMethod]
        public void TestAjoutChauffeurEtRechercheSalarie()
        {
            DirecteurGeneral dupond = Interface.CreationHierarchie();
            ChefEquipe benjaminRoyal = (ChefEquipe)dupond.RerchercheSalarie("Royal", "Benjamin");
            Chauffeur matthieu = new Chauffeur(true, new DateTime(2021, 5, 9), 39500, 66885599, "Moreau", "Matthieu", new DateTime(1970, 5, 6), "Rue Carnot Boulogne", "mat@gmail.com", 0610457811);
            Assert.AreEqual(benjaminRoyal.RerchercheSalarie("Moreau", "Matthieu"), null);

            benjaminRoyal.AjouterChauffeur(matthieu);
            Assert.AreEqual(benjaminRoyal.RerchercheSalarie("Moreau","Matthieu"), matthieu);

        }
    }
}