namespace ProceduralMapping
{
    partial class frmMapViewer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pctCanvas = new System.Windows.Forms.PictureBox();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.cmbAlgorithm = new System.Windows.Forms.ComboBox();
            this.numGridWidth = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chkGridLink = new System.Windows.Forms.CheckBox();
            this.numGridHeight = new System.Windows.Forms.NumericUpDown();
            this.numCellHeight = new System.Windows.Forms.NumericUpDown();
            this.chkCellLink = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numCellWidth = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numSeed = new System.Windows.Forms.NumericUpDown();
            this.numGridDepth = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.numLayer = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbSolver = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.numGenParam = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtEndPoint = new System.Windows.Forms.TextBox();
            this.txtStartPoint = new System.Windows.Forms.TextBox();
            this.btnOpenFileDialog = new System.Windows.Forms.Button();
            this.diaSaveFile = new System.Windows.Forms.SaveFileDialog();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rdDispValueBytes = new System.Windows.Forms.RadioButton();
            this.txtFlatDispColor = new System.Windows.Forms.TextBox();
            this.rdDispFlatValue = new System.Windows.Forms.RadioButton();
            this.rdDispEdges = new System.Windows.Forms.RadioButton();
            this.rdDispValue = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.pctCanvas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGridWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGridHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCellHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCellWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGridDepth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLayer)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGenParam)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pctCanvas
            // 
            this.pctCanvas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pctCanvas.Location = new System.Drawing.Point(179, 12);
            this.pctCanvas.Name = "pctCanvas";
            this.pctCanvas.Size = new System.Drawing.Size(0, 438);
            this.pctCanvas.TabIndex = 0;
            this.pctCanvas.TabStop = false;
            // 
            // btnGenerate
            // 
            this.btnGenerate.Location = new System.Drawing.Point(37, 386);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(103, 25);
            this.btnGenerate.TabIndex = 1;
            this.btnGenerate.Text = "Generate";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // cmbAlgorithm
            // 
            this.cmbAlgorithm.FormattingEnabled = true;
            this.cmbAlgorithm.Location = new System.Drawing.Point(6, 19);
            this.cmbAlgorithm.Name = "cmbAlgorithm";
            this.cmbAlgorithm.Size = new System.Drawing.Size(147, 21);
            this.cmbAlgorithm.TabIndex = 2;
            this.cmbAlgorithm.SelectedValueChanged += new System.EventHandler(this.cmbAlgorithm_SelectedValueChanged);
            // 
            // numGridWidth
            // 
            this.numGridWidth.Location = new System.Drawing.Point(9, 98);
            this.numGridWidth.Maximum = new decimal(new int[] {
            5156,
            0,
            0,
            0});
            this.numGridWidth.Name = "numGridWidth";
            this.numGridWidth.Size = new System.Drawing.Size(39, 20);
            this.numGridWidth.TabIndex = 3;
            this.numGridWidth.Value = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.numGridWidth.ValueChanged += new System.EventHandler(this.numGridWidth_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Start";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Grid Size";
            // 
            // chkGridLink
            // 
            this.chkGridLink.AutoSize = true;
            this.chkGridLink.Checked = true;
            this.chkGridLink.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkGridLink.Location = new System.Drawing.Point(51, 101);
            this.chkGridLink.Margin = new System.Windows.Forms.Padding(0);
            this.chkGridLink.Name = "chkGridLink";
            this.chkGridLink.Size = new System.Drawing.Size(15, 14);
            this.chkGridLink.TabIndex = 6;
            this.chkGridLink.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chkGridLink.UseVisualStyleBackColor = true;
            // 
            // numGridHeight
            // 
            this.numGridHeight.Location = new System.Drawing.Point(69, 98);
            this.numGridHeight.Maximum = new decimal(new int[] {
            5156,
            0,
            0,
            0});
            this.numGridHeight.Name = "numGridHeight";
            this.numGridHeight.Size = new System.Drawing.Size(39, 20);
            this.numGridHeight.TabIndex = 7;
            this.numGridHeight.Value = new decimal(new int[] {
            64,
            0,
            0,
            0});
            this.numGridHeight.ValueChanged += new System.EventHandler(this.numGridHeight_ValueChanged);
            // 
            // numCellHeight
            // 
            this.numCellHeight.Location = new System.Drawing.Point(69, 137);
            this.numCellHeight.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.numCellHeight.Name = "numCellHeight";
            this.numCellHeight.Size = new System.Drawing.Size(39, 20);
            this.numCellHeight.TabIndex = 11;
            this.numCellHeight.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numCellHeight.ValueChanged += new System.EventHandler(this.numCellHeight_ValueChanged);
            // 
            // chkCellLink
            // 
            this.chkCellLink.AutoSize = true;
            this.chkCellLink.Checked = true;
            this.chkCellLink.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCellLink.Location = new System.Drawing.Point(51, 138);
            this.chkCellLink.Margin = new System.Windows.Forms.Padding(0);
            this.chkCellLink.Name = "chkCellLink";
            this.chkCellLink.Size = new System.Drawing.Size(15, 14);
            this.chkCellLink.TabIndex = 10;
            this.chkCellLink.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.chkCellLink.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 121);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Cell Size";
            // 
            // numCellWidth
            // 
            this.numCellWidth.Location = new System.Drawing.Point(9, 137);
            this.numCellWidth.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.numCellWidth.Name = "numCellWidth";
            this.numCellWidth.Size = new System.Drawing.Size(39, 20);
            this.numCellWidth.TabIndex = 8;
            this.numCellWidth.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numCellWidth.ValueChanged += new System.EventHandler(this.numCellWidth_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Seed";
            // 
            // numSeed
            // 
            this.numSeed.Location = new System.Drawing.Point(9, 59);
            this.numSeed.Name = "numSeed";
            this.numSeed.Size = new System.Drawing.Size(57, 20);
            this.numSeed.TabIndex = 13;
            this.numSeed.Value = new decimal(new int[] {
            42,
            0,
            0,
            0});
            // 
            // numGridDepth
            // 
            this.numGridDepth.Location = new System.Drawing.Point(114, 98);
            this.numGridDepth.Maximum = new decimal(new int[] {
            5156,
            0,
            0,
            0});
            this.numGridDepth.Name = "numGridDepth";
            this.numGridDepth.Size = new System.Drawing.Size(39, 20);
            this.numGridDepth.TabIndex = 15;
            this.numGridDepth.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(69, 43);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(33, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Layer";
            // 
            // numLayer
            // 
            this.numLayer.Location = new System.Drawing.Point(72, 59);
            this.numLayer.Maximum = new decimal(new int[] {
            5156,
            0,
            0,
            0});
            this.numLayer.Name = "numLayer";
            this.numLayer.Size = new System.Drawing.Size(36, 20);
            this.numLayer.TabIndex = 22;
            this.numLayer.ValueChanged += new System.EventHandler(this.numLayer_ValueChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(79, 43);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 13);
            this.label11.TabIndex = 24;
            this.label11.Text = "Goal";
            // 
            // cmbSolver
            // 
            this.cmbSolver.FormattingEnabled = true;
            this.cmbSolver.Location = new System.Drawing.Point(6, 19);
            this.cmbSolver.Name = "cmbSolver";
            this.cmbSolver.Size = new System.Drawing.Size(147, 21);
            this.cmbSolver.TabIndex = 23;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.numGenParam);
            this.groupBox1.Controls.Add(this.cmbAlgorithm);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.numSeed);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.numLayer);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.numGridHeight);
            this.groupBox1.Controls.Add(this.numCellHeight);
            this.groupBox1.Controls.Add(this.numGridDepth);
            this.groupBox1.Controls.Add(this.chkCellLink);
            this.groupBox1.Controls.Add(this.numGridWidth);
            this.groupBox1.Controls.Add(this.numCellWidth);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.chkGridLink);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(161, 168);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Algorithm";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(114, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "Param";
            // 
            // numGenParam
            // 
            this.numGenParam.Location = new System.Drawing.Point(114, 59);
            this.numGenParam.Name = "numGenParam";
            this.numGenParam.Size = new System.Drawing.Size(38, 20);
            this.numGenParam.TabIndex = 23;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtEndPoint);
            this.groupBox2.Controls.Add(this.txtStartPoint);
            this.groupBox2.Controls.Add(this.cmbSolver);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(12, 186);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(161, 95);
            this.groupBox2.TabIndex = 26;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Solver";
            // 
            // txtEndPoint
            // 
            this.txtEndPoint.Location = new System.Drawing.Point(82, 64);
            this.txtEndPoint.Name = "txtEndPoint";
            this.txtEndPoint.Size = new System.Drawing.Size(70, 20);
            this.txtEndPoint.TabIndex = 28;
            // 
            // txtStartPoint
            // 
            this.txtStartPoint.Location = new System.Drawing.Point(6, 64);
            this.txtStartPoint.Name = "txtStartPoint";
            this.txtStartPoint.Size = new System.Drawing.Size(70, 20);
            this.txtStartPoint.TabIndex = 27;
            // 
            // btnOpenFileDialog
            // 
            this.btnOpenFileDialog.Location = new System.Drawing.Point(37, 417);
            this.btnOpenFileDialog.Name = "btnOpenFileDialog";
            this.btnOpenFileDialog.Size = new System.Drawing.Size(103, 25);
            this.btnOpenFileDialog.TabIndex = 1;
            this.btnOpenFileDialog.Text = "Save Image";
            this.btnOpenFileDialog.UseVisualStyleBackColor = true;
            this.btnOpenFileDialog.Click += new System.EventHandler(this.btnOpenFileDialog_Click);
            // 
            // diaSaveFile
            // 
            this.diaSaveFile.Title = "Save As...";
            this.diaSaveFile.FileOk += new System.ComponentModel.CancelEventHandler(this.diaSaveFile_FileOk);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rdDispValueBytes);
            this.groupBox3.Controls.Add(this.txtFlatDispColor);
            this.groupBox3.Controls.Add(this.rdDispFlatValue);
            this.groupBox3.Controls.Add(this.rdDispEdges);
            this.groupBox3.Controls.Add(this.rdDispValue);
            this.groupBox3.Location = new System.Drawing.Point(12, 287);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(161, 93);
            this.groupBox3.TabIndex = 27;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Display";
            // 
            // rdDispValueBytes
            // 
            this.rdDispValueBytes.AutoSize = true;
            this.rdDispValueBytes.Location = new System.Drawing.Point(82, 20);
            this.rdDispValueBytes.Name = "rdDispValueBytes";
            this.rdDispValueBytes.Size = new System.Drawing.Size(51, 17);
            this.rdDispValueBytes.TabIndex = 4;
            this.rdDispValueBytes.TabStop = true;
            this.rdDispValueBytes.Text = "Bytes";
            this.rdDispValueBytes.UseVisualStyleBackColor = true;
            // 
            // txtFlatDispColor
            // 
            this.txtFlatDispColor.Enabled = false;
            this.txtFlatDispColor.Location = new System.Drawing.Point(55, 67);
            this.txtFlatDispColor.Name = "txtFlatDispColor";
            this.txtFlatDispColor.Size = new System.Drawing.Size(100, 20);
            this.txtFlatDispColor.TabIndex = 3;
            // 
            // rdDispFlatValue
            // 
            this.rdDispFlatValue.AutoSize = true;
            this.rdDispFlatValue.Location = new System.Drawing.Point(6, 67);
            this.rdDispFlatValue.Name = "rdDispFlatValue";
            this.rdDispFlatValue.Size = new System.Drawing.Size(42, 17);
            this.rdDispFlatValue.TabIndex = 2;
            this.rdDispFlatValue.TabStop = true;
            this.rdDispFlatValue.Text = "Flat";
            this.rdDispFlatValue.UseVisualStyleBackColor = true;
            this.rdDispFlatValue.CheckedChanged += new System.EventHandler(this.rdDispFlatValue_CheckedChanged);
            // 
            // rdDispEdges
            // 
            this.rdDispEdges.AutoSize = true;
            this.rdDispEdges.Location = new System.Drawing.Point(7, 44);
            this.rdDispEdges.Name = "rdDispEdges";
            this.rdDispEdges.Size = new System.Drawing.Size(85, 17);
            this.rdDispEdges.TabIndex = 1;
            this.rdDispEdges.Text = "Edge Values";
            this.rdDispEdges.UseVisualStyleBackColor = true;
            // 
            // rdDispValue
            // 
            this.rdDispValue.AutoSize = true;
            this.rdDispValue.Checked = true;
            this.rdDispValue.Location = new System.Drawing.Point(7, 20);
            this.rdDispValue.Name = "rdDispValue";
            this.rdDispValue.Size = new System.Drawing.Size(72, 17);
            this.rdDispValue.TabIndex = 0;
            this.rdDispValue.TabStop = true;
            this.rdDispValue.Text = "Cell Value";
            this.rdDispValue.UseVisualStyleBackColor = true;
            // 
            // frmMapViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(186, 462);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.btnOpenFileDialog);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pctCanvas);
            this.Controls.Add(this.btnGenerate);
            this.Name = "frmMapViewer";
            this.Text = "Map Viewer";
            ((System.ComponentModel.ISupportInitialize)(this.pctCanvas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGridWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGridHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCellHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCellWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGridDepth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLayer)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numGenParam)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pctCanvas;
        private System.Windows.Forms.Button btnGenerate;
        private System.Windows.Forms.ComboBox cmbAlgorithm;
        private System.Windows.Forms.NumericUpDown numGridWidth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkGridLink;
        private System.Windows.Forms.NumericUpDown numGridHeight;
        private System.Windows.Forms.NumericUpDown numCellHeight;
        private System.Windows.Forms.CheckBox chkCellLink;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numCellWidth;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numSeed;
        private System.Windows.Forms.NumericUpDown numGridDepth;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown numLayer;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmbSolver;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtEndPoint;
        private System.Windows.Forms.TextBox txtStartPoint;
        private System.Windows.Forms.Button btnOpenFileDialog;
        private System.Windows.Forms.SaveFileDialog diaSaveFile;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtFlatDispColor;
        private System.Windows.Forms.RadioButton rdDispFlatValue;
        private System.Windows.Forms.RadioButton rdDispEdges;
        private System.Windows.Forms.RadioButton rdDispValue;
        private System.Windows.Forms.RadioButton rdDispValueBytes;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numGenParam;
    }
}

