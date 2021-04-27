namespace gitesForm
{
    partial class FGites
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.rb_resultat = new System.Windows.Forms.RichTextBox();
            this.btn_lancerRequetes = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_requetes = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // rb_resultat
            // 
            this.rb_resultat.Location = new System.Drawing.Point(39, 135);
            this.rb_resultat.Name = "rb_resultat";
            this.rb_resultat.Size = new System.Drawing.Size(641, 256);
            this.rb_resultat.TabIndex = 1;
            this.rb_resultat.Text = "";
            // 
            // btn_lancerRequetes
            // 
            this.btn_lancerRequetes.Location = new System.Drawing.Point(574, 70);
            this.btn_lancerRequetes.Name = "btn_lancerRequetes";
            this.btn_lancerRequetes.Size = new System.Drawing.Size(49, 31);
            this.btn_lancerRequetes.TabIndex = 2;
            this.btn_lancerRequetes.Text = "OK";
            this.btn_lancerRequetes.UseVisualStyleBackColor = true;
            this.btn_lancerRequetes.Click += new System.EventHandler(this.btn_lancerRequetes_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Requêtes";
            // 
            // cb_requetes
            // 
            this.cb_requetes.FormattingEnabled = true;
            this.cb_requetes.Location = new System.Drawing.Point(39, 77);
            this.cb_requetes.Name = "cb_requetes";
            this.cb_requetes.Size = new System.Drawing.Size(511, 24);
            this.cb_requetes.TabIndex = 4;
            // 
            // FGites
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(717, 421);
            this.Controls.Add(this.cb_requetes);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_lancerRequetes);
            this.Controls.Add(this.rb_resultat);
            this.Name = "FGites";
            this.Text = "Fgites";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox rb_resultat;
        private System.Windows.Forms.Button btn_lancerRequetes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cb_requetes;
    }
}

