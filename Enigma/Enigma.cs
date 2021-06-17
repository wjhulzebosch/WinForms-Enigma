using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Enigma
{
    public partial class EnigmaForm : Form
    {
        List<Label> labels = new List<Label>();
        Label currentlyLit;

        Rotor rotor0;
        Rotor rotor1;
        Rotor rotor2;
        public EnigmaForm()
        {
            InitializeComponent();
            SetupDisplay();

            rotor0 = new Rotor(this, 0);
            rotor1 = new Rotor(this, 1);
            rotor2 = new Rotor(this, 2);
        }

        bool listen = true;

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!listen)
            {
                return;
            }

            // Get the character that was pressed and make it upper case
            char charPressed = char.ToUpper(e.KeyChar);
            txbInput.Text += charPressed;

            // Get the encrypted value of the key that was pressed
            char lightUp = EnigmaEncrypt(charPressed);
            txbOutput.Text += lightUp;

            // Light up the char that belongs to the result of the encryption.
            // TODO: Right now, this works with a foreach, looping over all
            // the labels on the screen. Must be an easier way, right?
            foreach (Label label in labels)
            {
                if (label.Text == lightUp.ToString())
                {
                    label.BackColor = Color.Yellow;
                    currentlyLit = label;
                }
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            // Reset the label that is currently lit up.
            if (currentlyLit != null)
            {
                currentlyLit.BackColor = Color.DimGray;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txbInput.Text = "";
            txbOutput.Text = "";
        }

        void ResetRotors()
        {
            rotor0.ResetRotorPositions();
            rotor1.ResetRotorPositions();
            rotor2.ResetRotorPositions();
        }

        private char EnigmaEncrypt(char c)
        {
            int charNumber = letterToNumber(c);
            if(charNumber<0 || charNumber >65 )
            {
                return NumberToLetter(33-65);
            }

            int encryptedNumber = rotor0.KeyStroke(charNumber, false);
            // Debug.WriteLine("-----\r\nEncrypting...");
            encryptedNumber = rotor1.KeyStroke(encryptedNumber, false);
            encryptedNumber = rotor2.KeyStroke(encryptedNumber, false);
            int mirroredNumber = MirrorInput(encryptedNumber, rotor0.GetConnectionCount());
            

            // Debug.WriteLine("Mirroring:      " + encryptedNumber + " (" + NumberToLetter(encryptedNumber) + ") becomes " + mirroredNumber + " (" + NumberToLetter(mirroredNumber) + ").");

            int mirroredEncryptedNumber = rotor2.KeyStroke(mirroredNumber, true);
            mirroredEncryptedNumber = rotor1.KeyStroke(mirroredEncryptedNumber, true);
            mirroredEncryptedNumber = rotor0.KeyStroke(mirroredEncryptedNumber, true);

            // Debug.WriteLine("-----");

            return NumberToLetter(mirroredEncryptedNumber);
        }

        static Label styleLabel(Label label)
        {
            label.AutoSize = true;
            label.BackColor = System.Drawing.Color.DimGray;
            label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            label.ForeColor = System.Drawing.Color.White;
            label.Margin = new System.Windows.Forms.Padding(3);
            label.MinimumSize = new System.Drawing.Size(40, 40);
            label.Name = "lblChar" + label.Text.ToString();
            label.Size = new System.Drawing.Size(40, 40);
            label.TabIndex = 2;
            label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            label.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);

            return label;
        }

        private void SetupDisplay()
        {
            txbInput.Text = "";
            txbOutput.Text = "";

            int currRow = 0;
            int xPos = 50;
            int yPos = 100;
            char[] keyboardKeys = "QWERTYUIOP|ASDFGHJKL|ZXCVBNM".ToCharArray();

            foreach (char c in keyboardKeys)
            {
                if (c == '|')
                {
                    currRow++;
                    yPos += 50;
                    if (currRow == 0)
                    {
                        xPos = 50;
                    }
                    else if (currRow == 1)
                    {
                        xPos = 75;
                    }
                    else if (currRow == 2)
                    {
                        xPos = 125;
                    }

                    continue;
                }
                xPos += 50;

                Label newLabel = new Label();
                newLabel.Text = c.ToString();
                newLabel.Location = new Point(xPos, yPos);
                newLabel = styleLabel(newLabel);

                this.Controls.Add(newLabel);
                labels.Add(newLabel);
            }
        }
        static int letterToNumber(char c)
        {
            return Encoding.ASCII.GetBytes(new char[] { c })[0] - 65;
        }

        public static char NumberToLetter(int i)
        {
            return (char)(i + 65);
        }

        static int MirrorInput(int input, int numRotorConnections)
        {
            int reflectorCount = numRotorConnections;
            int mirrored = (reflectorCount - 1) - input;
            return mirrored;
        }

        private void btnOpenSettings_Click(object sender, EventArgs e)
        {
            SettingsPanel settingsPanel = new SettingsPanel();
            settingsPanel.Show();
        }

        private void btnCopyOutput_Click(object sender, EventArgs e)
        {
            string currentOutput = txbOutput.Text;
            if(currentOutput.Length > 0)
                Clipboard.SetText(currentOutput);
        }

        private void btnCopyInput_Click(object sender, EventArgs e)
        {
            string currentInput = txbInput.Text;
            if (currentInput.Length > 0)
                Clipboard.SetText(currentInput);
        }

        private void btnPasteInput_Click(object sender, EventArgs e)
        {
            string clipboard = Clipboard.GetText();
            txbInput.Text = clipboard;
        }

        private void btnDecodeInput_Click(object sender, EventArgs e)
        {
            string input = txbInput.Text;
            string output = "";
            foreach(char c in input)
            {
                char upper = char.ToUpper(c);
                output += EnigmaEncrypt(upper);
            }

            txbOutput.Text = output;
        }

        private void stopListening(object sender, EventArgs e)
        {
            listen = false;
        }

        private void startListening(object sender, EventArgs e)
        {
            listen = true;
        }
    }
}
