using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace fileShareStudy2
{
    class Program
    {
        static void Main(string[] args)
        {
            var fileAccess = FileAccess.Read;
            var fileShare = FileShare.Read;
            Console.WriteLine($"FileAccess.{fileAccess}, FileShare.{fileShare}");
            foreach (var arg in args)
            {
                Console.WriteLine($"{arg}");
            }
            Console.WriteLine("Hit Ctrl+C to exit.");

            while (true)
            {
                OpenAndCloseFiles(args, fileAccess, fileShare);
                Thread.Sleep(1);
            }
        }

        static void OpenAndCloseFiles(string[] args, FileAccess fileAccess, FileShare fileShare)
        {
            try
            {
                var ofsList = Array.ConvertAll(args, arg => new FileStream(arg, FileMode.Open, fileAccess, fileShare));

                // 下の処理を省略すると、Releaseビルドでは最適化の結果として、オープンした次の瞬間にクローズするらしい.
                foreach (var ofs in ofsList)
                {
                    ofs.Close();
                }
            }
            catch (IOException e)
            {
                if (((uint)e.HResult) == 0x80070020)
                {
                    Console.WriteLine($"Sharing violation.({e.Message})");
                }
                else
                    throw;
            }
        }
    }
}
