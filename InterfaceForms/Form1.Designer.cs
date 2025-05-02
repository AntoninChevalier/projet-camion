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

        // Main menu
        private Button btnGestionEffectif;
        private Button btnInfoClient;
        private Button btnGestionCommande;
        private Button btnGestionLogistique;

        // Gestion Effectif
        private Button btnRefreshHierarchie;
        private Button btnAjouterSalarie;
        private Button btnSupprimerSalarie;
        private Button btnRechercheInfoSalarie;
        private Button btnTrierSousDirecteurs;
        private Button btnBackGestionEffectif;
        private TreeView treeView1;

        // Info Client
        private Button btnListeClients;
        private Button btnRechercheClient;
        private Button btnBackInfoClient;
        private DataGridView dgvClients;

        // Gestion Commande
        private Button btnListeCommandesFuture;
        private Button btnListeCommandesPassees;

        private Button btnAjoutCommande;

        private Button btnTraiterCommandes;
        private Button btnBackGestionCommande;
        private DataGridView dgvCommandes;

        // Gestion Logistique
        private Button btnAfficherGraphe;
        private Button btnCalculerDistance;

        private Button btnDeplaceVehicule;
        private Button btnBackGestionLogistique;
        private PictureBox pictureBoxGraph;
        private TextBox textBoxOutput;

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

            // Main menu panel
            panelMainMenu = new Panel { Dock = DockStyle.Fill };
            btnGestionEffectif = new Button { Text = "Gestion Effectif", Dock = DockStyle.Top, Height = 75 };
            btnInfoClient = new Button { Text = "Info Client", Dock = DockStyle.Top, Height = 75 };
            btnGestionCommande = new Button { Text = "Gestion Commande", Dock = DockStyle.Top, Height = 75 };
            btnGestionLogistique = new Button { Text = "Gestion Logistique", Dock = DockStyle.Top, Height = 75 };
            btnGestionEffectif.Click += btnGestionEffectif_Click;
            btnInfoClient.Click += btnInfoClient_Click;
            btnGestionCommande.Click += btnGestionCommande_Click;
            btnGestionLogistique.Click += btnGestionLogistique_Click;
            panelMainMenu.Controls.AddRange(new Control[] { btnGestionLogistique, btnGestionCommande, btnInfoClient, btnGestionEffectif });

            // Gestion Effectif panel
            panelGestionEffectif = new Panel { Dock = DockStyle.Fill, Visible = false };
            btnRefreshHierarchie = new Button { Text = "Rafraîchir la hiérarchie", Dock = DockStyle.Top, Height = 40 };
            btnAjouterSalarie = new Button { Text = "Ajouter un salarié", Dock = DockStyle.Top , Height = 40 };
            btnSupprimerSalarie = new Button { Text = "Supprimer un salarié", Dock = DockStyle.Top, Height = 40 };
            btnRechercheInfoSalarie = new Button { Text = "Recherche info salarié", Dock = DockStyle.Top , Height = 40 };
            btnTrierSousDirecteurs = new Button { Text = "Trier les sous-directeurs", Dock = DockStyle.Top , Height = 40 };
            btnBackGestionEffectif = new Button { Text = "Retour", Dock = DockStyle.Bottom, Height = 40 };
            btnRefreshHierarchie.Click += btnRefreshHierarchie_Click;
            btnAjouterSalarie.Click += btnAjouterSalarie_Click;
            btnSupprimerSalarie.Click += btnSupprimerSalarie_Click;
            btnRechercheInfoSalarie.Click += btnRechercheInfoSalarie_Click;
            btnTrierSousDirecteurs.Click += btnTrierSousDirecteurs_Click;
            btnBackGestionEffectif.Click += (s, e) => ShowPanel(panelMainMenu);
            panelGestionEffectif.Controls.Add(treeView1);
            panelGestionEffectif.Controls.Add(btnTrierSousDirecteurs);
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
            dgvClients = new DataGridView { Dock = DockStyle.Top, Visible = false, AutoGenerateColumns = true };
            btnListeClients.Click += btnListeClients_Click;
            btnRechercheClient.Click += btnRechercheClient_Click;
            btnBackInfoClient.Click += (s, e) => ShowPanel(panelMainMenu);
            panelInfoClient.Controls.AddRange(new Control[] { btnRechercheClient,dgvClients, btnListeClients, btnBackInfoClient });

            // Gestion Commande panel
            panelGestionCommande = new Panel { Dock = DockStyle.Fill, Visible = false };
            btnListeCommandesFuture = new Button { Text = "Commandes futures", Dock = DockStyle.Top , Height = 40 };
            btnListeCommandesPassees = new Button { Text = "Commandes passées", Dock = DockStyle.Top , Height = 40 };
            btnAjoutCommande = new Button { Text = "Ajouter une commande", Dock = DockStyle.Top , Height = 40 };
            btnTraiterCommandes = new Button { Text = "Traiter les commandes", Dock = DockStyle.Top , Height = 40 };
            btnBackGestionCommande = new Button { Text = "Retour", Dock = DockStyle.Bottom, Height = 40 };
            dgvCommandes = new DataGridView { Dock = DockStyle.Bottom, Visible = false, AutoGenerateColumns = true };
            btnListeCommandesFuture.Click += btnListeCommandesFuture_Click;
            btnListeCommandesPassees.Click += btnListeCommandesPassees_Click;
            btnAjoutCommande.Click += btnAjoutCommande_Click;
            btnTraiterCommandes.Click += btnTraiterCommande_Click;
            btnBackGestionCommande.Click += (s, e) => ShowPanel(panelMainMenu);
            panelGestionCommande.Controls.AddRange(new Control[] { btnListeCommandesPassees,dgvCommandes, btnListeCommandesFuture,btnAjoutCommande,btnTraiterCommandes, btnBackGestionCommande });

            // Gestion Logistique panel
            panelGestionLogistique = new Panel { Dock = DockStyle.Fill, Visible = false };
            btnAfficherGraphe = new Button { Text = "Afficher le graphe", Dock = DockStyle.Top , Height = 40 };
            btnCalculerDistance = new Button { Text = "Calculer distance", Dock = DockStyle.Top , Height = 40 };
            btnDeplaceVehicule = new Button { Text = "Déplacer véhicule", Dock = DockStyle.Top , Height = 40 };
            btnBackGestionLogistique = new Button { Text = "Retour", Dock = DockStyle.Bottom, Height = 40 };
            btnAfficherGraphe.Click += btnAfficherGraphe_Click;
            btnCalculerDistance.Click += btnCalculerDistance_Click;
            btnDeplaceVehicule.Click += btnDeplaceVehicule_Click;
            btnBackGestionLogistique.Click += (s, e) => ShowPanel(panelMainMenu);
            panelGestionLogistique.Controls.Add(textBoxOutput);
            panelGestionLogistique.Controls.Add(pictureBoxGraph);
            panelGestionLogistique.Controls.Add(btnCalculerDistance);
            panelGestionLogistique.Controls.Add(btnDeplaceVehicule);
            panelGestionLogistique.Controls.Add(btnAfficherGraphe);
            panelGestionLogistique.Controls.Add(btnBackGestionLogistique);

            // Form settings and control hierarchy
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1303, 633);
            this.Controls.Add(panelMainMenu);
            this.Controls.Add(panelGestionEffectif);
            this.Controls.Add(panelInfoClient);
            this.Controls.Add(panelGestionCommande);
            this.Controls.Add(panelGestionLogistique);
            this.Text = "TransConnect Interface";
        }
    }
}