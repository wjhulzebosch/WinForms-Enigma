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

namespace EnigmaForm
{
    public partial class Form1 : Form
    {
        // char[] alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        List<Label> labels = new List<Label>();
        Label currentlyLit;

        Rotor rotor0;
        Rotor rotor1;
        Rotor rotor2;
        public Form1()
        {
            InitializeComponent();
            SetupDisplay();

            rotor0 = new Rotor(this, 0);
            rotor1 = new Rotor(this, 1);
            rotor2 = new Rotor(this, 2);

            // AutomaticTest();
        }

        void AutomaticTest()
        {
            List<String> testOutputList = new List<string>();
            for (int rotor = 0; rotor < 2; rotor++)
            {
                for (int input = 0; input < rotor0.GetConnectionCount(); input++)
                {
                    rotor0.SetRotorPosition(rotor);
                    char output = EnigmaEncrypt( NumberToLetter(input) );

                    string testOutput = "Rotor " + rotor0.GetRotorPosition() + ": input " + input + " -> output " + output;
                    Debug.WriteLine(testOutput);
                    testOutputList.Add(testOutput);
                }
            }

            Debug.WriteLine("----- Test output -----");
            Debug.WriteLine(String.Join("\n", testOutputList));
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Get the character that was pressed and make it upper case
            char charPressed = char.ToUpper(e.KeyChar);

            // Get the encrypted value of the key that was pressed
            char lightUp = EnigmaEncrypt(charPressed);

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
            lblKeysPressed.Text = "Keys Pressed:";
            lblKeysLitUp.Text = "Keys Lit Up:";
            ResetRotors();
        }

        void ResetRotors()
        {
            rotor0.SetRotorPosition(0);
        }

        private char EnigmaEncrypt(char c)
        {
            int charNumber = letterToNumber(c);

            int encryptedNumber = rotor0.KeyStroke(charNumber, false);
            Debug.WriteLine("-----\r\nEncrypting...");
            Debug.WriteLine($"rotor 1:        {charNumber} ({NumberToLetter(charNumber)}) becomes {encryptedNumber} ({NumberToLetter(encryptedNumber)}).");
            //encryptedNumber = rotor1.KeyStroke(encryptedNumber);
            //encryptedNumber = rotor2.KeyStroke(encryptedNumber);
            int mirroredNumber = MirrorInput(encryptedNumber, rotor0.GetConnectionCount());
            

            Debug.WriteLine("Mirroring:      " + encryptedNumber + " (" + NumberToLetter(encryptedNumber) + ") becomes " + mirroredNumber + " (" + NumberToLetter(mirroredNumber) + ").");

            //mirroredEncryptedNumber = rotor2.KeyStroke(mirroredEncryptedNumber, true);
            //mirroredEncryptedNumber = rotor1.KeyStroke(mirroredEncryptedNumber, true);
            int mirroreEncryptedNumber = rotor0.KeyStroke(mirroredNumber, true);
            Debug.WriteLine($"Rev. rotor 1:   {mirroredNumber} ({NumberToLetter(mirroredNumber)}) becomes {mirroreEncryptedNumber} ({NumberToLetter(mirroreEncryptedNumber)}).");


            Debug.WriteLine("-----");

            return NumberToLetter(mirroredNumber);
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
            lblKeysPressed.Text = "Keys Pressed:";
            lblKeysLitUp.Text = "Keys Lit Up:";

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
        //static int MirrorInput(int input)
        //{
        //    int[] mirrorConnections = Rotor.CharToIntArray("QYHOGNECVPUZTFDJAXWMKISRBL".ToCharArray());
        //    return mirrorConnections[input];
        //} 

        static int MirrorInput(int input, int numRotorConnections)
        {
            int reflectorCount = numRotorConnections;
            int mirrored = (reflectorCount - 1) - input;
            return mirrored;
        }
    }

    class Rotor
    {
        private int currentRotorPosition;
        private int[] rotorConnections = { 2, -1, 3, 1, 3, -2 };
        private int[] rotorConnectionsReverse = {1, -3, -2, 2, -1, -3};
        private int numRotorConnections;

        Label myLabel;
        int rotorNumber;

        public Rotor()
        {

        }

        public Rotor(Form form, int rotorNumber)
        {
            this.rotorNumber = rotorNumber;
            setupRotorButtons(form, rotorNumber);
            numRotorConnections = rotorConnections.Length;
        }

        public override string ToString()
        {
            string tempString= "";
            string toString = "";
            for(int i = 0; i < numRotorConnections; i++)
            {
                tempString = $"\n{i}: {rotorConnections[i]}";
                if (tempString.Length == 5)
                {
                    tempString += " ";
                }
                tempString += $" R: {rotorConnectionsReverse[i]}";

                if (i == currentRotorPosition )
                {
                    tempString += " <<";
                }
                toString += tempString;

            }

            return toString;
        }

        public int KeyStroke(int key, bool reverse)
        {
            int retVal;
            if(!reverse)
            {
                IncreaseRotorPosition();
                Debug.WriteLine($"Key: {key} + connection {rotorConnections[currentRotorPosition]}");
                retVal = (key + rotorConnections[currentRotorPosition]) % numRotorConnections;
            }
            else
            {
                retVal = (key + rotorConnectionsReverse[currentRotorPosition]) % numRotorConnections;
            }

            retVal += numRotorConnections;
            retVal = retVal % numRotorConnections;
            return retVal;
        }

        public void UpdateRotorPositionLabel()
        {
            myLabel.Text = ((char)(rotorConnections[currentRotorPosition] + 65)).ToString();
        }

        public void IncreaseRotorPosition()
        {
            currentRotorPosition++;
            currentRotorPosition = currentRotorPosition % numRotorConnections;

            Debug.WriteLine(this);
        }

        private void setupRotorButtons (Form form, int rotorNumber)
        {
            // 
            // lblCurrentRotorPosition
            // 
            Label lblCurrentRotorPosition = new Label();
            lblCurrentRotorPosition.AutoSize = true;
            lblCurrentRotorPosition.Location = new System.Drawing.Point((315 - (rotorNumber * 80)), 9);
            lblCurrentRotorPosition.Name = "lblCurrentRotorPosition";
            lblCurrentRotorPosition.Size = new System.Drawing.Size(15, 15);
            lblCurrentRotorPosition.TabIndex = 6;
            lblCurrentRotorPosition.Text = "?";
            form.Controls.Add(lblCurrentRotorPosition);
            myLabel = lblCurrentRotorPosition;

            // 
            // btnIncreaseRotorPosition
            // 
            Button btnIncreaseRotorPosition = new Button();
            btnIncreaseRotorPosition.Location = new System.Drawing.Point((310 - (rotorNumber * 80)), 28);
            btnIncreaseRotorPosition.Name = "btnIncreaseRotorPosition";
            btnIncreaseRotorPosition.Size = new System.Drawing.Size(25, 23);
            btnIncreaseRotorPosition.TabIndex = 7;
            btnIncreaseRotorPosition.Text = "v";
            btnIncreaseRotorPosition.UseVisualStyleBackColor = true;
            // Add clickEvent
            //btnIncreaseRotorPosition.Click += new System.EventHandler(this.IncreaseRotorPosition);
            form.Controls.Add(btnIncreaseRotorPosition);

            // 
            // btnChangeRotorLeft
            // 
            Button btnChangeRotorLeft = new Button();
            btnChangeRotorLeft.Location = new System.Drawing.Point((288 - (rotorNumber * 80)), 5);
            btnChangeRotorLeft.Name = "btnChangeRotorLeft";
            btnChangeRotorLeft.Size = new System.Drawing.Size(25, 23);
            btnChangeRotorLeft.TabIndex = 7;
            btnChangeRotorLeft.Text = "<";
            btnChangeRotorLeft.UseVisualStyleBackColor = true;
            // Add clickEvent
            // btnIncreaseRotorPosition.Click += new System.EventHandler();
            form.Controls.Add(btnChangeRotorLeft);

            // 
            // btnChangeRotorRight
            // 
            Button btnChangeRotorRight = new Button();
            btnChangeRotorRight.Location = new System.Drawing.Point((330 - (rotorNumber * 80)), 5);
            btnChangeRotorRight.Name = "btnChangeRotorRight";
            btnChangeRotorRight.Size = new System.Drawing.Size(25, 23);
            btnChangeRotorRight.TabIndex = 7;
            btnChangeRotorRight.Text = ">";
            btnChangeRotorRight.UseVisualStyleBackColor = true;
            // Add clickEvent
            // btnIncreaseRotorPosition.Click += new System.EventHandler();
            form.Controls.Add(btnChangeRotorRight);
        }

        public void SetRotorPosition(int position)
        {
            currentRotorPosition = position % numRotorConnections;
            UpdateRotorPositionLabel();
        }

        public int GetRotorPosition()
        {
            return currentRotorPosition;
        }

        public int GetConnectionCount()
        {
            return numRotorConnections;
        }
    }
}
