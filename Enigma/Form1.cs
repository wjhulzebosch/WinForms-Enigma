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
    public enum EncryptionMethod
    {
        ASCII,
        ENIGMA,
        NONE
    }

    public partial class Form1 : Form
    {
        // char[] alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        List<Label> labels = new List<Label>();
        Label currentlyLit;
        EncryptionMethod encryptionMethod = EncryptionMethod.ENIGMA;
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

            //AutomaticTest();
        }


        void AutomaticTest()
        {
            List<String> testOutputList = new List<string>();
            for (int rotor = 0; rotor < 2; rotor++)
            {
                for (int input = 0; input < 6; input++)
                {
                    rotor0.SetRotorPosition(rotor);
                    char output = EncryptKey( NumberToLetter(input) );

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
            char lightUp = EncryptKey(charPressed);

            // Add the keystroke and the encrypted result to the labels at the bottom of the screen.
            lblKeysPressed.Text += charPressed + " ";
            lblKeysLitUp.Text += lightUp + " ";

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

        private char EncryptKey(char c)
        {
            // This method later has to actually encrypt the key pressed, but
            // for now, let's just return the letter that was send.
            switch (encryptionMethod.ToString())
            {
                case "ASCII":
                    return AsciiEncrypt(c);
                case "ENIGMA":
                    return EnigmaEncrypt(c);
            }
            return c;
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

        private char AsciiEncrypt(char c)
        {
            int asciiCodePressed = Encoding.ASCII.GetBytes(c.ToString())[0];
            int encryptedCode = asciiCodePressed + 1;
            if (encryptedCode > 90)
            {
                encryptedCode = encryptedCode - 26;
            }

            char returnChar = (char)encryptedCode;

            return returnChar;
        }

        private char EnigmaEncrypt(char c)
        {
            int charNumber = letterToNumber(c);
            label1.Text += charNumber + ", ";
            int encryptedNumber = rotor0.KeyStroke(charNumber);
            label1.Text += encryptedNumber + ", ";
            //encryptedNumber = rotor1.KeyStroke(encryptedNumber);
            //encryptedNumber = rotor2.KeyStroke(encryptedNumber);
            int mirroredEncryptedNumber = MirrorInput(encryptedNumber);
            label1.Text += mirroredEncryptedNumber + ", ";

            Debug.WriteLine("Mirroring: " + encryptedNumber + " (" + NumberToLetter(encryptedNumber) + ") becomes " + mirroredEncryptedNumber + " (" + NumberToLetter(mirroredEncryptedNumber) + ")");

            //mirroredEncryptedNumber = rotor2.KeyStroke(mirroredEncryptedNumber, true);
            //mirroredEncryptedNumber = rotor1.KeyStroke(mirroredEncryptedNumber, true);
            mirroredEncryptedNumber = rotor0.KeyStroke(mirroredEncryptedNumber, true);
            label1.Text += mirroredEncryptedNumber + ", ";

            Debug.WriteLine("-----");

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

        static int MirrorInput(int input)
        {
            int reflectorCount = 6;

            int mirrored = (reflectorCount - 1) - input;

            return mirrored;



            //int[] rotorConns2D = new int[] { 25, 24, 23, 22, 21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 };
            //return rotorConns2D[input];
        }
    }

    class Rotor
    {
        private int currentRotorPosition;

        int[] rotorConnections = new int[26];
        int[] rotorConnectionsReversed = new int[26];
        char[] rotorConnectionsChars;
        char[] rotorConnectionsReversedChars;

        int[][] rotorConns2D;
        List<Tuple<int, int>> newRotors = new List<Tuple<int, int>>();
        List<Tuple<int, int>> newRotorsReversed = new List<Tuple<int, int>>();

        Label myLabel;
        int rotorNumber;

        public void SetRotorPosition(int position)
        {
            currentRotorPosition = position % 26;
            UpdateRotorPositionLabel();
        }
        public int GetRotorPosition()
        {
            return currentRotorPosition;
        }

        public Rotor()
        {

        }

        public static int[] CharToIntArray(char[] charArray)
        {
            int[] tempArray = new int[26];
            for (int i = 0; i < 26; i++)
            {
                tempArray[i] = Encoding.ASCII.GetBytes(charArray)[i] - 65;
            }
            return tempArray;
        }

        public Rotor(Form form, int rotorNumber)
        {
            rotorConns2D = new int[][]
            {
                //new int[] { 0 , 9 },
                //new int[] { 1 , 6 },
                //new int[] { 2 , 3 },
                //new int[] { 3 , 16 },
                //new int[] { 4 , 14 },
                //new int[] { 5 , 23 },
                //new int[] { 6 , 20 },
                //new int[] { 7 , 18 },
                //new int[] { 8 , 2 },
                //new int[] { 9 , 0 },
                //new int[] { 10 , 12 },
                //new int[] { 11 , 8 },
                //new int[] { 12 , 5 },
                //new int[] { 13 , 17 },
                //new int[] { 14 , 21 },
                //new int[] { 15 , 19 },
                //new int[] { 16 , 15 },
                //new int[] { 17 , 13 },
                //new int[] { 18 , 4 },
                //new int[] { 19 , 22 },
                //new int[] { 20 , 10 },
                //new int[] { 21 , 1 },
                //new int[] { 22 , 11 },
                //new int[] { 23 , 25 },
                //new int[] { 24 , 24 },
                //new int[] { 25 , 7 }
                

                new int[] { 0, 2 },
                new int[] { 1, 0 },
                new int[] { 2, 5 },
                new int[] { 3, 4 },
                new int[] { 4, 1 },
                new int[] { 5, 3 },

            };

            newRotors.Add(Tuple.Create(0,2));
            newRotors.Add(Tuple.Create(1,0));
            newRotors.Add(Tuple.Create(2,5));
            newRotors.Add(Tuple.Create(3,4));
            newRotors.Add(Tuple.Create(4,1));
            newRotors.Add(Tuple.Create(5,3));

            // newRotorsReversed in same order as newRotors
            /*
            newRotorsReversed.Add(Tuple.Create(2, 0));
            newRotorsReversed.Add(Tuple.Create(0, 1));
            newRotorsReversed.Add(Tuple.Create(5, 2));
            newRotorsReversed.Add(Tuple.Create(4, 3));
            newRotorsReversed.Add(Tuple.Create(1, 4));
            newRotorsReversed.Add(Tuple.Create(3, 5));
            */

            // newRotorsReversed in own ordering
            newRotorsReversed.Add(Tuple.Create(0, 1));
            newRotorsReversed.Add(Tuple.Create(1, 4));
            newRotorsReversed.Add(Tuple.Create(2, 0));
            newRotorsReversed.Add(Tuple.Create(3, 5));
            newRotorsReversed.Add(Tuple.Create(4, 3));
            newRotorsReversed.Add(Tuple.Create(5, 2));

            // rotorConnectionsChars = "JGDQOXUSCAMIFRVTPNEWKBLZYH".ToCharArray();
            // rotorConnectionsReversedChars = "HYZLBKWENPRVRFIMACSUXOQDGJ".ToCharArray();
            // rotorConnections = CharToIntArray(rotorConnectionsChars);
            // rotorConnectionsReversed = CharToIntArray(rotorConnectionsReversedChars);

            this.rotorNumber = rotorNumber;
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
            btnIncreaseRotorPosition.Click += new System.EventHandler(this.IncreaseRotorPosition);
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

            SetRotorPosition(0);
        }

        public static int AddRotorPosition( int input, int rotorPosition)
        {
            return (input + rotorPosition + 6) % 6;
        }
        public static int SubtractRotorPosition( int input, int rotorPosition)
        {
            return (input - rotorPosition + 6) % 6;
        }

        public int KeyStroke(int letter, bool reverse = false)
        {
            if (rotorNumber == 0 && !reverse)
            {
                IncreaseRotorPosition();
            }

            if(!reverse)
            {
                Tuple<int, int> connectedTuple = newRotors[letter];
                int leftConnection = connectedTuple.Item2;
                Debug.WriteLine($"Letter {letter} ({Form1.NumberToLetter(letter)}) exits at position {leftConnection} ({Form1.NumberToLetter(leftConnection)})");
                return leftConnection;
            }
            else
            {
                Tuple<int, int> connectedTuple = newRotorsReversed[letter];
                int rightConnection = connectedTuple.Item1;
                Debug.WriteLine($"Letter {letter} ({Form1.NumberToLetter(letter)}) exits at position {rightConnection} ({Form1.NumberToLetter(rightConnection)})");
                return rightConnection;
            }

            return -1;

            //int adjustedLetter = SubtractRotorPosition(letter, currentRotorPosition);

            //foreach (int left in newRotors.Keys)
            //{

            //    int fixedRight = newRotors[left]; // The default 'right' linked to 'left'
            //    int diff = (fixedRight - left);




            //    if (!reverse && left == adjustedLetter)
            //    {
            //        int right = SubtractRotorPosition(adjustedLetter, currentRotorPosition);
            //        Debug.WriteLine("Return RIGHT - Adjusted letter: " + adjustedLetter + " (" + Form1.NumberToLetter(adjustedLetter) + ")  Original letter: " + letter + " (" + Form1.NumberToLetter(letter) + ") - Output: " + right + " (" + Form1.NumberToLetter(right) + ") Diff " + diff);
            //        return right;
            //    }


            //    int tmpright = newRotors[left];

            //    if (reverse && tmpright == adjustedLetter)
            //    {
            //        int adjustedLeft = AddRotorPosition(left, currentRotorPosition);
            //        Debug.WriteLine("Return ADJUSTED LEFT - Original letter: " + letter + " (" + Form1.NumberToLetter(letter) + ") - Output: " + adjustedLeft + " (" + Form1.NumberToLetter(adjustedLeft) + ") Diff "+diff);
            //        return adjustedLeft;
            //    }

            //if (!reverse && left == letter)
            //{
            //    Debug.WriteLine("Return RIGHT - Input: " + letter + " ("+ Form1.NumberToLetter(letter)+") - Output: "+right+" ("+Form1.NumberToLetter(right)+")");
            //    return right;
            //}

            //if (reverse && right == letter)
            //{
            //    Debug.WriteLine("Return LEFT - Input: " + letter + " (" + Form1.NumberToLetter(letter) + ") - Output: " + left + " (" + Form1.NumberToLetter(left) + ")");
            //    return left;
            //}
            //}

            //if (reverse)
            //    Debug.WriteLine("Letter " + letter + " not found");
            //else
            //    Debug.WriteLine("Adjusted letter " + adjustedLetter + " not found");
            //return -1;
        }

        //public int KeyStroke(int letter, bool reverse)
        //{
        //    int returnKey = 0;
        //    // Code for reverse?
        //    int lookFor = (letter - currentPosition);
        //    if (lookFor < 0)
        //    {
        //        lookFor = 26 + lookFor; // Add, because it's a negative number
        //    }
        //    else
        //    {
        //        lookFor = lookFor % 26;
        //    }
        //    var result = from u in rotorConns2D
        //                 where u[1] == lookFor
        //                 select u;
        //    foreach (var item in result)
        //    {
        //        returnKey = item[0];
        //    }

        //    return returnKey;
        //}

        public void IncreaseRotorPosition(object sender, EventArgs e)
        {
            IncreaseRotorPosition();
        }

        public void IncreaseRotorPosition()
        {
            RotorShift();
            currentRotorPosition++;
            currentRotorPosition = currentRotorPosition % 6;
            UpdateRotorPositionLabel();
        }

        public void RotorShift()
        {
            Tuple<int, int> firstTuple = newRotors.First<Tuple<int, int>>();
            newRotors.RemoveAt(0);
            newRotors.Add(firstTuple);

            Tuple<int, int> firstTupleReversed = newRotorsReversed.First<Tuple<int, int>>();
            newRotorsReversed.RemoveAt(0);
            newRotorsReversed.Add(firstTupleReversed);

            string rotorString = "";
            string rotorStringReverse = "";

            for (int i = 0; i < newRotors.Count; i++)
            {
                rotorString += $"Position {i}: NORMAL: {newRotors[i].Item1} -> {newRotors[i].Item2}\n";
                rotorStringReverse+= $"Position {i}: REVERS: {newRotorsReversed[i].Item1} -> {newRotorsReversed[i].Item2}\n";
            }

            Debug.WriteLine(rotorString);
            Debug.WriteLine(rotorStringReverse);
        }

        public void UpdateRotorPositionLabel()
        {
            myLabel.Text = ((char)(rotorConnections[currentRotorPosition] + 65)).ToString();
        }
    }
}
