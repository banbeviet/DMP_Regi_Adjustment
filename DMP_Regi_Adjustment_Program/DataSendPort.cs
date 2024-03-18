using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace DMP_Regi_Adjustment_Program
{
    class DataSendPort
    {
        // Methods
        [DllImport("winspool.Drv", CallingConvention = CallingConvention.StdCall, SetLastError = true, ExactSpelling = true)]
        public static extern bool ClosePrinter(IntPtr hPrinter);
        [DllImport("winspool.Drv", CallingConvention = CallingConvention.StdCall, SetLastError = true, ExactSpelling = true)]
        public static extern bool EndDocPrinter(IntPtr hPrinter);
        [DllImport("winspool.Drv", CallingConvention = CallingConvention.StdCall, SetLastError = true, ExactSpelling = true)]
        public static extern bool EndPagePrinter(IntPtr hPrinter);
        [DllImport("winspool.Drv", EntryPoint = "OpenPrinterA", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern bool OpenPrinter([MarshalAs(UnmanagedType.LPStr)] string szPrinter, out IntPtr hPrinter, IntPtr pd);
        public static bool SendBytesToPrinter(string szPrinterName, IntPtr pBytes, int dwCount)
        {
            int dwWritten = 0;
            IntPtr hPrinter = new IntPtr(0);
            DOCINFOA di = new DOCINFOA();
            bool flag = false;
            di.pDocName = "DSG_Canon";
            di.pDataType = "RAW";
            if (OpenPrinter(szPrinterName.Normalize(), out hPrinter, IntPtr.Zero))
            {
                if (StartDocPrinter(hPrinter, 1, di))
                {
                    if (StartPagePrinter(hPrinter))
                    {
                        flag = WritePrinter(hPrinter, pBytes, dwCount, out dwWritten);
                        EndPagePrinter(hPrinter);
                    }
                    EndDocPrinter(hPrinter);
                }
                ClosePrinter(hPrinter);
            }
            if (!flag)
            {
                Marshal.GetLastWin32Error();
            }
            return flag;
        }

        public static bool SendFileToPrinter(string szPrinterName, string szFileName)
        {
            FileStream input = new FileStream(szFileName, FileMode.Open);
            BinaryReader reader = new BinaryReader(input);
            byte[] source = new byte[input.Length];
            bool flag = false;
            IntPtr destination = new IntPtr(0);
            int count = Convert.ToInt32(input.Length);
            source = reader.ReadBytes(count);
            destination = Marshal.AllocCoTaskMem(count);
            Marshal.Copy(source, 0, destination, count);
            flag = SendBytesToPrinter(szPrinterName, destination, count);
            Marshal.FreeCoTaskMem(destination);
            return flag;
        }

        public static bool SendStringToPrinter(string szPrinterName, string szString)
        {
            int length = szString.Length;
            IntPtr pBytes = Marshal.StringToCoTaskMemAnsi(szString);
            SendBytesToPrinter(szPrinterName, pBytes, length);
            Marshal.FreeCoTaskMem(pBytes);
            return true;
        }

        [DllImport("winspool.Drv", EntryPoint = "StartDocPrinterA", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern bool StartDocPrinter(IntPtr hPrinter, int level, [In, MarshalAs(UnmanagedType.LPStruct)] DOCINFOA di);
        [DllImport("winspool.Drv", CallingConvention = CallingConvention.StdCall, SetLastError = true, ExactSpelling = true)]
        public static extern bool StartPagePrinter(IntPtr hPrinter);
        [DllImport("winspool.Drv", CallingConvention = CallingConvention.StdCall, SetLastError = true, ExactSpelling = true)]
        public static extern bool WritePrinter(IntPtr hPrinter, IntPtr pBytes, int dwCount, out int dwWritten);

        // Nested Types
        [StructLayout(LayoutKind.Sequential)]
        public class DOCINFOA
        {
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDocName;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pOutputFile;
            [MarshalAs(UnmanagedType.LPStr)]
            public string pDataType;
        }
    }
}
