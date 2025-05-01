using projetcamion;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System;
using System.IO;
using System.Windows.Forms;

namespace InterfaceForms
{
    public partial class Form1 : Form
    {
        Transconnect transconnect = Interface.transconnect;
        DirecteurGeneral dg = Interface.transconnect.DirecteurGeneral;
        Graphe graphe = Interface.transconnect.Graphe;
        
        public Form1()
        {
            InitializeComponent();
            ShowPanel(panelMainMenu);
            dgvClients.Visible = false;
            dgvCommandes.Visible = false;
        }

        private void ShowPanel(Panel panel)
        {
            panelMainMenu.Visible = false;
            panelGestionEffectif.Visible = false;
            panelInfoClient.Visible = false;
            panelGestionCommande.Visible = false;
            panelGestionLogistique.Visible = false;
            dgvClients.Visible = false;
            dgvCommandes.Visible = false;

            panel.Visible = true;
        }

        // Main menu
        private void btnGestionEffectif_Click(object sender, EventArgs e) => ShowPanel(panelGestionEffectif);
        private void btnInfoClient_Click(object sender, EventArgs e) => ShowPanel(panelInfoClient);
        private void btnGestionCommande_Click(object sender, EventArgs e) => ShowPanel(panelGestionCommande);
        private void btnGestionLogistique_Click(object sender, EventArgs e) => ShowPanel(panelGestionLogistique);

        // Gestion Effectif
        private void btnRefreshHierarchie_Click(object sender, EventArgs e) => button1_Click(sender, e);
        private void btnAjouterSalarie_Click(object sender, EventArgs e) => button2_Click(sender, e);
        private void btnSupprimerSalarie_Click(object sender, EventArgs e) => button3_Click(sender, e);
        private void btnRechercheInfoSalarie_Click(object sender, EventArgs e) => button4_Click(sender, e);
        private void btnTrierSousDirecteurs_Click(object sender, EventArgs e) => button5_Click(sender, e);

        // Info Client
        private void btnListeClients_Click(object sender, EventArgs e)
        {
            dgvClients.DataSource = null;
            dgvClients.DataSource = transconnect.Clients;
            dgvClients.Visible = true;
        }
        private void btnRechercheClient_Click(object sender, EventArgs e)
        {
            string nom = Microsoft.VisualBasic.Interaction.InputBox("Nom du client", "Recherche client");
            string prenom = Microsoft.VisualBasic.Interaction.InputBox("Prénom du client", "Recherche client");
            var c = Interface.transconnect.Clients.Find(x => x.Nom == nom && x.Prenom == prenom);
            if (c == null)
                MessageBox.Show("Client non trouvé");
            else
                MessageBox.Show(c.ToString());
        }

        // Gestion Commande
        private void btnListeCommandesFuture_Click(object sender, EventArgs e)
        {
            dgvCommandes.DataSource = null;
            dgvCommandes.DataSource = transconnect.ListeCommandesFuture;
            dgvCommandes.Visible = true;
        }
        private void btnListeCommandesPassees_Click(object sender, EventArgs e)
        {
            dgvCommandes.DataSource = null;
            dgvCommandes.DataSource = transconnect.ListeCommandesPasse;
            dgvCommandes.Visible = true;
        }
        // Gestion Logistique
        private void btnAfficherGraphe_Click(object sender, EventArgs e) => button6_Click(sender, e);
        private void btnCalculerDistance_Click(object sender, EventArgs e) => button7_Click(sender, e);

        // Original Buttons' implementations
        private void button1_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            var dg = Interface.transconnect.DirecteurGeneral;
            TreeNode root = new TreeNode($"{dg.Nom} {dg.Prenom} (Directeur Général)");

            foreach (var sousDir in dg.SousDirecteurs)
            {
                TreeNode sdNode = new TreeNode($"{sousDir.Nom} {sousDir.Prenom} ({sousDir.GetType().Name})");

                if (sousDir is DirecteurOperation dOp)
                {
                    foreach (var chef in dOp.Chefs)
                    {
                        TreeNode ceNode = new TreeNode($"{chef.Nom} {chef.Prenom} (Chef d'équipe)");
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

        private void SupprimerSalarieViaFormulaire()
        {
            try
            {
                string type = Microsoft.VisualBasic.Interaction.InputBox("Type de salarié (1 à 5)\n1. Directeur des opérations\n2. Chef d'équipe\n3. Chauffeur\n4. Directeur Commercial\n5. Commercial", "Type");
                string nom = Microsoft.VisualBasic.Interaction.InputBox("Nom du salarié à supprimer", "Nom");
                string prenom = Microsoft.VisualBasic.Interaction.InputBox("Prénom", "Prénom");

                string nomSup = Microsoft.VisualBasic.Interaction.InputBox("Nom du supérieur", "Supérieur");
                string prenomSup = Microsoft.VisualBasic.Interaction.InputBox("Prénom du supérieur", "Supérieur");

                var superieur = Interface.transconnect.DirecteurGeneral.RerchercheSalarie(nomSup, prenomSup);
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
                        Interface.transconnect.DirecteurGeneral.SupprimerSalarie(nom, prenom);
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

        private void AjouterSalarieViaFormulaire()
        {
            try
            {
                // Tu peux créer un petit formulaire de saisie ou faire ça en MessageBox/InputBox
                string type = Microsoft.VisualBasic.Interaction.InputBox("Type de salarié (1 à 5)\n1. Directeur des opérations\n2. Chef d'équipe\n3. Chauffeur\n4. Directeur Commercial\n5. Commercial", "Type");
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



        private void button2_Click(object sender, EventArgs e) => AjouterSalarieViaFormulaire();

        private void button3_Click(object sender, EventArgs e) => SupprimerSalarieViaFormulaire();

        private void button4_Click(object sender, EventArgs e)
        {
            string nom = Microsoft.VisualBasic.Interaction.InputBox("Nom du salarié à rechercher", "Nom");
            string prenom = Microsoft.VisualBasic.Interaction.InputBox("Prénom", "Prénom");
            var salarie = Interface.transconnect.DirecteurGeneral.RerchercheSalarie(nom, prenom);
            MessageBox.Show(salarie != null ? salarie.ToString() : "Salarié n’existe pas !");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Interface.transconnect.DirecteurGeneral.SousDirecteurs.Sort();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                string fichier = Path.Combine(AppContext.BaseDirectory, "graphe.png");
                var visualiseur = new VisualiseurGrapheSkia();
                visualiseur.Visualiser(Interface.graphe, fichier);

                if (File.Exists(fichier))
                {
                    using (var bmpTemp = new System.Drawing.Bitmap(fichier))
                    {
                        pictureBoxGraph.Image?.Dispose();
                        pictureBoxGraph.Image = new System.Drawing.Bitmap(bmpTemp);
                    }
                }
                else
                {
                    MessageBox.Show("Le fichier de visualisation n'a pas été généré.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la visualisation : {ex.Message}");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string villeDepart = Microsoft.VisualBasic.Interaction.InputBox("Ville de départ", "Commande Graphe");
            string villeArrivee = Microsoft.VisualBasic.Interaction.InputBox("Ville d'arrivée", "Commande Graphe");
            string typeVehicule = Microsoft.VisualBasic.Interaction.InputBox("Type de véhicule (Voiture, CamionBenne, ...)", "Commande Graphe");

            var (vehiculeUtilise, villeVehicule, distanceTotal) = Interface.graphe.CommandeGraphe(villeDepart, villeArrivee, typeVehicule);

            var sb = new StringWriter();
            sb.WriteLine();
            sb.WriteLine($"Pour la livraison entre {villeDepart} et {villeArrivee}");
            if (villeVehicule != null)
                sb.WriteLine($"Le chauffeur fait {distanceTotal} km (de {villeVehicule.Ville} via {villeVehicule.Ville} jusqu'à {villeArrivee})");
            sb.WriteLine(vehiculeUtilise != null
                ? $"Le véhicule {vehiculeUtilise.Immatriculation} est maintenant à {villeArrivee}" : "Pas de véhicule trouvé.");

            textBoxOutput.AppendText(sb.ToString());
            textBoxOutput.AppendText(Environment.NewLine);
        }
    }
}
