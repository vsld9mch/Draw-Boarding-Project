using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projekt3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void bttn_LabaratoriumNr3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Laboratorna Form2 = new Laboratorna();
            Form2.Show();

        }

        private void bttn_ProjektNr3_Click(object sender, EventArgs e)
        {
            this.Hide();
            ProjectN3_68932 Form3 = new ProjectN3_68932();
            Form3.Show();
        }
    }
}
