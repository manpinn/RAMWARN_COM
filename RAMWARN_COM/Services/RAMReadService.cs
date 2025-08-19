using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RAMWARN_COM.Services
{
    internal static class RAMReadService
    {
        public static ulong freeMemoryKB;

        public static ulong totalMemoryKB;

        public static ulong usedMemoryKB;



        public async static Task ReadRAM(TextBlock RamTextBlock)
        {
            try
            {
                await Task.Run(() =>
                {
                    ObjectQuery wql = new ObjectQuery("SELECT FreePhysicalMemory, TotalVisibleMemorySize FROM Win32_OperatingSystem");

                    ManagementObjectSearcher searcher = new ManagementObjectSearcher(wql);

                    foreach (ManagementObject result in searcher.Get())
                    {
                        freeMemoryKB = (ulong)result["FreePhysicalMemory"];

                        totalMemoryKB = (ulong)result["TotalVisibleMemorySize"];

                        usedMemoryKB = totalMemoryKB - freeMemoryKB;

                    }
                });

                Application.Current.Dispatcher.Invoke(() =>
                {
                    RamTextBlock.Text = $"Total: {totalMemoryKB / 1024} MB\n" +
                                        $"Used: {usedMemoryKB / 1024} MB\n" +
                                        $"Free: {freeMemoryKB / 1024} MB";
                });
            }
            catch (Exception ex)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    RamTextBlock.Text = "ERROR at reading RAM: " + ex.Message;
                });
            }
        }
    }
}
