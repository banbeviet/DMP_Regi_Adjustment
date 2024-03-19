﻿// v0.1
// Initial Draft version

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO; // To handle the file
using System.Runtime.InteropServices;
using System.Diagnostics;
using LibUsbDotNet.DeviceNotify;
using LibUsbDotNet;
using LibUsbDotNet.Info;
using LibUsbDotNet.Main;
using System.Threading;
using System.Drawing.Printing;


using InTheHand.Net;
using InTheHand.Net.Sockets;
using InTheHand.Net.Bluetooth;
using System.Net.Sockets;

namespace DMP_Regi_Adjustment_Program
{

    public enum PrinterStatus
    {
        USB_DISCONNECTED,
        USB_CONNECTED_FIRST,
        USB_CONNECTED_READY,
    };

    public partial class Form1 : Form
    {
        int Nail_Y;
        int Nail_M;
        int Nail_C;
        int Photo_M;
        int Photo_C;

        FileStream stream_Logs;

        bool isFormClosing = false;

        #region INI Handling code - config.ini
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string Section, string Key, string Value, string Filepath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string Section, string Key, string def, StringBuilder retVal, int size, string Filepath);
        public void Setinifile(string section, string key, string value)
        {
            WritePrivateProfileString(section, key, value, System.Windows.Forms.Application.StartupPath + "\\config.ini");
        }
        public string Getinifile(string section, string key)
        {
            StringBuilder STBD = new StringBuilder(1000);
            GetPrivateProfileString(section, key, null, STBD, 5000, System.Windows.Forms.Application.StartupPath + "\\config.ini");
            return STBD.ToString().Trim();
        }
        #endregion

        public static IDeviceNotifier UsbDeviceNotifier = DeviceNotifier.OpenDeviceNotifier();


        Process processDbgMon, readingProcess;
        Thread thread;

        PrinterStatus sPrinterStatus;

        SQLiteManager db;
        string RemainedMedia = "";
        String bluetoothError = null;
        Boolean isManufacturingReset = false;
        public Form1()
        {
            InitializeComponent();
            //enable_works();

            //Photo_groupbox.Enabled = false;

            ProcessStartInfo startInfo = new ProcessStartInfo("taskkill");
            startInfo.Arguments = "/im dbgmon.exe /f";
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            Process.Start(startInfo);

            processDbgMon = new Process();
            processDbgMon.EnableRaisingEvents = true;
            processDbgMon.StartInfo.CreateNoWindow = false;
            processDbgMon.StartInfo.UseShellExecute = false;

            foreach (string fileName in Directory.GetFiles(Application.StartupPath+ "\\dbgmon", "*.log"))
            {
                try
                {
                    File.Delete(fileName);
                }
                catch
                {
                    continue;
                }
            }

            readingProcess = new Process();
            readingProcess.StartInfo = new ProcessStartInfo
            {
                FileName = Application.StartupPath + "\\dbgmon\\dbgmon.exe",
                Arguments = "read.txt",
                UseShellExecute = false,
                CreateNoWindow = true
            };
            //readingProcess.StartInfo.FileName = Application.StartupPath + "\\dbgmon\\dbgmon.exe";
            //readingProcess.StartInfo.Arguments = "read.txt result.txt";
            //readingProcess.StartInfo.CreateNoWindow = true;
            //readingProcess.StartInfo.UseShellExecute = false;
            readingProcess.Exited += readingProcess_Exited;

            UsbDeviceNotifier.OnDeviceNotify += OnDeviceNotifyEvent; // 이벤트 핸들러 추가
            UsbDeviceNotifier.Enabled = true;  // 장치인식하는 걸 안하게 하는거!!
            UsbDevice.ForceLibUsbWinBack = true;

            thread = new Thread(StatusMonitor);
            CheckForIllegalCrossThreadCalls = false;

            CreateTheLog_csv();

            thread.Start();
        }

        void readingProcess_Exited(object sender, EventArgs e)
        {
            String btMac = null;

            try
            {
                using (StreamReader reader = new StreamReader(new FileStream("dbgmon\\ds_AppManage.log", FileMode.Open)))
                {
                    Char[] delimeterChars = { ' ', '=', ',', '\t', '\"', '%', '(', ')', '|' };
                    String[] strArray = null;

                    while (!reader.EndOfStream)
                    {
                        strArray = reader.ReadLine().Split(delimeterChars, StringSplitOptions.RemoveEmptyEntries);

                        try
                        {
                            for (Int32 i = 0; i < strArray.Length; i++)
                            {
                                if (strArray[i].Equals("[BT]"))
                                {
                                    btMac = strArray[i + 1];
                                }
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void OnDeviceNotifyEvent(object sender, DeviceNotifyEventArgs e)
        {
            //UsbRegDeviceList allDevices = UsbDevice.AllDevices;
            switch (e.EventType)
            {
                case EventType.DeviceRemoveComplete:
                    if (e.Device != null)
                    {
                        if ((e.Device.IdVendor == 0x3501) && (e.Device.IdProduct == 0x0201))
                        {
                            Debug.WriteLine("Device Outgoing");
                            OnUsbDisconnect(0201);
                        }
                    }
                    break;

                case EventType.DeviceArrival:
                    if (e.Device != null)
                    {
                        if ((e.Device.IdVendor == 0x3501) && (e.Device.IdProduct == 0x0201) && (PrinterSN_textBox.TextLength == 18))
                        {
                            Debug.WriteLine("Device Incomming");
                            OnUsbConnect(0201);
                        }
                    }
                    break;
            }
        }
        private void OnUsbConnect(int PID)
        {
            if (PID == 0201)
            {
                toolStripStatusLabel1.Text = @"USB Connected";
            }
            sPrinterStatus = PrinterStatus.USB_CONNECTED_FIRST;

            //processStatus = DbgmonProcessStatus.PROCESS_RUN;
            //Update();
        }
        private void OnUsbDisconnect(int PID)
        {

            if (PID == 0201)
            {
                toolStripStatusLabel1.Text = @"USB Disconnected";
            }
            sPrinterStatus = PrinterStatus.USB_DISCONNECTED;
            disable_works();
        }

        private void enable_works()
        {
            NailY_numericUpDown.Enabled = true;
            NailM_numericUpDown.Enabled = true;
            NailC_numericUpDown.Enabled = true;
            PhotoM_numericUpDown.Enabled = true;
            PhotoC_numericUpDown.Enabled = true;

            PrintNail_button.Enabled = true;
            PrintPhoto_button.Enabled = true;
            Nail_Regi_write_button.Enabled = true;
            Photo_Regi_write_button.Enabled = true;
        }
        private void disable_works()
        {
            NailY_numericUpDown.Enabled = false;
            NailM_numericUpDown.Enabled = false;
            NailC_numericUpDown.Enabled = false;
            PhotoM_numericUpDown.Enabled = false;
            PhotoC_numericUpDown.Enabled = false;

            NailY_numericUpDown.Value = 0;
            NailM_numericUpDown.Value = 0;
            NailC_numericUpDown.Value = 0;
            PhotoM_numericUpDown.Value = 0;
            PhotoC_numericUpDown.Value = 0;

            PrintNail_button.Enabled = false;
            PrintPhoto_button.Enabled = false;
            Nail_Regi_write_button.Enabled = false;
            Photo_Regi_write_button.Enabled = false;

            PrinterSN_textBox.Text = "";
            PrinterSN_textBox.Focus();
        }

        private void StatusMonitor()
        {
            string caption = null;

            try
            {
                // always run in this loop
                while (true)
                {
                    switch (sPrinterStatus)
                    {
                        case PrinterStatus.USB_DISCONNECTED:
                            caption = "@DISCONNECT";
                            DeviceStatus_label.Text = "DISCONNECT";
                            break;
                        case PrinterStatus.USB_CONNECTED_FIRST:
                            caption = "@CONNECTED_FIRST";
                            ParseAddressFirmware();
                            break;
                        case PrinterStatus.USB_CONNECTED_READY:
                            caption = "@CONNECTED_READY";
                            DBGMonStatus_label.Text = "READY";
                            //ParseAddressFirmware();
                            break;

                        default:
                            break;
                    }
                    Thread.Sleep(100);

                    // for debugging
                    //Thread.Sleep(5000);
                }
            }
            catch (Exception ex)
            {
                if (!isFormClosing)
                {
                    Invoke(new Action(delegate ()
                    {
                        MessageBox.Show(ex.Message, caption);
                        MessageBox.Show(ex.StackTrace, caption);
                    }));

                    try
                    {
                        processDbgMon.Kill();
                        processDbgMon.Close();
                    }
                    catch
                    {
                        Debug.WriteLine("KillProcess error catch");
                    }

                    Application.Exit();
                }
            }
        }

        private void ParseAddressFirmware()
        {
            processDbgMon.StartInfo.FileName = ".\\dbgmon\\GetRegi.bat";
            processDbgMon.StartInfo.UseShellExecute = false;
            processDbgMon.StartInfo.CreateNoWindow = true;
            DBGMonStatus_label.Text = "Parse PSB...";
            processDbgMon.Start();
            while (!processDbgMon.HasExited)
            {
                if (sPrinterStatus == PrinterStatus.USB_DISCONNECTED)
                {
                    try
                    {
                        processDbgMon.Kill();
                        processDbgMon.Close();
                    }
                    catch
                    {
                        Debug.WriteLine("KillProcess error catch");
                    }
                    break;
                }
            }
			
			if (sPrinterStatus != PrinterStatus.USB_DISCONNECTED)
            {
                using (FileStream fileStream = new System.IO.FileStream(Environment.CurrentDirectory + "\\dbgmon\\NAY.bin", FileMode.Open))
                { Nail_Y = fileStream.ReadByte(); }

                using (FileStream fileStream = new System.IO.FileStream(Environment.CurrentDirectory + "\\dbgmon\\NAM.bin", FileMode.Open))
                { Nail_M = fileStream.ReadByte(); }

                using (FileStream fileStream = new System.IO.FileStream(Environment.CurrentDirectory + "\\dbgmon\\NAC.bin", FileMode.Open))
                { Nail_C = fileStream.ReadByte(); }

                using (FileStream fileStream = new System.IO.FileStream(Environment.CurrentDirectory + "\\dbgmon\\PHM.bin", FileMode.Open))
                { Photo_M = fileStream.ReadByte(); }

                using (FileStream fileStream = new System.IO.FileStream(Environment.CurrentDirectory + "\\dbgmon\\PHC.bin", FileMode.Open))
                { Photo_C = fileStream.ReadByte(); }

                NailY_numericUpDown.Value = (decimal)Byte2StringData(Nail_Y);
                NailM_numericUpDown.Value = (decimal)Byte2StringData(Nail_M);
                NailC_numericUpDown.Value = (decimal)Byte2StringData(Nail_C);
                PhotoM_numericUpDown.Value = (decimal)Byte2StringData(Photo_M);
                PhotoC_numericUpDown.Value = (decimal)Byte2StringData(Photo_C);
         
            }
            else { }
            sPrinterStatus = PrinterStatus.USB_CONNECTED_READY;
            enable_works();
            DBGMonStatus_label.Text = "";
        }

        private float Byte2StringData(int input)
        {
            float result = 0;

            result = (float)((input - 128)*0.01);
            
            return result;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            isFormClosing = true;

            try
            {
                thread.Abort();
                thread.Join();
            }
            catch
            {
            }
            try
            {
                processDbgMon.Kill();
                processDbgMon.Close();
            }
            catch
            {
                Debug.WriteLine("KillProcess error catch");
            }
        }

        

        private void PrintNail_button_Click(object sender, EventArgs e)
        {
            if (sPrinterStatus == PrinterStatus.USB_CONNECTED_READY) { 
            

                processDbgMon.StartInfo.FileName = ".\\dbgmon\\PrintNail.bat";
                processDbgMon.StartInfo.UseShellExecute = false;
                processDbgMon.StartInfo.CreateNoWindow = true;
                DBGMonStatus_label.Text = "Print Nail...";
                processDbgMon.Start();
                while (!processDbgMon.HasExited)
                {
                    if (sPrinterStatus == PrinterStatus.USB_DISCONNECTED)
                    {
                        try
                        {
                            processDbgMon.Kill();
                            processDbgMon.Close();
                        }
                        catch
                        {
                            Debug.WriteLine("KillProcess error catch");
                        }
                        break;
                    }
                }
                DBGMonStatus_label.Text = "";
                sPrinterStatus = PrinterStatus.USB_CONNECTED_READY;
            }
        }

        private void PrintPhoto_button_Click(object sender, EventArgs e)
        {
            if (sPrinterStatus == PrinterStatus.USB_CONNECTED_READY)
            {
                processDbgMon.StartInfo.FileName = ".\\dbgmon\\PrintPhoto.bat";
                processDbgMon.StartInfo.UseShellExecute = false;
                processDbgMon.StartInfo.CreateNoWindow = true;
                DBGMonStatus_label.Text = "Print Photo...";
                processDbgMon.Start();
                while (!processDbgMon.HasExited)
                {
                    if (sPrinterStatus == PrinterStatus.USB_DISCONNECTED)
                    {
                        try
                        {
                            processDbgMon.Kill();
                            processDbgMon.Close();
                        }
                        catch
                        {
                            Debug.WriteLine("KillProcess error catch");
                        }
                        break;
                    }
                }
                sPrinterStatus = PrinterStatus.USB_CONNECTED_READY;
                DBGMonStatus_label.Text = "";
            }
        }

        private void Nail_Regi_write_button_Click(object sender, EventArgs e)
        {
            string Nail_Y, Nail_M, Nail_C;

            if (sPrinterStatus == PrinterStatus.USB_CONNECTED_READY)
            {

                Nail_Y = "0x" + String.Format("{0:X}", ((int)(NailY_numericUpDown.Value * 100) + 128).ToString("X"));
                Nail_M = "0x" + String.Format("{0:X}", ((int)(NailM_numericUpDown.Value * 100) + 128).ToString("X"));
                Nail_C = "0x" + String.Format("{0:X}", ((int)(NailC_numericUpDown.Value * 100) + 128).ToString("X"));

                if (File.Exists(".\\dbgmon\\Nail_write.txt"))
                    File.Delete(".\\dbgmon\\Nail_write.txt");

                using (StreamWriter writer = new StreamWriter(new FileStream(".\\dbgmon\\Nail_write.txt", FileMode.Create)))
                {
                    writer.WriteLine("wr ac_scratch3 " + Nail_Y);
                    writer.WriteLine("msleep 100");
                    writer.WriteLine("wr ac_scratch4 " + Nail_M);
                    writer.WriteLine("msleep 100");
                    writer.WriteLine("wr ac_scratch5 " + Nail_C);
                    writer.WriteLine("msleep 100");
                    writer.WriteLine("wr ac_scratch2 0x20");
                    writer.WriteLine("exit");
                }

                processDbgMon.StartInfo.FileName = ".\\dbgmon\\Nail_write_execute.bat";
                processDbgMon.StartInfo.UseShellExecute = false;
                processDbgMon.StartInfo.CreateNoWindow = true;
                DBGMonStatus_label.Text = "Nail Write...";
                processDbgMon.Start();
                while (!processDbgMon.HasExited)
                {
                    if (sPrinterStatus == PrinterStatus.USB_DISCONNECTED)
                    {
                        try
                        {
                            processDbgMon.Kill();
                            processDbgMon.Close();
                        }
                        catch
                        {
                            Debug.WriteLine("KillProcess error catch");
                        }
                        break;
                    }
                }
                SaveTheLog_csv();
                DBGMonStatus_label.Text = "...";
                sPrinterStatus = PrinterStatus.USB_CONNECTED_READY;
            }
        }

        private void Photo_Regi_write_button_Click(object sender, EventArgs e)
        {
            string Photo_M, Photo_C;

            if (sPrinterStatus == PrinterStatus.USB_CONNECTED_READY)
            {

                Photo_M = "0x" + String.Format("{0:X}", ((int)(PhotoM_numericUpDown.Value * 100) + 128).ToString("X"));
                Photo_C = "0x" + String.Format("{0:X}", ((int)(PhotoC_numericUpDown.Value * 100) + 128).ToString("X"));

                if (File.Exists(".\\dbgmon\\Photo_write.txt"))
                    File.Delete(".\\dbgmon\\Photo_write.txt");

                using (StreamWriter writer = new StreamWriter(new FileStream(".\\dbgmon\\Photo_write.txt", FileMode.Create)))
                {
                    writer.WriteLine("wr ac_scratch3 " + Photo_M);
                    writer.WriteLine("msleep 100");
                    writer.WriteLine("wr ac_scratch4 " + Photo_C);
                    writer.WriteLine("msleep 100");
                    writer.WriteLine("wr ac_scratch2 0x21");
                    writer.WriteLine("exit");
                }

                processDbgMon.StartInfo.FileName = ".\\dbgmon\\Photo_write_execute.bat";
                processDbgMon.StartInfo.UseShellExecute = false;
                processDbgMon.StartInfo.CreateNoWindow = true;
                DBGMonStatus_label.Text = "Photo write...";
                processDbgMon.Start();
                while (!processDbgMon.HasExited)
                {
                    if (sPrinterStatus == PrinterStatus.USB_DISCONNECTED)
                    {
                        try
                        {
                            processDbgMon.Kill();
                            processDbgMon.Close();
                        }
                        catch
                        {
                            Debug.WriteLine("KillProcess error catch");
                        }
                        break;
                    }
                }
                SaveTheLog_csv();
                DBGMonStatus_label.Text = "...";
                sPrinterStatus = PrinterStatus.USB_CONNECTED_READY;
            }
        }
        private void SaveTheLog_csv()
        {
            using (stream_Logs)
            {
                stream_Logs = new FileStream(System.Environment.CurrentDirectory.ToString() + @"\Logs\" + DateTime.Now.ToString("yyyy-MM-dd") + "_Regi.csv", FileMode.Append, FileAccess.Write);
                using (StreamWriter writer = new StreamWriter(stream_Logs, Encoding.Default))
                {
                    writer.Write(string.Concat(new object[] {                                                                        
                                                                        NailY_numericUpDown.Value, ",",
                                                                        NailM_numericUpDown.Value,",",
                                                                        NailC_numericUpDown.Value,",",
                                                                        PhotoM_numericUpDown.Value, ",",
                                                                        PhotoC_numericUpDown.Value ,",",
                                                                        '\'' + PrinterSN_textBox.Text,",",
                                                                        DateTime.Now.ToString("HH:mm:ss"),",",                                                                        
                                                                    }));

                    writer.WriteLine();
                    writer.Flush();
                }
            }
        }

        private void CreateTheLog_csv()
        {
            FileInfo LogFile = new FileInfo(System.Environment.CurrentDirectory.ToString() + @"\Logs\" + DateTime.Now.ToString("yyyy-MM-dd") + "_Regi.csv");
            if (!LogFile.Exists)
            {
                stream_Logs = new FileStream(System.Environment.CurrentDirectory.ToString() + @"\Logs\" + DateTime.Now.ToString("yyyy-MM-dd") + "_Regi.csv", FileMode.CreateNew, FileAccess.Write);
                using (stream_Logs)
                {
                    using (StreamWriter writer = new StreamWriter(stream_Logs, Encoding.Default))
                    {
                        writer.WriteLine(string.Concat(new object[] {
                    "Nail_Y",",", "Nail_M",",","Nail_C",",","Photo_M",",",
                    "Photo_C",",","PrinterSN", ",",
                    "PrintTime",","
                    }));
                        writer.Flush();
                    }
                }
            }

        }

        private void BTPrintPhoto_button_Click(object sender, EventArgs e)
        {

        }

        private void BTPrintNail_button_Click(object sender, EventArgs e)
        {

            readingProcess.EnableRaisingEvents = true;
            readingProcess.Start();
        }

        private void Nail_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_Nail.Checked)
            {
                cbx_Photo.Checked = false;
                EnableNail();
                //Update();
            }
            else
            {
                DisableNail();
            }
        }
        private void cbx_Photo_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_Photo.Checked)
            {
                cbx_Nail.Checked = false;
                EnablePhoto();
            }
            else
            {
                DisablePhoto();
            }

        }

        void EnableNail()
        {
            NailY_numericUpDown.Enabled = true;
            NailM_numericUpDown.Enabled = true;
            NailC_numericUpDown.Enabled = true;
            PrintNail_button.Enabled = true;
            Nail_Regi_write_button.Enabled = true;
            BTPrintNail_button.Enabled = true;
        }
        void DisableNail()
        {
            NailY_numericUpDown.Value = 0;
            NailM_numericUpDown.Value = 0;
            NailC_numericUpDown.Value = 0;
            NailY_numericUpDown.Enabled = false;
            NailM_numericUpDown.Enabled = false;
            NailC_numericUpDown.Enabled = false;
            PrintNail_button.Enabled = false;
            Nail_Regi_write_button.Enabled = false;
            BTPrintNail_button.Enabled = false;
        }

        void EnablePhoto()
        {
            PhotoM_numericUpDown.Enabled = true;
            PhotoC_numericUpDown.Enabled = true;
            PrintPhoto_button.Enabled = true;
            Photo_Regi_write_button.Enabled = true;
            BTPrintPhoto_button.Enabled = true;
            
        }
        void DisablePhoto()
        {
            PhotoM_numericUpDown.Enabled = false;
            PhotoC_numericUpDown.Enabled = false;
            PrintPhoto_button.Enabled = false;
            Photo_Regi_write_button.Enabled = false;
            BTPrintPhoto_button.Enabled = false;

            PhotoM_numericUpDown.Value = 0;
            PhotoC_numericUpDown.Value = 0;
        }


        public Boolean SendImageViaBluetooth(String mac, bool isNail)
        {
            Boolean result = true;
            string imageName = "";

            if (isNail == true) imageName = "DMP_FPQ_image_nail.jpg";
            else imageName = "DMP_FPQ_image_photo.jpg";

            using (BluetoothClient client = new BluetoothClient())
            {
                try
                {
                    try
                    {
                        client.Authenticate = true;
                        client.SetPin(BluetoothAddress.Parse(mac), "0000");
                        client.Connect(BluetoothAddress.Parse(mac), BluetoothService.SerialPort);
                    }
                    catch
                    {
                        //bluetoothError = (rb_korean.Checked) ? "Check Bluetooth status" : "Kiểm tra trạng thái Bluetooth";
                        throw new Exception();
                    }

                    try
                    {
                        if (!client.Connected)
                            throw new Exception();

                        using (NetworkStream networkStream = client.GetStream())
                        {
                            networkStream.ReadTimeout = 10000;

                            Int32 imageSize = (int)new FileInfo(imageName).Length;
                            Byte[] sizeArray = BitConverter.GetBytes(imageSize);
                            Byte[] buffer = new Byte[34];



                            for (Int32 i = 0; i < buffer.Length; i++)
                                buffer[i] = 0x00;

                            buffer[0] = 0x1b;
                            buffer[1] = 0x2a;
                            buffer[2] = 0x44;
                            buffer[3] = 0x53;
                            buffer[4] = 0x00;
                            buffer[5] = 0x02;
                            buffer[6] = 0x01;
                            buffer[7] = 0x01;
                            buffer[8] = 0x00;
                            if (isNail == true)
                            {
                                buffer[9] = 0x01; // Nail
                                buffer[10] = 0x00; // 4x3"
                            }
                            else
                            {
                                buffer[9] = 0x02; // Photo
                                buffer[10] = 0x03; // 4x6"
                            }

                            networkStream.Write(buffer, 0, buffer.Length);
                            networkStream.Read(buffer, 0, buffer.Length);

                            RemainedMedia = "";
                            RemainedMedia = RemainedMedia.Insert(0, string.Format("{0:x2}", buffer[28]));
                            RemainedMedia = Convert.ToString(Convert.ToInt32(RemainedMedia, 16));

                            for (Int32 i = 0; i < buffer.Length; i++)
                                buffer[i] = 0x00;

                            buffer[0] = 0x1b;
                            buffer[1] = 0x2a;
                            buffer[2] = 0x44;
                            buffer[3] = 0x53;
                            buffer[4] = 0x00;
                            buffer[5] = 0x02;
                            buffer[6] = 0x00;
                            buffer[7] = 0x00;
                            buffer[8] = sizeArray[2];
                            buffer[9] = sizeArray[1];
                            buffer[10] = sizeArray[0];
                            buffer[11] = 0x01;

                            networkStream.Write(buffer, 0, buffer.Length);
                            networkStream.Read(buffer, 0, buffer.Length);

                            if (buffer[6] == 0x02 && buffer[7] == 0x00)
                            {
                                if (buffer[8] == 0x00)
                                {
                                    using (FileStream fileStream = new FileStream(imageName, FileMode.Open, FileAccess.Read))
                                    using (BinaryReader binaryReader = new BinaryReader(fileStream))
                                    {
                                        Byte[] imageBuffer = binaryReader.ReadBytes(imageSize);
                                        networkStream.Write(imageBuffer, 0, imageBuffer.Length);
                                    }

                                    networkStream.Read(buffer, 0, buffer.Length);

                                    if (buffer[6] != 0x02 || buffer[7] != 0x01)
                                        result = false;
                                }
                                else
                                {
                                    switch (buffer[8])
                                    {
                                        case 0x01:
                                            bluetoothError = "RIBBON REPLACE ERROR";
                                            break;
                                        case 0x02:
                                            bluetoothError = "RIBBON EMPTY";
                                            break;
                                        case 0x03:
                                            bluetoothError = "BUSY";
                                            break;
                                        case 0x04:
                                            bluetoothError = "PAPER MISFEED";
                                            break;
                                        case 0x05:
                                            bluetoothError = "WRONG CUSTOMER";
                                            break;
                                        case 0x06:
                                            bluetoothError = "DATA ERROR";
                                            break;
                                        case 0x07:
                                            bluetoothError = "CARTRIDGE EMPTY";
                                            break;
                                        case 0x08:
                                            bluetoothError = "SYSTEM ERROR";
                                            break;
                                        case 0x09:
                                            bluetoothError = "BATTERY LOW";
                                            break;
                                        case 0x0a:
                                            bluetoothError = "HIGH TEMPERATURE";
                                            break;
                                        case 0x0b:
                                            bluetoothError = "LOW TEMPERATURE";
                                            break;
                                        case 0x0c:
                                            bluetoothError = "COOLING MODE";
                                            break;
                                        case 0x0d:
                                            bluetoothError = "BATTERY TOO LOW";
                                            break;
                                    }

                                    result = false;
                                }
                            }
                            else
                            {
                                switch (buffer[8])
                                {
                                    case 0x01:
                                        bluetoothError = "RIBBON REPLACE ERROR";
                                        break;
                                    case 0x02:
                                        bluetoothError = "RIBBON EMPTY";
                                        break;
                                    case 0x03:
                                        bluetoothError = "BUSY";
                                        break;
                                    case 0x04:
                                        bluetoothError = "PAPER MISFEED";
                                        break;
                                    case 0x05:
                                        bluetoothError = "WRONG CUSTOMER";
                                        break;
                                    case 0x06:
                                        bluetoothError = "DATA ERROR";
                                        break;
                                    case 0x07:
                                        bluetoothError = "CARTRIDGE EMPTY";
                                        break;
                                    case 0x08:
                                        bluetoothError = "SYSTEM ERROR";
                                        break;
                                    case 0x09:
                                        bluetoothError = "BATTERY LOW";
                                        break;
                                    case 0x0a:
                                        bluetoothError = "HIGH TEMPERATURE";
                                        break;
                                    case 0x0b:
                                        bluetoothError = "LOW TEMPERATURE";
                                        break;
                                    case 0x0c:
                                        bluetoothError = "COOLING MODE";
                                        break;
                                    case 0x0d:
                                        bluetoothError = "BATTERY TOO LOW";
                                        break;
                                }

                                result = false;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    //bluetoothError = (rb_korean.Checked) ? "TIMEOUT" : "Hết giờ";
                    result = false;
                }
                catch (Exception ex)
                {
                    result = false;
                }
            }
            return result;
        }
    }
}

