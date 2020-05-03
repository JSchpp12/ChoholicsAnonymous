namespace ChoholicsAnonymous
{
    partial class Login
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
            this.login_radManager = new System.Windows.Forms.RadioButton();
            this.login_radOperator = new System.Windows.Forms.RadioButton();
            this.login_radProvider = new System.Windows.Forms.RadioButton();
            this.panel_managerID = new System.Windows.Forms.Panel();
            this.login_managerID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel_operatorID = new System.Windows.Forms.Panel();
            this.login_operatorID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel_providerID = new System.Windows.Forms.Panel();
            this.login_providerID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.login_bttn_login = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.panel_managerID.SuspendLayout();
            this.panel_operatorID.SuspendLayout();
            this.panel_providerID.SuspendLayout();
            this.SuspendLayout();
            // 
            // login_radManager
            // 
            this.login_radManager.AutoSize = true;
            this.login_radManager.Location = new System.Drawing.Point(43, 64);
            this.login_radManager.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.login_radManager.Name = "login_radManager";
            this.login_radManager.Size = new System.Drawing.Size(85, 21);
            this.login_radManager.TabIndex = 0;
            this.login_radManager.TabStop = true;
            this.login_radManager.Tag = "manager";
            this.login_radManager.Text = "Manager";
            this.login_radManager.UseVisualStyleBackColor = true;
            this.login_radManager.CheckedChanged += new System.EventHandler(this.login_rad_CheckedChanged);
            // 
            // login_radOperator
            // 
            this.login_radOperator.AutoSize = true;
            this.login_radOperator.Location = new System.Drawing.Point(207, 64);
            this.login_radOperator.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.login_radOperator.Name = "login_radOperator";
            this.login_radOperator.Size = new System.Drawing.Size(86, 21);
            this.login_radOperator.TabIndex = 1;
            this.login_radOperator.TabStop = true;
            this.login_radOperator.Tag = "operator";
            this.login_radOperator.Text = "Operator";
            this.login_radOperator.UseVisualStyleBackColor = true;
            this.login_radOperator.CheckedChanged += new System.EventHandler(this.login_rad_CheckedChanged);
            // 
            // login_radProvider
            // 
            this.login_radProvider.AutoSize = true;
            this.login_radProvider.Location = new System.Drawing.Point(395, 64);
            this.login_radProvider.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.login_radProvider.Name = "login_radProvider";
            this.login_radProvider.Size = new System.Drawing.Size(82, 21);
            this.login_radProvider.TabIndex = 2;
            this.login_radProvider.TabStop = true;
            this.login_radProvider.Tag = "provider";
            this.login_radProvider.Text = "Provider";
            this.login_radProvider.UseVisualStyleBackColor = true;
            this.login_radProvider.CheckedChanged += new System.EventHandler(this.login_rad_CheckedChanged);
            // 
            // panel_managerID
            // 
            this.panel_managerID.Controls.Add(this.login_managerID);
            this.panel_managerID.Controls.Add(this.label1);
            this.panel_managerID.Location = new System.Drawing.Point(43, 92);
            this.panel_managerID.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel_managerID.Name = "panel_managerID";
            this.panel_managerID.Size = new System.Drawing.Size(437, 123);
            this.panel_managerID.TabIndex = 3;
            this.panel_managerID.Visible = false;
            // 
            // login_managerID
            // 
            this.login_managerID.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.login_managerID.Location = new System.Drawing.Point(101, 48);
            this.login_managerID.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.login_managerID.Name = "login_managerID";
            this.login_managerID.Size = new System.Drawing.Size(237, 29);
            this.login_managerID.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 52);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Manager ID:";
            // 
            // panel_operatorID
            // 
            this.panel_operatorID.Controls.Add(this.login_operatorID);
            this.panel_operatorID.Controls.Add(this.label2);
            this.panel_operatorID.Location = new System.Drawing.Point(43, 92);
            this.panel_operatorID.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel_operatorID.Name = "panel_operatorID";
            this.panel_operatorID.Size = new System.Drawing.Size(437, 123);
            this.panel_operatorID.TabIndex = 4;
            this.panel_operatorID.Visible = false;
            // 
            // login_operatorID
            // 
            this.login_operatorID.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.login_operatorID.Location = new System.Drawing.Point(101, 48);
            this.login_operatorID.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.login_operatorID.Name = "login_operatorID";
            this.login_operatorID.Size = new System.Drawing.Size(237, 29);
            this.login_operatorID.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(4, 52);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 19);
            this.label2.TabIndex = 0;
            this.label2.Text = "Operator ID:";
            // 
            // panel_providerID
            // 
            this.panel_providerID.Controls.Add(this.login_providerID);
            this.panel_providerID.Controls.Add(this.label3);
            this.panel_providerID.Location = new System.Drawing.Point(43, 92);
            this.panel_providerID.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel_providerID.Name = "panel_providerID";
            this.panel_providerID.Size = new System.Drawing.Size(437, 123);
            this.panel_providerID.TabIndex = 4;
            this.panel_providerID.Visible = false;
            // 
            // login_providerID
            // 
            this.login_providerID.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.login_providerID.Location = new System.Drawing.Point(101, 48);
            this.login_providerID.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.login_providerID.Name = "login_providerID";
            this.login_providerID.Size = new System.Drawing.Size(237, 29);
            this.login_providerID.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(4, 52);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 19);
            this.label3.TabIndex = 0;
            this.label3.Text = "Provider ID:";
            // 
            // login_bttn_login
            // 
            this.login_bttn_login.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.login_bttn_login.Location = new System.Drawing.Point(43, 236);
            this.login_bttn_login.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.login_bttn_login.Name = "login_bttn_login";
            this.login_bttn_login.Size = new System.Drawing.Size(112, 37);
            this.login_bttn_login.TabIndex = 8;
            this.login_bttn_login.Text = "Login";
            this.login_bttn_login.UseVisualStyleBackColor = true;
            this.login_bttn_login.Visible = false;
            this.login_bttn_login.Click += new System.EventHandler(this.login_bttn_login_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(33, 11);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(392, 45);
            this.label19.TabIndex = 9;
            this.label19.Text = "Enter Login Information";
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(541, 332);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.login_bttn_login);
            this.Controls.Add(this.login_radProvider);
            this.Controls.Add(this.login_radOperator);
            this.Controls.Add(this.login_radManager);
            this.Controls.Add(this.panel_providerID);
            this.Controls.Add(this.panel_operatorID);
            this.Controls.Add(this.panel_managerID);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Login";
            this.Text = "Login";
            this.panel_managerID.ResumeLayout(false);
            this.panel_managerID.PerformLayout();
            this.panel_operatorID.ResumeLayout(false);
            this.panel_operatorID.PerformLayout();
            this.panel_providerID.ResumeLayout(false);
            this.panel_providerID.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton login_radManager;
        private System.Windows.Forms.RadioButton login_radOperator;
        private System.Windows.Forms.RadioButton login_radProvider;
        private System.Windows.Forms.Panel panel_managerID;
        private System.Windows.Forms.TextBox login_managerID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel_operatorID;
        private System.Windows.Forms.TextBox login_operatorID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel_providerID;
        private System.Windows.Forms.TextBox login_providerID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button login_bttn_login;
        private System.Windows.Forms.Label label19;
    }
}