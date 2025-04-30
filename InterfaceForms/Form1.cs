using projetcamion;

namespace InterfaceForms
{
    public partial class Form1 : Form
    {
        DirecteurGeneral dg = Interface.dg;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Text = "Rafraîchir la hiérarchie";
            button2.Text = "Ajouter un salarié";
            button3.Text = "Supprimer un salarié";
            button4.Text = "Recherche information salarié";
            button5.Text = "Trier les sous-directeurs par salaire décroissant";
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            TreeNode root = new TreeNode($"{dg.Nom} {dg.Prenom} (Directeur Général)");

            foreach (var sousDir in dg.SousDirecteurs)
            {
                TreeNode sdNode = new TreeNode($"{sousDir.Nom} {sousDir.Prenom} ({sousDir.GetType().Name})");

                if (sousDir is DirecteurOperation dOp)
                {
                    foreach (var chef in dOp.Chefs)
                    {
                        TreeNode ceNode = new TreeNode($"{chef.Nom} {chef.Prenom} (Chef d'Équipe)");
                        foreach (var chauffeur in chef.Chauffeurs)
                        {
                            TreeNode chNode = new TreeNode($"{chauffeur.Nom} {chauffeur.Prenom} (Chauffeur)");
                            ceNode.Nodes.Add(chNode);
                        }
                        sdNode.Nodes.Add(ceNode);
                    }
                }
                else if (sousDir is DirecteurCommercial dCom)
                {
                    foreach (var comm in dCom.Commerciaux)
                    {
                        TreeNode comNode = new TreeNode($"{comm.Nom} {comm.Prenom} (Commercial)");
                        sdNode.Nodes.Add(comNode);
                    }
                }

                root.Nodes.Add(sdNode);
            }
            treeView1.Nodes.Add(root);
            treeView1.ExpandAll();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AjouterSalarieViaFormulaire();
        }
        private void AjouterSalarieViaFormulaire()
        {
            try
            {
                // Tu peux créer un petit formulaire de saisie ou faire ça en MessageBox/InputBox
                string type = Microsoft.VisualBasic.Interaction.InputBox("Type de salarié (1 à 5)\n1. Directeur des opérations\n2. Chef d'Équipe\n3. Chauffeur\n4. Directeur Commercial\n5. Commercial", "Type");
                string nom = Microsoft.VisualBasic.Interaction.InputBox("Nom", "Nom");
                string prenom = Microsoft.VisualBasic.Interaction.InputBox("Prénom", "Prénom");
                string salaireStr = Microsoft.VisualBasic.Interaction.InputBox("Salaire", "Salaire");
                string telStr = Microsoft.VisualBasic.Interaction.InputBox("Téléphone", "Téléphone");
                string naissanceStr = Microsoft.VisualBasic.Interaction.InputBox("Date naissance (JJ/MM/AAAA)", "Naissance");
                string adresse = Microsoft.VisualBasic.Interaction.InputBox("Adresse", "Adresse");
                string mail = Microsoft.VisualBasic.Interaction.InputBox("Mail", "Mail");

                float salaire = float.Parse(salaireStr);
                int tel = int.Parse(telStr);
                DateTime naissance = DateTime.Parse(naissanceStr);

                // Trouver le supérieur hiérarchique
                string nomSup = Microsoft.VisualBasic.Interaction.InputBox("Nom du supérieur", "Supérieur");
                string prenomSup = Microsoft.VisualBasic.Interaction.InputBox("Prénom du supérieur", "Supérieur");

                var superieur = dg.RerchercheSalarie(nomSup, prenomSup);
                if (superieur == null)
                {
                    MessageBox.Show("Supérieur non trouvé !");
                    return;
                }

                DateTime embauche = DateTime.Now;
                Salarie nouveau = null;

                switch (type)
                {
                    case "1":
                        nouveau = new DirecteurOperation(new List<ChefEquipe>(), embauche, salaire, 9999, nom, prenom, naissance, adresse, mail, tel);
                        ((DirecteurGeneral)superieur).AjouterSalarie((DirecteurOperation)nouveau);
                        break;
                    case "2":
                        nouveau = new ChefEquipe(new List<Chauffeur>(), embauche, salaire, 9998, nom, prenom, naissance, adresse, mail, tel);
                        ((DirecteurOperation)superieur).AjouterChefEquipe((ChefEquipe)nouveau);
                        break;
                    case "3":
                        nouveau = new Chauffeur(true, embauche, salaire, 9997, nom, prenom, naissance, adresse, mail, tel);
                        ((ChefEquipe)superieur).AjouterChauffeur((Chauffeur)nouveau);
                        break;
                    case "4":
                        nouveau = new DirecteurCommercial(new List<Commercial>(), embauche, salaire, 9996, nom, prenom, naissance, adresse, mail, tel);
                        ((DirecteurGeneral)superieur).AjouterSalarie((DirecteurCommercial)nouveau);
                        break;
                    case "5":
                        nouveau = new Commercial(0, embauche, salaire, 9995, nom, prenom, naissance, adresse, mail, tel);
                        ((DirecteurCommercial)superieur).AjouterCommercial((Commercial)nouveau);
                        break;
                    default:
                        MessageBox.Show("Type de salarié invalide.");
                        return;
                }

                MessageBox.Show("Salarié ajouté !");
                //button1.PerformClick();
            }
            catch(Exception)
            {
                MessageBox.Show("Format incorrect");
            }
        }
        private void button3_Click_1(object sender, EventArgs e)
        {
            SupprimerSalarieViaFormulaire();
        }
        private void SupprimerSalarieViaFormulaire()
        {
            try
            {
                string type = Microsoft.VisualBasic.Interaction.InputBox("Type de salarié (1 à 5)\n1. Directeur des opérations\n2. Chef d'Équipe\n3. Chauffeur\n4. Directeur Commercial\n5. Commercial", "Type");
                string nom = Microsoft.VisualBasic.Interaction.InputBox("Nom du salarié à supprimer", "Nom");
                string prenom = Microsoft.VisualBasic.Interaction.InputBox("Prénom", "Prénom");

                string nomSup = Microsoft.VisualBasic.Interaction.InputBox("Nom du supérieur", "Supérieur");
                string prenomSup = Microsoft.VisualBasic.Interaction.InputBox("Prénom du supérieur", "Supérieur");

                var superieur = Interface.dg.RerchercheSalarie(nomSup, prenomSup);
                if (superieur == null)
                {
                    MessageBox.Show("Supérieur non trouvé !");
                    return;
                }
                if(dg.RerchercheSalarie(nomSup,prenomSup).RerchercheSalarie(nom,prenom) is null) 
                {
                    MessageBox.Show("Salarié introuvable");
                    return;
                }
                switch (type)
                {
                    case "1":
                    case "4":
                        Interface.dg.SupprimerSalarie(nom, prenom);
                        break;
                    case "2":
                        ((DirecteurOperation)superieur).SupprimerChefEquipe(nom, prenom);
                        break;
                    case "3":
                        ((ChefEquipe)superieur).SupprimerChauffeur(nom, prenom);
                        break;
                    case "5":
                        ((DirecteurCommercial)superieur).SupprimerCommercial(nom, prenom);
                        break;
                    default:
                        MessageBox.Show("Type de salarié invalide.");
                        return;
                }

                MessageBox.Show("Salarié supprimé !");
                //button1.PerformClick(); 
            }
            catch (Exception)
            {
                MessageBox.Show("Format incorrect");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string nom = Microsoft.VisualBasic.Interaction.InputBox("Nom du salarié à rechercher", "Nom");
            string prenom = Microsoft.VisualBasic.Interaction.InputBox("Prénom", "Prénom");
            var salarie = dg.RerchercheSalarie(nom, prenom);
            if (salarie == null)
            {
                MessageBox.Show("Salarié n’existe pas !");
                return;
            }
            else
            {
                MessageBox.Show(salarie.ToString());
                return;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            dg.SousDirecteurs.Sort();
        }
    }
}