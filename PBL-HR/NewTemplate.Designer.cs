namespace PBL_HR {
    partial class NewTemplate {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose (bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent () {
            this.Editor = new System.Windows.Forms.RichTextBox();
            this.NewTemplateLabel = new System.Windows.Forms.Label();
            this.VariablesLabel = new System.Windows.Forms.Label();
            this.OKButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Editor
            // 
            this.Editor.Location = new System.Drawing.Point(13, 59);
            this.Editor.Name = "Editor";
            this.Editor.Size = new System.Drawing.Size(556, 252);
            this.Editor.TabIndex = 0;
            this.Editor.Text = "";
            // 
            // NewTemplateLabel
            // 
            this.NewTemplateLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.NewTemplateLabel.AutoSize = true;
            this.NewTemplateLabel.Location = new System.Drawing.Point(10, 9);
            this.NewTemplateLabel.Name = "NewTemplateLabel";
            this.NewTemplateLabel.Size = new System.Drawing.Size(154, 13);
            this.NewTemplateLabel.TabIndex = 1;
            this.NewTemplateLabel.Text = "Please Define a New Template";
            this.NewTemplateLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // VariablesLabel
            // 
            this.VariablesLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.VariablesLabel.AutoSize = true;
            this.VariablesLabel.Location = new System.Drawing.Point(10, 32);
            this.VariablesLabel.Name = "VariablesLabel";
            this.VariablesLabel.Size = new System.Drawing.Size(203, 13);
            this.VariablesLabel.TabIndex = 2;
            this.VariablesLabel.Text = "Use The Variables @TITLE and @BODY";
            this.VariablesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OKButton
            // 
            this.OKButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKButton.Location = new System.Drawing.Point(494, 317);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(75, 23);
            this.OKButton.TabIndex = 3;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = true;
            // 
            // CancelButton
            // 
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(12, 317);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 4;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            // 
            // NewTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 352);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.VariablesLabel);
            this.Controls.Add(this.NewTemplateLabel);
            this.Controls.Add(this.Editor);
            this.Name = "NewTemplate";
            this.Text = "New Template";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.RichTextBox Editor;
        private System.Windows.Forms.Label NewTemplateLabel;
        private System.Windows.Forms.Label VariablesLabel;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.Button CancelButton;
    }
}