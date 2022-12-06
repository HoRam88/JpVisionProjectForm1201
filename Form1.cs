using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JpVisionProjectForm1201
{
    public partial class Form1 : Form
    {
        String JSON = "./jpvisonpj-Key.json";

        Image image;
        String strFilename;
        const String BUCKET = "jp_vision1201";

        String filePath;
        public Form1()
        {
            InitializeComponent();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

            // menu script >> File >> Open 버튼 클릭시 실행
            openFileDialog1.Title = "Image File Open";
            openFileDialog1.Filter = "All Files(*.*) |*.*|BItmap File(*.bmp) |*.bmp|Jpeg File(*.jpg) |*.jpg";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Console.WriteLine(openFileDialog1.FileName);
                strFilename = openFileDialog1.FileName;
                image = Image.FromFile(strFilename);
                pictureBox1.Image = image;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                filePath = strFilename;
            }
            else
            {
                // 파일이 열리지 않을경우
                Console.WriteLine("File Open Fail");
            }
            Invalidate();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "영상 파일 저장";
            sfd.OverwritePrompt = true;
            sfd.Filter = " All Files(*.*) |*.*| Bitmap File(*.bmp) | *.bmp |Jpeg File(*.jpg) | *.jpg";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string strFilename = sfd.FileName;
                Image saveImage = pictureBox2.Image;
                saveImage.Save(strFilename);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            UseVision uv = new UseVision(strFilename, JSON);
            textBox1.Text = uv.passText();
            pictureBox2.Image = uv.passBitmap();
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            uv = null;
        }

        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Console.WriteLine(openFileDialog1.FileName);
                JSON = openFileDialog1.FileName;
            }
            else
            {
                // 파일이 열리지 않을경우
                Console.WriteLine("JSON File Open Fail");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TransText tt = new TransText(JSON);
            int check = 0;
            if (radioButton1.Checked == true)
            {
                check = 0;
            }else if(radioButton2.Checked == true)
            {
                check = 1;
            }else if(radioButton3.Checked == true)
            {
                check = 2;
            }else if(radioButton4.Checked == true)
            {
                check = 3;
            }

            textBox2.Text = tt.DoTrans(textBox1.Text, check);

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
