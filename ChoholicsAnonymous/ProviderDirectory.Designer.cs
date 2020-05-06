namespace ChoholicsAnonymous
{
    partial class ProviderDirectory
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
            this.directoryText = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // directoryText
            // 
            this.directoryText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.directoryText.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.directoryText.Location = new System.Drawing.Point(0, 0);
            this.directoryText.Name = "directoryText";
            this.directoryText.ReadOnly = true;
            this.directoryText.Size = new System.Drawing.Size(1079, 599);
            this.directoryText.TabIndex = 0;
            this.directoryText.Text = "";
            // 
            // ProviderDirectory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1079, 599);
            this.Controls.Add(this.directoryText);
            this.Name = "ProviderDirectory";
            this.Text = "Provider Directory";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox directoryText;
    }
}