namespace EnigmaForm
{
    partial class Form1
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
            this.lblKeysPressed = new System.Windows.Forms.Label();
            this.lblKeysLitUp = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblKeysPressed
            // 
            this.lblKeysPressed.AutoSize = true;
            this.lblKeysPressed.Location = new System.Drawing.Point(12, 383);
            this.lblKeysPressed.Name = "lblKeysPressed";
            this.lblKeysPressed.Size = new System.Drawing.Size(77, 15);
            this.lblKeysPressed.TabIndex = 3;
            this.lblKeysPressed.Text = "Keys Pressed:";
            // 
            // lblKeysLitUp
            // 
            this.lblKeysLitUp.AutoSize = true;
            this.lblKeysLitUp.Location = new System.Drawing.Point(21, 400);
            this.lblKeysLitUp.Name = "lblKeysLitUp";
            this.lblKeysLitUp.Size = new System.Drawing.Size(68, 15);
            this.lblKeysLitUp.TabIndex = 3;
            this.lblKeysLitUp.Text = "Keys Lit Up:";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(706, 383);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(82, 32);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.lblKeysLitUp);
            this.Controls.Add(this.lblKeysPressed);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblKeysPressed;
        private System.Windows.Forms.Label lblKeysLitUp;
        private System.Windows.Forms.Button btnClear;
    }
}

