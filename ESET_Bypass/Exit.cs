using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ESET_Bypass
{
    public partial class Exit : Form
    {
        public Exit()
        {
            InitializeComponent();
        }

        private void gunaButton2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("shutdown.exe", "-r -t 0");
            this.Close();
        }

        private void Exit_Load(object sender, EventArgs e)
        {

        }
    }
}
