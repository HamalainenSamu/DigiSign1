using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing.Imaging;
using System.Text;
using System.Windows.Forms;
using VBTablet;
using System.IO;
using System.Diagnostics;
using System.Globalization;
using CsvHelper;

namespace DigiSign
{
    public partial class Form1 : Form
    {
        Bitmap DrawArea;

        public Tablet Digitizer;

        int Xold, Yold;
        int count;
        int totalSampleAmount;

        string imageDirectoryPath;
        string videoDirectoryPath;
        string newDirectoryPath;
        string newFileName;
        string fileName;
        string header;
        string sampleName;
        string forgeSample;
        string directory;
        string forgeryPicPath;
        string exampleFile;
        string fileChar;
        string inputLine;

        bool forgeMode;


        List<string> dataInputList;

        DateTime Start = DateTime.Now;

        private FolderBrowserDialog fbd;
  
        public Form1()
        {
            InitializeComponent();

            DrawArea = new Bitmap(signatureBox.Width, signatureBox.Height);
            signatureBox.Image = DrawArea;

            dataInputList = new List<string>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            startSamplingBtn.Enabled = false;

            retryBtn.Enabled = false;
            retryBtn.Hide();

            nextSampleBtn.Enabled = false;
            nextSampleBtn.Hide();

            endBtn.Enabled = false;
            endBtn.Hide();


            fbd = new FolderBrowserDialog();

            count = 0;

            System.Drawing.Graphics g;
            g = Graphics.FromImage(DrawArea);

            Pen ppen = new Pen(Brushes.White);
            g.DrawLine(ppen, 0, 0, 200, 200);
            g.Clear(Color.Black);
            g.Dispose();        

            try
            {
                Digitizer = new Tablet();

                //sldGranularity.Value = 2;

                Digitizer.UnavailableIsError = false;

                Digitizer.PacketArrival += new Tablet.PacketArrivalEventHandler(PacketArrival);

                //connect wacom tablet
                IntPtr Hwnd;
                bool IsDigitizingContext = false;
                string ContextID = "FirstContext";

                Hwnd = this.Handle;
                Digitizer.hWnd = Hwnd;
                Digitizer.AddContext(ContextID, ref IsDigitizingContext);
                Digitizer.SelectContext(ref ContextID);
                Digitizer.Connected = true;
                Digitizer.Context.QueueSize = 32;//Set queue size to a reasonable value

                Digitizer.Context.TrackingMode = true;
                Digitizer.Context.Enabled = true;

            }

            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "Digitizer device is not connected!");

            }
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            bool temp, res;

            if (!Digitizer.Context.hCtx.Equals(IntPtr.Zero))
            {
                temp = true;
                res = Digitizer.Context.Reposition(ref temp);
            }
        }

        private void Form1_Deactivated(object sender, EventArgs e)
        {
            bool temp, res;

            if (!Digitizer.Context.hCtx.Equals(IntPtr.Zero))
            {

                temp = false;
                res = Digitizer.Context.Reposition(ref temp);
            }
        }

        private void sldGranularity_Scroll(object sender, EventArgs e)
        {
            //Digitizer.PktGranularity = (short)sldGranularity.Value;

        }

        private void setup2()
        {
            DirectoryInfo di = Directory.CreateDirectory(newDirectoryPath);

            newFileName = newDirectoryPath + sampleName + count + "Raw" + ".csv";

            header = "x;y;pressure;z;time;velocityx;velocityy;accelx;accely" + Environment.NewLine;

            File.WriteAllText(newFileName, header);

            long xextent;
            long yextent;

            //Digitizer.PktGranularity = 5;

            xextent = signatureBox.Width;
            yextent = signatureBox.Height;


            Digitizer.Context.Options.IsSystemCtx = false;
            Digitizer.Context.OutputExtentX = (int)xextent;
            Digitizer.Context.OutputExtentY = (int)yextent;
            Digitizer.Context.Update();

            if (count == totalSampleAmount)
            {
                nextSampleBtn.Enabled = false;
                nextSampleBtn.Dispose();

                endBtn.Show();
                endBtn.Enabled = true;
            }
        }

        private void setup()
        {
            //clear box
            Graphics g;
            g = Graphics.FromImage(DrawArea);

            Pen ppen = new Pen(Brushes.White);
            g.DrawLine(ppen, 0, 0, 200, 200);
            g.Clear(Color.Black);
            g.Dispose();

            count++;

            countLb.Text = "Sign " + count + "/" + totalSampleAmount;

            

            //iniate lists for saving tablet data of each signature before appending it to csv in order to optimize performance
            List<List<string>> tabletData = new List<List<string>>();
            
            List<string> dataInputList = new List<string>();



            //information for csv saving

            if (forgeMode == true)
            {
                newDirectoryPath = "C:\\Users/Samu/Desktop/DigiSign/DigiSign/SignData/" + forgeSample + "/RawData/";

                //videoDirectoryPath = "C:\\Users/Samu/Desktop/DigiSign/DigiSign/SignData/USER1/Video/";

                //DirectoryInfo di3 = Directory.CreateDirectory(videoDirectoryPath);

                setup2();
            }

            if (forgeMode == false)
            {
                newDirectoryPath = "C:\\Users/Samu/Desktop/DigiSign/DigiSign/SignData/" + sampleNameTextBox.Text + "/RawData/";

                imageDirectoryPath = "C:\\Users/Samu/Desktop/DigiSign/DigiSign/SignData/" + sampleNameTextBox.Text + "/Images/";

                DirectoryInfo di2 = Directory.CreateDirectory(imageDirectoryPath);

                setup2();
            }
                        
        }

        public void PacketArrival(ref IntPtr ContextHandle, ref int Cursor_Renamed, ref int X, ref int Y, ref int Z, ref int Buttons, ref int Pressure, ref int TangentPressure, ref int Azimuth, ref int Altitude, ref int Twist, ref int Pitch, ref int Roll, ref int Yaw, ref int PacketSerial, ref int PacketTim)
        {

            Graphics g;
            g = Graphics.FromImage(DrawArea);
            Pen ppen = new Pen(Color.White);

            if (Pressure > 0) // 'catch normalpressure and button 1
            {  
                try
                {
                    g.DrawLine(ppen, X, signatureBox.Height - Y, Xold, signatureBox.Height - Yold + 1);

                    signatureBox.Image = DrawArea;

                    g.Dispose();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            //dataInputList.Add(X + ";" + Y + ";" + Pressure + ";" + Z + ";" + DateTime.Now.Subtract(Start).TotalMilliseconds.ToString() + ";" + "0" + ";" + "0" + ";" + "0" + ";" + "0");

            Xold = X;
            Yold = Y;

            //Append to data to CSV
            File.AppendAllText(newFileName, X + ";" + Y + ";" + Pressure + ";" + Z + ";" + DateTime.Now.Subtract(Start).TotalMilliseconds.ToString() + ";" + "0" + ";" + "0" + ";" + "0" + ";" + "0" + "\n");
        }

        //Append to data to CSV
        private void appendDataToCSV()
        {
            foreach (var line in dataInputList)
            {
                File.AppendAllText(newFileName, line + "\n");
            }
        }

        //Clean csv data to correct format
        private void csvCleaner()
        {
            inputLine = "";
            
            for (int i = 1; i <= totalSampleAmount; i++)
            {
                string openFilePath = File.ReadAllText(@"C:\\Users/Samu/Desktop/DigiSign/DigiSign/SignData/" + directory +  "/RawData/" + sampleName + i + "Raw" + ".csv");
                StringReader reader = new StringReader(openFilePath);
                List<List<string>> data = new List<List<string>>();

                while ((inputLine = reader.ReadLine()) != null)
                {
                    if (inputLine.Trim().Length > 0)
                    {
                        string[] inputArray = inputLine.Split(new char[] { ';' });
                        data.Add(inputArray.ToList());
                    }
                }

                int j = 1;

                //remove data rows that are created before pen touches the tablet
                while (data[j][2] == "0")
                {
                    data.RemoveAt(j);
                }

                //remove data rows that are created after signature is completed
                j = data.Count - 1;
                while (data[j][2] == "0")
                {
                    data.RemoveAt(j);
                    j--;
                }


                //format time feature to start from zero
                float sum;
                j = 1;
                float zeroTime = float.Parse(data[1][4]);

                while (j < data.Count)
                {
                    sum = float.Parse(data[j][4]) - zeroTime;

                    data[j][4] = sum.ToString();
                    j++;
                }

                //normalize x and y to positive relative values
                j = 1;
                int sumX;
                int sumY;

                int firstX = Int32.Parse(data[1][0]);
                int firstY = Int32.Parse(data[1][1]);

                //Most negative x and y
                int minX = 0;
                int minY = 0;

                while (j < data.Count)
                {
                    sumX = Int32.Parse(data[j][0]);
                    sumY = Int32.Parse(data[j][1]);

                    data[j][0] = (sumX - firstX).ToString();
                    data[j][1] = (sumY - firstY).ToString();

                    //get min X from column
                    if (Int32.Parse(data[j][0]) < minX)
                    {
                        minX = Int32.Parse(data[j][0]);
                    }

                    //get min Y from column
                    if (Int32.Parse(data[j][1]) < minY)
                    {
                        minY = Int32.Parse(data[j][1]);
                    }

                    j++;
                }


                //shift all x and y coordinates to positive, minX and minY will be zero after this
                j = 1;

                if (minX < 0)
                {
                    int minXPos = System.Math.Abs(minX);

                    while (j < data.Count)
                    {
                        data[j][0] = (Int32.Parse(data[j][0]) + minXPos).ToString();
                        j++;
                    }
                }

                j = 1;

                if (minY < 0)
                {
                    int minYPos = System.Math.Abs(minY);

                    while (j < data.Count)
                    {
                        data[j][1] = (Int32.Parse(data[j][1]) + minYPos).ToString();
                        j++;
                    }
                }

         
                j = 2;

                //adding velocities for x and y
                while (j < data.Count)
                {
                    // x
                    data[j][5] = ((float.Parse(data[j][0]) - float.Parse(data[j - 1][0])) / (float.Parse(data[j][4]) - float.Parse(data[j - 1][4]))).ToString();

                    // y
                    data[j][6] = ((float.Parse(data[j][1]) - float.Parse(data[j - 1][1])) / (float.Parse(data[j][4]) - float.Parse(data[j - 1][4]))).ToString();
                    j++;
                }

                j = 2;

                //adding accelerations of x, y
                while (j < data.Count)
                {
                    // x
                    data[j][7] = ((float.Parse(data[j][5]) - float.Parse(data[j - 1][5])) / (float.Parse(data[j][4]) - float.Parse(data[j - 1][4]))).ToString();

                    // y
                    data[j][8] = ((float.Parse(data[j][6]) - float.Parse(data[j - 1][6])) / (float.Parse(data[j][4]) - float.Parse(data[j - 1][4]))).ToString();
                    j++;
                }

                //write to a new csv file
                string output = string.Join("\r\n", data.AsEnumerable()
                .Select(x => string.Join(";", x.Select(y => y).ToArray())).ToArray());
                File.WriteAllText(@"C:\\Users/Samu/Desktop/DigiSign/DigiSign/SignData/" + directory + "/" + sampleName + i + ".csv", output);
            }
        }

        private void saveSignatureImage()
        {
            Bitmap bmp = new Bitmap(signatureBox.ClientSize.Width, signatureBox.ClientSize.Height);
            signatureBox.DrawToBitmap(bmp, signatureBox.ClientRectangle);
            bmp.Save(imageDirectoryPath + sampleNameTextBox.Text + count + ".jpg", ImageFormat.Jpeg);
        }

        private void nextSampleBtn_Click(object sender, EventArgs e)
        {

            setup();
        }

        private void startSamplingBtn_Click(object sender, EventArgs e)
        {
            startSamplingBtn.Dispose();
            sampleCountBox.Dispose();
            genBtn.Dispose();
            forBtn.Dispose();
            label1.Dispose();

            sampleNameTextBox.Enabled = false;

            retryBtn.Show();
            retryBtn.Enabled = true;

            nextSampleBtn.Show();
            nextSampleBtn.Enabled = true;

            //grab info relating to filename in order to clean csv data in csvCleaner()

            if (!forgeMode)
            {
                sampleName = "SIGN_GEN_" + sampleNameTextBox.Text + "_" + sampleNameTextBox.Text + "_";
                totalSampleAmount = Convert.ToInt32(sampleCountBox.Value);


                if (String.IsNullOrEmpty(sampleNameTextBox.Text))
                {
                    sampleNameLb.Text = "Name the sample!!";
                }

                setup();

            }

            if (forgeMode)
            {
                sampleName = "SIGN_FOR_" + forgeSample + "_" + sampleNameTextBox.Text + "_";
                totalSampleAmount = Convert.ToInt32(sampleCountBox.Value);


                if (String.IsNullOrEmpty(sampleNameTextBox.Text))
                {
                    sampleNameLb.Text = "Name the sample!!";
                }

                setup();
            }
        }

        private void retryBtn_Click(object sender, EventArgs e)
        {
            count--;
            setup();
        }

        private void randomSelectImage()
        {
            var rand = new Random();
            var files = Directory.GetFiles(forgeryPicPath, "*.csv");
            string file = files[rand.Next(files.Length)];

            fileChar = file.Replace(sampleNameTextBox.Text, "");
            exampleFile = fileChar;


            createImages(exampleFile); 
        }


        private void createImages(string exampleFile)
        {
            string openFilePath = File.ReadAllText(exampleFile);
            StringReader reader = new StringReader(openFilePath);
            List<List<string>> data2 = new List<List<string>>();

            while ((inputLine = reader.ReadLine()) != null)
            {
                if (inputLine.Trim().Length > 0)
                {
                    string[] inputArray = inputLine.Split(new char[] { ';' });
                    data2.Add(inputArray.ToList());
                }
            }

            Bitmap drawSignBit = new Bitmap(signatureBox.Size.Width, signatureBox.Size.Height);
            ImageList images = new ImageList();
            Pen mypen = new Pen(Brushes.White);
            Graphics e;
            

            int k;

            for (k = 2; k < data2.Count; k++)
            {
                e = Graphics.FromImage(drawSignBit);

                e.DrawLine(mypen, int.Parse(data2[k][0]), signatureBox.Height - int.Parse(data2[k][1]), int.Parse(data2[k - 1][0]), signatureBox.Height - int.Parse(data2[k - 1][1]) + 1);

                signatureBox.Image = drawSignBit;
                
                Bitmap bmp = new Bitmap(signatureBox.ClientSize.Width, signatureBox.ClientSize.Height);
                signatureBox.DrawToBitmap(bmp, signatureBox.ClientRectangle);
                bmp.Save(@"C:\\Users/Samu/Desktop/forgery/" + k + ".jpg", ImageFormat.Jpeg);

                e.Dispose();
            }
        }



        private void forBtn_Click(object sender, EventArgs e)
        {

            forgeMode = true;

            if (String.IsNullOrEmpty(sampleNameTextBox.Text))
            {
                sampleNameLb.Text = "Name the sample!!";
            }

            if (fbd.ShowDialog() == DialogResult.OK)
            {
                forgeSample = new DirectoryInfo(fbd.SelectedPath).Name;
                forgeryPicPath = fbd.SelectedPath;
            }
            
            randomSelectImage();
            
            sampleName = "SIGN_FOR_" + forgeSample + "_" + sampleNameTextBox.Text + "_";

            forBtn.Enabled = false;
            genBtn.Enabled = false;
            sampleCountBox.Enabled = false;
            startSamplingBtn.Enabled = true;
            sampleNameTextBox.Enabled = false;
        }

        private void genBtn_Click(object sender, EventArgs e)
        {

            forgeMode=false;

            if (String.IsNullOrEmpty(sampleNameTextBox.Text))
            {
                sampleNameLb.Text = "Name the sample!!";
            }

            sampleName = "SIGN_GEN_" + sampleNameTextBox.Text + "_" + sampleNameTextBox.Text + "_";

            genBtn.Enabled = false;
            forBtn.Enabled = false;
            sampleCountBox.Enabled = false;
            startSamplingBtn.Enabled = true;
            sampleNameTextBox.Enabled = false;
        }


        private void endBtn_Click(object sender, EventArgs e)
        {
            if (forgeMode == true)
            {
                directory = forgeSample;

                //appendDataToCSV();
                csvCleaner();
                this.Close();
            }

            directory = sampleNameTextBox.Text;

            saveSignatureImage();
            //appendDataToCSV();
            csvCleaner();
            this.Close();
        }
    }
}