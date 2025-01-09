using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace printerswitch
{
    internal class printerclass
    {
        [DllImport("winspool.drv",
             CharSet = CharSet.Auto,
             SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern Boolean SetDefaultPrinter(String name);
    }
}
