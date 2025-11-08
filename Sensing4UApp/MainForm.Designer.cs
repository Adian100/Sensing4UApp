namespace Sensing4UApp
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuMain = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            menuItemLoad = new ToolStripMenuItem();
            menuItemSave = new ToolStripMenuItem();
            panelTopControls = new FlowLayoutPanel();
            labelLow = new Label();
            textBoxLow = new TextBox();
            labelHigh = new Label();
            textBoxHigh = new TextBox();
            buttonSetBounds = new Button();
            labelSearch = new Label();
            textBoxSearch = new TextBox();
            buttonSearch = new Button();
            buttonPreviousDataset = new Button();
            buttonNextDataset = new Button();
            labelAverage = new Label();
            statusStripMain = new StatusStrip();
            toolStripStatusLabelMessage = new ToolStripStatusLabel();
            dataGridViewSensors = new DataGridView();
            menuMain.SuspendLayout();
            panelTopControls.SuspendLayout();
            statusStripMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewSensors).BeginInit();
            SuspendLayout();
            // 
            // menuMain
            // 
            menuMain.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            menuMain.Location = new Point(0, 0);
            menuMain.Name = "menuMain";
            menuMain.Size = new Size(1013, 24);
            menuMain.TabIndex = 0;
            menuMain.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { menuItemLoad, menuItemSave });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // menuItemLoad
            // 
            menuItemLoad.Name = "menuItemLoad";
            menuItemLoad.Size = new Size(109, 22);
            menuItemLoad.Text = "Load...";
            menuItemLoad.Click += MenuItemLoad_Click;
            // 
            // menuItemSave
            // 
            menuItemSave.Name = "menuItemSave";
            menuItemSave.Size = new Size(109, 22);
            menuItemSave.Text = "Save...";
            menuItemSave.Click += MenuItemSave_Click;
            // 
            // panelTopControls
            // 
            panelTopControls.AutoSize = true;
            panelTopControls.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panelTopControls.Controls.Add(labelLow);
            panelTopControls.Controls.Add(textBoxLow);
            panelTopControls.Controls.Add(labelHigh);
            panelTopControls.Controls.Add(textBoxHigh);
            panelTopControls.Controls.Add(buttonSetBounds);
            panelTopControls.Controls.Add(labelSearch);
            panelTopControls.Controls.Add(textBoxSearch);
            panelTopControls.Controls.Add(buttonSearch);
            panelTopControls.Controls.Add(buttonPreviousDataset);
            panelTopControls.Controls.Add(buttonNextDataset);
            panelTopControls.Controls.Add(labelAverage);
            panelTopControls.Dock = DockStyle.Top;
            panelTopControls.Location = new Point(0, 24);
            panelTopControls.Name = "panelTopControls";
            panelTopControls.Padding = new Padding(8);
            panelTopControls.Size = new Size(1013, 47);
            panelTopControls.TabIndex = 1;
            panelTopControls.WrapContents = false;
            // 
            // labelLow
            // 
            labelLow.AutoSize = true;
            labelLow.Location = new Point(11, 8);
            labelLow.Name = "labelLow";
            labelLow.Padding = new Padding(0, 10, 4, 0);
            labelLow.Size = new Size(36, 25);
            labelLow.TabIndex = 0;
            labelLow.Text = "Low:";
            // 
            // textBoxLow
            // 
            textBoxLow.Location = new Point(50, 16);
            textBoxLow.Margin = new Padding(0, 8, 8, 0);
            textBoxLow.Name = "textBoxLow";
            textBoxLow.Size = new Size(80, 23);
            textBoxLow.TabIndex = 1;
            // 
            // labelHigh
            // 
            labelHigh.AutoSize = true;
            labelHigh.Location = new Point(141, 8);
            labelHigh.Name = "labelHigh";
            labelHigh.Padding = new Padding(8, 10, 4, 0);
            labelHigh.Size = new Size(48, 25);
            labelHigh.TabIndex = 2;
            labelHigh.Text = "High:";
            // 
            // textBoxHigh
            // 
            textBoxHigh.Location = new Point(192, 16);
            textBoxHigh.Margin = new Padding(0, 8, 8, 0);
            textBoxHigh.Name = "textBoxHigh";
            textBoxHigh.Size = new Size(80, 23);
            textBoxHigh.TabIndex = 3;
            // 
            // buttonSetBounds
            // 
            buttonSetBounds.Location = new Point(284, 14);
            buttonSetBounds.Margin = new Padding(4, 6, 8, 0);
            buttonSetBounds.Name = "buttonSetBounds";
            buttonSetBounds.Size = new Size(75, 23);
            buttonSetBounds.TabIndex = 4;
            buttonSetBounds.Text = "Set Bounds";
            buttonSetBounds.UseVisualStyleBackColor = true;
            buttonSetBounds.Click += ButtonSetBounds_Click;
            // 
            // labelSearch
            // 
            labelSearch.AutoSize = true;
            labelSearch.Location = new Point(370, 8);
            labelSearch.Name = "labelSearch";
            labelSearch.Padding = new Padding(16, 10, 4, 0);
            labelSearch.Size = new Size(58, 25);
            labelSearch.TabIndex = 5;
            labelSearch.Text = "Label:";
            // 
            // textBoxSearch
            // 
            textBoxSearch.Location = new Point(431, 16);
            textBoxSearch.Margin = new Padding(0, 8, 8, 0);
            textBoxSearch.Name = "textBoxSearch";
            textBoxSearch.Size = new Size(180, 23);
            textBoxSearch.TabIndex = 6;
            // 
            // buttonSearch
            // 
            buttonSearch.Location = new Point(623, 14);
            buttonSearch.Margin = new Padding(4, 6, 8, 0);
            buttonSearch.Name = "buttonSearch";
            buttonSearch.Size = new Size(75, 23);
            buttonSearch.TabIndex = 7;
            buttonSearch.Text = "Find";
            buttonSearch.UseVisualStyleBackColor = true;
            buttonSearch.Click += ButtonSearch_Click;
            // 
            // buttonPreviousDataset
            // 
            buttonPreviousDataset.Location = new Point(710, 14);
            buttonPreviousDataset.Margin = new Padding(4, 6, 8, 0);
            buttonPreviousDataset.Name = "buttonPreviousDataset";
            buttonPreviousDataset.Size = new Size(75, 23);
            buttonPreviousDataset.TabIndex = 8;
            buttonPreviousDataset.Text = "◀ Prev";
            buttonPreviousDataset.UseVisualStyleBackColor = true;
            buttonPreviousDataset.Click += ButtonPreviousDataset_Click;
            // 
            // buttonNextDataset
            // 
            buttonNextDataset.Location = new Point(797, 14);
            buttonNextDataset.Margin = new Padding(4, 6, 8, 0);
            buttonNextDataset.Name = "buttonNextDataset";
            buttonNextDataset.Size = new Size(75, 23);
            buttonNextDataset.TabIndex = 9;
            buttonNextDataset.Text = "Next ▶";
            buttonNextDataset.UseVisualStyleBackColor = true;
            buttonNextDataset.Click += ButtonNextDataset_Click;
            // 
            // labelAverage
            // 
            labelAverage.AutoSize = true;
            labelAverage.Location = new Point(883, 8);
            labelAverage.Name = "labelAverage";
            labelAverage.Padding = new Padding(16, 10, 0, 0);
            labelAverage.Size = new Size(78, 25);
            labelAverage.TabIndex = 10;
            labelAverage.Text = "Average: –";
            // 
            // statusStripMain
            // 
            statusStripMain.Items.AddRange(new ToolStripItem[] { toolStripStatusLabelMessage });
            statusStripMain.Location = new Point(0, 428);
            statusStripMain.Name = "statusStripMain";
            statusStripMain.Size = new Size(1013, 22);
            statusStripMain.TabIndex = 2;
            statusStripMain.Text = "statusStrip1";
            // 
            // toolStripStatusLabelMessage
            // 
            toolStripStatusLabelMessage.Name = "toolStripStatusLabelMessage";
            toolStripStatusLabelMessage.Size = new Size(998, 17);
            toolStripStatusLabelMessage.Spring = true;
            toolStripStatusLabelMessage.Text = "Ready";
            // 
            // dataGridViewSensors
            // 
            dataGridViewSensors.AllowUserToAddRows = false;
            dataGridViewSensors.AllowUserToDeleteRows = false;
            dataGridViewSensors.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewSensors.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewSensors.Dock = DockStyle.Fill;
            dataGridViewSensors.Location = new Point(0, 71);
            dataGridViewSensors.Name = "dataGridViewSensors";
            dataGridViewSensors.ReadOnly = true;
            dataGridViewSensors.RowHeadersVisible = false;
            dataGridViewSensors.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dataGridViewSensors.Size = new Size(1013, 357);
            dataGridViewSensors.TabIndex = 3;
            dataGridViewSensors.CellFormatting += DataGridViewSensors_CellFormatting;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1013, 450);
            Controls.Add(dataGridViewSensors);
            Controls.Add(statusStripMain);
            Controls.Add(panelTopControls);
            Controls.Add(menuMain);
            MainMenuStrip = menuMain;
            Name = "MainForm";
            Text = "Sensing4U - Main Window";
            menuMain.ResumeLayout(false);
            menuMain.PerformLayout();
            panelTopControls.ResumeLayout(false);
            panelTopControls.PerformLayout();
            statusStripMain.ResumeLayout(false);
            statusStripMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewSensors).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuMain;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem menuItemLoad;
        private ToolStripMenuItem menuItemSave;
        private FlowLayoutPanel panelTopControls;
        private Label labelLow;
        private TextBox textBoxLow;
        private Label labelHigh;
        private TextBox textBoxHigh;
        private Button buttonSetBounds;
        private Label labelSearch;
        private TextBox textBoxSearch;
        private Button buttonSearch;
        private Button buttonPreviousDataset;
        private Button buttonNextDataset;
        private Label labelAverage;
        private StatusStrip statusStripMain;
        private ToolStripStatusLabel toolStripStatusLabelMessage;
        private DataGridView dataGridViewSensors;
    }
}
