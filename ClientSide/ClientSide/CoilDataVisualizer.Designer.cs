namespace ClientSide
{
    partial class CoilDataVisualizer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea4 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend4 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CoilDataVisualizer));
            this.panel3 = new System.Windows.Forms.Panel();
            this.cht1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnCht1 = new System.Windows.Forms.Button();
            this.btnFilter = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCoil = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cht1)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.cht1);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Location = new System.Drawing.Point(12, 76);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1355, 578);
            this.panel3.TabIndex = 66;
            // 
            // cht1
            // 
            this.cht1.BackColor = System.Drawing.Color.LightSlateGray;
            chartArea4.BackColor = System.Drawing.Color.Gray;
            chartArea4.BackSecondaryColor = System.Drawing.Color.Gray;
            chartArea4.CursorX.IsUserEnabled = true;
            chartArea4.CursorX.IsUserSelectionEnabled = true;
            chartArea4.CursorY.IsUserEnabled = true;
            chartArea4.CursorY.IsUserSelectionEnabled = true;
            chartArea4.Name = "ChartArea1";
            chartArea4.Position.Auto = false;
            chartArea4.Position.Height = 80F;
            chartArea4.Position.Width = 94F;
            chartArea4.Position.X = 3F;
            chartArea4.Position.Y = 3F;
            this.cht1.ChartAreas.Add(chartArea4);
            this.cht1.Cursor = System.Windows.Forms.Cursors.Default;
            legend4.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Bottom;
            legend4.Name = "Legend1";
            this.cht1.Legends.Add(legend4);
            this.cht1.Location = new System.Drawing.Point(5, 47);
            this.cht1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cht1.Name = "cht1";
            this.cht1.Size = new System.Drawing.Size(1347, 529);
            this.cht1.TabIndex = 67;
            this.cht1.Text = "chart1";
            this.cht1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.cht_MouseUp);
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Location = new System.Drawing.Point(2548, 2);
            this.panel5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(840, 346);
            this.panel5.TabIndex = 66;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(51)))));
            this.panel6.Controls.Add(this.button1);
            this.panel6.Location = new System.Drawing.Point(5, 5);
            this.panel6.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(831, 36);
            this.panel6.TabIndex = 58;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(51)))));
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(795, -1);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(35, 37);
            this.button1.TabIndex = 58;
            this.button1.TabStop = false;
            this.button1.Tag = "1";
            this.button1.UseVisualStyleBackColor = false;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(51)))));
            this.panel4.Controls.Add(this.lblCoil);
            this.panel4.Controls.Add(this.btnCht1);
            this.panel4.Location = new System.Drawing.Point(5, 5);
            this.panel4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(1347, 36);
            this.panel4.TabIndex = 58;
            // 
            // btnCht1
            // 
            this.btnCht1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(46)))), ((int)(((byte)(51)))));
            this.btnCht1.FlatAppearance.BorderSize = 0;
            this.btnCht1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCht1.Image = ((System.Drawing.Image)(resources.GetObject("btnCht1.Image")));
            this.btnCht1.Location = new System.Drawing.Point(1308, 0);
            this.btnCht1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCht1.Name = "btnCht1";
            this.btnCht1.Size = new System.Drawing.Size(35, 37);
            this.btnCht1.TabIndex = 58;
            this.btnCht1.TabStop = false;
            this.btnCht1.Tag = "1";
            this.btnCht1.UseVisualStyleBackColor = false;
            // 
            // btnFilter
            // 
            this.btnFilter.BackColor = System.Drawing.Color.LightSlateGray;
            this.btnFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnFilter.ForeColor = System.Drawing.Color.White;
            this.btnFilter.Location = new System.Drawing.Point(1200, 31);
            this.btnFilter.Margin = new System.Windows.Forms.Padding(4);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(164, 39);
            this.btnFilter.TabIndex = 67;
            this.btnFilter.Text = "Procurar";
            this.btnFilter.UseVisualStyleBackColor = false;
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 32F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(408, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(513, 63);
            this.label1.TabIndex = 68;
            this.label1.Text = "Visualizador Gráfico";
            // 
            // lblCoil
            // 
            this.lblCoil.AutoSize = true;
            this.lblCoil.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.lblCoil.ForeColor = System.Drawing.Color.White;
            this.lblCoil.Location = new System.Drawing.Point(4, 2);
            this.lblCoil.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCoil.Name = "lblCoil";
            this.lblCoil.Size = new System.Drawing.Size(196, 36);
            this.lblCoil.TabIndex = 69;
            this.lblCoil.Text = "ABC9999999";
            // 
            // CoilDataVisualizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(87)))), ((int)(((byte)(101)))), ((int)(((byte)(115)))));
            this.ClientSize = new System.Drawing.Size(1379, 667);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnFilter);
            this.Controls.Add(this.panel3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "CoilDataVisualizer";
            this.Text = "Visualizador Gráfico";
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cht1)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnCht1;
        private System.Windows.Forms.DataVisualization.Charting.Chart cht1;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblCoil;
    }
}

