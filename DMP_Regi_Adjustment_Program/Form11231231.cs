// v0.1
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


namespace NailPoP_Cartridge_EEPROM_Program
{

    public enum PrinterStatus
    {
        USB_DISCONNECTED,
        USB_CONNECTED_FIRST,
        USB_CONNECTED_CARTRIDGE_NOT_INSERTED,
        USB_CONNECTED_CARTRIDGE_INSERTED_NULL_EEPROM,
        USB_CONNECTED_CARTRIDGE_INSERTED_EXIST_EEPROM,
        USB_CONNECTED_CARTRIDGE_ERROR,
        USB_CONNECTED_WRITECOMPLETE,
    };

    public partial class Form1 : Form
    {

        byte[] page0 = new byte[32];
        byte[] page0_dump = new byte[32];
        byte[] page1 = new byte[32];
        byte[] page1_dump = new byte[32];
        byte[] dumpedStatus = new byte[32];

        byte[] Nail_Y = new byte[32];
        byte[] Nail_M = new byte[32];
        byte[] Nail_C = new byte[32];
        byte[] Photo_M = new byte[32];
        byte[] Photo_C = new byte[32];
        string SN_generated = "";
        //int SN_idx = 0;

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

        
        Process processDbgMon;
        Thread thread;

        PrinterStatus sPrinterStatus;

        SQLiteManager db;

        Boolean isManufacturingReset = false;
        Font regularFont;
        Font boldFont;

        public Form1()
        {
            InitializeComponent();

            #region setup database
            if (!Directory.Exists(".\\DB"))
                Directory.CreateDirectory(".\\DB");

            db = new SQLiteManager(".\\DB\\DMP_REGI_DB.db");
            db.CreateSQLiteDB();
            db.NonQuery("CREATE TABLE IF NOT EXISTS NAILPOP_CARTRIDGE (NAIL_Y VARCHAR(3), NAIL_M VARCHAR(3), NAIL_C VARCHAR(3), PHOTO_M VARCHAR(3), PHOTO_C VARCHAR(3), SN VARCHAR(18), DATE DATETIME)");
            #endregion

            regularFont = new Font(cb_manufacturing_reset.Font, FontStyle.Regular);
            boldFont = new Font(cb_manufacturing_reset.Font, FontStyle.Bold);

            ProcessStartInfo startInfo = new ProcessStartInfo("taskkill");
            startInfo.Arguments = "/im dbgmon.exe /f";
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            Process.Start(startInfo);

            processDbgMon = new Process();
            processDbgMon.EnableRaisingEvents = true;
            processDbgMon.StartInfo.CreateNoWindow = false;
            processDbgMon.StartInfo.UseShellExecute = false;

            UsbDeviceNotifier.OnDeviceNotify += OnDeviceNotifyEvent; // 이벤트 핸들러 추가
            UsbDeviceNotifier.Enabled = true;  // 장치인식하는 걸 안하게 하는거!!
            UsbDevice.ForceLibUsbWinBack = true;

            thread = new Thread(StatusMonitor);
            CheckForIllegalCrossThreadCalls = false;

            thread.Start();
        }

        private void OnDeviceNotifyEvent(object sender, DeviceNotifyEventArgs e)
        {
            //UsbRegDeviceList allDevices = UsbDevice.AllDevices;
            switch (e.EventType)
            {
                case EventType.DeviceRemoveComplete:
                    if (e.Device != null)
                    {
                        if ((e.Device.IdVendor == 0x0572) && (e.Device.IdProduct == 0x5100))
                        {
                            Debug.WriteLine("Device Outgoing");
                            OnUsbDisconnect(5100);
                        }
                    }
                    break;

                case EventType.DeviceArrival:
                    if (e.Device != null)
                    {
                        if ((e.Device.IdVendor == 0x0572) && (e.Device.IdProduct == 0x5100))
                        {
                            Debug.WriteLine("Device Incomming");
                            OnUsbConnect(5100);
                        }
                    }
                    break;
            }
        }
        private void OnUsbConnect(int PID)
        {
            if (PID == 5100)
            {
                toolStripStatusLabel1.Text = @"USB Connected";
            }
            sPrinterStatus = PrinterStatus.USB_CONNECTED_FIRST;

            //processStatus = DbgmonProcessStatus.PROCESS_RUN;
            //Update();
        }
        private void OnUsbDisconnect(int PID)
        {

            if (PID == 5100)
            {
                toolStripStatusLabel1.Text = @"USB Disconnected";
            }
            sPrinterStatus = PrinterStatus.USB_DISCONNECTED;
            InitializeValues();
        }

        // Make the structure of EEPROM data and save it to *.bin files
        // Page0 - Manufacturing information
        // Page1 - Working Information
        // Page2 - Debuging Information
        public void Make_EEPROM_Structure()
        {
            byte p0_Year = 0;
            byte p0_Month = 0;
            byte p0_Day = 0;
            byte p0_Model = 0;
            byte p0_Seller = 0;
            byte p0_MediaType = 0;
            byte p0_MediaSize = 0;
            byte p0_Purpose = 0;
            byte p0_Manufacturer = 0;
            byte[] p0_SerialNo = new byte[22];
            byte[] p0_EndOfPage = new byte[1] { 0xAD };
            byte p1_RibbonNo = 0;
            byte[] p1_RESERVED = new byte[31];

            page0.Initialize();
            page1.Initialize();

            //SN_idx = Convert.ToInt16(SerialOrder_wv_textBox.Text);

            p0_Year = Decimal.ToByte(Year_wv_numericUpDown.Value-2000);
            p0_Month = Decimal.ToByte(Month_wv_numericUpDown.Value);
            p0_Day = Decimal.ToByte(Day_wv_numericUpDown.Value);
            p0_Model = Decimal.ToByte(Model_wv_comboBox.SelectedIndex);
            p0_Seller = Decimal.ToByte(Seller_wv_comboBox.SelectedIndex);
            p0_MediaType = Decimal.ToByte(MediaType_wv_comboBox.SelectedIndex);
            p0_MediaSize = Decimal.ToByte(MediaSize_wv_comboBox.SelectedIndex);
            p0_Purpose = Decimal.ToByte(Purpose_wv_comboBox.SelectedIndex);
            p0_Manufacturer = Decimal.ToByte(Manufacturer_wv_comboBox.SelectedIndex);
            p1_RibbonNo = Decimal.ToByte(RibbonNo_wv_numericUpDown.Value);

            SN_generated = null;

            SN_generated = String.Concat(SN_generated, p0_Year.ToString("D2"));
            if (p0_Month < 10) SN_generated = String.Concat(SN_generated, p0_Month.ToString("D1"));
            else if (p0_Month == 10) SN_generated = String.Concat(SN_generated, "A");
            else if (p0_Month == 11) SN_generated = String.Concat(SN_generated, "B");
            else if (p0_Month == 12) SN_generated = String.Concat(SN_generated, "C");

            //SN_generated = String.Concat(SN_generated, p0_Day.ToString("D2"));

            if (p0_Model == 0) SN_generated = String.Concat(SN_generated, "NPCRTG");
            else if (p0_Model == 1) SN_generated = String.Concat(SN_generated, "SPCRTG");
            else SN_generated = String.Concat(SN_generated, "UNKNOW");

            if(p0_Seller == 0x09) SN_generated = String.Concat(SN_generated, "B");  // South Africa
            else SN_generated = String.Concat(SN_generated, (p0_Seller + 1).ToString("D1"));

            if (p0_Purpose == 0) SN_generated = String.Concat(SN_generated, "N");
            else if (p0_Purpose == 1) SN_generated = String.Concat(SN_generated, "P");
            else if (p0_Purpose == 2) SN_generated = String.Concat(SN_generated, "K");
            else if (p0_Purpose == 3) SN_generated = String.Concat(SN_generated, "T");
            else if (p0_Purpose == 4) SN_generated = String.Concat(SN_generated, "F");
            else if (p0_Purpose == 5) SN_generated = String.Concat(SN_generated, "L");
            else if (p0_Purpose == 6) SN_generated = String.Concat(SN_generated, "D");
            else SN_generated = String.Concat(SN_generated, "X");

            if (p0_Manufacturer == 0) SN_generated = String.Concat(SN_generated, "A");
            else if (p0_Manufacturer == 1) SN_generated = String.Concat(SN_generated, "U");
            else if (p0_Manufacturer == 2) SN_generated = String.Concat(SN_generated, "2");
            else SN_generated = String.Concat(SN_generated, "X");

            if (p0_MediaType == 0) SN_generated = String.Concat(SN_generated, "W");
            else if (p0_MediaType == 1) SN_generated = String.Concat(SN_generated, "T");
            else if (p0_MediaType == 2) SN_generated = String.Concat(SN_generated, "M");
            else SN_generated = String.Concat(SN_generated, "X");

            if (p0_MediaSize == 0) SN_generated = String.Concat(SN_generated, "M");
            else if (p0_MediaSize == 1) SN_generated = String.Concat(SN_generated, "B");
            else if (p0_MediaSize == 2) SN_generated = String.Concat(SN_generated, "S");
            else if (p0_MediaSize == 3) SN_generated = String.Concat(SN_generated, "K");
            else if (p0_MediaSize == 4) SN_generated = String.Concat(SN_generated, "F");
            else if (p0_MediaSize == 5) SN_generated = String.Concat(SN_generated, "L");
            else if (p0_MediaSize == 6) SN_generated = String.Concat(SN_generated, "T");
            else if (p0_MediaSize == 7) SN_generated = String.Concat(SN_generated, "D");
            else if (p0_MediaSize == 8) SN_generated = String.Concat(SN_generated, "P");
            else if (p0_MediaSize == 9) SN_generated = String.Concat(SN_generated, "F");
            else SN_generated = String.Concat(SN_generated, "X");

            //SN_generated = String.Concat(SN_generated, SN_idx.ToString("D6"));
            //SN_generated = String.Concat(SN_generated, num_order.Value.ToString("D6"));
            SN_generated = String.Concat(SN_generated, num_order.Value.ToString().PadLeft(6, '0'));

            if (isManufacturingReset)
                SerialNumber_wv_label.Text = "MANUFACTURING1234567";
            else
                SerialNumber_wv_label.Text = SN_generated;
            p0_SerialNo = Encoding.UTF8.GetBytes(SerialNumber_wv_label.Text.ToString());
            
            Setinifile("PAGE0", "Model", p0_Model.ToString());
            Setinifile("PAGE0", "Seller", p0_Seller.ToString());
            Setinifile("PAGE0", "Purpose", p0_Purpose.ToString());
            Setinifile("PAGE0", "Manufacturer", p0_Manufacturer.ToString());
            Setinifile("PAGE0", "MediaType", p0_MediaType.ToString());
            Setinifile("PAGE0", "MediaSize", p0_MediaSize.ToString());

            page0[0] = p0_Year;
            page0[1] = p0_Month;
            page0[2] = p0_Day;
            page0[3] = p0_Model;
            page0[4] = p0_Seller;
            page0[5] = p0_MediaType;
            page0[6] = p0_MediaSize;
            page0[7] = p0_Purpose;
            page0[8] = p0_Manufacturer;
            
            Array.Copy(p0_SerialNo, 0, page0, 9, p0_SerialNo.Length);
            Array.Copy(p0_EndOfPage, 0, page0, 31, p0_EndOfPage.Length);

            page1[0] = p1_RibbonNo;
            Array.Copy(p1_RESERVED, 0, page1, 1, p1_RESERVED.Length);

            ByteArrayToFile(Environment.CurrentDirectory + "\\dbgmon\\page0.bin", page0);
            ByteArrayToFile(Environment.CurrentDirectory + "\\dbgmon\\page1.bin", page1);

#if false
            SN_idx++;
            SerialOrder_wv_textBox.Text = SN_idx.ToString("D6");
            Setinifile("PAGE0", "SerialOrder", SN_idx.ToString("D6"));
#endif
        }

        private void WriteEEPROM()
        {
            Make_EEPROM_Structure();
            //TODO
            // Need to porting it to DBGMon
            processDbgMon.StartInfo.FileName = ".\\dbgmon\\SetEEPROM.bat";
            processDbgMon.StartInfo.UseShellExecute = false;
            processDbgMon.StartInfo.CreateNoWindow = true;
            DBGMonStatus_label.Text = "Write value...";
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
            sPrinterStatus = PrinterStatus.USB_CONNECTED_WRITECOMPLETE;
        }

        public bool ByteArrayToFile(string path, byte[] buffer) 
        { 
            try {
                File.WriteAllBytes(path, buffer);
                return true;
            }
            catch (Exception e) {}
            return false;
        }

        private void ReadEEPROM_button_Click(object sender, EventArgs e)
        {
            using (FileStream fileStream = new System.IO.FileStream(Environment.CurrentDirectory + "\\dbgmon\\page0.bin", FileMode.Open))
            {
      
                fileStream.Read(page0, 0, 32);
            }
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
                            GetStatusAndData();
                            break;
                        case PrinterStatus.USB_CONNECTED_CARTRIDGE_NOT_INSERTED:
                            caption = "@NOT_INSERTED";
                            DeviceStatus_label.Text = "Not Inserted";
                            InitializeValues();
                            GetStatusAndData();
                            break;
                        case PrinterStatus.USB_CONNECTED_CARTRIDGE_INSERTED_NULL_EEPROM:
                            caption = "@NULL_EEPR";
                            DeviceStatus_label.Text = "NULL EEPR";
                            if (AutoWrite_checkBox.Checked == true)
                            {
                                caption = "@NULL_EEPR_AW_W";
                                WriteEEPROM();
                            }
                            else if (isManufacturingReset)
                            {
                                caption = "@NULL_EEPR_MR_W";
                                WriteEEPROM();
                            }
                            else
                            {
                                caption = "@NULL_EEPR_R";
                                GetStatusAndData();
                            }
                            break;
                        case PrinterStatus.USB_CONNECTED_CARTRIDGE_INSERTED_EXIST_EEPROM:
                            caption = "@EXIST_EEPR";
                            DeviceStatus_label.Text = "Exist EEPR";
                            caption = "@EXIST_EEPR_DISPLAY";
                            DisplayEEPROM_Data();
                            if (WriteData_checkBox.Checked == true)
                            {
                                caption = "@EXIST_EEPR_W";
                                WriteEEPROM();
                                WriteData_checkBox.Checked = false;
                            }
                            else if (isManufacturingReset)
                            {
                                caption = "@EXIST_EEPR_MR_W";
                                WriteEEPROM();
                            }
                            else
                            {
                                caption = "@EXIST_EEPR_R";
                                GetStatusAndData();
                            }
                            break;
                        case PrinterStatus.USB_CONNECTED_CARTRIDGE_ERROR:
                            caption = "@CARTRIDGE_ERROR";
                            DeviceStatus_label.Text = "Cartridge ERROR";
                            GetStatusAndData();
                            break;
                        case PrinterStatus.USB_CONNECTED_WRITECOMPLETE:
                            caption = "@USB_CONNECTED_WRITECOMPLETE_R";
                            GetStatusAndData();
                            caption = "@USB_CONNECTED_WRITECOMPLETE_DISPLAY";
                            DisplayEEPROM_Data();

                            caption = "@USB_CONNECTED_BEFORE_VERIFY";
                            if (VerifyData())
                            {
                                caption = "@USB_CONNECTED_AFTER_VERIFY_P";
                                WriteResult_label.Text = "Write Complete";
                                WriteResult_label.ForeColor = Color.Green;

                                if (!isManufacturingReset)
                                {
                                    caption = "@USB_CONNECTED_AFTER_VERIFY_PRINT";
                                    Print(SN_generated);
                                    caption = "@USB_CONNECTED_AFTER_VERIFY_DB";
                                    db.NonQuery(String.Format("INSERT INTO NAILPOP_CARTRIDGE VALUES ('{0}', '{1}', '{2}')", num_order.Value.ToString().PadLeft(6, '0'), SN_generated, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                                    ++num_order.Value;
                                }
                            }
                            else
                            {
                                caption = "@USB_CONNECTED_AFTER_VERIFY_F";
                                WriteResult_label.Text = "Write Error";
                                WriteResult_label.ForeColor = Color.Red;
                            }

                            if (!cb_reset_continous.Checked && isManufacturingReset)
                                cb_manufacturing_reset.Checked = false;
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

        private void GetStatusAndData()
        {
            processDbgMon.StartInfo.FileName = ".\\dbgmon\\GetEEPROM.bat";
            processDbgMon.StartInfo.UseShellExecute = false;
            processDbgMon.StartInfo.CreateNoWindow = true;
            DBGMonStatus_label.Text = "Get value...";
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

            if (sPrinterStatus != PrinterStatus.USB_DISCONNECTED)
            {
                using (FileStream fileStream = new System.IO.FileStream(Environment.CurrentDirectory + "\\dbgmon\\pstat.bin", FileMode.Open))
                { fileStream.Read(dumpedStatus, 0, 1); }

                using (FileStream fileStream = new System.IO.FileStream(Environment.CurrentDirectory + "\\dbgmon\\page0_dump.bin", FileMode.Open))
                { fileStream.Read(page0_dump, 0, 32); }

                using (FileStream fileStream = new System.IO.FileStream(Environment.CurrentDirectory + "\\dbgmon\\page1_dump.bin", FileMode.Open))
                { fileStream.Read(page1_dump, 0, 32); }

                if (dumpedStatus[0] == 0)
                    sPrinterStatus = PrinterStatus.USB_CONNECTED_CARTRIDGE_NOT_INSERTED;
                else if (dumpedStatus[0] == 1)
                    if (page0_dump[0] == 0xFF)
                        sPrinterStatus = PrinterStatus.USB_CONNECTED_CARTRIDGE_INSERTED_NULL_EEPROM;
                    else
                        sPrinterStatus = PrinterStatus.USB_CONNECTED_CARTRIDGE_INSERTED_EXIST_EEPROM;
                else if (dumpedStatus[0] == 2)
                    sPrinterStatus = PrinterStatus.USB_CONNECTED_CARTRIDGE_ERROR;
            }
            else { }
        }

        private void DisplayEEPROM_Data()
        {
           byte[] SerialNo_dump = new byte[22];

           Year_rv_label.Text = (page0_dump[0]+2000).ToString();
           Month_rv_label.Text = page0_dump[1].ToString();
           Day_rv_label.Text = page0_dump[2].ToString();

           if (page0_dump[3] == 0) Model_rv_label.Text = "NPCRTG";
           else if (page0_dump[3] == 1) Model_rv_label.Text = "SPCRTG";

           if (page0_dump[4] == 0) Seller_rv_label.Text = "Korea";
           else if (page0_dump[4] == 1) Seller_rv_label.Text = "Japan";
           else if (page0_dump[4] == 2) Seller_rv_label.Text = "Vietnam";
           else if (page0_dump[4] == 3) Seller_rv_label.Text = "Singapore";
           else if (page0_dump[4] == 4) Seller_rv_label.Text = "Thailand";
           else if (page0_dump[4] == 5) Seller_rv_label.Text = "UAE";
           else if (page0_dump[4] == 6) Seller_rv_label.Text = "North America";
           else if (page0_dump[4] == 7) Seller_rv_label.Text = "Europe";
           else if (page0_dump[4] == 8) Seller_rv_label.Text = "China";
           else if (page0_dump[4] == 9) Seller_rv_label.Text = "South Africa";

            if (page0_dump[5] == 0) MediaType_rv_label.Text = "White";
           else if (page0_dump[5] == 1) MediaType_rv_label.Text = "Transparent";
           else if (page0_dump[5] == 2) MediaType_rv_label.Text = "Matt";

           if (page0_dump[6] == 0) MediaSize_rv_label.Text = "Medium";
           else if (page0_dump[6] == 1) MediaSize_rv_label.Text = "Large";
           else if (page0_dump[6] == 2) MediaSize_rv_label.Text = "Small";
           else if (page0_dump[6] == 3) MediaSize_rv_label.Text = "Kids";
           else if (page0_dump[6] == 4) MediaSize_rv_label.Text = "Photo";
           else if (page0_dump[6] == 5) MediaSize_rv_label.Text = "Sticker-Label";
           else if (page0_dump[6] == 6) MediaSize_rv_label.Text = "Sticker-Shape";
           else if (page0_dump[6] == 7) MediaSize_rv_label.Text = "DIY Sticker";
           else if (page0_dump[6] == 8) MediaSize_rv_label.Text = "Pedi";
           else if (page0_dump[6] == 9) MediaSize_rv_label.Text = "Photo (Somac)";

            if (page0_dump[7] == 0) Purpose_rv_label.Text = "Nail";
           else if (page0_dump[7] == 1) Purpose_rv_label.Text = "Pedi";
           else if (page0_dump[7] == 2) Purpose_rv_label.Text = "Kids";
           else if (page0_dump[7] == 3) Purpose_rv_label.Text = "Shape";
           else if (page0_dump[7] == 4) Purpose_rv_label.Text = "Photo";
           else if (page0_dump[7] == 5) Purpose_rv_label.Text = "Label";
           else if (page0_dump[7] == 6) Purpose_rv_label.Text = "DIY";

            if (page0_dump[8] == 0) Manufacturer_rv_label.Text = "(OLD)Altec";
           else if (page0_dump[8] == 1) Manufacturer_rv_label.Text = "(OLD)UIL";
           else if (page0_dump[8] == 2) Manufacturer_rv_label.Text = "DPS Ribbon";

            RibbonNo_rv_label.Text = page1_dump[0].ToString();

           Array.Copy(page0_dump, 9, SerialNo_dump, 0, SerialNo_dump.Length);

           SerialNumber_rv_label.Text = Encoding.Default.GetString(SerialNo_dump);

        }

        private Boolean VerifyData()
        {
            if (page0.SequenceEqual(page0_dump))
            {
                ChangeColorOfReadValue(Color.Green);
                return true;
            }
            else
            {
                ChangeColorOfReadValue(Color.Red);
                return false;
            }
        }

        private void ChangeColorOfReadValue(Color readValue_color)
        {
            Year_rv_label.ForeColor = readValue_color;
            Month_rv_label.ForeColor = readValue_color;
            Day_rv_label.ForeColor = readValue_color;
            Manufacturer_rv_label.ForeColor = readValue_color;
            MediaSize_rv_label.ForeColor = readValue_color;
            MediaType_rv_label.ForeColor = readValue_color;
            Seller_rv_label.ForeColor = readValue_color;
            Purpose_rv_label.ForeColor = readValue_color;
            RibbonNo_rv_label.ForeColor = readValue_color;
            SerialNumber_rv_label.ForeColor = readValue_color;
            Model_rv_label.ForeColor = readValue_color;
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
                { fileStream.Read(dumpedStatus, 0, 1); }

                using (FileStream fileStream = new System.IO.FileStream(Environment.CurrentDirectory + "\\dbgmon\\page0_dump.bin", FileMode.Open))
                { fileStream.Read(page0_dump, 0, 32); }

                using (FileStream fileStream = new System.IO.FileStream(Environment.CurrentDirectory + "\\dbgmon\\page1_dump.bin", FileMode.Open))
                { fileStream.Read(page1_dump, 0, 32); }

                if (dumpedStatus[0] == 0)
                    sPrinterStatus = PrinterStatus.USB_CONNECTED_CARTRIDGE_NOT_INSERTED;
                else if (dumpedStatus[0] == 1)
                    if (page0_dump[0] == 0xFF)
                        sPrinterStatus = PrinterStatus.USB_CONNECTED_CARTRIDGE_INSERTED_NULL_EEPROM;
                    else
                        sPrinterStatus = PrinterStatus.USB_CONNECTED_CARTRIDGE_INSERTED_EXIST_EEPROM;
                else if (dumpedStatus[0] == 2)
                    sPrinterStatus = PrinterStatus.USB_CONNECTED_CARTRIDGE_ERROR;
            }
            else { }
        }
        DBGMonStatus_label.Text = "";
        }

        private void InitializeValues()
        {
            ChangeColorOfReadValue(Color.Black);

            Year_rv_label.Text = "---";
            Month_rv_label.Text = "---";
            Day_rv_label.Text = "---";
            Manufacturer_rv_label.Text = "---";
            MediaSize_rv_label.Text = "---";
            MediaType_rv_label.Text = "---";
            Seller_rv_label.Text = "---";
            Purpose_rv_label.Text = "---";
            RibbonNo_rv_label.Text = "---";
            SerialNumber_rv_label.Text = "---";
            Model_rv_label.Text = "---";

            WriteResult_label.Text = "---";
            WriteResult_label.ForeColor = Color.Black;

            SN_generated = null;
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

        void LoadOrderNumber()
        {
            String dirName = Application.StartupPath + "\\ORDER";
            String fileName = Year_wv_numericUpDown.Value.ToString() + "_" + Month_wv_numericUpDown.Value.ToString().PadLeft(2, '0') + ".txt";

            if (!Directory.Exists(dirName))
                Directory.CreateDirectory(dirName);

            try
            {
                num_order.Value = Int32.Parse(File.ReadAllText(dirName + "\\" + fileName));
            }
            catch
            {
                File.Delete(dirName + "\\" + fileName);
                num_order.Value = 1;
            }
        }

        void SaveOrderNumber()
        {
            String dirName = Application.StartupPath + "\\ORDER";
            String fileName = Year_wv_numericUpDown.Value.ToString() + "_" + Month_wv_numericUpDown.Value.ToString().PadLeft(2, '0') + ".txt";

            if (!Directory.Exists(dirName))
                Directory.CreateDirectory(dirName);

            File.WriteAllText(dirName + "\\" + fileName, num_order.Value.ToString());
        }

        private void Year_wv_numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            LoadOrderNumber();
        }

        private void Month_wv_numericUpDown_ValueChanged(object sender, EventArgs e)
        {
            LoadOrderNumber();
        }

        private void num_order_ValueChanged(object sender, EventArgs e)
        {
            SaveOrderNumber();
        }

        void Print(String serialNumber)
        {
            String tail_char="";
            String substr;

            if (serialNumber.Substring(13, 1) == "M") tail_char = "MD";  // Nail (M)
            else if (serialNumber.Substring(13, 1) == "K") tail_char = "KD"; // Kids
            else if (serialNumber.Substring(13, 1) == "F") tail_char = "PH"; // Photo 
            else if (serialNumber.Substring(13, 1) == "L") tail_char = "LB"; // Label
            else if (serialNumber.Substring(13, 1) == "T") tail_char = "SH"; // Shape
            else if (serialNumber.Substring(13, 1) == "D") tail_char = "DY"; // DIY
            else if (serialNumber.Substring(13, 1) == "P") tail_char = "PD"; // Pedi
            else if (serialNumber.Substring(13, 1) == "B") tail_char = "LG"; // Nail (L)
            else if (serialNumber.Substring(13, 1) == "S") tail_char = "SM"; // Nail (S)
            


            Int32 barcodeStartX = (Int32)num_x.Value;
            Int32 barcodeStartY = (Int32)num_y.Value;
            Int32 barcodeLineWidth = 3;
            Int32 barcodeHeight = 100;

            Int32 textStartX = (Int32)num_x.Value + 120;
            Int32 textStartY = (Int32)num_y.Value + 44;
            Int32 textWidth = 50;
            Int32 textHeight = 50;

            List<String> strList = new List<String>();
            strList.Add("^XA~TA000~JSN^MNW^MMR,Y^MTT^PON^PMN^LH0,0^JMA^PR2,2^MD10^JUS^LRN^CI0^XZ^XA^MMT^LL1000^PW2400^LS0");
            strList.Add(String.Format("^BY{0},3,{1}^FT{2},{3}^BCN,,N,N^FD>:{4}^FS", barcodeLineWidth, barcodeHeight, barcodeStartX, barcodeStartY, serialNumber));
            strList.Add(String.Format("^FT{0},{1}^A0N,{2},{3}^FH\\^FD{4}^FS", textStartX, textStartY, textHeight, textWidth, serialNumber));
            strList.Add(String.Format("^FT{0},{1}^A0N,{2},{3}^FH\\^FD{4}^FS", textStartX+630, textStartY, textHeight, textWidth, tail_char));
            strList.Add("^PQ1,0,1,Y");

            if (tail_char == "KD") 
            {
                strList.Add(String.Format("^FO{0},{1}", barcodeStartX - 450, barcodeStartY - 130));
                if(cb_korean.Checked == true) strList.Add(System.IO.File.ReadAllText("MiniNail_KR.dat"));
                else strList.Add(System.IO.File.ReadAllText("MiniNail.dat"));
            }
            else if (tail_char == "PH") 
            {
                strList.Add(String.Format("^FO{0},{1}", barcodeStartX - 450, barcodeStartY - 130));
                if (cb_korean.Checked == true) strList.Add(System.IO.File.ReadAllText("Photo_KR.dat"));
                else strList.Add(System.IO.File.ReadAllText("Photo.dat"));
            }
            else if (tail_char == "LB") 
            {
                strList.Add(String.Format("^FO{0},{1}", barcodeStartX - 450, barcodeStartY - 130));
                if (cb_korean.Checked == true) strList.Add(System.IO.File.ReadAllText("Label_KR.dat"));
                else strList.Add(System.IO.File.ReadAllText("Label.dat"));
            }
            else if (tail_char == "SH") 
            {
                strList.Add(String.Format("^FO{0},{1}", barcodeStartX - 450, barcodeStartY - 130));
                if (cb_korean.Checked == true) strList.Add(System.IO.File.ReadAllText("Shape_KR.dat"));
                else strList.Add(System.IO.File.ReadAllText("Shape.dat"));
            }
            else if (tail_char == "DY") 
            {
                strList.Add(String.Format("^FO{0},{1}", barcodeStartX - 450, barcodeStartY - 130));
                if (cb_korean.Checked == true) strList.Add(System.IO.File.ReadAllText("DIY_KR.dat"));
                else strList.Add(System.IO.File.ReadAllText("DIY.dat"));
            }
            else if (tail_char == "MD")
            {
                strList.Add(String.Format("^FO{0},{1}", barcodeStartX - 450, barcodeStartY - 130));
                strList.Add(System.IO.File.ReadAllText("Nail_M.dat"));
            }
            else if (tail_char == "LG")
            {
                strList.Add(String.Format("^FO{0},{1}", barcodeStartX - 450, barcodeStartY - 130));
                strList.Add(System.IO.File.ReadAllText("Nail_L.dat"));
            }
            else if (tail_char == "SM")
            {
                strList.Add(String.Format("^FO{0},{1}", barcodeStartX - 450, barcodeStartY - 130));
                strList.Add(System.IO.File.ReadAllText("Nail_S.dat"));
            }
            else if (tail_char == "PD")
            {
                strList.Add(String.Format("^FO{0},{1}", barcodeStartX - 450, barcodeStartY - 130));
                strList.Add(System.IO.File.ReadAllText("Pedi.dat"));
            }
            else { } // Pedi and Nail Mid


            strList.Add("^XZ");

            substr = String.Concat(strList.ToArray());

            //Clipboard.Clear();
            //Clipboard.SetText(substr);

            DataSendPort.SendStringToPrinter(cmb_printer.SelectedItem.ToString(), substr);
        }

        private void btn_db_search_Click(object sender, EventArgs e)
        {
            String sql = "SELECT * FROM NAILPOP_CARTRIDGE WHERE DATE BETWEEN '{0} 00:00:00' AND '{1} 23:59:59'";

            if (txt_db_sn.Text.Length != 0)
                sql += String.Format(" AND SN = '{0}'", txt_db_sn.Text);

            dgv_db_search_list.DataSource = db.Query(String.Format(sql, dtp_db_from.Value.ToString("yyyy-MM-dd"), dtp_db_to.Value.ToString("yyyy-MM-dd")));
            dgv_db_search_list.Sort(dgv_db_search_list.Columns["DATE"], ListSortDirection.Descending);
            dgv_db_search_list.ClearSelection();
        }

        private void btn_db_delete_Click(object sender, EventArgs e)
        {
            if (dgv_db_search_list.CurrentCell == null)
                return;

            if (dgv_db_search_list.CurrentCell.RowIndex == -1)
                return;

            if (dgv_db_search_list.SelectedRows.Count == 0)
                return;

            if (txt_db_password.Text != "12311")
            {
                txt_db_password.Clear();
                return;
            }

            for (Int32 i = 0; i < dgv_db_search_list.SelectedRows.Count; i++)
                db.NonQuery(String.Format("DELETE FROM NAILPOP_CARTRIDGE WHERE SN = '{0}'", dgv_db_search_list.SelectedRows[i].Cells["SN"].Value.ToString()));

            txt_db_password.Clear();
            btn_db_search.PerformClick();
        }

        private void btn_db_reprint_Click(object sender, EventArgs e)
        {
            if (dgv_db_search_list.CurrentCell == null)
                return;

            if (dgv_db_search_list.CurrentCell.RowIndex == -1)
                return;

            if (dgv_db_search_list.SelectedRows.Count == 0)
                return;

            if (txt_db_password.Text != "12311")
            {
                txt_db_password.Clear();
                return;
            }

            String serialNumber = null;

            for (Int32 i = 0; i < dgv_db_search_list.SelectedRows.Count; i++)
            {
                serialNumber = dgv_db_search_list.SelectedRows[i].Cells["SN"].Value.ToString();
                Print(serialNumber);
            }
        }

        private void btn_db_save_to_excel_file_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "csv files (*.csv)|*.csv";

                try
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        using (StreamWriter writer = new StreamWriter(new FileStream(dialog.FileName, FileMode.OpenOrCreate), Encoding.UTF8))
                        {
                            writer.WriteLine("ORDERNUM,SN,DATE");

                            for (Int32 i = 0; i < dgv_db_search_list.Rows.Count; i++)
                            {
                                writer.WriteLine(String.Format("{0},{1},{2}",
                                    dgv_db_search_list.Rows[i].Cells["ORDERNUM"].Value.ToString(),
                                    dgv_db_search_list.Rows[i].Cells["SN"].Value.ToString(),
                                    dgv_db_search_list.Rows[i].Cells["DATE"].Value.ToString()));
                            }
                        }
                    }
                }
                catch
                {
                    MessageBox.Show(this, "File save error");
                }
            }
        }

        private void AutoWrite_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (AutoWrite_checkBox.Checked)
            {
                WriteData_checkBox.Checked = cb_manufacturing_reset.Checked = false;
            }
        }

        private void WriteData_checkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (WriteData_checkBox.Checked)
                AutoWrite_checkBox.Checked = cb_manufacturing_reset.Checked = false;
        }

        private void cb_manufacturing_reset_CheckedChanged(object sender, EventArgs e)
        {
            isManufacturingReset = false;

            cb_manufacturing_reset.Font = regularFont;
            cb_reset_continous.Enabled = cb_reset_continous.Checked = false;

            if (cb_manufacturing_reset.Checked)
            {
                using (Form2 form = new Form2())
                {
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        isManufacturingReset = true;

                        cb_manufacturing_reset.Font = boldFont;

                        cb_reset_continous.Enabled = true;

                        WriteData_checkBox.Checked = AutoWrite_checkBox.Checked = false;

                        RibbonNo_wv_numericUpDown.Value = 255;
                    }
                    else
                    {
                        cb_manufacturing_reset.Checked = false;
                        RibbonNo_wv_numericUpDown.Value = 10;
                    }
                }
            }
            else
            {
                RibbonNo_wv_numericUpDown.Value = 10;
            }

            cb_manufacturing_reset.ForeColor = (isManufacturingReset) ? Color.Blue : Color.Black;
            
        }

        private void cb_reset_continous_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_reset_continous.Checked)
                cb_manufacturing_reset.ForeColor = Color.Red;
            else
                cb_manufacturing_reset.ForeColor = Color.Blue;
        }       
    }
}
