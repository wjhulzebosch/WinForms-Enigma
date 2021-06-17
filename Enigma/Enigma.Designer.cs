namespace Enigma
{
    partial class EnigmaForm
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
            this.btnClear = new System.Windows.Forms.Button();
            this.btnOpenSettings = new System.Windows.Forms.Button();
            this.txbInput = new System.Windows.Forms.TextBox();
            this.txbOutput = new System.Windows.Forms.TextBox();
            this.btnPasteInput = new System.Windows.Forms.Button();
            this.btnCopyInput = new System.Windows.Forms.Button();
            this.btnDecodeInput = new System.Windows.Forms.Button();
            this.btnCopyOutput = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(666, 424);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(82, 32);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnClear_Click);
            // 
            // btnOpenSettings
            // 
            this.btnOpenSettings.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnOpenSettings.Location = new System.Drawing.Point(754, 424);
            this.btnOpenSettings.Name = "btnOpenSettings";
            this.btnOpenSettings.Size = new System.Drawing.Size(34, 32);
            this.btnOpenSettings.TabIndex = 6;
            this.btnOpenSettings.Text = "⚙";
            this.btnOpenSettings.UseVisualStyleBackColor = true;
            this.btnOpenSettings.Click += new System.EventHandler(this.btnOpenSettings_Click);
            // 
            // txbInput
            // 
            this.txbInput.Location = new System.Drawing.Point(22, 260);
            this.txbInput.Multiline = true;
            this.txbInput.Name = "txbInput";
            this.txbInput.Size = new System.Drawing.Size(296, 150);
            this.txbInput.TabIndex = 7;
            this.txbInput.Enter += new System.EventHandler(this.stopListening);
            this.txbInput.Leave += new System.EventHandler(this.startListening);
            // 
            // txbOutput
            // 
            this.txbOutput.Location = new System.Drawing.Point(348, 260);
            this.txbOutput.Multiline = true;
            this.txbOutput.Name = "txbOutput";
            this.txbOutput.Size = new System.Drawing.Size(296, 150);
            this.txbOutput.TabIndex = 7;
            this.txbOutput.Enter += new System.EventHandler(this.stopListening);
            this.txbOutput.Leave += new System.EventHandler(this.startListening);
            // 
            // btnPasteInput
            // 
            this.btnPasteInput.Location = new System.Drawing.Point(103, 424);
            this.btnPasteInput.Name = "btnPasteInput";
            this.btnPasteInput.Size = new System.Drawing.Size(75, 32);
            this.btnPasteInput.TabIndex = 8;
            this.btnPasteInput.Text = "Paste";
            this.btnPasteInput.UseVisualStyleBackColor = true;
            this.btnPasteInput.Click += new System.EventHandler(this.btnPasteInput_Click);
            // 
            // btnCopyInput
            // 
            this.btnCopyInput.Location = new System.Drawing.Point(22, 424);
            this.btnCopyInput.Name = "btnCopyInput";
            this.btnCopyInput.Size = new System.Drawing.Size(75, 32);
            this.btnCopyInput.TabIndex = 9;
            this.btnCopyInput.Text = "Copy";
            this.btnCopyInput.UseVisualStyleBackColor = true;
            this.btnCopyInput.Click += new System.EventHandler(this.btnCopyInput_Click);
            // 
            // btnDecodeInput
            // 
            this.btnDecodeInput.Location = new System.Drawing.Point(243, 424);
            this.btnDecodeInput.Name = "btnDecodeInput";
            this.btnDecodeInput.Size = new System.Drawing.Size(75, 32);
            this.btnDecodeInput.TabIndex = 10;
            this.btnDecodeInput.Text = "Decode";
            this.btnDecodeInput.UseVisualStyleBackColor = true;
            this.btnDecodeInput.Click += new System.EventHandler(this.btnDecodeInput_Click);
            // 
            // btnCopyOutput
            // 
            this.btnCopyOutput.Location = new System.Drawing.Point(348, 424);
            this.btnCopyOutput.Name = "btnCopyOutput";
            this.btnCopyOutput.Size = new System.Drawing.Size(75, 32);
            this.btnCopyOutput.TabIndex = 11;
            this.btnCopyOutput.Text = "Copy";
            this.btnCopyOutput.UseVisualStyleBackColor = true;
            this.btnCopyOutput.Click += new System.EventHandler(this.btnCopyOutput_Click);
            // 
            // EnigmaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 468);
            this.Controls.Add(this.btnCopyOutput);
            this.Controls.Add(this.btnDecodeInput);
            this.Controls.Add(this.btnCopyInput);
            this.Controls.Add(this.btnPasteInput);
            this.Controls.Add(this.txbOutput);
            this.Controls.Add(this.txbInput);
            this.Controls.Add(this.btnOpenSettings);
            this.Controls.Add(this.btnClear);
            this.KeyPreview = true;
            this.Name = "EnigmaForm";
            this.Text = "Form1";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnOpenSettings;
        private System.Windows.Forms.TextBox txbInput;
        private System.Windows.Forms.TextBox txbOutput;
        private System.Windows.Forms.Button btnPasteInput;
        private System.Windows.Forms.Button btnCopyInput;
        private System.Windows.Forms.Button btnDecodeInput;
        private System.Windows.Forms.Button btnCopyOutput;
    }
}

