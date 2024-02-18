using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Task_2
{
    public partial class Form1 : Form
    {

        private int score = 0;
        private int missed = 0;
       
        private Random rand = new Random();
        private List<PictureBox> pictureBoxes = new List<PictureBox>();
        //private List<PictureBox> pictureForDelete = new List<PictureBox>();
        private object lockObject = new object();

        public Form1()
        {
            InitializeComponent();
            timer3.Interval = 300;
            timer3.Start();
            timer1.Interval = 10;
            timer1.Start();
            timer2.Interval = 1500;
            timer2.Start();
            timer4.Start();
            timer4.Interval = 300;



        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (var item in pictureBoxes)
            {

                item.Location = new Point(item.Location.X, item.Location.Y + 1);
            }


        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            PictureBox picture1 = (PictureBox)sender;

            this.Controls.Remove(picture1);
            pictureBoxes.Remove(picture1);

            score++;
            label1.Text = $"Score: {score.ToString()}";
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            PictureBox picture = new PictureBox();
            picture.Location = new Point(randForY(), 9);
            picture.Size = new Size(50, 50);
            picture.Image = Image.FromFile("C:\\Visual\\18.02.2024\\Task 2\\Task 2\\assets\\ball.png");
            picture.Click += pictureBox1_Click;
            this.Controls.Add(picture);
            pictureBoxes.Add(picture);
        }


        private int randForY()
        {
            return rand.Next(100, Screen.PrimaryScreen.Bounds.Width - 50);
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            lock (lockObject)
            {
                List<PictureBox> pictureToDelete = new List<PictureBox>();
                foreach (var item in pictureBoxes)
                {
                    if (item.Location.Y > this.Bounds.Height)
                    {
                        this.Controls.Remove(item);
                        pictureToDelete.Add(item);

                    }
                }

                foreach (var item in pictureToDelete)
                {

                    pictureBoxes.Remove(item);
                    addIncrement();
                }
            }



        }

        private void timer4_Tick(object sender, EventArgs e)
        {

            label2.Text = $"Missed: {missed.ToString()}";
        }

        private void addIncrement()
        {
            Interlocked.Increment(ref missed);
        }
    }
}
