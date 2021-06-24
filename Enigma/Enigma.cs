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
    // Main form met alle toetsen, in- & uitvoer
    public partial class EnigmaForm : Form
    {
        // Een lijs waarin we alle letter-labels gaan opslaan
        List<Label> labels = new List<Label>();

        // Een label-variabele waar we in op slaan welke letter oplicht
        Label currentlyLit;

        // Een bool die aangeeft of we naar toetsaanslagen moeten luisteren
        bool listen = true;

        // We hebben drie rotors nodig
        Rotor rotor0;
        Rotor rotor1;
        Rotor rotor2;

        // En een lijst waar we alle rotors opzetten
        List<Rotor> rotors = new List<Rotor>();

        // Form constructor
        public EnigmaForm()
        {
            // Doe wat WinFormsdingen
            InitializeComponent();

            // Doe alle noodzakelijke dingen om het scherm en de machine op te zetten
            SetupDisplay();

            // Creeer drie nieuwe rotors en voeg die toe aan de lijst
            rotor0 = new Rotor(this, 0);
            rotors.Add(rotor0);
            rotor1 = new Rotor(this, 1);
            rotors.Add(rotor1);
            rotor2 = new Rotor(this, 2);
            rotors.Add(rotor2);
        }

        // Methode vanaf formulier die wordt aangeroepen bij iedere keypress
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Als we op het moment niet luisteren, ga dan terug
            if(!listen)
            {
                return;
            }

            // We gaan nu een invoer behandelen, dus stop voor nu met luisteren
            listen = false;

            // Haal op welke toets werd ingedrukt
            char charPressed = e.KeyChar;

            // Voeg deze letter toe aan het input-veld
            txbInput.Text += charPressed;

            // Codeer de ingedrukte toets
            char lightUp = EnigmaEncrypt(charPressed);

            // Voeg de output toe aan het output-veld
            txbOutput.Text += lightUp;

            // Laat het bijbehorende label "oplichten" door de achtergrond
            // geel te maken.
            // TODO: Kan dit niet makkelijker met iets anders dan een foreachloop?
            foreach (Label label in labels)
            {
                if (label.Text == lightUp.ToString())
                {
                    label.BackColor = Color.Yellow;
                    currentlyLit = label;
                }
            }
        }

        // Deze methode wordt aangeroepen wanneer een toets wordt losgelaten
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (currentlyLit != null)
            {
                // Stel de achtergrondkleur terug in op grijs
                currentlyLit.BackColor = Color.DimGray;

                // We zijn klaar en moeten dus naar de volgende toetsaanslag luisteren
                listen = true;

            }
            // En stel de variabele currentlyLit in op null.
            currentlyLit = null;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            // Maak het in- en outputveld leeg
            txbInput.Text = "";
            txbOutput.Text = "";

            // Stel de rotors in naar hun oorspronkelijke instellingen
            ResetRotors();
        }

        void ResetRotors()
        {
            // Roep voor alle gebruikte rotors de resetfunctie aan
            foreach(Rotor rotor in rotors)
            {
                rotor.ResetRotor();
            }
        }

        // Where the Mike happens
        private char EnigmaEncrypt(char c)
        {
            // Alles naar hoofdletters
            c = char.ToUpper(c);
            // Debug.WriteLine(Helper.letterToNumber(c));

            // Vertall naar getallen
            int charNumber = Helper.letterToNumber(c);
            
            // Alle speciale tekens worden een !
            if(charNumber<0 || charNumber >25 )
            {
                return Helper.NumberToLetter(33-65);
            }

            // Door rotor 0, 1 en 2
            int encryptedNumber = rotor0.KeyStroke(charNumber, false);
            encryptedNumber = rotor1.KeyStroke(encryptedNumber, false);
            encryptedNumber = rotor2.KeyStroke(encryptedNumber, false);

            // Door de reflector
            int mirroredNumber = MirrorInput(encryptedNumber, rotor0.GetConnectionCount());

            // Terug door rotor 2, 1 en 0            
            int mirroredEncryptedNumber = rotor2.KeyStroke(mirroredNumber, true);
            mirroredEncryptedNumber = rotor1.KeyStroke(mirroredEncryptedNumber, true);
            mirroredEncryptedNumber = rotor0.KeyStroke(mirroredEncryptedNumber, true);

            // Vertaal naar letters en retourneer
            return Helper.NumberToLetter(mirroredEncryptedNumber);
        }

        // Methode die ik niet ga uitleggen, maar dit zorgt ervoor dat alle knoppen
        // mooi gestyled zijn. ("mooi"...)
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
            // Maak de tekstvakken leeg
            txbInput.Text = "";
            txbOutput.Text = "";

            // currRow houdt bij met welke rij letters we bezig zijn
            int currRow = 0;
            
            // Beginwaarden van x- en yPos
            int xPos = 50;
            int yPos = 100;

            // Alle toetsen die op het scherm moeten komen, | betekent "volgende regel"
            char[] keyboardKeys = "QWERTYUIOP|ASDFGHJKL|ZXCVBNM".ToCharArray();

            // Loop door alle toetsen die op het scherm moeten komen
            foreach (char c in keyboardKeys)
            {
                // Begin een nieuwe regel als je bij een | bent
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
                // En stel de x- en yPos in
                xPos += 50;

                // Maak een nieuw label aan en stel de juiste waarden in
                Label newLabel = new Label();
                newLabel.Text = c.ToString();
                newLabel.Location = new Point(xPos, yPos);
                newLabel = styleLabel(newLabel);
                this.Controls.Add(newLabel);

                // Voeg het label toe aan de label-list
                labels.Add(newLabel);
            }
        }

        // Deze methode weerspiegelt de reflector in de echte wereld... 
        // Man-man-man, wat een humor... 
        static int MirrorInput(int input, int numRotorConnections)
        {
            int reflectorCount = numRotorConnections;
            int mirrored = (reflectorCount - 1) - input;
            return mirrored;
        }

        private void btnOpenSettings_Click(object sender, EventArgs e)
        {
            // maak een nieuw settingsPanel
            SettingsPanel settingsPanel = new SettingsPanel();

            // Toon het nieuwe settingsPanel
            settingsPanel.Show();
        }

        // Kopieer de inhoud van het output-tekstveld naar je Windows-clipboard
        private void btnCopyOutput_Click(object sender, EventArgs e)
        {
            string currentOutput = txbOutput.Text;
            if(currentOutput.Length > 0)
                Clipboard.SetText(currentOutput);
        }

        // Kopieer de inhoud van het input-tekstveld naar je Windows-clipboard
        private void btnCopyInput_Click(object sender, EventArgs e)
        {
            string currentInput = txbInput.Text;
            if (currentInput.Length > 0)
                Clipboard.SetText(currentInput);
        }

        // Kopieer de inhoud van je Windows-clipboard het input-tekstveld
        private void btnPasteInput_Click(object sender, EventArgs e)
        {
            string clipboard = Clipboard.GetText();
            txbInput.Text = clipboard;
        }

        // Decodeer het hele inputveld in één keer
        private void btnDecodeInput_Click(object sender, EventArgs e)
        {
            // Haal de input op uit het tekstveld
            string input = txbInput.Text;

            // Stel een lege output-variabele in
            string output = "";

            // Loop door alle individuele chars in de input
            foreach(char c in input)
            {
                // Sla het resultaat van encryptie op in outputvar
                output += EnigmaEncrypt(c);
            }

            // Stel het outputveld in op de waarde in de outputvar
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
