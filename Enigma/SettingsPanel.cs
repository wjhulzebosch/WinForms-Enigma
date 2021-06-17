using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Enigma
{
    public partial class SettingsPanel : Form
    {
        public SettingsPanel()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string rotorConfig = Rotor.generateRotorConfig();
            txbRotorSettingsOutput.Text = rotorConfig;
        }
    }
}
