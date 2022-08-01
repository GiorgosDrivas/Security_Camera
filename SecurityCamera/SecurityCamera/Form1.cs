using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Imaging;
using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Vision.Motion;

namespace SecurityCamera
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        VideoCaptureDevice device;
        FilterInfoCollection fic;
        MotionDetector motiondetector;        

        private void Form1_Load(object sender, EventArgs e)
        {
            motiondetector = new MotionDetector(new TwoFramesDifferenceDetector(), new MotionAreaHighlighting());
            fic = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo item in fic)
            {
                comboBox1.Items.Add(item.Name);
            }
            comboBox1.SelectedIndex = 0;
        }


        private void btnStart_Click(object sender, EventArgs e)
        {
            device = new VideoCaptureDevice(fic[comboBox1.SelectedIndex].MonikerString);
            videoSourcePlayer1.VideoSource = device;
            videoSourcePlayer1.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            videoSourcePlayer1.Stop();
            this.Close();
        }

        private void videoSourcePlayer1_NewFrame(object sender, ref Bitmap image)
        {
           motiondetector.ProcessFrame(image);
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            videoSourcePlayer1.Stop();
        }
    }
}
