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
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.lblKeysPressed = new System.Windows.Forms.Label();
            this.lblKeysLitUp = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(662, 235);
            this.checkedListBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(9, 4);
            this.checkedListBox1.TabIndex = 0;
            // 
            // lblKeysPressed
            // 
            this.lblKeysPressed.AutoSize = true;
            this.lblKeysPressed.Location = new System.Drawing.Point(14, 511);
            this.lblKeysPressed.Name = "lblKeysPressed";
            this.lblKeysPressed.Size = new System.Drawing.Size(96, 20);
            this.lblKeysPressed.TabIndex = 3;
            this.lblKeysPressed.Text = "Keys Pressed:";
            // 
            // lblKeysLitUp
            // 
            this.lblKeysLitUp.AutoSize = true;
            this.lblKeysLitUp.Location = new System.Drawing.Point(24, 533);
            this.lblKeysLitUp.Name = "lblKeysLitUp";
            this.lblKeysLitUp.Size = new System.Drawing.Size(85, 20);
            this.lblKeysLitUp.TabIndex = 3;
            this.lblKeysLitUp.Text = "Keys Lit Up:";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(807, 511);
            this.btnClear.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(94, 43);
            this.btnClear.TabIndex = 4;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(914, 600);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.lblKeysLitUp);
            this.Controls.Add(this.lblKeysPressed);
            this.Controls.Add(this.checkedListBox1);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Form1_KeyPress);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Label lblKeysPressed;
        private System.Windows.Forms.Label lblKeysLitUp;
        private System.Windows.Forms.Button btnClear;
    }
}

