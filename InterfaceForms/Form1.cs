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
            button1.Text = "Rafra�chir la hi�rarchie";
            button2.Text = "Ajouter un salari�";
            button3.Text = "Supprimer un salari�";
            button4.Text = "Recherche information salari�";
            button5.Text = "Trier les sous-directeurs par salaire d�croissant";
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            TreeNode root = new TreeNode($"{dg.Nom} {dg.Prenom} (Directeur G�n�ral)");

            foreach (var sousDir in dg.SousDirecteurs)
            {
                TreeNode sdNode = new TreeNode($"{sousDir.Nom} {sousDir.Prenom} ({sousDir.GetType().Name})");

                if (sousDir is DirecteurOperation dOp)
                {
                    foreach (var chef in dOp.Chefs)
                    {
                        TreeNode ceNode = new TreeNode($"{chef.Nom} {chef.Prenom} (Chef d'�quipe)");
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
                // Tu peux cr�er un petit formulaire de saisie ou faire �a en MessageBox/InputBox
                string type = Microsoft.VisualBasic.Interaction.InputBox("Type de salari� (1 � 5)\n1. Directeur des op�rations\n2. Chef d'�quipe\n3. Chauffeur\n4. Directeur Commercial\n5. Commercial", "Type");
                string nom = Microsoft.VisualBasic.Interaction.InputBox("Nom", "Nom");
                string prenom = Microsoft.VisualBasic.Interaction.InputBox("Pr�nom", "Pr�nom");
                string salaireStr = Microsoft.VisualBasic.Interaction.InputBox("Salaire", "Salaire");
                string telStr = Microsoft.VisualBasic.Interaction.InputBox("T�l�phone", "T�l�phone");
                string naissanceStr = Microsoft.VisualBasic.Interaction.InputBox("Date naissance (JJ/MM/AAAA)", "Naissance");
                string adresse = Microsoft.VisualBasic.Interaction.InputBox("Adresse", "Adresse");
                string mail = Microsoft.VisualBasic.Interaction.InputBox("Mail", "Mail");

                float salaire = float.Parse(salaireStr);
                int tel = int.Parse(telStr);
                DateTime naissance = DateTime.Parse(naissanceStr);

                // Trouver le sup�rieur hi�rarchique
                string nomSup = Microsoft.VisualBasic.Interaction.InputBox("Nom du sup�rieur", "Sup�rieur");
                string prenomSup = Microsoft.VisualBasic.Interaction.InputBox("Pr�nom du sup�rieur", "Sup�rieur");

                var superieur = dg.RerchercheSalarie(nomSup, prenomSup);
                if (superieur == null)
                {
                    MessageBox.Show("Sup�rieur non trouv� !");
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
                        MessageBox.Show("Type de salari� invalide.");
                        return;
                }

                MessageBox.Show("Salari� ajout� !");
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
                string type = Microsoft.VisualBasic.Interaction.InputBox("Type de salari� (1 � 5)\n1. Directeur des op�rations\n2. Chef d'�quipe\n3. Chauffeur\n4. Directeur Commercial\n5. Commercial", "Type");
                string nom = Microsoft.VisualBasic.Interaction.InputBox("Nom du salari� � supprimer", "Nom");
                string prenom = Microsoft.VisualBasic.Interaction.InputBox("Pr�nom", "Pr�nom");

                string nomSup = Microsoft.VisualBasic.Interaction.InputBox("Nom du sup�rieur", "Sup�rieur");
                string prenomSup = Microsoft.VisualBasic.Interaction.InputBox("Pr�nom du sup�rieur", "Sup�rieur");

                var superieur = Interface.dg.RerchercheSalarie(nomSup, prenomSup);
                if (superieur == null)
                {
                    MessageBox.Show("Sup�rieur non trouv� !");
                    return;
                }
                if(dg.RerchercheSalarie(nomSup,prenomSup).RerchercheSalarie(nom,prenom) is null) 
                {
                    MessageBox.Show("Salari� introuvable");
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
                        MessageBox.Show("Type de salari� invalide.");
                        return;
                }

                MessageBox.Show("Salari� supprim� !");
                //button1.PerformClick(); 
            }
            catch (Exception)
            {
                MessageBox.Show("Format incorrect");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string nom = Microsoft.VisualBasic.Interaction.InputBox("Nom du salari� � rechercher", "Nom");
            string prenom = Microsoft.VisualBasic.Interaction.InputBox("Pr�nom", "Pr�nom");
            var salarie = dg.RerchercheSalarie(nom, prenom);
            if (salarie == null)
            {
                MessageBox.Show("Salari� n�existe pas !");
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