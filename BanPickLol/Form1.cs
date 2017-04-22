using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Drawing.Imaging;
using System.Net.Http;
using System.Net;

namespace BanPickLol
{
    public partial class Form1 : Form
    {
        string selected;
        int time = 30;
        PictureBox[] arrbox = new PictureBox[20];
        List<string> arrName = new List<string>();
        string currenName;

        int banType;

        public Form1(int BanType)
        {
            
            banType = BanType;
            InitializeComponent();
            arrbox[0] = ban1;
            arrbox[1] = ban2;
            arrbox[2] = ban3;
            arrbox[3] = ban4;
            arrbox[4] = ban5;
            arrbox[5] = ban6;
            arrbox[6] = pick1;
            arrbox[7] = pick6;
            arrbox[8] = pick7;
            arrbox[9] = pick2;
            arrbox[10] = pick3;
            arrbox[11] = pick8;
            arrbox[12] = ban8;
            arrbox[13] = ban7;
            arrbox[14] = ban10;
            arrbox[15] = ban9;
            arrbox[16] = pick9;
            arrbox[17] = pick4;
            arrbox[18] = pick5;
            arrbox[19] = pick10;

            if(banType == 0)
            {
                ban7.Hide();
                ban8.Hide();
                ban9.Hide();
                ban10.Hide();
            }


            populate();
            listView1.View = View.Details;
            //construct columns
            listView1.Columns.Add("Tìm", 250);

        }

       
        private static Bitmap ToGrayScale(Bitmap original) // làm đen ảnh
        {
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            Graphics g = Graphics.FromImage(newBitmap);

            ColorMatrix colorMatrix = new ColorMatrix
                (
                new float[][]
                {
                    new float[] {0.299f, 0.299f, 0.299f, 0, 0},
                    new float[] {0.587f, 0.587f, 0.587f, 0, 0},
                    new float[] {0.114f, 0.114f, 0.114f, 0, 0},
                    new float[] {0,0,0,1,0},
                    new float[] {0,0,0,0,1}
                }
                );
            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(colorMatrix);

            g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
                0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);
            g.Dispose();
            return newBitmap;
        }
        private static void DrawCross(Bitmap original)
        {
            Bitmap mask = new Bitmap("ban.png");

            for (int i = 0; i < mask.Width; i++)
            {
                for (int j = 0; j < mask.Height; j++)
                {
                    if (mask.GetPixel(i, j).R < 100)
                    {
                        original.SetPixel(i + original.Width / 2 - mask.Width / 2, j + original.Height - mask.Height, Color.Red);
                    }
                }
            }
        }

        //private static void DrawCross(Bitmap original)
        //{
        //    Graphics originalGraphics = Graphics.FromImage(original);

        //    Pen pen = new Pen(Color.Red, 5);
        //    int x1 = 0;
        //    int x2 = original.Width;
        //    int y1 = 0;
        //    int y2 = original.Height;

        //    originalGraphics.DrawLine(pen, x1, y1, x2, y2);
        //    originalGraphics.DrawLine(pen, x2, y1, x1, y2);

        //    //Bitmap mask = new Bitmap("ban.png");

        //    //for(int i = original.Height; i >= original.Height - mask.Height; i--)
        //    //{
        //    //    for(int j)
        //    //}

        //    originalGraphics.Dispose();
        //}


        List<string> arrPath = new List<string>();
        string pa = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\imgs";
        private void populate() // nạp database vào list
        {
            listView1.Items.Clear();
            //img LIST
            ImageList imgs = new ImageList();
            imgs.ImageSize = new Size(50, 50);

            //load image from file
            String[] paths = { };




            paths = Directory.GetFiles(pa);
            DirectoryInfo d = new DirectoryInfo(pa);//Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.png"); //Getting Text files
            string[] name = new string[Files.Length];
            for (int i = 0; i < Files.Length;i++)
            {
                name[i] = Files[i].Name;
            }


            //MessageBox.Show(name[0]);


            try
            {


                foreach(string _name in arrName)
                {
                    string cac = pa + "\\" + _name + ".png";
                    if (!arrPath.Contains(cac))
                    {
                        arrPath.Add(pa + "\\" + _name + ".png");
                    }

                }
                var unPicked = paths.ToList<string>().Except(arrPath);
                unPicked = unPicked.ToArray();

                foreach (string path in paths)
                {
                    //imgs.Images.Add(ToGrayScale((Bitmap)Image.FromFile(path)));
                    if(arrPath.Contains(path))
                    {
                        imgs.Images.Add(ToGrayScale((Bitmap)Image.FromFile(path)));
                    }
                    else
                    {
                        imgs.Images.Add(Image.FromFile(path));

                    }

                }
            }
            catch
            {
                MessageBox.Show("cac");
            }



            //add image to listview
            listView1.SmallImageList = imgs;
            for (int i = 0; i < Files.Length;i++)
            {
                listView1.Items.Add(name[i].Replace(".png",""), i);
            }




        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            populate();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                for (int i = listView1.Items.Count - 1; i >= 0; i--)
                {
                    var item = listView1.Items[i];
                    if (item.Text.ToLower().Contains(textBox1.Text.ToLower()))
                    {
                        item.BackColor = SystemColors.Highlight;
                        item.ForeColor = SystemColors.HighlightText;
                    }
                    else
                    {
                        listView1.Items.Remove(item);
                    }
                }
                if (listView1.SelectedItems.Count == 1)
                {
                    listView1.Focus();
                }
            }
            else
            {
                populate();
                //LoadContacts();
                //RefreshAll();
                //populate();
                ////listView1.Update();
                //listView1.Refresh();
            }
        } // xử lý search

        void refresh() // refresh lại list khi search
        {
            if (textBox1.Text != "")
            {
                for (int i = listView1.Items.Count - 1; i >= 0; i--)
                {
                    var item = listView1.Items[i];
                    if (item.Text.ToLower().Contains(textBox1.Text.ToLower()))
                    {
                        item.BackColor = SystemColors.Highlight;
                        item.ForeColor = SystemColors.HighlightText;
                    }
                    else
                    {
                        listView1.Items.Remove(item);
                    }
                }
                if (listView1.SelectedItems.Count == 1)
                {
                    listView1.Focus();
                }
            }
            else
            {
                populate();
                //LoadContacts();
                //RefreshAll();
                //populate();
                ////listView1.Update();
                //listView1.Refresh();
            }
        }

        private void listView1_Enter(object sender, EventArgs e)// highlight item search đc trong listview
        {
            foreach (ListViewItem item in listView1.Items)
            {
                item.BackColor = SystemColors.Window;
                item.ForeColor = SystemColors.WindowText;
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)// xóa ký tự trong lúc search vẫn search tiếp ký tự còn lại
        {
            if(e.KeyCode == Keys.Back)
            {
                listView1.Items.Clear();
                refresh();
                populate();
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        Thread a; // tiểu trình thay đổi màu + time
        Thread b; // hiện label đang pick

        private void Form1_Load(object sender, EventArgs e)
        {


            // enable
            SwapButton01.Visible = false;
            SwapButton02.Visible = false;
            SwapButton03.Visible = false;
            SwapButton04.Visible = false;
            SwapButton05.Visible = false;
            SwapButton11.Visible = false;
            SwapButton12.Visible = false;
            SwapButton13.Visible = false;
            SwapButton14.Visible = false;
            SwapButton15.Visible = false;

            SwapText01.Visible = false;
            SwapText02.Visible = false;
            SwapText03.Visible = false;
            SwapText04.Visible = false;
            SwapText05.Visible = false;
            SwapText11.Visible = false;
            SwapText12.Visible = false;
            SwapText13.Visible = false;
            SwapText14.Visible = false;
            SwapText15.Visible = false;

            //
            //this.TopMost = true;
            //splash

            Thread t = new Thread(new ThreadStart(splashscreen));
            t.Start();

            Thread.Sleep(4000);

            t.Abort();




            button1.Enabled = false;
            arrbox[turn].BackColor = Color.Gold;
            a = new Thread(ColorChange);
            //receiveData = new Thread(receive);
            //receiveData.Start();
            a.Start();

            b = new Thread(appearPick);
            b.Start();


        }

        private void appearPick() // đổi màu chữ "chọn tướng" nhấp nháy
        {
            while (time > 0)
            {

                try
                {
                    lblpick.ForeColor = Color.Orange;
                    Thread.Sleep(500);
                    lblpick.ForeColor = Color.Black;
                    Thread.Sleep(500);
                }
                catch
                { }

            }
        }

        private void splashscreen()
        {
            Application.Run(new Splash());
        }

        DialogResult result;
        private void ColorChange()
        {
            if(time == 30)
            {
                MessageBox.Show("Are you ready ?");
            }
            while(time>0)
            {
                time--;
                try
                {
                    arrbox[turn].BackColor = Color.Goldenrod;
                    Thread.Sleep(500);
                    arrbox[turn].BackColor = Color.Yellow;
                    Thread.Sleep(500);
                }
                catch
                { }
               
            }
            result = MessageBox.Show("Người chơi không ban/pick tướng\n Nhấn OK để out !", "LỖI BAN/PICK", MessageBoxButtons.OK);

        } // đổi màu + time countdown

        private void listView1_DoubleClick(object sender, EventArgs e)
        {

        }
        int turn = 0; // lượt pick

        //DialogResult res;
        int res = 0;
        private void button1_Click_1(object sender, EventArgs e)
        {
            time = 30; // hồi lại 30s khi pick xong
            arrName.Add(currenName);// sau khi click no them cai path moi vao list hinh đã ban pick
            populate();// load lai list
            turn++; // tăng lượt

            //Bitmap img = (Bitmap) arrbox[turn - 1].Image;
            // pa + "\\" + currenName + ".png"; path
            if(arrbox[turn-1].Name[0] == 'b')
            {
                Bitmap img = new Bitmap(pa + "\\" + currenName + ".png");
                DrawCross(img);
                arrbox[turn - 1].Image = img;
            }


            arrbox[turn - 1].SizeMode = PictureBoxSizeMode.StretchImage; // hiển thỉ đã pick lượt vừa rồi

            if (turn == 12 && banType == 0)
            {
              turn = 16;
            }
            try
            {
                arrbox[turn].BackColor = Color.Gold; // hiển thị vàng lượt tiếp theo
            }
            catch
            {
                a.Abort();
                b.Abort();
                //res = MessageBox.Show("BAN/PICK IS ENDED !","Hết", MessageBoxButtons.OK);
                res = 999;

            }

            //MessageBox.Show(selected);
            

            if(listView1.FocusedItem == null) // ko chọn thì ko đc pick
            {
                button1.Enabled = false;
            }

        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {

        }

        private void listView1_Click(object sender, EventArgs e)
        {
            button1.Enabled = true;
            selected = listView1.SelectedItems[0].SubItems[0].Text; // lấy tên của tướng đc chọn
            currenName = selected;              // đường dẫn của tướng được chọn hiện tại ( chưa pick )
            //MessageBox.Show(selected);


            if(arrName.Contains(selected))          // nếu tướng đã ban/pcik rồi thì ko chọn được
            {
                button1.Enabled = false;
                //MessageBox.Show("d chon dc");
                return;
            }
            switch(turn)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 12:
                case 13:
                case 14:
                case 15:
                    string path2 = pa + "\\" + selected + ".png";       // tạo đường dẫn từ tên
                    try
                    {
                        arrbox[turn].Image = Image.FromFile(path2); // load hình tưỡng đang chọn lên 
                        arrbox[turn].SizeMode = PictureBoxSizeMode.Zoom; // hiển thị màu vàng xung quanh hình để bik đang chọn tướng đó
                    }
                    catch
                    {
                        MessageBox.Show("lỖI");
                    }
                    break;
                default:
                    string path = pa + "\\pick\\" + selected + "_Splash_0.jpg";       // tạo đường dẫn từ tên
                    try
                    {
                        arrbox[turn].Image = Image.FromFile(path); // load hình tưỡng đang chọn lên 
                        arrbox[turn].SizeMode = PictureBoxSizeMode.Zoom; // hiển thị màu vàng xung quanh hình để bik đang chọn tướng đó
                    }
                    catch
                    {
                        MessageBox.Show("lỖI");
                    }
                    break;
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            a.Abort();
            b.Abort();
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            if (banType == 0) // set vị trí của chữ "chọn tướng"
            {
                switch (turn)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        lblpick.Visible = false;
                        break;
                    case 6:
                    case 9:
                    case 10:
                    case 13:
                    case 14:
                        lblpick.Visible = true;
                        lblpick.Location = new Point(arrbox[turn].Location.X + 148, arrbox[turn].Location.Y + 26);
                        break;
                    default:
                        try
                        {
                            lblpick.Visible = true;
                            lblpick.Location = new Point(626, arrbox[turn].Location.Y + 26);
                        }
                        catch
                        { }

                        break;

                }
            }
            else
            {
                switch (turn)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 12:
                    case 13:
                    case 14:
                    case 15:
                        lblpick.Visible = false;
                        break;
                    case 6:
                    case 9:
                    case 10:
                    case 17:
                    case 18:
                        lblpick.Visible = true;
                        lblpick.Location = new Point(arrbox[turn].Location.X + 148, arrbox[turn].Location.Y + 26);
                        break;
                    default:
                        try
                        {
                            lblpick.Visible = true;
                            lblpick.Location = new Point(626, arrbox[turn].Location.Y + 26);
                        }
                        catch
                        { }

                        break;

                }
            }
            if (res == 999)
            {
                listView1.Enabled = false;
                button1.Enabled = false;
                lblpick.Visible = false;

                label2.Text = "Giai đoạn Swap !";
                label1.Enabled = false;

                // enable
                SwapButton01.Visible = true;
                SwapButton02.Visible = true;
                SwapButton03.Visible = true;
                SwapButton04.Visible = true;
                SwapButton05.Visible = true;
                SwapButton11.Visible = true;
                SwapButton12.Visible = true;
                SwapButton13.Visible = true;
                SwapButton14.Visible = true;
                SwapButton15.Visible = true;

                SwapText01.Visible = true;
                SwapText02.Visible = true;
                SwapText03.Visible = true;
                SwapText04.Visible = true;
                SwapText05.Visible = true;
                SwapText11.Visible = true;
                SwapText12.Visible = true;
                SwapText13.Visible = true;
                SwapText14.Visible = true;
                SwapText15.Visible = true;

            }
            if (result == DialogResult.OK)
            {
                b.Abort();
                a.Abort();
                this.Close();
            }
            switch(turn)
            {
                case 0:
                    label2.Text = "Giai đoạn cấm 1";
                    break;
                case 1:
                    label2.Text = "Giai đoạn cấm 2";
                    break;
                case 2:
                    label2.Text = "Giai đoạn cấm 3";
                    break;
                case 3:
                    label2.Text = "Giai đoạn cấm 4";
                    break;
                case 4:
                    label2.Text = "Giai đoạn cấm 5";
                    break;
                case 5:
                    label2.Text = "Giai đoạn cấm 6";
                    break;
                case 6:
                    label2.Text = "Giai đoạn chọn 1";
                    break;
                case 7:
                    label2.Text = "Giai đoạn chọn 2";
                    break;
                case 8:
                    label2.Text = "Giai đoạn chọn 3";
                    break;
                case 9:
                    label2.Text = "Giai đoạn chọn 4";
                    break;
                case 10:
                    label2.Text = "Giai đoạn chọn 5";
                    break;
                case 11:
                    label2.Text = "Giai đoạn chọn 6";
                    break;
                case 12:
                    label2.Text = "Giai đoạn cấm 7";
                    break;
                case 13:
                    label2.Text = "Giai đoạn cấm 8";
                    break;
                case 14:
                    label2.Text = "Giai đoạn cấm 9";
                    break;
                case 15:
                    label2.Text = "Giai đoạn cấm 10";
                    break;
                case 16:
                    label2.Text = "Giai đoạn chọn 7";
                    break;
                case 17:
                    label2.Text = "Giai đoạn chọn 8";
                    break;
                case 18:
                    label2.Text = "Giai đoạn chọn 9";
                    break;
                case 19:
                    label2.Text = "Giai đoạn chọn 10";
                    break;
            }
            label1.Text = time.ToString();
        }

        private void Swap(object sender, EventArgs e)
        {
            string name = ((Button)sender).Name.Substring(((Button)sender).Name.Length - 2, 2);
            TextBox temp = (TextBox) this.Controls["SwapText" + name];

            int team = name[0] - 48;
            int currentPosition = name[1] - 48;
            int swapPosition;

            if (int.TryParse(temp.Text, out swapPosition))
            {
                if(swapPosition>=1 && swapPosition <=5)
                {
                    currentPosition += team * 5;
                    swapPosition += team * 5;
                    PictureBox currentBox = (PictureBox)this.Controls["pick" + currentPosition];
                    PictureBox swapBox = (PictureBox)this.Controls["pick" + swapPosition];

                    var tempImage = currentBox.Image;
                    currentBox.Image = swapBox.Image;
                    swapBox.Image = tempImage;
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblpick_Click(object sender, EventArgs e)
        {

        }
    }
}
