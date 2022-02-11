using System;
using System.IO;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;
using System.Linq;
using System.Threading.Tasks;
using System.Media;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private SoundPlayer simpleSound;
        private SoundPlayer simpleSound2;

        static SerialPort _serialPort;
        public Form1()
        {
            InitializeComponent();
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            _serialPort = new SerialPort();
            _serialPort.PortName = "COM4"; //Set your board COM
            _serialPort.BaudRate = 9600;
            _serialPort.Open();
            simpleSound = new SoundPlayer("StartRecord.wav");
        }

        NAudio.Wave.WaveIn sourceStream = null;
        NAudio.Wave.DirectSoundOut waveOut = null;
        NAudio.Wave.WaveFileWriter waveWriter = null;

        private void button3_Click(object sender, EventArgs e)
        {
            List<NAudio.Wave.WaveInCapabilities> sources = new List<NAudio.Wave.WaveInCapabilities>();

            for (int i = 0; i < NAudio.Wave.WaveIn.DeviceCount; i++)
            {
                sources.Add(NAudio.Wave.WaveIn.GetCapabilities(i));
            }

            sourceList.Items.Clear();

            foreach (var source in sources)
            {
                ListViewItem item = new ListViewItem(source.ProductName);
                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, source.Channels.ToString()));
                sourceList.Items.Add(item);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string a;
            string b;
            string c;
            string d;
            string f;
            int confirmResult = 0;
            bool ticks = false;
            a = _serialPort.ReadExisting();
            Console.WriteLine(a);
            if (a != "\r" && ticks == false)
            {
                Thread.Sleep(250);
                b = _serialPort.ReadLine();
                Thread.Sleep(250);
                c = _serialPort.ReadLine();
                Thread.Sleep(250);
                d = _serialPort.ReadLine();
                Thread.Sleep(250);
                f = _serialPort.ReadLine();
                Thread.Sleep(250);

                if (b == c && b == d && b == f && b != "\r")
                {
                    confirmResult = Int32.Parse(b);

                    try
                    {
                        string path = "C:\\Users\\GEOG-student\\OneDrive - University of Toronto\\Documents\\100. Luke\\100. School\\130. University of Toronto\\2020-2021\\Courses\\Semester02\\Test.txt";
                        if (File.ReadAllLines(path).Contains(confirmResult.ToString()))
                        {
                            Console.WriteLine("already in");
                        }
                        else 
                        {
                            button7.PerformClick();
                            timer1.Interval = 5000;

                            using (StreamWriter sw = File.AppendText(path))
                            {
                                
                            }
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("this escept");
                    }
                }
            }
            stopRecording();
        }

        private void sourceStream_DataAvailable(object sender, NAudio.Wave.WaveInEventArgs e)
        {
            if (waveWriter == null) return;

            waveWriter.WriteData(e.Buffer, 0, e.BytesRecorded);
            waveWriter.Flush();
        }

        private void recording(string name)
        {
            if (sourceList.SelectedItems.Count == 0) return;

            int deviceNumber = sourceList.SelectedItems[0].Index;

            sourceStream = new NAudio.Wave.WaveIn();
            sourceStream.DeviceNumber = deviceNumber;
            sourceStream.WaveFormat = new NAudio.Wave.WaveFormat(44100, NAudio.Wave.WaveIn.GetCapabilities(deviceNumber).Channels);

            string part = "C:\\Users\\GEOG-student\\OneDrive - University of Toronto\\Documents\\100. Luke\\100. School\\130. University of Toronto\\2020-2021\\Courses\\Semester02\\";
            sourceStream.DataAvailable += new EventHandler<NAudio.Wave.WaveInEventArgs>(sourceStream_DataAvailable);
            waveWriter = new NAudio.Wave.WaveFileWriter(part + name + ".wav", sourceStream.WaveFormat);

            sourceStream.StartRecording();
        }

        public void stopRecording()
        {
            if (waveOut != null)
            {
                waveOut.Stop();
                waveOut.Dispose();
                waveOut = null;
            }
            if (sourceStream != null)
            {
                sourceStream.StopRecording();
                sourceStream.Dispose();
                sourceStream = null;
            }
            if (waveWriter != null)
            {
                waveWriter.Dispose();
                waveWriter = null;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
        }


        private void button5_Click(object sender, EventArgs e)
        {
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (sourceList.SelectedItems.Count == 0) return;

            int deviceNumber = sourceList.SelectedItems[0].Index;

            sourceStream = new NAudio.Wave.WaveIn();
            sourceStream.DeviceNumber = deviceNumber;
            sourceStream.WaveFormat = new NAudio.Wave.WaveFormat(44100, NAudio.Wave.WaveIn.GetCapabilities(deviceNumber).Channels);

            sourceStream.DataAvailable += new EventHandler<NAudio.Wave.WaveInEventArgs>(sourceStream_DataAvailable);
            string part = "C:\\Users\\GEOG-student\\OneDrive - University of Toronto\\Documents\\100. Luke\\100. School\\130. University of Toronto\\2020-2021\\Courses\\Semester02\\";
            waveWriter = new NAudio.Wave.WaveFileWriter("C:\\Users\\GEOG-student\\OneDrive - University of Toronto\\Documents\\100. Luke\\100. School\\130. University of Toronto\\2020-2021\\Courses\\Semester02\\hello2.wav", sourceStream.WaveFormat);

            sourceStream.StartRecording();
        }

        int confirmResult = 0;
        private void timer2_Tick(object sender, EventArgs e)
        {
            string a;
            string b;
            string c;
            string d;
            string f;
            bool ticks = false;
            a = _serialPort.ReadExisting();
            Console.WriteLine(a);
            if (a != "\r" && ticks == false)
            {
                Thread.Sleep(250);
                b = _serialPort.ReadLine();
                Thread.Sleep(250);
                c = _serialPort.ReadLine();
                Thread.Sleep(250);
                d = _serialPort.ReadLine();
                Thread.Sleep(250);
                f = _serialPort.ReadLine();
                Thread.Sleep(250);

                if (b == c && b == d && b == f && b != "\r")
                {
                    confirmResult = Int32.Parse(b);

                    try
                    {
                        string path = "C:\\Users\\GEOG-student\\OneDrive - University of Toronto\\Documents\\100. Luke\\100. School\\130. University of Toronto\\2020-2021\\Courses\\Semester02\\Test.txt";
                        if (File.ReadAllLines(path).Contains(confirmResult.ToString()))
                        {
                            Console.WriteLine("already in");
                            
                            timer5.Start();
                            timer2.Stop();
                        }
                        else
                        {
                            timer4.Start();
                            timer2.Stop();
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("this escept");
                    }
                }
            }
        }

        bool first = false;
        int count = 0;

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (first == false)
            {
                Console.WriteLine("Recording Start");
                recording(confirmResult.ToString());
                first = true;
            }
            if (count < 5)
            {
                count++;
            }
            else if (count >= 5)
            {
                string path = "C:\\Users\\GEOG-student\\OneDrive - University of Toronto\\Documents\\100. Luke\\100. School\\130. University of Toronto\\2020-2021\\Courses\\Semester02\\Test.txt";
                count = 0;
                stopRecording();
                Console.WriteLine("Recording End");
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(confirmResult.ToString());
                }
                first = false;
                timer3.Stop();
                timer2.Start();
            }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            
            
        }

        bool first2 = false;
        int count2 = 0;
        private void timer4_Tick(object sender, EventArgs e)
        {
            if (first2 == false)
            {
                simpleSound.Play();
                first2 = true;
            }
            if(count2 < 3)
            {
                count2++;
            }
            else if (count2 >= 3)
            {
                count2 = 0;
           
                first2 = false;
                timer4.Stop();
                timer3.Start();
            }
        }

        bool first3 = false;
        int count3 = 0;
        private void timer5_Tick(object sender, EventArgs e)
        {
            if (first3 == false)
            {
                simpleSound2 = new SoundPlayer("C:\\Users\\GEOG-student\\OneDrive - University of Toronto\\Documents\\100. Luke\\100. School\\130. University of Toronto\\2020-2021\\Courses\\Semester02\\" + confirmResult.ToString() + ".wav");
                simpleSound2.Play();
                first3 = true;
            }
            if (count3 < 5)
            {
                count3++;
            }
            else if (count3 >= 5)
            {
                count3 = 0;

                first3 = false;
                timer5.Stop();
                timer2.Start();
            }
        }
    }
}
