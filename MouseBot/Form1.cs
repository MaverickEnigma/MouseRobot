﻿using System;
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
        bool pause=false;
        public FileInfo file;
        int waitTime=15;
        long bytes=0;
        bool stop = false;
        long temp;
        int starter = 1;
        NetworkInterface[] networks = NetworkInterface.GetAllNetworkInterfaces();
        int timeHolder=0;
        Stopwatch watch = new Stopwatch();
        Stopwatch totalLength=new Stopwatch();
        DirectoryInfo dir;
        bool offcycle = false;
        StreamWriter writer;
        public mouseBot_Form()
        {
            InitializeComponent();
            dir = new DirectoryInfo(@"C:\Users\Taylor Collins\Documents\Untitled");
            openFileDialog1 = new OpenFileDialog();
        }


        private void btn_Go_Click(object sender, EventArgs e)
        {    //Select the .tpp file to watch what TeleportPro is adding
            if (!stop)
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    file = new FileInfo(@"C:\Users\Taylor Collins\Documents\Untitled.tpp");
                    temp = file.Length;
                    time.Start();
                    watch.Start();
                    totalLength.Start();
                    old = dir.GetFiles().Length;
                    time.Interval = 300000;
                    time.Tick += new EventHandler(time_Tick);
                    stop = true;
                    btn_Go.Text = "Stop";
                }
                else
                    this.Close();
            }
            else
                this.Close();

        }


        /// <summary>
        /// Timer event.  Keeps occuring until timer is stopped. 
        /// </summary>
        /// <param name="myObject"></param>
        /// <param name="myEventArg"></param>
        private void time_Tick(Object myObject,EventArgs myEventArg)
        {
            bytes = 0;
            foreach (NetworkInterface n in networks)
            {
                bytes += n.GetIPv4Statistics().BytesReceived;
            }
            using (writer = new StreamWriter(@"C:\Users\Taylor Collins\Documents\School\SeniorProj\outtimes.txt", true))
            {
                if (offcycle && watch.ElapsedMilliseconds > (waitTime * 1000))
                {
                    Process.Start(@"C:\Users\Taylor Collins\Documents\MouseRobot Tasks\Samples\PP.xtsk");
                    if (waitTime > 1200)
                        Process.Start(@"C:\Users\Taylor Collins\Documents\MouseRobot Tasks\Samples\QQ.xtsk");
                }
              
                if (starter - 1 == 0)// =>? starter-- > 0
                {
                    writer.WriteLine("\n\nBeginning Run");
                    starter = 0;
                }
                else { }

                check = dir.GetFiles().Length;
                if (check <= old&&file.Length<=temp)
                {
                    if (!offcycle)
                    {
                        // This instance should always be pausing the crawl.
                        Process.Start(@"C:\Users\Taylor Collins\Documents\MouseRobot Tasks\Samples\PP.xtsk");
                        pause = true;
                        timeHolder = time.Interval;
                        writer.WriteLine(Convert.ToInt32(time.Interval.ToString()) / 1000+" second interval");
                        writer.WriteLine("After " + watch.Elapsed.Seconds+" seconds");
                        writer.WriteLine("Using " + bytes+" bytes");
                        watch.Restart();
                        offcycle = true;
                        time.Interval = (waitTime*1000);
                        btn_Go.ForeColor = System.Drawing.Color.CadetBlue;
                        return;
                    }
                    else
                    {
                        waitTime += 2;
                        time.Interval += 5000;
                        //If the program is running and not moving forward it should be paused again.
                        if (!pause)
                        {
                            Process.Start(@"C:\Users\Taylor Collins\Documents\MouseRobot Tasks\Samples\PP.xtsk");
                            pause = true;
                        }
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
                    Process.Start(@"C:\Users\Taylor Collins\Documents\MouseRobot Tasks\Samples\PP.xtsk");
                }
                else
                {
                    time.Interval += 2000;
                }
                offcycle = false;
                old = dir.GetFiles().Length;
                temp = file.Length;
            }
        }

        private void mouseBot_Form_Load(object sender, EventArgs e)
        {

        }

        private void mouseBot_Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            using (writer = new StreamWriter(@"C:\Users\Taylor Collins\Documents\School\SeniorProj\outtimes.txt", true))
            {
                writer.WriteLine("Total Run Time" + totalLength.Elapsed);
                writer.WriteLine("Longest wait Time:" +waitTime);
            }
            writer.Close();

        }

        
    }
}
