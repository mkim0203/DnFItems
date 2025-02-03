namespace MySetItem
{
    partial class Main
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.cbServer = new System.Windows.Forms.ComboBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblStat = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 13.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(49, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(172, 47);
            this.label1.TabIndex = 0;
            this.label1.Text = "캐릭터 명";
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("맑은 고딕", 13.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.txtName.Location = new System.Drawing.Point(222, 38);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(268, 54);
            this.txtName.TabIndex = 1;
            // 
            // cbServer
            // 
            this.cbServer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbServer.Font = new System.Drawing.Font("맑은 고딕", 13.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cbServer.FormattingEnabled = true;
            this.cbServer.Items.AddRange(new object[] {
            "카인",
            "디레지에",
            "시로코",
            "프레이",
            "카시야스",
            "힐더",
            "안톤",
            "바칼"});
            this.cbServer.Location = new System.Drawing.Point(512, 38);
            this.cbServer.Name = "cbServer";
            this.cbServer.Size = new System.Drawing.Size(189, 55);
            this.cbServer.TabIndex = 2;
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("맑은 고딕", 13.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnSearch.Location = new System.Drawing.Point(717, 34);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(128, 62);
            this.btnSearch.TabIndex = 3;
            this.btnSearch.Text = "조회";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblStat);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 114);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(894, 100);
            this.panel1.TabIndex = 4;
            // 
            // lblStat
            // 
            this.lblStat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStat.Font = new System.Drawing.Font("맑은 고딕", 13.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblStat.Location = new System.Drawing.Point(0, 0);
            this.lblStat.Name = "lblStat";
            this.lblStat.Size = new System.Drawing.Size(894, 100);
            this.lblStat.TabIndex = 0;
            this.lblStat.Text = "Nugget - 누깃";
            this.lblStat.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cbServer);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.btnSearch);
            this.panel2.Controls.Add(this.txtName);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(894, 114);
            this.panel2.TabIndex = 5;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 214);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Text = "중천 캐릭터 기준 세트템 조회";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.ComboBox cbServer;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblStat;
        private System.Windows.Forms.Panel panel2;
    }
}