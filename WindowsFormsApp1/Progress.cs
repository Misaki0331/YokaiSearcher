using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YokaiSearcher
{
    public partial class ProgressWindow : Form
    {
        public string ExecuteTitle;
        int dotcount;
        public ProgressWindow()
        {
            InitializeComponent();
            
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            string str = "";
            dotcount++;
            if (dotcount > 5) dotcount = 1;
            for(int i = 0; i < dotcount; i++)
            {
                str += ".";
            }
            TitleName.Text = ExecuteTitle + str;
            Text = ExecuteTitle + str;
        }

        private void ProgressWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
