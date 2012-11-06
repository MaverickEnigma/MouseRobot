using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Net.NetworkInformation;

namespace MouseBot
{
    public partial class mouseBot_Form : Form
    {
        public Timer time = new Timer();
        public long old;
        public long check;
        public FileInfo file;
        int waitTime=1;
        long temp;
        IPv4InterfaceProperties thing;//Exploratory addition
        int timeHolder=0;
        Stopwatch watch = new Stopwatch();
        DirectoryInfo dir;
        bool offcycle = false;
        string file1 = "";
        Process proc = new Process();
        StreamWriter writer;
        public mouseBot_Form()
        {
            InitializeComponent();
            dir = new DirectoryInfo(@"C:\Users\Taylor Collins\Documents\Untitled");
            openFileDialog1 = new OpenFileDialog();
        }

        private void btn_Go_Click(object sender, EventArgs e)
        {
            IPv4InterfaceStatistics dunno;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                file1 = openFileDialog1.FileName;
                file = new FileInfo(@"C:\Users\Taylor Collins\Documents\Untitled.tpp");
                proc = Process.Start(file1);
              
                time.Start();
                watch.Start();
                old = dir.GetFiles().Length;
                time.Interval = 15000;
                time.Tick += new EventHandler(time_Tick);
                temp = file.Length;
                
            }
        }


        /// <summary>
        /// Timer event.  Keeps occuring until timer is stopped, even if event is not passed 
        /// </summary>
        /// <param name="myObject"></param>
        /// <param name="myEventArg"></param>
        private void time_Tick(Object myObject,EventArgs myEventArg)
        {
            using (writer = new StreamWriter(@"C:\Users\Taylor Collins\Documents\School\SeniorProj\outtimes.txt", true))
            {

                if (offcycle && watch.ElapsedMilliseconds > (waitTime*1000))
                    //Process.Start(@"C:\Users\Taylor Collins\Documents\MouseRobot Tasks\Samples\QQ.xtsk");

               
                check = dir.GetFiles().Length;
                if (check <= old)
                {
                    //if (!(temp <= file.Length))
                    //    proc.Start();
                    //    return;
                    if (!offcycle)
                    {
                        Process.Start(@"C:\Users\Taylor Collins\Documents\MouseRobot Tasks\Samples\PP.xtsk");
                        timeHolder = time.Interval;
                        writer.WriteLine(Convert.ToInt32(time.Interval.ToString()) / 1000);
                        writer.WriteLine("After " + watch.Elapsed.Seconds);
                        watch.Restart();
                        offcycle = true;
                        time.Interval = 3000;
                        btn_Go.ForeColor = System.Drawing.Color.CadetBlue;
                        return;
                    }
                    else
                    {
                        return;
                    }

                    //time.Stop();
                    //this.Close();
                }
                if (offcycle)
                {
                    btn_Go.ForeColor = System.Drawing.Color.Crimson;
                    writer.WriteLine("Restarted after " + watch.Elapsed);
                    time.Interval = timeHolder;
                    proc.Start();
                }
                else
                {
                    time.Interval += 2000;
                }
                offcycle = false;
                old = file.Length;
            }
        }

        private void mouseBot_Form_Load(object sender, EventArgs e)
        {

        }

        private void mouseBot_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            writer.Close();
        }

    }
}
