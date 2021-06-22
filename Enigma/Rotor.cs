using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;

namespace Enigma
{    class Rotor
    {
        int[] originalRotorConnections = new int[26];
        int[] originalRotorConnectionsReverse = new int[26];
        int[] originalCharArray = new int[26];

        private int[] rotorConnections = new int[26];
        private int[] charArray = new int[26];
        private int[] rotorConnectionsReverse = new int[26]; //= { 24, 9, 18, 15, 9, 9, 19, 9, -3, -2, 13, 11, -8, 4, -8, 6, -14, -6, -18, -11, -1, -6, -21, -14, -12, -22 };

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

            // Set the original arrays
            originalRotorConnections = RotorSettings.GetRotorConnections(rotorNumber);
            originalRotorConnectionsReverse = RotorSettings.GetReverseConnections(rotorNumber);
            originalCharArray = RotorSettings.GetRotorChars(rotorNumber);

            // Copy original arrays to the arrays we actually use
            rotorConnections = (int[])originalRotorConnections.Clone();
            rotorConnectionsReverse = (int[])originalRotorConnectionsReverse.Clone();
            charArray = (int[])originalCharArray.Clone();
        }

        public override string ToString()
        {
            string tempString = "";
            string toString = "";
            for (int i = 0; i < numRotorConnections; i++)
            {
                tempString = $"\n{i}: {rotorConnections[i]}";
                if (tempString.Length == 5)
                {
                    tempString += " ";
                }
                tempString += $" R: {rotorConnectionsReverse[i]}";

                toString += tempString;

            }

            return toString;
        }

        public int KeyStroke(int key, bool reverse)
        {
            int retVal;
            if (!reverse)
            {
                IncreaseRotorPosition();
                // Debug.WriteLine($"Key: {key} + connection {rotorConnections[currentRotorPosition]}");
                retVal = (key + rotorConnections[key]) % numRotorConnections;
            }
            else
            {
                retVal = (key + rotorConnectionsReverse[key]) % numRotorConnections;
            }

            retVal += numRotorConnections;
            retVal = retVal % numRotorConnections;
            return retVal;
        }

        public void IncreaseRotorPosition()
        {
            // rotor connections, shift one place
            // Get last element from array
            int tempInt = rotorConnections[0];
            for (int i = 0; i < numRotorConnections - 1; i++)
            {
                rotorConnections[i] = rotorConnections[i + 1];
            }
            rotorConnections[numRotorConnections - 1] = tempInt;

            tempInt = rotorConnectionsReverse[0];
            for (int i = 0; i < numRotorConnections - 1; i++)
            {
                rotorConnectionsReverse[i] = rotorConnectionsReverse[i + 1];
            }
            rotorConnectionsReverse[numRotorConnections - 1] = tempInt;


            // Debug.WriteLine(this);
        }

        private void setupRotorButtons(Form form, int rotorNumber)
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
            // btnIncreaseRotorPosition.Click += new System.EventHandler(this.IncreaseRotorPosition);
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

        public void ResetRotorPositions()
        {
            for (int i = 0; i < numRotorConnections; i++)
            {
                rotorConnections[i] = originalRotorConnections[i];
                rotorConnectionsReverse[i] = originalRotorConnectionsReverse[i];
            }
        }

        public void ResetRotor()
        {
            // Copy original arrays to the arrays we actually use
            rotorConnections = (int[])originalRotorConnections.Clone();
            rotorConnectionsReverse = (int[])originalRotorConnectionsReverse.Clone();
            charArray = (int[])originalCharArray.Clone();
        }

        public int GetConnectionCount()
        {
            return numRotorConnections;
        }

        public static string generateRotorConfig()
        {
            string retVal = "";

            int[] rotor = new int[26];
            int[] rotorReverse = new int[26];

            rotor.Fill(-1);
            rotorReverse.Fill(-1);

            // This sets up the array as the rotor connections: rotor[0] = 12 means that 0 is connected
            // to 12. This also means that there must be a zero in rotor[12].
            for (int i = 0; i < rotor.Length; i++)
            {
                int nextEmptySpace = findEmptySpace(rotorReverse);
                // Debug.WriteLine($"Now filling rotor[{i}] with {nextEmptySpace}.");

                rotor[i] = nextEmptySpace;
                rotorReverse[rotor[i]] = i;
            }

            Debug.WriteLine("-----CONNECTIONS------");
            foreach (int setting in rotor)
            {
                Debug.Write($"{setting}, ");
            }
            Debug.WriteLine("");

            foreach (int setting in rotorReverse)
            {
                Debug.Write($"{setting}, ");
            }
            Debug.WriteLine("");
            Debug.WriteLine("-----------");

            // Create alphabetical rotorsettings
            foreach (int setting in rotor)
            {
                Debug.Write($"{Helper.NumberToLetter(setting)}, ");
                retVal += ($"{Helper.NumberToLetter(setting)}, ");
            }
            retVal = retVal.Remove(retVal.Length - 2);
            retVal += "\r\n";

            // However, we _need_ the difference, because we want to use the numbers in the array as 
            // "the number we have to add to the connection, to get the connection on the other side".
            for (int i = 0; i < rotor.Length; i++)
            {
                // rotor[i] has to become x - i, with x the current value of rotor[i];
                // TODO: This can be modulo'd _I think_...
                rotor[i] = rotor[i] - i;
                rotorReverse[i] = rotorReverse[i] - i;
            }

            Debug.WriteLine("-----DIFFERENCES------");
            foreach (int setting in rotor)
            {
                Debug.Write($"{setting}, ");
                retVal+=($"{setting}, ");
            }
            Debug.WriteLine("");
            retVal = retVal.Remove(retVal.Length - 2);
            retVal += "\r\n";
            foreach (int setting in rotorReverse)
            {
                Debug.Write($"{setting}, ");
                retVal += ($"{setting}, ");
            }
            retVal = retVal.Remove(retVal.Length - 2);
            Debug.WriteLine("");
            Debug.WriteLine("-----------");

            return retVal;
        }

        static int findEmptySpace(int[] arr)
        {
            Random random = new Random();

            while (true)
            {
                int rnd = random.Next(0, 26);

                // Debug.WriteLine($"Now testing arr[{rnd}]: {arr[rnd]}.");
                if (arr[rnd] == -1)
                {
                    return rnd;
                }
            }
        }
    }
}
