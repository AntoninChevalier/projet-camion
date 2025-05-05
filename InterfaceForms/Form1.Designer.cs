namespace InterfaceForms
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        private Panel panelMainMenu;
        private Panel panelGestionEffectif;
        private Panel panelInfoClient;
        private Panel panelGestionCommande;
        private Panel panelGestionLogistique;

        private Panel panelStatistiques;

        // Main menu
        private Button btnGestionEffectif;
        private Button btnInfoClient;
        private Button btnGestionCommande;
        private Button btnGestionLogistique;

        private Button btnStatistiques;

        // Gestion Effectif
        private Button btnRefreshHierarchie;
        private Button btnAjouterSalarie;
        private Button btnSupprimerSalarie;
        private Button btnRechercheInfoSalarie;
        private Button btnTrierSousDirecteurs;
        private Button btnModifierSalarie;
        private Button btnBackGestionEffectif;
        private TreeView treeView1;

        // Info Client
        private Button btnListeClients;
        private Button btnRechercheClient;
        private Button btnAjouterClient;
        private Button btnModifierClient;
        private Button btnBackInfoClient;
        private DataGridView dgvClients;

        // Gestion Commande
        private Button btnListeCommandesFuture;
        private Button btnListeCommandesPassees;

        private Button btnAjoutCommande;

        private Button btnTraiterCommandes;

        private Button btnModifierCommande;
        private Button btnSupprimerCommande;



        private Button btnBackGestionCommande;
        private DataGridView dgvCommandes;

        // Gestion Logistique
        private Button btnAfficherGraphe;
        

        private Button btnDeplaceVehicule;
        private Button btnBackGestionLogistique;
        private PictureBox pictureBoxGraph;
        private TextBox textBoxOutput;

        // Statistiques
        

        private Button btnStatistiquesClient;

        private Button btnStaistiquesChauffeur;

        private Button btnStaistiquesListeCommandesFuturePeriode;

        private Button  btnStatistiquesListeCommandesParClinet;

        private Button btnStatistiquesVehiculePlus10000;
        private Button btnBackStatistiques;

        private TextBox textBoxOutput2;

        private DataGridView dgvStatistiques;



        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            // Shared controls
            treeView1 = new TreeView { Dock = DockStyle.Fill };
            pictureBoxGraph = new PictureBox { Dock = DockStyle.Top, Height = 700 , SizeMode = PictureBoxSizeMode.Zoom };
            textBoxOutput = new TextBox { Multiline = true, Dock = DockStyle.Fill, ScrollBars = ScrollBars.Vertical };
            textBoxOutput2 = new TextBox { Multiline = true, Dock = DockStyle.Bottom,Height=600, ScrollBars = ScrollBars.Vertical };

            // Main menu panel
            panelMainMenu = new Panel { Dock = DockStyle.Fill };
            btnGestionEffectif = new Button { Text = "Gestion Effectif", Dock = DockStyle.Top, Height = 75 };
            btnInfoClient = new Button { Text = "Info Client", Dock = DockStyle.Top, Height = 75 };
            
            btnGestionCommande = new Button { Text = "Gestion Commande", Dock = DockStyle.Top, Height = 75 };
            btnGestionLogistique = new Button { Text = "Gestion Logistique", Dock = DockStyle.Top, Height = 75 };
            btnStatistiques = new Button { Text = "Statistiques", Dock = DockStyle.Top, Height = 75 };

            btnGestionEffectif.Click += btnGestionEffectif_Click;
            btnInfoClient.Click += btnInfoClient_Click;
            btnStatistiques.Click += btnStatistiques_Click;
            
            btnGestionCommande.Click += btnGestionCommande_Click;
            btnGestionLogistique.Click += btnGestionLogistique_Click;
            panelMainMenu.Controls.AddRange(new Control[] { btnStatistiques,btnGestionLogistique, btnGestionCommande, btnInfoClient, btnGestionEffectif });

            // Gestion Effectif panel
            panelGestionEffectif = new Panel { Dock = DockStyle.Fill, Visible = false };
            btnRefreshHierarchie = new Button { Text = "Rafraîchir la hiérarchie", Dock = DockStyle.Top, Height = 40 };
            btnAjouterSalarie = new Button { Text = "Ajouter un salarié", Dock = DockStyle.Top , Height = 40 };
            btnSupprimerSalarie = new Button { Text = "Supprimer un salarié", Dock = DockStyle.Top, Height = 40 };
            btnRechercheInfoSalarie = new Button { Text = "Recherche info salarié", Dock = DockStyle.Top , Height = 40 };
            btnTrierSousDirecteurs = new Button { Text = "Trier les sous-directeurs par salaire décroissant", Dock = DockStyle.Top , Height = 40 };
            btnModifierSalarie = new Button { Text = "Modifier un salarié", Dock = DockStyle.Top, Height = 40 };
            btnBackGestionEffectif = new Button { Text = "Retour", Dock = DockStyle.Bottom, Height = 40 };
            btnRefreshHierarchie.Click += btnRefreshHierarchie_Click;
            btnAjouterSalarie.Click += btnAjouterSalarie_Click;
            btnSupprimerSalarie.Click += btnSupprimerSalarie_Click;
            btnRechercheInfoSalarie.Click += btnRechercheInfoSalarie_Click;
            btnTrierSousDirecteurs.Click += btnTrierSousDirecteurs_Click;
            btnModifierSalarie.Click += btnModifierSalarie_Click;
            btnBackGestionEffectif.Click += (s, e) => ShowPanel(panelMainMenu);
            panelGestionEffectif.Controls.Add(treeView1);
            panelGestionEffectif.Controls.Add(btnTrierSousDirecteurs);
            panelGestionEffectif.Controls.Add(btnModifierSalarie);
            panelGestionEffectif.Controls.Add(btnRechercheInfoSalarie);
            panelGestionEffectif.Controls.Add(btnSupprimerSalarie);
            panelGestionEffectif.Controls.Add(btnAjouterSalarie);
            panelGestionEffectif.Controls.Add(btnRefreshHierarchie);
            panelGestionEffectif.Controls.Add(btnBackGestionEffectif);

            // Info Client panel
            panelInfoClient = new Panel { Dock = DockStyle.Fill, Visible = false };
            btnListeClients = new Button { Text = "Liste des clients", Dock = DockStyle.Top , Height = 40 };
            btnRechercheClient = new Button { Text = "Recherche client", Dock = DockStyle.Top , Height = 40 };
            btnBackInfoClient = new Button { Text = "Retour", Dock = DockStyle.Bottom, Height = 40 };
            btnAjouterClient = new Button { Text = "Ajouter client", Dock = DockStyle.Top, Height = 40 };
            btnModifierClient = new Button { Text = "Modifier client", Dock = DockStyle.Top, Height = 40 };
            dgvClients = new DataGridView { Dock = DockStyle.Top, Visible = false, AutoGenerateColumns = true };
            btnListeClients.Click += btnListeClients_Click;
            btnRechercheClient.Click += btnRechercheClient_Click;
            btnAjouterClient.Click += btnAjouterClient_Click;
            btnModifierClient.Click += btnModifierClient_Click;
            btnBackInfoClient.Click += (s, e) => ShowPanel(panelMainMenu);
            panelInfoClient.Controls.AddRange(new Control[] { btnRechercheClient,dgvClients, btnListeClients,btnAjouterClient,btnModifierClient, btnBackInfoClient });

            // Gestion Commande panel
            panelGestionCommande = new Panel { Dock = DockStyle.Fill, Visible = false };
            btnListeCommandesFuture = new Button { Text = "Commandes futures", Dock = DockStyle.Top , Height = 40 };
            btnListeCommandesPassees = new Button { Text = "Commandes passées", Dock = DockStyle.Top , Height = 40 };
            btnAjoutCommande = new Button { Text = "Ajouter une commande", Dock = DockStyle.Top , Height = 40 };
            btnTraiterCommandes = new Button { Text = "Traiter les commandes", Dock = DockStyle.Top , Height = 40 };
            btnBackGestionCommande = new Button { Text = "Retour", Dock = DockStyle.Bottom, Height = 40 };
            btnModifierCommande = new Button { Text = "Modifier une commande", Dock = DockStyle.Top , Height = 40 };
            btnSupprimerCommande = new Button { Text = "Supprimer une commande", Dock = DockStyle.Top , Height = 40 };

            dgvCommandes = new DataGridView { Dock = DockStyle.Bottom,Height =300, Visible = false, AutoGenerateColumns = true };
            btnListeCommandesFuture.Click += btnListeCommandesFuture_Click;
            btnListeCommandesPassees.Click += btnListeCommandesPassees_Click;
            btnModifierCommande.Click += btnModifierCommande_Click;
            btnAjoutCommande.Click += btnAjoutCommande_Click;
            btnSupprimerCommande.Click += btnSupprimerCommande_Click;

            btnTraiterCommandes.Click += btnTraiterCommande_Click;
            btnBackGestionCommande.Click += (s, e) => ShowPanel(panelMainMenu);
            panelGestionCommande.Controls.AddRange(new Control[] { btnListeCommandesPassees,dgvCommandes, btnListeCommandesFuture,btnSupprimerCommande,btnAjoutCommande,btnModifierCommande ,btnTraiterCommandes, btnBackGestionCommande });

            // Gestion Logistique panel
            panelGestionLogistique = new Panel { Dock = DockStyle.Fill, Visible = false };
            btnAfficherGraphe = new Button { Text = "Afficher le graphe", Dock = DockStyle.Top , Height = 40 };
            
            btnDeplaceVehicule = new Button { Text = "Déplacer véhicule", Dock = DockStyle.Top , Height = 40 };
            btnBackGestionLogistique = new Button { Text = "Retour", Dock = DockStyle.Bottom, Height = 40 };
            btnAfficherGraphe.Click += btnAfficherGraphe_Click;
           
            btnDeplaceVehicule.Click += btnDeplaceVehicule_Click;
            btnBackGestionLogistique.Click += (s, e) => ShowPanel(panelMainMenu);
            panelGestionLogistique.Controls.Add(textBoxOutput);
            panelGestionLogistique.Controls.Add(pictureBoxGraph);
            
            panelGestionLogistique.Controls.Add(btnDeplaceVehicule);
            panelGestionLogistique.Controls.Add(btnAfficherGraphe);
            panelGestionLogistique.Controls.Add(btnBackGestionLogistique);

            // Statistiques panel
            panelStatistiques = new Panel { Dock = DockStyle.Fill, Visible = false };

            btnBackStatistiques = new Button { Text = "Retour", Dock = DockStyle.Bottom, Height = 40 };
            btnStatistiquesClient = new Button { Text = "Statistiques Client", Dock = DockStyle.Top , Height = 40 };
            btnStatistiquesVehiculePlus10000 = new Button { Text = "Statistiques Véhicule + 10000", Dock = DockStyle.Top , Height = 40 };
            btnStaistiquesListeCommandesFuturePeriode = new Button { Text = "Statistiques Commandes Futures par période", Dock = DockStyle.Top , Height = 40 };
            btnStatistiquesListeCommandesParClinet = new Button { Text = "Statistiques Commandes par Client", Dock = DockStyle.Top , Height = 40 };
            btnStaistiquesChauffeur = new Button { Text = "Statistiques Chauffeur", Dock = DockStyle.Top , Height = 40 };
            dgvStatistiques = new DataGridView { Dock = DockStyle.Top,Height =400, Visible = false, AutoGenerateColumns = true };

            btnStatistiquesClient.Click += btnStatistiquesClient_Click;
            btnStatistiquesVehiculePlus10000.Click += btnStatistiquesVehiculePlus10000_Click;
            btnStaistiquesListeCommandesFuturePeriode.Click += btnStatistiquesListeCommandesFuturePeriode_Click;
            btnStatistiquesListeCommandesParClinet.Click += btnStatistiquesListeCommandesParClinet_Click;
            btnStaistiquesChauffeur.Click += btnStatistiquesChauffeur_Click;
            btnBackStatistiques.Click += (s, e) => ShowPanel(panelMainMenu);
            
            panelStatistiques.Controls.Add(dgvStatistiques);
            panelStatistiques.Controls.Add(btnStatistiquesClient);
            panelStatistiques.Controls.Add(btnStatistiquesVehiculePlus10000);
            panelStatistiques.Controls.Add(btnStaistiquesListeCommandesFuturePeriode);
            panelStatistiques.Controls.Add(btnStatistiquesListeCommandesParClinet);
            panelStatistiques.Controls.Add(btnStaistiquesChauffeur);
            
            panelStatistiques.Controls.Add(textBoxOutput2);
            panelStatistiques.Controls.Add(btnBackStatistiques);


            // Form settings and control hierarchy
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1303, 633);
            this.Controls.Add(panelMainMenu);
            this.Controls.Add(panelGestionEffectif);
            this.Controls.Add(panelInfoClient);
            this.Controls.Add(panelGestionCommande);
            this.Controls.Add(panelGestionLogistique);
            this.Controls.Add(panelStatistiques);
            this.Text = "TransConnect Interface";
        }
    }
}