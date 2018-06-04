using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BanPickLol
{
  public partial class MenuForm : Form
  {
    public MenuForm()
    {
      InitializeComponent();
      comboBox1.Items.Add(@"Cấm 6 con");
      comboBox1.Items.Add(@"Cấm 10 con");
      comboBox1.SelectedIndex = 0;
    }

    private void button1_Click(object sender, EventArgs e)
    {
      Form1 f1 = new Form1(comboBox1.SelectedIndex,textBox1.Text.ToString(), textBox2.Text.ToString(), textBox3.Text.ToString(), textBox4.Text.ToString(), textBox5.Text.ToString(), textBox6.Text.ToString(), textBox7.Text.ToString(), textBox8.Text.ToString(), textBox9.Text.ToString(), textBox10.Text.ToString());
      f1.Show();
    }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Specify that the link was visited.
            this.linkLabel1.LinkVisited = true;

            // Navigate to a URL.
            System.Diagnostics.Process.Start("https://www.facebook.com/Ph%E1%BA%A7n-m%E1%BB%81m-ti%E1%BB%87n-%C3%ADch-gi%C3%A1-c%E1%BA%A3-sinh-vi%C3%AAn-2085723524987625/");
        }

        private void MenuForm_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
