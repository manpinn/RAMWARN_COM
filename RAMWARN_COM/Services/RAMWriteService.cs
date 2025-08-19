using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RAMWARN_COM.Services
{
    internal class RAMWriteService
    {
        private static List<byte[]> memoryBlocks = new List<byte[]>();

        public async static Task WriteToRAM()
        {
            try
            {
                await Task.Run(() =>
                {
                    int mbToAllocate = 1024;

                    const int blockSize = 1024 * 1024; // 1 MB

                    for (int i = 0; i < mbToAllocate; i++)
                    {
                        byte[] block = new byte[blockSize];

                        for (int j = 0; j < block.Length; j += 4096)
                        {
                            block[j] = 0xFF;
                        }

                        memoryBlocks.Add(block);
                    }
                });
            }
            catch (OutOfMemoryException)
            {
                throw;
            }
        }
    }
}
