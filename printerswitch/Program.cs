// See https://aka.ms/new-console-template for more information
using printerswitch;
using System;
using System.Drawing.Printing;
using System.Linq;
using System.Management;
using System.Runtime.InteropServices;
//string deviceName = "Microsoft Print to PDF";

string deviceName = "";

foreach( string printername in args)
{
    deviceName = printername;
}

deviceName = deviceName.Replace("|"," ");

bool isDefaultPrinter = IsPrinterDefault(deviceName);

if (isDefaultPrinter)
{
    Console.WriteLine($"{deviceName} is the default printer.");
}
else
{
    if (PrinterExists(deviceName))
    {
        foreach (string printer in PrinterSettings.InstalledPrinters)
        {
            if (string.Equals(printer, deviceName, StringComparison.OrdinalIgnoreCase))
            {
                SetDefaultPrinterWMI(deviceName);
                Console.WriteLine("Default Printer is SET");
            }
        }

    }
    else
    {
        Console.WriteLine("Printer Not Found");
    }

}

static void SetDefaultPrinterWMI(string printerName)
{
    try
    {
        // WMI query to set default printer
        ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Printer");

        foreach (ManagementObject printer in searcher.Get())
        {
            string currentPrinterName = printer["Name"].ToString();
            if (currentPrinterName.Equals(printerName, StringComparison.OrdinalIgnoreCase))
            {
                printerclass.SetDefaultPrinter(printerName);             
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error setting default printer: {ex.Message}");
    }
}
static bool IsPrinterDefault(string deviceName)
{
    // Get the current default printer
    string defaultPrinter = new PrinterSettings().PrinterName;

    return string.Equals(defaultPrinter, deviceName, StringComparison.OrdinalIgnoreCase);
}
static bool PrinterExists(string deviceName)
{
    // Get the list of installed printers
    foreach (string printer in PrinterSettings.InstalledPrinters)
    {
        if (string.Equals(printer, deviceName, StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }
    }
    return false;
}