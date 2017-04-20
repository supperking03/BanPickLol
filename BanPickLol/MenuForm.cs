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
      Form1 f1 = new Form1(comboBox1.SelectedIndex);
      f1.Show();
    }
  }
}
