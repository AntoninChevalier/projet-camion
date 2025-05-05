using projetcamion;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System;
using System.IO;
using System.Windows.Forms;
using System.Reflection;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
            panelStatistiques.Visible = false;
            
            dgvClients.Visible = false;
            dgvCommandes.Visible = false;

            panel.Visible = true;
        }

        // Main menu
        private void btnGestionEffectif_Click(object sender, EventArgs e) => ShowPanel(panelGestionEffectif);
        private void btnInfoClient_Click(object sender, EventArgs e) => ShowPanel(panelInfoClient);
        private void btnGestionCommande_Click(object sender, EventArgs e) => ShowPanel(panelGestionCommande);
        private void btnGestionLogistique_Click(object sender, EventArgs e) => ShowPanel(panelGestionLogistique);

        private void btnStatistiques_Click(object sender, EventArgs e) => ShowPanel(panelStatistiques);

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
            dgvClients.DataSource = Interface.transconnect.Clients;
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
            Interface.transconnect.ListeCommandesFuture.Sort((x, y) => x.Date.CompareTo(y.Date));
            dgvCommandes.DataSource = Interface.transconnect.ListeCommandesFuture;
            dgvCommandes.Visible = true;
        }
        private void btnListeCommandesPassees_Click(object sender, EventArgs e)
        {
            dgvCommandes.DataSource = null;
            dgvCommandes.DataSource = Interface.transconnect.ListeCommandesPasse;
            dgvCommandes.Visible = true;
        }
        private void btnAjoutCommande_Click(object sender, EventArgs e)
        {
            AjouterUneCommande(sender, e);
        }

        private void btnTraiterCommande_Click(object sender, EventArgs e)   
        {
            FaireLaCommandeLaPlusAncienne(sender, e);
        }
        private void btnModifierCommande_Click(object sender, EventArgs e)
        {
            ModifierCommandeViaFormulaire();
        }
        private void btnSupprimerCommande_Click(object sender, EventArgs e)
        {
            SupprimerCommandeViaFormulaire();
        }

        
        
        // Gestion Logistique
        private void btnAfficherGraphe_Click(object sender, EventArgs e) => button6_Click(sender, e);
        
        private void btnDeplaceVehicule_Click(object sender, EventArgs e) => buttonDeplaceVehicule(sender, e);
        
        private void btnAjouterClient_Click(object sender, EventArgs e) => AjouterClientViaFormulaire();

        private void btnModifierClient_Click(object sender, EventArgs e)
        {
            ModifierClientViaFormulaire();
        }

        // Statistiques

        private void btnStatistiquesClient_Click(object sender, EventArgs e)
        {
            bouttonStatistiquesClient(sender, e);
        }
        
        private void btnStatistiquesChauffeur_Click(object sender, EventArgs e)
        {
            bouttonStatistiquesChaffeur(sender, e);
        }

        private void btnStatistiquesListeCommandesFuturePeriode_Click(object sender, EventArgs e)
        {
            DateTime dateDebut = Convert.ToDateTime(Microsoft.VisualBasic.Interaction.InputBox("Date de début", "Date"));
            DateTime dateFin = Convert.ToDateTime(Microsoft.VisualBasic.Interaction.InputBox("Date de fin", "Date"));
            var commandesParDate = Interface.transconnect.ListeCommandesFuture.Where(c => c.Date >=dateDebut && c.Date <= dateFin ).ToList();
            dgvStatistiques.DataSource = null;
            dgvStatistiques.DataSource = commandesParDate;
            dgvStatistiques.Visible = true;
        }

        private void btnStatistiquesListeCommandesParClinet_Click(object sender, EventArgs e)
        {
            
            string nom = Microsoft.VisualBasic.Interaction.InputBox("Nom du client", "Client");
            string prenom = Microsoft.VisualBasic.Interaction.InputBox("Prénom du client", "Client");
            var commandesParClient = Interface.transconnect.ListeCommandesPasse.Where(c => c.Client.Nom == nom && c.Client.Prenom == prenom ).ToList();
            dgvStatistiques.DataSource = null;
            dgvStatistiques.DataSource = commandesParClient;
            dgvStatistiques.Visible = true;
        }

        private void btnStatistiquesVehiculePlus10000_Click(object sender, EventArgs e)
        {
            bouttonStatistiquesVehiculePlus10000(sender, e);
        }






        private void ModifierClientViaFormulaire()
        {
            try
            {
                // Demander les informations pour identifier le client
                string nom = Microsoft.VisualBasic.Interaction.InputBox("Nom du client à modifier", "Nom");
                string prenom = Microsoft.VisualBasic.Interaction.InputBox("Prénom du client à modifier", "Prénom");

                // Rechercher le client
                var client = transconnect.Clients.Find(x => x.Nom == nom && x.Prenom == prenom);
                if (client == null)
                {
                    MessageBox.Show("Client introuvable !");
                    return;
                }

                // Demander quelle information modifier
                string infoAModifier = Microsoft.VisualBasic.Interaction.InputBox("Quelle information voulez-vous modifier ? (nom, prenom, nss, naissance, adresse, mail, numero, montantAchatCumule, remise)", "Information à modifier");
                string nouvelleValeur = Microsoft.VisualBasic.Interaction.InputBox("Nouvelle valeur", "Nouvelle valeur");

                // Mettre à jour l'information du client
                switch (infoAModifier.ToLower())
                {
                    case "nom":
                        client.Nom = nouvelleValeur;
                        break;
                    case "prenom":
                        client.Prenom = nouvelleValeur;
                        break;
                    case "nss":
                        if (int.TryParse(nouvelleValeur, out int nouveauNss))
                        {
                            client.Nss = nouveauNss;
                        }
                        else
                        {
                            MessageBox.Show("Format de NSS invalide.");
                            return;
                        }
                        break;
                    case "naissance":
                        if (DateTime.TryParse(nouvelleValeur, out DateTime nouvelleNaissance))
                        {
                            client.Naissance = nouvelleNaissance;
                        }
                        else
                        {
                            MessageBox.Show("Format de date invalide.");
                            return;
                        }
                        break;
                    case "adresse":
                        client.Adresse = nouvelleValeur;
                        break;
                    case "mail":
                        client.Mail = nouvelleValeur;
                        break;
                    case "numero":
                        if (int.TryParse(nouvelleValeur, out int nouveauNumero))
                        {
                            client.Numero = nouveauNumero;
                        }
                        else
                        {
                            MessageBox.Show("Format de numéro invalide.");
                            return;
                        }
                        break;
                    case "montantachatcumule":
                        if (double.TryParse(nouvelleValeur, out double nouveauMontantAchatCumule))
                        {
                            client.MontantAchatCumule = nouveauMontantAchatCumule;
                        }
                        else
                        {
                            MessageBox.Show("Format de montant invalide.");
                            return;
                        }
                        break;
                    case "remise":
                        if (double.TryParse(nouvelleValeur, out double nouvelleRemise))
                        {
                            client.Remise = nouvelleRemise;
                        }
                        else
                        {
                            MessageBox.Show("Format de remise invalide.");
                            return;
                        }
                        break;
                    default:
                        MessageBox.Show("Information invalide.");
                        return;
                }

                MessageBox.Show("Information du client mise à jour !");
            }
            catch (Exception)
            {
                MessageBox.Show("Format incorrect");
            }
        }


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
                visualiseur.Visualiser(Interface.transconnect.Graphe, fichier);

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

            var (vehiculeUtilise, villeVehicule, distanceTotal,chemin) = Interface.transconnect.Graphe.CommandeGraphe(villeDepart, villeArrivee, typeVehicule);

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
        private void btnModifierSalarie_Click(object sender, EventArgs e)
        {
            ModifierSalarieViaFormulaire();
        }

        private void ModifierSalarieViaFormulaire()
        {
            try
            {
                string nom = Microsoft.VisualBasic.Interaction.InputBox("Nom du salarié à modifier", "Nom");
                string prenom = Microsoft.VisualBasic.Interaction.InputBox("Prénom du salarié à modifier", "Prénom");

                var salarie = dg.RerchercheSalarie(nom, prenom);
                if (salarie == null)
                {
                    MessageBox.Show("Salarié introuvable !");
                    return;
                }

                string infoAModifier = Microsoft.VisualBasic.Interaction.InputBox("Quelle information voulez-vous modifier ? (nom, prenom, dateEntree, salaire, adresse, mail, numero)", "Information à modifier");
                string nouvelleValeur = Microsoft.VisualBasic.Interaction.InputBox("Nouvelle valeur", "Nouvelle valeur");

                switch (infoAModifier.ToLower())
                {
                    case "nom":
                        salarie.Nom = nouvelleValeur;
                        break;
                    case "prenom":
                        salarie.Prenom = nouvelleValeur;
                        break;
                    case "dateentree":
                        if (DateTime.TryParse(nouvelleValeur, out DateTime nouvelleDate))
                        {
                            salarie.DateEntree = nouvelleDate;
                        }
                        else
                        {
                            MessageBox.Show("Format de date invalide.");
                            return;
                        }
                        break;
                    case "salaire":
                        if (float.TryParse(nouvelleValeur, out float nouveauSalaire))
                        {
                            salarie.Salaire = nouveauSalaire;
                        }
                        else
                        {
                            MessageBox.Show("Format de salaire invalide.");
                            return;
                        }
                        break;
                    case "adresse":
                        salarie.Adresse = nouvelleValeur;
                        break;
                    case "mail":
                        salarie.Mail = nouvelleValeur;
                        break;
                    case "numero":
                        if (int.TryParse(nouvelleValeur, out int nouveauNumero))
                        {
                            salarie.Numero = nouveauNumero;
                        }
                        else
                        {
                            MessageBox.Show("Format de numéro invalide.");
                            return;
                        }
                        break;
                    default:
                        MessageBox.Show("Information invalide.");
                        return;
                }

                MessageBox.Show("Information du salarié mise à jour !");
            }
            catch (Exception)
            {
                MessageBox.Show("Format incorrect");
            }
        }

      

        private void SupprimerCommandeViaFormulaire()
        {
            try
            {
                string nom = Microsoft.VisualBasic.Interaction.InputBox("Nom du client", "Commande");
                string prenom = Microsoft.VisualBasic.Interaction.InputBox("Prénom du client", "Commande");
                string villeDepart = Microsoft.VisualBasic.Interaction.InputBox("Ville de départ", "Commande");
                string villeArrivee = Microsoft.VisualBasic.Interaction.InputBox("Ville d'arrivée", "Commande");
                string typeVehicule = Microsoft.VisualBasic.Interaction.InputBox("Type de véhicule (Voiture, CamionBenne, ...)", "Commande");
                DateTime date  = Convert.ToDateTime(Microsoft.VisualBasic.Interaction.InputBox("Date", "Commande"));

                var client = Interface.transconnect.Clients.Find(x => x.Nom == nom && x.Prenom == prenom);
                var commande = Interface.transconnect.ListeCommandesFuture.Find(x => x.VilleDepart == villeDepart && x.VilleArrivee == villeArrivee && x.TypeVehicule == typeVehicule);
                if (client == null || commande==null)
                {
                    MessageBox.Show("Commande introuvable !");
                    return;
                }
                Interface.transconnect.SupprimerCommande(nom, prenom,villeDepart,villeArrivee,typeVehicule,date);

                MessageBox.Show("Commande supprimée !");
            }
            catch (Exception)
            {
                MessageBox.Show("Format incorrect");
            }
        }

        private void AjouterClientViaFormulaire()
        {
            try
            {
                // Demander les informations du client
                string nom = Microsoft.VisualBasic.Interaction.InputBox("Nom du client", "Nom");
                string prenom = Microsoft.VisualBasic.Interaction.InputBox("Prénom du client", "Prénom");
                string nssStr = Microsoft.VisualBasic.Interaction.InputBox("NSS du client", "NSS");
                string naissanceStr = Microsoft.VisualBasic.Interaction.InputBox("Date de naissance (JJ/MM/AAAA)", "Naissance");
                string adresse = Microsoft.VisualBasic.Interaction.InputBox("Adresse du client", "Adresse");
                string mail = Microsoft.VisualBasic.Interaction.InputBox("Mail du client", "Mail");
                string numeroStr = Microsoft.VisualBasic.Interaction.InputBox("Numéro de téléphone du client", "Numéro");
                string montantAchatCumuleStr = Microsoft.VisualBasic.Interaction.InputBox("Montant achat cumulé", "Montant achat cumulé");
                string remiseStr = Microsoft.VisualBasic.Interaction.InputBox("Remise", "Remise");

                // Convertir les entrées en types appropriés
                int nss = int.Parse(nssStr);
                DateTime naissance = DateTime.Parse(naissanceStr);
                int numero = int.Parse(numeroStr);
                double montantAchatCumule = double.Parse(montantAchatCumuleStr);
                double remise = double.Parse(remiseStr);

                // Créer un nouveau client
                Client nouveauClient = new Client(montantAchatCumule, remise, nss, nom, prenom, naissance, adresse, mail, numero);

                // Ajouter le client à la liste des clients
                transconnect.Clients.Add(nouveauClient);

                MessageBox.Show("Client ajouté avec succès !");
            }
            catch (Exception)
            {
                MessageBox.Show("Format incorrect");
            }
        }



        private async void buttonDeplaceVehicule(object sender, EventArgs e)
        {
            string villeDepart = Microsoft.VisualBasic.Interaction.InputBox("Ville de départ", "Commande Graphe");
            if(Interface.transconnect.Graphe.Noeuds.ContainsKey(villeDepart) == false)
            {
                MessageBox.Show("Ville de départ introuvable !");
                return;
            }
            string typeVehicule = Microsoft.VisualBasic.Interaction.InputBox("Type de véhicule (Voiture, Camion Benne, Camion Citerne, Camion Frigorifique ou Camionnette)", "Commande Graphe");

            Vehicule vehicule = null;
            
            if(Interface.transconnect.Graphe.ContientVehicule(Interface.transconnect.Graphe.Noeuds[villeDepart].ListeVehicules, typeVehicule) != null)
            {
                 vehicule = Interface.transconnect.Graphe.ContientVehicule(Interface.transconnect.Graphe.Noeuds[villeDepart].ListeVehicules, typeVehicule);
            }
            else{
                MessageBox.Show("Aucun véhicule trouvé dans cette ville !");
                return;
            }

            string villeArrivee = Microsoft.VisualBasic.Interaction.InputBox("Ville d'arrivée", "Commande Graphe");


            var (vehiculeUtilise, villeVehicule, distanceTota,chemin) = Interface.transconnect.Graphe.CommandeGraphe(villeDepart, villeArrivee, typeVehicule);

            

            foreach (var v in chemin)
            {
                
            
                
                        // Génère l’image du graphe avec le véhicule à la position "v"
                string fichier = Path.Combine(AppContext.BaseDirectory, "graphe.png");
                var visualiseur = new VisualiseurGrapheSkia();
                visualiseur.VisualiserNoeud(Interface.transconnect.Graphe, v, fichier);

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
                await Task.Delay(2000);
            }

            var sb = new StringWriter();
            sb.WriteLine();
            sb.WriteLine($"Le véhicule a été déplacer de {villeDepart} à {villeArrivee}");
            
            sb.WriteLine($"Le véhicule {vehicule.Immatriculation} est maintenant à {villeArrivee}" );

            textBoxOutput.AppendText(sb.ToString());
            textBoxOutput.AppendText(Environment.NewLine);
        }

        private void AjouterUneCommande(object sender, EventArgs e)
        {
            string nom = Microsoft.VisualBasic.Interaction.InputBox("Nom du client", "Commande");
            string prenom = Microsoft.VisualBasic.Interaction.InputBox("Prénom du client", "Commande");
            string villeDepart = Microsoft.VisualBasic.Interaction.InputBox("Ville de départ", "Commande");
            string villeArrivee = Microsoft.VisualBasic.Interaction.InputBox("Ville d'arrivée", "Commande");
            string typeVehicule = Microsoft.VisualBasic.Interaction.InputBox("Type de véhicule (Voiture, CamionBenne, ...)", "Commande");

            var client = Interface.transconnect.Clients.Find(x => x.Nom == nom && x.Prenom == prenom);
            if (client == null)
            {
                MessageBox.Show("Client non trouvé !");
                return;
            }

            var commande = new Commande(client, villeDepart, villeArrivee, DateTime.Now, typeVehicule);
            Interface.transconnect.ListeCommandesFuture.Add(commande);
            MessageBox.Show("Commande ajoutée !");
        }

        private void FaireLaCommandeLaPlusAncienne(object sender, EventArgs e)
        {
            if (Interface.transconnect.ListeCommandesFuture.Count == 0)
            {
                MessageBox.Show("Aucune commande à traiter !");
                return;
            }

            Interface.transconnect.ListeCommandesFuture.Sort((x, y) => x.Date.CompareTo(y.Date));

            var commande = Interface.transconnect.ListeCommandesFuture[0];
            

            List<Vehicule> listeVehiculeIndisponible = Interface.transconnect.ListeCommandesPasse.Where(c => c.Date == commande.Date).Select(c => c.Vehicule).ToList();

            //(Vehicule v, Noeud villeVehicule, int distanceTotal,List<Noeud> chemin) = Interface.transconnect.Graphe.CommandeGraphe(commande.VilleDepart, commande.VilleArrivee, commande.TypeVehicule);

            (Vehicule v, Noeud villeVehicule, int distanceTotal,List<Noeud> chemin) = Interface.transconnect.Graphe.CommandeGrapheDisponible(commande.VilleDepart, commande.VilleArrivee, commande.TypeVehicule, listeVehiculeIndisponible);


            Commande commandeTraitee = new Commande(commande.Client, commande.VilleDepart, commande.VilleArrivee,v,v.Chauffeur, DateTime.Today, distanceTotal, commande.TypeVehicule);
            Interface.transconnect.ListeCommandesFuture.Remove(commande);
            Interface.transconnect.ListeCommandesPasse.Add(commandeTraitee);
            // ajouter le prix de la commande et calculer la remise

            Interface.transconnect.Clients.Find(x => x.Nom == commande.Client.Nom && x.Prenom == commande.Client.Prenom).Remise = commande.Client.MontantAchatCumule < 30000 ? (commande.Client.MontantAchatCumule/10000) : 0.3;
            Interface.transconnect.Clients.Find(x => x.Nom == commande.Client.Nom && x.Prenom == commande.Client.Prenom).MontantAchatCumule += commandeTraitee.Prix*(1-commande.Client.Remise);
            // ajouter une livraison effectuée au chauffeur
            v.Chauffeur.NombreLivraisonEffectuee++;
            MessageBox.Show($"Commande de {commande.Client.Nom} {commande.Client.Prenom} traitée !");
        }

        

        private void ModifierCommandeViaFormulaire()
        {
            try
            {
                string nom = Microsoft.VisualBasic.Interaction.InputBox("Nom du client", "Commande");
                string prenom = Microsoft.VisualBasic.Interaction.InputBox("Prénom du client", "Commande");
                string villeDepart = Microsoft.VisualBasic.Interaction.InputBox("Ville de départ", "Commande");
                string villeArrivee = Microsoft.VisualBasic.Interaction.InputBox("Ville d'arrivée", "Commande");
                string typeVehicule = Microsoft.VisualBasic.Interaction.InputBox("Type de véhicule (Voiture, CamionBenne, ...)", "Commande");

                var client = Interface.transconnect.Clients.Find(x => x.Nom == nom && x.Prenom == prenom);
                var commande = Interface.transconnect.ListeCommandesFuture.Find(x => x.VilleDepart == villeDepart && x.VilleArrivee == villeArrivee && x.TypeVehicule == typeVehicule);
                if (client == null || commande==null)
                {
                    MessageBox.Show("Commande introuvable !");
                    return;
                }

                string infoAModifier = Microsoft.VisualBasic.Interaction.InputBox("Quelle information voulez-vous modifier ? (ville de départ, ville d'arrivée, type de véhicule)", "Information à modifier");
                string nouvelleValeur = Microsoft.VisualBasic.Interaction.InputBox("Nouvelle valeur", "Nouvelle valeur");

            
                switch (infoAModifier.ToLower())
                {
                    case "ville de départ":
                        commande.VilleDepart = nouvelleValeur;
                        break;
                    case "ville d'arrivée":
                        commande.VilleArrivee = nouvelleValeur;
                        break;
                    case "type de véhicule":
                        commande.TypeVehicule = nouvelleValeur;
                        break;
                    default:
                        MessageBox.Show("Information invalide.");
                        return;
                }

                MessageBox.Show("Information de la commande mise à jour !");
            }
            catch (Exception)
            {
                MessageBox.Show("Format incorrect");
            }
        }

        private void bouttonStatistiquesClient(object sender, EventArgs e)
        {
            double montantTotal = 0;
            foreach (var c in Interface.transconnect.Clients)
            {
                montantTotal += c.MontantAchatCumule ;
            }
            montantTotal = Math.Round(montantTotal, 2);
            //MessageBox.Show($"Montant total des achats : {montantTotal} €\nNombre de clients : {Interface.transconnect.Clients.Count}");

            double montantMoyen = montantTotal / Interface.transconnect.Clients.Count;
            montantMoyen = Math.Round(montantMoyen, 2);
            var sb = new StringWriter();
            sb.WriteLine();
            sb.WriteLine($"Montant total des achats : {montantTotal}");
            
            sb.WriteLine($"Nombre de clients : {Interface.transconnect.Clients.Count}" );

            sb.WriteLine($"Montant moyen des achats par client: {montantMoyen}");
            textBoxOutput2.Clear();
            textBoxOutput2.AppendText(sb.ToString());
            textBoxOutput2.AppendText(Environment.NewLine);
        }

        private void bouttonStatistiquesChaffeur(object sender, EventArgs e)
        {
            
            var dg = Interface.transconnect.DirecteurGeneral;
            List<Chauffeur> listeChauffeurs = new List<Chauffeur>();

            foreach (var sousDir in dg.SousDirecteurs)
            {
                

                if (sousDir is DirecteurOperation dOp)
                {
                    foreach (var chef in dOp.Chefs)
                    {
                        
                        foreach (var chauffeur in chef.Chauffeurs)
                        {
                            listeChauffeurs.Add(chauffeur);
                        }
                        
                    }
                }
               
            }
            double moyenneDeLivaison = 0;
            int cpt=0;
            foreach (var chauffeur in listeChauffeurs)
            {
                moyenneDeLivaison += chauffeur.NombreLivraisonEffectuee;
                cpt++;
            }
            
            moyenneDeLivaison = moyenneDeLivaison / cpt;
            moyenneDeLivaison = Math.Round(moyenneDeLivaison, 2);
            string listeChauffeursString =  "";
            
            var sb = new StringWriter();

            sb.WriteLine($"Liste des chauffeurs : ");
            foreach (var c in listeChauffeurs)
            {
                sb.WriteLine($"{c.Nom} {c.Prenom} ({c.NombreLivraisonEffectuee}) " );
            }

            
            sb.WriteLine();
            sb.WriteLine($"La moyenne de livraisons par chaffeur est de : { moyenneDeLivaison}");
            
            sb.WriteLine($"Nombre de chauffeurs : {listeChauffeurs.Count}" );

            
            textBoxOutput2.Clear();
            textBoxOutput2.AppendText(sb.ToString());
            textBoxOutput2.AppendText(Environment.NewLine);
        }

        private void bouttonStatistiquesVehiculePlus10000(object sender, EventArgs e)
        {
            List<Vehicule> listeVehicules = new List<Vehicule>();
            foreach (var ville in Interface.transconnect.Graphe.Noeuds.Values)
            {
                foreach (var vehicule in ville.ListeVehicules)
                {
                    listeVehicules.Add(vehicule);
                }
            }
            List<Vehicule> listeVehiculesHautKM = new List<Vehicule>();
            foreach (var v in listeVehicules)
            {
                if (v.DistanceParcourue > 1000)
                {
                    listeVehiculesHautKM.Add(v);
                }
            }
            
            var sb = new StringWriter();
            sb.WriteLine();
            foreach (var v in listeVehiculesHautKM)
            {
                sb.WriteLine($"Le véhicule immatriculé {v.Immatriculation} a fait {v.DistanceParcourue} km " );
            }
            
            
            textBoxOutput2.Clear();
            textBoxOutput2.AppendText(sb.ToString());
            textBoxOutput2.AppendText(Environment.NewLine);
        }
        


    }
}
